using ff14bot.Managers;

namespace Kombatant.Extensions
{
    /// <summary>
    /// Extensions for the PartyMember class.
    /// This entire class is basically just syntax sugar. It defers the calls to the CharacterExtension's methods.
    /// </summary>
    internal static class PartyMemberExtension
    {
        /// <summary>
        /// Determines whether the given party member is a tank.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        internal static bool IsTank(this PartyMember member)
        {
            return member.BattleCharacter.IsTank();
        }

        /// <summary>
        /// Determines whether the given party member is a healer.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        internal static bool IsHealer(this PartyMember member)
        {
            return member.BattleCharacter.IsHealer();
        }

        /// <summary>
        /// Determines whether the given party member is a melee dps.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        internal static bool IsMeleeDps(this PartyMember member)
        {
            return member.BattleCharacter.IsMeleeDps();
        }

        /// <summary>
        /// Determines whether the given party member is a ranged dps.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        internal static bool IsRangedDps(this PartyMember member)
        {
            return member.BattleCharacter.IsRangedDps();
        }

        /// <summary>
        /// Determines whether the given party member is a Disciple of Magic.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        internal static bool IsDoM(this PartyMember member)
        {
            return member.BattleCharacter.IsDoM();
        }

        /// <summary>
        /// Determines whether the given party member is a Disciple of War.
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        internal static bool IsDoW(this PartyMember member)
        {
            return member.BattleCharacter.IsDoW();
        }
    }
}