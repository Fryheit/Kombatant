using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Media;
using ff14bot.Behavior;
using ff14bot.Managers;
using Kombatant.Extensions;
using Kombatant.Helpers;
using Kombatant.Interfaces;

namespace Kombatant.Logic
{
    /// <summary>
    /// Logic for avoidance support.
    /// </summary>
    /// <inheritdoc cref="M:Kombatant.Interfaces.LogicExecutor"/>
    internal class Avoidance : LogicExecutor
    {
        #region Singleton

        private static Avoidance _avoidance;
        internal static Avoidance Instance => _avoidance ?? (_avoidance = new Avoidance());

        #endregion

        private bool _wasAvoiding;
        private bool _isFighting;

        /// <summary>
        /// Main task executor for the Avoidance logic.
        /// </summary>
        /// <returns>Returns <c>true</c> if any action was executed, otherwise <c>false</c>.</returns>
        internal new async Task<bool> ExecuteLogic()
        {
            if (ShouldExecuteAvoidance())
            {
                if (Settings.BotBase.Instance.IsPaused)
                    return await Task.FromResult(false);

                if(PulseFlagHelper.Instance.EnablePulseFlag(PulseFlags.Avoidance))
                    return await Task.FromResult(false);

                if (AvoidanceManager.IsRunningOutOfAvoid)
                {
                    _wasAvoiding = true;
                    return await Task.FromResult(true);
                }

                // For non-autonomous operation, we really want the bot to not move more than it needs to.
                // Since we know whether or not we just came out of a avoidance loop, we can stop the movement
                // here to prevent unwanted chicken runs.
                if (_wasAvoiding)
                {
                    _wasAvoiding = false;
                    LogHelper.Instance.Log(Resources.Localization.Msg_AvoidanceFinished);
                    MovementManager.MoveStop();
                    return await Task.FromResult(true);
                }
            }
            else
            {
                if (PulseFlagHelper.Instance.DisablePulseFlag(PulseFlags.Avoidance))
                    return await Task.FromResult(false);
            }

            return await Task.FromResult(false);
        }

        /// <summary>
        /// Determines whether or not avoidance informations should be honoured.
        /// </summary>
        /// <returns></returns>
        private bool ShouldExecuteAvoidance()
        {
            // User doesn't want RebornBuddy to avoid.
            if (!Settings.BotBase.Instance.EnableAvoidance)
                return false;

            // Pause avoidance because player is fighting a known boss?
            if (PauseAvoidanceBecauseBoss())
                return false;

            return true;
        }

        /// <summary>
        /// Determines whether or not the user wants to disable avoidance during bossfights.
        /// </summary>
        /// <returns></returns>
        private bool PauseAvoidanceBecauseBoss()
        {
            if (Settings.BotBase.Instance.PauseAvoidanceOnBosses &&
                GameObjectManager.Attackers.Any(attacker => attacker.IsBoss()))
            {
                // Fight started
                if (!_isFighting)
                {
                    var bossMonster = GameObjectManager.Attackers.FirstOrDefault(attacker => attacker.IsBoss());
                    var toastMsg = string.Format(Resources.Localization.Msg_AvoidanceDisabledOnBossStart, bossMonster?.Name);
                    LogHelper.Instance.Log(Resources.Localization.Msg_Log_AvoidanceDisabledOnBossStart);
                    OverlayHelper.Instance.AddToast(toastMsg, Colors.Coral, Colors.Chocolate, new TimeSpan(0, 0, 0, 5));
                }

                _isFighting = true;
                return true;
            }

            // Fight ended
            if (_isFighting)
            {
                LogHelper.Instance.Log(Resources.Localization.Msg_Log_AvoidanceDisabledOnBossEnd);
                OverlayHelper.Instance.AddToast(Resources.Localization.Msg_AvoidanceDisabledOnBossEnd, Colors.LimeGreen, Colors.DarkGreen, new TimeSpan(0, 0, 0, 5));
                _isFighting = false;
            }

            return false;
        }
    }
}