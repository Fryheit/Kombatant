using System.Linq;
using ff14bot.Enums;
using ff14bot.Managers;
using ff14bot.Objects;
using Aura = Kombatant.Constants.Aura;

namespace Kombatant.Extensions
{
    /// <summary>
    /// Extensions for the Character class.
    /// </summary>
    internal static class CharacterExtension
    {
        /// <summary>
        /// Return whether a given character has been tagged by a party member.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static bool HasBeenTaggedByPartyMember(this Character character)
        {
            return character.TaggerType == 2;
        }

        /// <summary>
        /// Returns whether a given character is still untagged.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static bool IsUntagged(this Character character)
        {
            return character.TaggerType == 0;
        }

        /// <summary>
        /// Checks if a given character is in the same party as the player.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static bool IsInMyParty(this Character character)
        {
            return PartyManager.IsInParty && PartyManager.VisibleMembers.Any(member => member.BattleCharacter == character);
        }

        /// <summary>
        /// Checks if a given character is the party leader.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static bool IsPartyLeader(this Character character)
        {
            return PartyManager.PartyLeader.BattleCharacter == character;
        }

        /// <summary>
        /// Determines whether a given character's role is tank.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static bool IsTank(this Character character)
        {
            return    character.CurrentJob == ClassJobType.DarkKnight
                   || character.CurrentJob == ClassJobType.Gladiator
                   || character.CurrentJob == ClassJobType.Marauder
                   || character.CurrentJob == ClassJobType.Paladin
                   || character.CurrentJob == ClassJobType.Warrior;
        }

        /// <summary>
        /// Determines whether a given character's role is healer.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static bool IsHealer(this Character character)
        {
            return    character.CurrentJob == ClassJobType.Astrologian
                   || character.CurrentJob == ClassJobType.Conjurer
                   || character.CurrentJob == ClassJobType.Scholar
                   || character.CurrentJob == ClassJobType.WhiteMage;
        }

        /// <summary>
        /// Determines whether a given character's job is a melee dps.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static bool IsMeleeDps(this Character character)
        {
            return    character.CurrentJob == ClassJobType.Dragoon
                   || character.CurrentJob == ClassJobType.Lancer
                   || character.CurrentJob == ClassJobType.Monk
                   || character.CurrentJob == ClassJobType.Ninja
                   || character.CurrentJob == ClassJobType.Pugilist
                   || character.CurrentJob == ClassJobType.Rogue
                   || character.CurrentJob == ClassJobType.Samurai;
        }

        /// <summary>
        /// Determines whether a given character's job is a ranged dps.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static bool IsRangedDps(this Character character)
        {
            return    character.CurrentJob == ClassJobType.Arcanist
                   || character.CurrentJob == ClassJobType.Archer
                   || character.CurrentJob == ClassJobType.Bard
                   || character.CurrentJob == ClassJobType.BlackMage
                   || character.CurrentJob == ClassJobType.Machinist
                   || character.CurrentJob == ClassJobType.RedMage
                   || character.CurrentJob == ClassJobType.Summoner
                   || character.CurrentJob == ClassJobType.Thaumaturge;
        }

        /// <summary>
        /// Determines whether a given character's job is part of the Disciples of Magic.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static bool IsDoM(this Character character)
        {
            return    IsHealer(character)
                   || character.CurrentJob == ClassJobType.Arcanist
                   || character.CurrentJob == ClassJobType.BlackMage
                   || character.CurrentJob == ClassJobType.RedMage
                   || character.CurrentJob == ClassJobType.Summoner;
        }

        /// <summary>
        /// Determines whether a given character's job is part of the Disciples of War.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static bool IsDoW(this Character character)
        {
            return !IsDoM(character) && IsTank(character) || IsMeleeDps(character) || IsRangedDps(character);
        }

        /// <summary>
        /// Checks whether a character has one of the known Invincibility auras.
        /// </summary>
        /// <param name="character"></param>
        /// <returns></returns>
        internal static bool IsInvincible(this Character character)
        {
            return character.Auras.AuraList.Select(a => a.Id).Intersect(Aura.Invincibility).Any();
        }
    }
}