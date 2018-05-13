using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Media;
using System.Resources;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ff14bot.RemoteWindows;
using Kombatant.Helpers;
using Kombatant.Interfaces;

namespace Kombatant.Logic
{
    /// <summary>
    /// Logic for automatically commencing duties.
    /// </summary>
    /// <inheritdoc cref="M:Komabatant.Interfaces.LogicExecutor"/>
    internal class CommenceDuty : LogicExecutor
    {
        #region Singleton

        private static CommenceDuty _commenceDuty;
        internal static CommenceDuty Instance => _commenceDuty ?? (_commenceDuty = new CommenceDuty());

        #endregion

        private IEnumerable<DictionaryEntry> _psycheList;
        private IEnumerable<string> _audioFiles;
        private readonly Random _random = new Random();

        /// <summary>
        /// Constructor for CommenceDuty.
        /// </summary>
        private CommenceDuty()
        {
            PopulatePsyches();
            PopulateSounds();
        }

        /// <summary>
        /// Main task executor for the Commence Duty logic.
        /// </summary>
        /// <returns>Returns <c>true</c> if any action was executed, otherwise <c>false</c>.</returns>
        internal new async Task<bool> ExecuteLogic()
        {
            // Do not execute this logic if the botbase is paused
            if (Settings.BotBase.Instance.IsPaused)
                return await Task.FromResult(false);

            // Play Duty notification sound
            if (ShouldPlayDutyReadySound())
            {
                ShowLogNotification();
                PlayNotificationSound();
                return await Task.FromResult(true);
            }

            // Auto accept Duty Finder
            if (ShouldAcceptDutyFinder())
            {
                LogHelper.Instance.Log(Resources.Localization.Msg_DutyConfirm);
                ContentsFinderConfirm.Commence();
                WaitHelper.Instance.RemoveWait(@"CommenceDuty.DutyNotificationSound");
                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }

        /// <summary>
        /// Determines whether we should play a sassy sound file when the Duty Finder pops.
        /// </summary>
        /// <returns></returns>
        private bool ShouldPlayDutyReadySound()
        {
            if (!Settings.BotBase.Instance.AutoAcceptDutyFinder)
                return false;

            if (ContentsFinderReady.IsOpen)
                return false;

            if (ContentsFinderConfirm.IsOpen && WaitHelper.Instance.IsDoneWaiting(@"CommenceDuty.DutyNotificationSound", TimeSpan.FromSeconds(45), true))
                return true;

            return false;
        }

        /// <summary>
        /// Determines whether a duty should automatically be commended.
        /// </summary>
        /// <returns></returns>
        private bool ShouldAcceptDutyFinder()
        {
            if (!Settings.BotBase.Instance.AutoAcceptDutyFinder || ContentsFinderReady.IsOpen || Core.Me.IsDead)
                return false;

            return ContentsFinderConfirm.IsOpen &&
                   WaitHelper.Instance.IsDoneWaiting(
                       @"CommenceDuty.AutoAcceptDutyFinder",
                       TimeSpan.FromSeconds(Settings.BotBase.Instance.DutyFinderWaitTime));
        }

        /// <summary>
        /// Populates the list of available /psyches.
        /// </summary>
        private void PopulatePsyches()
        {
            ResourceSet resourceSet = Resources.Localization.ResourceManager
                .GetResourceSet(CultureInfo.CurrentCulture, true, true);

            _psycheList = resourceSet.Cast<DictionaryEntry>()
                .Where(psyche => psyche.Key.ToString().StartsWith(@"Msg_DutyPsyche"));
        }

        /// <summary>
        /// Populates the list of possible notification sound files.
        /// </summary>
        private void PopulateSounds()
        {
            _audioFiles = Directory.EnumerateFiles(Path.Combine(BotManager.BotBaseDirectory, @"Kombatant", @"Resources", @"Audio"), @"*.wav");
        }

        /// <summary>
        /// Prints an entry into RebornBuddy's log indicating that the Duty is ready.
        /// </summary>
        private void ShowLogNotification()
        {
            // ReSharper disable once RedundantAssignment
            var psyche = _psycheList.ElementAt(_random.Next(_psycheList.Count())).Value.ToString();
            LogHelper.Instance.Log($@"{Resources.Localization.Msg_DutyReady} {psyche}");
        }

        /// <summary>
        /// Plays one of the available notification sounds.
        /// No kekeke though, because it scares the carbuncle.
        /// </summary>
        private void PlayNotificationSound()
        {
            if(_audioFiles.Any())
                new SoundPlayer(_audioFiles.ElementAt(_random.Next(_audioFiles.Count()))).Play();
        }
    }
}