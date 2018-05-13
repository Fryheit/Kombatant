using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ff14bot.Objects;
using Kombatant.Enums;
using Kombatant.Extensions;
using Kombatant.Helpers;
using Kombatant.Interfaces;

namespace Kombatant.Logic
{
    /// <summary>
    /// Logic for automatically targeting enemies.
    /// </summary>
    /// <inheritdoc cref="M:Komabatant.Interfaces.LogicExecutor"/>
    internal class Target : LogicExecutor
    {
        #region Singleton

        private static Target _target;
        internal static Target Instance => _target ?? (_target = new Target());

        #endregion

        /// <summary>
        /// Main task executor for the targeting logic.
        /// </summary>
        /// <returns>Returns <c>true</c> if any action was executed, otherwise <c>false</c>.</returns>
        internal new async Task<bool> ExecuteLogic()
        {
            // Do not execute this logic if the botbase is paused.
            if (Settings.BotBase.Instance.IsPaused || IsTraveling())
                return await Task.FromResult(false);

            // Auto face target
            if (ShouldExecuteAutoFaceTarget())
            {
                GameSettingsManager.FaceTargetOnAction = Settings.BotBase.Instance.AutoFaceTarget;
                return await Task.FromResult(true);
            }

            // Try to determine current target. Possibly a target chosen by the prior mechanisms.
            var currentTarget = Core.Me.CurrentTarget as BattleCharacter;
            var potentialTarget = null as BattleCharacter;

            // Automatically select a target if we do not have one. Uses one of the many colourful selection modes!
            if (ShouldExecuteAutoTarget())
            {
                switch (Settings.BotBase.Instance.AutoTargetingMode)
                {
                    case TargetingMode.None:
                        break;

                    case TargetingMode.BestAoE:
                        potentialTarget = TargetBestAoeEnemy();
                        break;

                    case TargetingMode.OnlyWhitelisted:
                        potentialTarget = TargetOnlyWhitelistedEnemy();
                        break;

                    case TargetingMode.Nearest:
                        potentialTarget = TargetNearestEnemy();
                        break;

                    case TargetingMode.LowestHealth:
                        potentialTarget = TargetLowestHpEnemy();
                        break;

                    case TargetingMode.LowestHealthPercent:
                        potentialTarget = TargetLowestHpPercentEnemy();
                        break;

                    case TargetingMode.HighestHealth:
                        potentialTarget = TargetHighestHpEnemy();
                        break;

                    case TargetingMode.HighestHealthPercent:
                        potentialTarget = TargetHighestHpPercentEnemy();
                        break;

                    case TargetingMode.AssistTank:
                        potentialTarget = TargetAssistTank();
                        break;

                    case TargetingMode.AssistLeader:
                        potentialTarget = TargetAssistLeader();
                        break;
                }

                // Player target differs from chosen target or is a FATE mob?
                if (potentialTarget != null && potentialTarget.CheckAliveAndValid() && potentialTarget.InLineOfSight() && currentTarget != potentialTarget)
                {
                    LogHelper.Instance.Log(Resources.Localization.Msg_NewTargetSelected,
                        potentialTarget.Name,
                        $@"0x{potentialTarget.ObjectId:X8}");

                    potentialTarget.Target();
                    return await Task.FromResult(true);
                }
            }

            // No target? Then the following checks would return false anyway...
            if (currentTarget == null)
                return await Task.FromResult(false);

            // Target not in line of sight?
            if (!currentTarget.InLineOfSight())
                return await Task.FromResult(true);

            // Target is invincible?
            if (currentTarget.IsInvincible())
                return await Task.FromResult(true);

            // Make sure we don't pull stuff as a non-tank when in a party and smart pull is enabled.
            if (!IsAllowedToFight(currentTarget))
                return await Task.FromResult(true);

            return await Task.FromResult(false);
        }

        /// <summary>
        /// Determines whether the combat routine is allowed to pull/do combat with the current target.
        /// </summary>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool IsAllowedToFight(BattleCharacter target)
        {
            // Only limit when we have Smart Pull enabled, are not a tank ourselves and there is a tank nearby!
            if(Settings.BotBase.Instance.EnableSmartPull &&
               !Core.Me.IsTank() &&
               PartyManager.VisibleMembers.Any(member => member.IsTank()))
                return Core.Me.IsInMyParty()
                       && target.IsEnemy()
                       && target.HasBeenTaggedByPartyMember();

            return true;
        }

        /// <summary>
        /// Helper method to determine whether a given GameObject is a valid target
        /// for the target strategy "Whitelist only".
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        private bool IsValidWhitelistTarget(GameObject obj)
        {
            return Settings.BotBase.Instance.TargetWhitelist.Any(whiteListEntry => whiteListEntry.NpcId == obj.NpcId) ||
                   GameObjectManager.Attackers.Contains(obj);
        }

        /// <summary>
        /// Is the player swimming, flying, moving or in any other way occupied?
        /// </summary>
        /// <returns></returns>
        private bool IsTraveling()
        {
            return MovementManager.IsSwimming || MovementManager.IsFlying || MovementManager.IsMoving ||
                   MovementManager.IsOccupied;
        }

        /// <summary>
        /// Determines whether auto facing a given target should be executed.
        /// </summary>
        /// <returns></returns>
        private bool ShouldExecuteAutoFaceTarget()
        {
            return GameSettingsManager.FaceTargetOnAction != Settings.BotBase.Instance.AutoFaceTarget
                   && WaitHelper.Instance.IsDoneWaiting(@"Target.AutoFace");
        }

        /// <summary>
        /// Determines whether we should select a new target, taking into account the current state and settings.
        /// The order in which these conditions are checked is important, so please be careful!
        /// </summary>
        /// <returns></returns>
        private bool ShouldExecuteAutoTarget()
        {
            // Target search disabled
            if (!Settings.BotBase.Instance.AutoTarget ||
                Settings.BotBase.Instance.AutoTargetingMode == TargetingMode.None)
                return false;

            // Too soon to select a new target?
            if (!WaitHelper.Instance.IsDoneWaiting(@"Target.AutoTarget"))
                return false;

            // Does the player allow us to switch targets?
            if (Settings.BotBase.Instance.AutoTargetSwitch)
                return true;

            return !Core.Me.HasTarget;
        }

        /// <summary>
        /// General caller methods for all post-target filters.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private IEnumerable<BattleCharacter> ApplyPostFilters(IEnumerable<BattleCharacter> group)
        {
            var result = group;

            result = PostFilterDistance(result);
            result = PostFilterFate(result);

            return result;
        }

        private IEnumerable<BattleCharacter> PostFilterDistance(IEnumerable<BattleCharacter> group)
        {
            if (Settings.BotBase.Instance.TargetScanMaxDistance == 0)
                return group.Where(o => o.IsInPullRange());

            return group.Where(o => o.Distance2D() <= Settings.BotBase.Instance.TargetScanMaxDistance);
        }

        /// <summary>
        /// Applies a filter for FATE-only targets to a group of BattleCharacters.
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private IEnumerable<BattleCharacter> PostFilterFate(IEnumerable<BattleCharacter> group)
        {
            // FATE filter disabled? Return the group as-is.
            if (!Settings.BotBase.Instance.FateTargetFilter)
                return group;

            // Do we have aggro from something else? Prioritize those targets!
            if (GameObjectManager.Attackers.Any(o => o.TargetCharacter == Core.Me))
                return GameObjectManager.Attackers
                    .Where(o => o.TargetCharacter == Core.Me)
                    .OrderBy(o => o.Distance2D());

            // No external attackers, prioritize FATE mobs.
            if (FateManager.WithinFate)
                return group
                    .Where(o => o.IsFate)
                    .OrderBy(o => o.Distance2D());

            // Nothing special? Return group as-is.
            return group;
        }

        /// <summary>
        /// Target selection: Assist party leader.
        /// </summary>
        /// <returns>Potential BattleCharacter object as the new target or null when no suitable target was found.</returns>
        private BattleCharacter TargetAssistLeader()
        {
            // Not in party or party leader?
            if (!Core.Me.IsInMyParty() || Core.Me.IsInMyParty() && Core.Me.IsPartyLeader())
                return null;

            // Party leader is not in the vicinity...
            if (!PartyManager.PartyLeader.IsInObjectManager)
                return null;

            // Leader doesn't have a target or it's not what we consider an enemy.
            if (!PartyManager.PartyLeader.BattleCharacter.HasTarget ||
                !PartyManager.PartyLeader.BattleCharacter.TargetGameObject.IsEnemy())
                return null;

            return PartyManager.PartyLeader.BattleCharacter.TargetGameObject as BattleCharacter;
        }

        /// <summary>
        /// Target selection: Assist tank.
        /// </summary>
        /// <returns>Potential BattleCharacter object as the new target or null when no suitable target was found.</returns>
        private BattleCharacter TargetAssistTank()
        {
            // I am not in a party? I am the tank? Or there is no tank in my party?
            if (!Core.Me.IsInMyParty() || Core.Me.IsTank() || !PartyManager.VisibleMembers.Any(member => member.IsTank() && !member.IsMe))
                return null;

            // Find the closest tank in my party and target whatever they are targeting!
            var nearestTank = PartyManager.VisibleMembers
                .Where(member => member.IsTank())
                .OrderBy(member => member.BattleCharacter.Distance2D())
                .FirstOrDefault();

            // No tanksywhirls?
            if(nearestTank == null)
                return null;

            // No targetsie?
            if(!nearestTank.BattleCharacter.HasTarget || !nearestTank.BattleCharacter.TargetGameObject.IsEnemy())
                return null;

            return nearestTank.BattleCharacter.TargetGameObject as BattleCharacter;
        }

        /// <summary>
        /// Target selection: Best AOE target.
        /// </summary>
        /// <returns>Potential BattleCharacter object as the new target or null when no suitable target was found.</returns>
        private BattleCharacter TargetBestAoeEnemy()
        {
            var potentialTargets = GameObjectManager.GetObjectsOfType<BattleCharacter>()
                .Where(o => !o.IsStrikingDummy() && o.IsEnemy())
                .OrderByDescending(o => o.NearbyEnemyCount())
                .ThenBy(o => o.Distance2D());

            var bestEnemy = ApplyPostFilters(potentialTargets).FirstOrDefault();

            return bestEnemy;
        }

        /// <summary>
        /// Target selection: Nearest enemy.
        /// </summary>
        /// <returns>Potential BattleCharacter object as the new target or null when no suitable target was found.</returns>
        private BattleCharacter TargetNearestEnemy()
        {
            var potentialTargets = GameObjectManager.GetObjectsOfType<BattleCharacter>()
                .Where(o => !o.IsStrikingDummy() && o.IsEnemy())
                .OrderBy(o => o.Distance2D());

            var nearestEnemy = ApplyPostFilters(potentialTargets).FirstOrDefault();

            return nearestEnemy;
        }

        /// <summary>
        /// Target selection: Whitelisted enemies only.
        /// </summary>
        /// <returns>Potential BattleCharacter object as the new target or null when no suitable target was found.</returns>
        private BattleCharacter TargetOnlyWhitelistedEnemy()
        {
            var potentialTargets = GameObjectManager.GetObjectsOfType<BattleCharacter>()
                .Where(o => !o.IsStrikingDummy() && o.IsEnemy() && IsValidWhitelistTarget(o))
                .OrderBy(o => o.Distance2D());

            var validEnemy = ApplyPostFilters(potentialTargets).FirstOrDefault();

            return validEnemy;
        }

        /// <summary>
        /// Target selection: Highest HP enemy.
        /// </summary>
        /// <returns>Potential BattleCharacter object as the new target or null when no suitable target was found.</returns>
        private BattleCharacter TargetHighestHpEnemy()
        {
            var potentialTargets = GameObjectManager.GetObjectsOfType<BattleCharacter>()
                .Where(o => !o.IsStrikingDummy() && o.IsEnemy())
                .OrderByDescending(o => o.CurrentHealth)
                .ThenBy(o => o.Distance2D());

            var strongestEnemy = ApplyPostFilters(potentialTargets).FirstOrDefault();

            return strongestEnemy;
        }

        /// <summary>
        /// Target selection: Highest HP percent enemy.
        /// </summary>
        /// <returns>Potential BattleCharacter object as the new target or null when no suitable target was found.</returns>
        private BattleCharacter TargetHighestHpPercentEnemy()
        {
            var potentialTargets = GameObjectManager.GetObjectsOfType<BattleCharacter>()
                .Where(o => !o.IsStrikingDummy() && o.IsEnemy())
                .OrderByDescending(o => o.CurrentHealthPercent)
                .ThenBy(o => o.Distance2D());

            var strongestEnemy = ApplyPostFilters(potentialTargets).FirstOrDefault();

            return strongestEnemy;
        }

        /// <summary>
        /// Target selection: Lowest HP enemy.
        /// </summary>
        /// <returns>Potential BattleCharacter object as the new target or null when no suitable target was found.</returns>
        private BattleCharacter TargetLowestHpEnemy()
        {
            var potentialTargets = GameObjectManager.GetObjectsOfType<BattleCharacter>()
                .Where(o => !o.IsStrikingDummy() && o.IsEnemy())
                .OrderBy(o => o.CurrentHealth)
                .ThenBy(o => o.Distance2D());

            var weakestEnemy = ApplyPostFilters(potentialTargets).FirstOrDefault();

            return weakestEnemy;
        }

        /// <summary>
        /// Target selection: Lowest HP percent enemy.
        /// </summary>
        /// <returns>Potential BattleCharacter object as the new target or null when no suitable target was found.</returns>
        private BattleCharacter TargetLowestHpPercentEnemy()
        {
            var potentialTargets = GameObjectManager.GetObjectsOfType<BattleCharacter>()
                .Where(o => !o.IsStrikingDummy() && o.IsEnemy())
                .OrderBy(o => o.CurrentHealthPercent)
                .ThenBy(o => o.Distance2D());

            var weakestEnemy = ApplyPostFilters(potentialTargets).FirstOrDefault();

            return weakestEnemy;
        }
    }
}