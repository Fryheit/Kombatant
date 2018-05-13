using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using ff14bot;
using ff14bot.Managers;
using Kombatant.Extensions;
using Kombatant.Helpers;
using Kombatant.Interfaces;

namespace Kombatant.Logic
{
    /// <summary>
    /// Logic for handling special mechanics like gaze attacks or standstill debuffs.
    /// </summary>
    /// <inheritdoc cref="M:Komabatant.Interfaces.LogicExecutor"/>
    internal class Mechanics : LogicExecutor
    {
        #region Singleton

        private static Mechanics _mechanics;
        internal static Mechanics Instance => _mechanics ?? (_mechanics = new Mechanics());

        #endregion

        /// <summary>
        /// Milliseconds left on an aura that will cause us to stop casting and stand still before it reaches zero.
        /// </summary>
        private const double StandStillOnZeroSafetyCushion = 2500;

        /// <summary>
        /// Milliseconds to add on top of the real aura duration to compensate for latency and/or server ticks.
        /// </summary>
        private const double StandStillLatencyAdd = 250;

        // Colors for the toast messages
        private Color ToastFontColor => Colors.OrangeRed;
        private Color ToastShadowColor => Colors.Black;

        /// <summary>
        /// Main task executor for the Mechanics logic.
        /// </summary>
        /// <returns>Returns <c>true</c> if any action was executed, otherwise <c>false</c>.</returns>
        internal new async Task<bool> ExecuteLogic()
        {
            if (!Settings.BotBase.Instance.MechanicWarnings)
                return await Task.FromResult(false);

            if (Settings.BotBase.Instance.IsPaused)
                return await Task.FromResult(false);

            if (!Core.Me.CanFight() || Core.Me.IsOccupied())
                return await Task.FromResult(false);

            // Handle casts of our primary target that force the player to avoid looking at the enemy.
            // IMPORTANT: This will only work if GameSettingsManager.FaceTargetOnAction is set to false!
            if (ShouldAvertGaze())
                return await Task.FromResult(ExecuteAvertGaze());

            // Handle auras that force the player to stand still until it fades.
            if (ShouldStandStill())
            {
                if (ExecuteStandStill())
                {
                    if(Core.Me.IsCasting)
                        ActionManager.StopCasting();

                    return await Task.FromResult(true);
                }
            }

            // Handle auras that force the player to stand still when the aura reaches zero.
            if (ShouldStandStillOnZero())
            {
                if (ExecuteStandStillOnZero())
                {
                    if(Core.Me.IsCasting)
                        ActionManager.StopCasting();

                    return await Task.FromResult(true);
                }
            }

            return await Task.FromResult(false);
        }

        /// <summary>
        /// Determines whether the player should automatically avoid looking at the target to dodge a gaze attack.
        /// </summary>
        /// <returns></returns>
        private bool ShouldAvertGaze()
        {
            if (!Core.Me.HasTarget)
                return false;

            if (!Core.Target.IsEnemy())
                return false;

            var target = Core.Target.GetCharacter();
            return target.IsCasting && Constants.Attack.Gazes.Contains(target.CastingSpellId);
        }

        /// <summary>
        /// Determines whether the player has an aura that forces a standstill.
        /// </summary>
        /// <returns></returns>
        private bool ShouldStandStill()
        {
            return Core.Me.Auras.Any(aura => Constants.Aura.ForceStandStill.Contains(aura.Id));
        }

        /// <summary>
        /// Determines whether the player has an aura that forces a standstill once it reaches zero.
        /// </summary>
        /// <returns></returns>
        private bool ShouldStandStillOnZero()
        {
            return Core.Me.Auras.Any(aura => Constants.Aura.ForceStandStillOnZero.Contains(aura.Id));
        }

        /// <summary>
        /// Executes looking away from a gaze attack.
        /// </summary>
        private bool ExecuteAvertGaze()
        {
            var target = Core.Target.GetCharacter();
            TimeSpan gazeCastTime = target.SpellCastInfo.RemainingCastTime.Add(new TimeSpan(0, 0, 0, 1));

            // TODO: Localization and cleanup!
            LogHelper.Instance.Log("AVERT GAZE: TURN AWAY!!");
            OverlayHelper.Instance.AddToast("AVERT GAZE!", ToastFontColor, ToastShadowColor, gazeCastTime);

            if(Core.Me.LookingAt(target.Location))
                Core.Me.SetFacing(Core.Me.Heading - (float) Math.PI);

            return true;
        }

        /// <summary>
        /// Executes the standstill for an aura.
        /// </summary>
        private bool ExecuteStandStill()
        {
            // Select the aura that still has the longest time left.
            var longestAura = Core.Me.Auras
                .Where(aura => Constants.Aura.ForceStandStill.Contains(aura.Id) && aura.TimeLeft > 0.5)
                .OrderByDescending(aura => aura.TimeLeft)
                .FirstOrDefault();

            // If all auras are gone (i.e. the boss died), we do not need to wait.
            if (longestAura == null)
            {
                WaitHelper.Instance.RemoveWait(@"Mechanics.StandStillMessage");
                return false;
            }

            // Already waiting for an aura to disappear.
            if (WaitHelper.Instance.IsWaiting(@"Mechanics.StandStillMessage"))
                return true;

            WaitHelper.Instance.AddWait(@"Mechanics.StandStillMessage", longestAura.TimespanLeft.Add(TimeSpan.FromMilliseconds(StandStillLatencyAdd)));
            var toastMessage = string.Format(Resources.Localization.Msg_MechanicWarning_StandStill, longestAura.Name);
            OverlayHelper.Instance.AddToast(toastMessage, ToastFontColor, ToastShadowColor, longestAura.TimespanLeft.Subtract(TimeSpan.FromMilliseconds(100)));

            // Return true because we want to wait the entirety of the aura.
            return true;
        }

        /// <summary>
        /// Executes the standstill for an aura when it reaches zero.
        /// </summary>
        private bool ExecuteStandStillOnZero()
        {
            // Select the evil aura that still has the longest time left.
            var shortestAura = Core.Me.Auras
                .Where(aura => Constants.Aura.ForceStandStillOnZero.Contains(aura.Id) && aura.TimeLeft > 0.1)
                .OrderBy(aura => aura.TimeLeft)
                .FirstOrDefault();

            // If all auras are gone (i.e. the boss died), we do not need to wait.
            if (shortestAura == null)
            {
                WaitHelper.Instance.RemoveWait(@"Mechanics.StandStillMessage");
                return false;
            }

            // Have we finally reached the critical time left?
            if (WaitHelper.Instance.IsWaiting(@"Mechanics.StandStillMessage"))
            {
                var timeLeft = WaitHelper.Instance.TimeLeft(@"Mechanics.StandStillMessage");
                if (timeLeft.TotalMilliseconds >= 0 && timeLeft.TotalMilliseconds <= StandStillOnZeroSafetyCushion)
                    return true;
            }

            WaitHelper.Instance.AddWait(@"Mechanics.StandStillMessage", shortestAura.TimespanLeft.Add(TimeSpan.FromMilliseconds(StandStillLatencyAdd)));
            var toastMessage = string.Format(Resources.Localization.Msg_MechanicWarning_StandStillOnZero, shortestAura.Name);
            OverlayHelper.Instance.AddToast(toastMessage, ToastFontColor, ToastShadowColor, shortestAura.TimespanLeft.Subtract(TimeSpan.FromMilliseconds(100)));

            // Return false because we only want to stop only in the last few moments.
            return false;
        }
    }
}