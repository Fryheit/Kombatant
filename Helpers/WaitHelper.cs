using System;
using System.Collections.Generic;
using Clio.Utilities.Helpers;

namespace Kombatant.Helpers
{
    /// <summary>
    /// Helper class to manage time waits. Used so we do not pulse certain tasks 30 times a second.
    /// </summary>
    internal class WaitHelper
    {
        #region Singleton

        private static WaitHelper _waitHelper;
        internal static WaitHelper Instance => _waitHelper ?? (_waitHelper = new WaitHelper());

        #endregion

        /// <summary>
        /// Holds a list of all managed waits.
        /// </summary>
        private readonly Dictionary<string, WaitTimer> _waitTimerList;

        /// <summary>
        /// Constructor WaitHelper()
        /// </summary>
        private WaitHelper()
        {
            _waitTimerList = new Dictionary<string, WaitTimer>();
        }

        /// <summary>
        /// <para>Adds a wait key.</para>
        /// <para>Will remove old keys under the same name before adding.</para>
        /// </summary>
        /// <param name="name">Name of the wait key to add</param>
        /// <param name="timeToWait">TimeSpan to wait</param>
        internal void AddWait(string name, TimeSpan timeToWait)
        {
            RemoveWait(name);

            WaitTimer timerToAdd = new WaitTimer(timeToWait);
            _waitTimerList.Add(name, timerToAdd);
            _waitTimerList[name].Reset();
        }

        /// <summary>
        /// Checks whether a given wait exists.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal bool HasWait(string name)
        {
            return _waitTimerList.ContainsKey(name);
        }

        /// <summary>
        /// Checks whether a given wait has finished.
        /// </summary>
        /// <param name="name">Name of the wait key to check</param>
        /// <param name="timeToWait">TimeSpan to wait</param>
        /// <param name="initialReturnValue">When set to true, the initial run that creates the waiter will already return true. This is useful when you wish to execute something at the beginning of the wait period rather than at the end.</param>
        /// <returns></returns>
        internal bool IsDoneWaiting(string name, TimeSpan timeToWait, bool initialReturnValue = false)
        {
            // TODO: Remove the old code still using this!
            if (!HasWait(name))
            {
                WaitTimer timerToAdd = new WaitTimer(timeToWait);
                _waitTimerList.Add(name, timerToAdd);
                _waitTimerList[name].Reset();

                return initialReturnValue;
            }

            if (_waitTimerList[name].IsFinished)
            {
                _waitTimerList.Remove(name);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Overload for IsDoneWaiting
        /// </summary>
        /// <param name="name">Name of the wait key to check</param>
        /// <returns></returns>
        internal bool IsDoneWaiting(string name)
        {
            // TODO: Remove the old code still using this!
            return IsDoneWaiting(name, TimeSpan.FromSeconds(1));
        }

        /// <summary>
        /// Syntax sugar, opposite of IsWaiting.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal bool IsFinished(string name)
        {
            return !IsWaiting(name);
        }

        /// <summary>
        /// Checks if a wait exists and is still waiting.
        /// </summary>
        /// <param name="name">Name of the wait key to remove</param>
        /// <returns></returns>
        internal bool IsWaiting(string name)
        {
            return HasWait(name) && !_waitTimerList[name].IsFinished;
        }

        /// <summary>
        /// Removes a given wait.
        /// </summary>
        /// <param name="name">Name of the wait key to remove</param>
        internal void RemoveWait(string name)
        {
            if (HasWait(name))
                _waitTimerList.Remove(name);
        }

        /// <summary>
        /// Resets a given wait.
        /// </summary>
        /// <param name="name">Name of the wait key to check</param>
        /// <returns></returns>
        internal void ResetWait(string name)
        {
            if (HasWait(name))
                _waitTimerList[name].Reset();
        }

        /// <summary>
        /// Returns the time left on a wait.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        internal TimeSpan TimeLeft(string name)
        {
            if (HasWait(name))
                return _waitTimerList[name].TimeLeft;

            return TimeSpan.Zero;
        }
    }
}