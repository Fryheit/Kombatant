using System.Runtime.CompilerServices;

namespace Kombatant.Helpers
{
    /// <summary>
    /// Helper class to handle some of the more esoteric callstack things in Kombatant...
    /// </summary>
    internal class CallStackHelper
    {
        #region Singleton

        private static CallStackHelper _callstackHelper;
        internal static CallStackHelper Instance => _callstackHelper ?? (_callstackHelper = new CallStackHelper());

        #endregion

        /// <summary>
        /// Returns the name of the method that called it.
        /// </summary>
        /// <param name="caller"></param>
        /// <returns></returns>
        internal string GetCaller([CallerMemberName] string caller = null)
        {
            return caller;
        }
    }
}