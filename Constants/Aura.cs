namespace Kombatant.Constants
{
    /// <summary>
    /// Static list of noteworthy auras.
    /// Thanks for the status search, xivdb.com!
    /// </summary>
    internal static class Aura
    {
        /// <summary>
        /// Sprint aura
        /// </summary>
        internal static readonly uint Sprint = 50;

        /// <summary>
        /// Auras that make the enemy invincible.
        /// Note: Always add *all* variants from xivdb.com!
        /// </summary>
        internal static readonly uint[] Invincibility =
        {
            325,
            529,
            656,
            671,
            775,
            776,
            895,
            969,
            981,
            1125
        };

        /// <summary>
        /// Auras that require you to stand very, very still until they fade.
        /// Note: Always add *all* variants from xivdb.com!
        /// </summary>
        internal static readonly uint[] ForceStandStill =
        {
            639,     // Pyretic
            690,     // Pyretic
            1049,    // Pyretic
            1133,    // Pyretic
        };

        /// <summary>
        /// Auras that require you to stand still when they reach zero.
        /// Note: Always add *all* variants from xivdb.com!
        /// </summary>
        internal static readonly uint[] ForceStandStillOnZero =
        {
            1072,    // Acceleration Bomb
            1384,    // Acceleration Bomb
        };
    }
}