using ff14bot.Behavior;

namespace Kombatant.Helpers
{
    /// <summary>
    /// Helper for managing <see cref="ff14bot.Behavior.PulseFlags"/> in this botbase.
    /// Does use bitwise operations instead of the managed methods to get things done without performance impact.
    /// </summary>
    internal class PulseFlagHelper
    {
        #region Singleton

        private static PulseFlagHelper _pulseFlagHelper;
        internal static PulseFlagHelper Instance => _pulseFlagHelper ?? (_pulseFlagHelper = new PulseFlagHelper());

        #endregion

        /// <summary>
        /// Enables a <see cref="ff14bot.Behavior.PulseFlags"/> flag for this botbase.
        /// </summary>
        /// <param name="pulseFlag"><see cref="ff14bot.Behavior.PulseFlags"/> to enable.</param>
        /// <returns><c>true</c> when value was changed, <c>false</c> when the value was already set.</returns>
        internal bool EnablePulseFlag(PulseFlags pulseFlag)
        {
            if ((Settings.Fleeting.Instance.BotBasePulseFlags & pulseFlag) == pulseFlag)
                return false;

            LogHelper.Instance.Log(Resources.Localization.Msg_PulseFlagEnabled, pulseFlag);
            Settings.Fleeting.Instance.BotBasePulseFlags |= pulseFlag;
            return true;
        }

        /// <summary>
        /// Disables a <see cref="ff14bot.Behavior.PulseFlags"/> flag for this botbase.
        /// </summary>
        /// <param name="pulseFlag"><see cref="ff14bot.Behavior.PulseFlags"/> to disable.</param>
        /// <returns><c>true</c> when value was changed, <c>false</c> when the value was already set.</returns>
        internal bool DisablePulseFlag(PulseFlags pulseFlag)
        {
            if ((Settings.Fleeting.Instance.BotBasePulseFlags & pulseFlag) != pulseFlag)
                return false;

            LogHelper.Instance.Log(Resources.Localization.Msg_PulseFlagDisabled, pulseFlag);
            Settings.Fleeting.Instance.BotBasePulseFlags &= ~pulseFlag;
            return true;
        }
    }
}