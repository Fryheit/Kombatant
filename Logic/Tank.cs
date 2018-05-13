using System.Threading.Tasks;
using ff14bot;
using ff14bot.Behavior;
using ff14bot.Managers;
using Kombatant.Extensions;
using Kombatant.Interfaces;

namespace Kombatant.Logic
{
    /// <summary>
    /// Logic for the tank role interactions with the combat routine.
    /// </summary>
    /// <inheritdoc cref="M:Komabatant.Interfaces.LogicExecutor"/>
    internal class Tank : LogicExecutor
    {
        #region Singleton

        private static Tank _tank;
        internal static Tank Instance => _tank ?? (_tank = new Tank());

        #endregion

        /// <summary>
        /// Main task executor for the tank logic.
        /// </summary>
        /// <returns>Returns <c>true</c> if any action was executed, otherwise <c>false</c>.</returns>
        internal new async Task<bool> ExecuteLogic()
        {
            // Do not execute this logic if the botbase is paused.
            if (Settings.BotBase.Instance.IsPaused)
                return false;

            if (!Core.Me.IsTank() || !Core.Me.CanFight())
                return false;

            if (Core.Me.InCombat)
            {
                if (ShouldExecuteHeal())
                    if (await RoutineManager.Current.HealBehavior.ExecuteCoroutine())
                        return true;

                if (ShouldExecuteCombatBuff())
                    if(await RoutineManager.Current.CombatBuffBehavior.ExecuteCoroutine())
                        return true;

                if (ShouldExecuteCombat())
                    if(await RoutineManager.Current.CombatBehavior.ExecuteCoroutine())
                        return true;
            }
            else
            {
                if (ShouldExecuteHeal())
                    if(await RoutineManager.Current.HealBehavior.ExecuteCoroutine())
                        return true;

                if (ShouldExecuteRest())
                    if(await RoutineManager.Current.RestBehavior.ExecuteCoroutine())
                        return true;

                if (ShouldExecutePreCombatBuff())
                    if(await RoutineManager.Current.PreCombatBuffBehavior.ExecuteCoroutine())
                        return true;

                if (ShouldExecutePullBuff())
                    if(await RoutineManager.Current.PullBuffBehavior.ExecuteCoroutine())
                        return true;

                if (ShouldExecutePull())
                    if(await RoutineManager.Current.PullBehavior.ExecuteCoroutine())
                        return true;
            }

            return false;
        }
    }
}