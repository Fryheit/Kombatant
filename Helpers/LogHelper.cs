using System.Windows.Media;
using ff14bot.Helpers;

namespace Kombatant.Helpers
{
    /// <summary>
    /// Helper class to handle all logging-related matters in the botbase.
    /// </summary>
    internal class LogHelper
    {
        #region Singleton

        private static LogHelper _logHelper;
        internal static LogHelper Instance => _logHelper ?? (_logHelper = new LogHelper());

        #endregion

        internal void Log(object entry, params object[] args)
        {
            Logging.Write(Colors.Crimson, $@"[{Resources.Localization.Name_Kombatant}] {entry}", args);
        }
    }
}