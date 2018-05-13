using ff14bot.Behavior;

namespace Kombatant.Settings
{
    /// <summary>
    /// Fleeting settings that only persist in the context of this running RebornBuddy session.
    /// </summary>
    internal class Fleeting
    {
        #region Singleton

        private static Fleeting _fleeting;
        internal static Fleeting Instance => _fleeting ?? (_fleeting = new Fleeting());

        #endregion

        private Fleeting()
        {
            BotBasePulseFlags = PulseFlags.All;
        }

        #region BotBase PulseFlags

        /// <summary>
        /// Internally accessable property for the botbase's PulseFlags property.
        /// Used to enable/disable avoidance.
        /// </summary>
        internal PulseFlags BotBasePulseFlags { get; set; }

        #endregion
    }
}