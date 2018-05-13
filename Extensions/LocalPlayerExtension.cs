using System.Linq;
using ff14bot;
using ff14bot.Behavior;
using ff14bot.Enums;
using ff14bot.Managers;
using ff14bot.Objects;
using ff14bot.RemoteWindows;

namespace Kombatant.Extensions
{
    internal static class LocalPlayerExtension
    {
        /// <summary>
        /// Can a player fight an enemy right away?
        ///
        /// Checks for !IsMounted and !IsDead.
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        internal static bool CanFight(this LocalPlayer player)
        {
            return !player.IsMounted && !player.IsDead;
        }

        /// <summary>
        /// Returns the current Fate the player is in (location-wise).
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        internal static FateData GetCurrentFate(this LocalPlayer player)
        {
            var fate = FateManager.ActiveFates.FirstOrDefault(f => f.Within2D(player.Location));
            if (fate != null && fate.Status != FateStatus.COMPLETE)
                return fate;

            return null;
        }

        /// <summary>
        /// Is a player busy with loading or watching a cutscene?
        /// </summary>
        /// <param name="player"></param>
        /// <returns></returns>
        // ReSharper disable once UnusedParameter.Global
        internal static bool IsOccupied(this LocalPlayer player)
        {
            return CommonBehaviors.IsLoading || QuestLogManager.InCutscene;
        }

        /// <summary>
        /// <para>When being inside a Fate, this will try to sync the level of the player.</para>
        /// <para>The method contains checks that make it safe to call without prior safeguarding.</para>
        /// </summary>
        /// <param name="player"></param>
        internal static void SyncToFate(this LocalPlayer player)
        {
            var fate = player.GetCurrentFate();

            if (fate == null)
                return;

            if(fate.Status != FateStatus.COMPLETE && Core.Me.ClassLevel > fate.MaxLevel)
            {
                ToDoList.LevelSync();
            }
        }
    }
}