using System;
using System.Linq;
using Clio.Common;
using Clio.Utilities;
using ff14bot.Enums;
using ff14bot.Helpers;
using ff14bot.Managers;
using ff14bot.Objects;

namespace Kombatant.Extensions
{
    /// <summary>
    /// Extensions for the GameObject class.
    /// </summary>
    internal static class GameObjectExtension
    {
        /// <summary>
        /// <para>Counts the number of nearby enemies.</para>
        /// <para>The radius is fixed to 5.5f, this will work fine for abilities with a radius of 5f as well as 8f.</para>
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static int NearbyEnemyCount(this GameObject obj)
        {
            return GameObjectManager.GameObjects
                .Count(g => g.IsEnemy() && g.Distance2D(obj.Location) <= 5.5f);
        }

        /// <summary>
        /// Checks whether the given gameobject is a battle character.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool IsBattleCharacter(this GameObject obj)
        {
            return obj is BattleCharacter;
        }

        /// <summary>
        /// Checks whether the given gameobject is a known boss monster.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool IsBoss(this GameObject obj)
        {
            return Constants.GameObject.DungeonBosses.Contains(obj.NpcId);
        }

        /// <summary>
        /// Checks whether the given gameobject is a character.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool IsCharacter(this GameObject obj)
        {
            return obj is Character;
        }

        /// <summary>
        /// Checks whether a given gameobject could be considered an enemy.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool IsEnemy(this GameObject obj)
        {
            if (!obj.CheckAliveAndValid())
                return false;

            var character = obj.GetCharacter();
            return character.CanAttack || character.StatusFlags == StatusFlags.Hostile;
        }

        /// <summary>
        /// Checks whether the given gameobject is a striking dummy.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool IsStrikingDummy(this GameObject obj)
        {
            return Constants.GameObject.StrikingDummy == obj.NpcId;
        }

        /// <summary>
        /// Checks whether a given gameobject is in pull range.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool IsInPullRange(this GameObject obj)
        {
            return obj.Distance2D() <= RoutineManager.Current.PullRange;
        }

        /// <summary>
        /// Checks whether a given gameobject is valid and alive.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static bool CheckAliveAndValid(this GameObject obj)
        {
            if(!obj.IsCharacter())
                return false;

            var character = obj.GetCharacter();

            return character.IsValid && !character.IsMe && character.IsVisible &&
                   character.IsAlive && character.IsTargetable;
        }

        /// <summary>
        /// Gets the given GameObject as a BattleCharacter object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static BattleCharacter GetBattleCharacter(this GameObject obj)
        {
            if(obj.IsBattleCharacter())
                return obj as BattleCharacter;

            return null;
        }

        /// <summary>
        /// Gets the given GameObject as a Character object.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static Character GetCharacter(this GameObject obj)
        {
            if (obj.IsCharacter())
                return obj as Character;

            return null;
        }

        /// <summary>
        /// Checks if the given gameobject is looking at a given target location
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="targetLocation"></param>
        /// <returns></returns>
        internal static bool LookingAt(this GameObject obj, Vector3 targetLocation)
        {
            float single = Math.Abs
                (MathEx.NormalizeRadian
                    (obj.Heading - MathEx.NormalizeRadian
                         (MathHelper.CalculateHeading(obj.Location, targetLocation) + (float)Math.PI)
                    )
                );

            if (single > Math.PI)
                single = Math.Abs(single - (float)(Math.PI * 2));

            return single < (float)(Math.PI / 4);
        }
    }
}