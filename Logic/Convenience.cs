using System;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using ff14bot;
using ff14bot.Managers;
using ff14bot.Objects;
using ff14bot.RemoteAgents;
using ff14bot.RemoteWindows;
using Kombatant.Extensions;
using Kombatant.Helpers;
using Kombatant.Interfaces;

namespace Kombatant.Logic
{
    /// <summary>
    /// Logic for convenience functions like auto-sprint, skip cutscenes et al.
    /// </summary>
    /// <inheritdoc cref="M:Komabatant.Interfaces.LogicExecutor"/>
    internal class Convenience : LogicExecutor
    {
        #region Singleton

        private static Convenience _convenience;
        internal static Convenience Instance => _convenience ?? (_convenience = new Convenience());

        #endregion

        private const string AutoEndActWaitName = @"Convenience.AutoEndActEncounters";
        private const string AutoEmoteWaitName = @"Convenience.AutoEmote";

        /// <summary>
        /// Loop store to determine whether the player/the party was previously in a fight or not.
        /// </summary>
        private bool _wasFighting;

        /// <summary>
        /// Main task executor for the convenience logic.
        /// </summary>
        /// <returns>Returns <c>true</c> if any action was executed, otherwise <c>false</c>.</returns>
        internal new async Task<bool> ExecuteLogic()
        {
            // Do not execute this logic if the botbase is paused
            if (Settings.BotBase.Instance.IsPaused)
                return await Task.FromResult(false);

            // Auto end ACT encounters. Place before CanFight check to ensure it fires regardless!
            if (ShouldExecuteAutoEndActEncounters())
                ExecuteAutoEndActEncounters();

            // We can't do anything if we are dead...
            if (!Core.Me.CanFight())
                return await Task.FromResult(false);

            // Automatically keep up an emote
            if (ShouldExecuteAutoEmote())
                ExecuteAutoEmote();

            // Auto skip cutscenes
            if (Settings.BotBase.Instance.AutoSkipCutscenes && QuestLogManager.InCutscene)
                if (ExecuteSkipCutscene())
                    return await Task.FromResult(true);

            // Auto advance dialogue
            if (Settings.BotBase.Instance.AutoAdvanceDialogue)
                if (ExecuteAutoAdvanceDialogue())
                    return await Task.FromResult(true);

            // Auto accept/complete quests
            if (Settings.BotBase.Instance.AutoAcceptQuests)
                if (ExecuteAutoAcceptQuests())
                    return await Task.FromResult(true);

            // Auto sprint
            if (Settings.BotBase.Instance.AutoSprint)
                if (ExecuteAutoSprint())
                    return await Task.FromResult(true);

            // Auto sync FATE
            if (Settings.BotBase.Instance.AutoSyncFate)
                if (ExecuteAutoSyncFate())
                    return await Task.FromResult(true);

            return await Task.FromResult(false);
        }

        /// <summary>
        /// Determines whether the botbase should perform auto-emotes.
        /// </summary>
        /// <returns></returns>
        private bool ShouldExecuteAutoEmote()
        {
            return Settings.BotBase.Instance.AutoEmoteInterval > 0 &&
                   !string.IsNullOrEmpty(Settings.BotBase.Instance.AutoEmoteCommand.Trim());
        }

        /// <summary>
        /// Determines whether the botbase should automatically try to end ACT encounters.
        /// </summary>
        /// <returns></returns>
        private bool ShouldExecuteAutoEndActEncounters()
        {
            return Settings.BotBase.Instance.AutoEndActEncounters;
        }

        /// <summary>
        /// Auto accepts/completes quests.
        /// </summary>
        /// <returns></returns>
        private bool ExecuteAutoAcceptQuests()
        {
            // Auto accept quest
            if (JournalAccept.IsOpen)
            {
                JournalAccept.Accept();
                return true;
            }

            // Auto complete quest
            if (JournalResult.IsOpen)
            {
                if (!JournalResult.ButtonClickable)
                    JournalResult.SelectSlot(0);

                if (JournalResult.ButtonClickable)
                {
                    JournalResult.Complete();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Will automatically advance dialogue when possible.
        /// </summary>
        /// <returns></returns>
        private bool ExecuteAutoAdvanceDialogue()
        {
            // Auto progress text box
            if (Talk.DialogOpen)
            {
                Talk.Next();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Will automatically trigger an emote every x seconds.
        /// </summary>
        /// <returns></returns>
        private void ExecuteAutoEmote()
        {
            if(!WaitHelper.Instance.HasWait(AutoEmoteWaitName))
                WaitHelper.Instance.AddWait(AutoEmoteWaitName, new TimeSpan(0, 0, 0, Settings.BotBase.Instance.AutoEmoteInterval));

            if (WaitHelper.Instance.IsFinished(AutoEmoteWaitName))
            {
                ChatManager.SendChat(Settings.BotBase.Instance.AutoEmoteCommand);
                WaitHelper.Instance.ResetWait(AutoEmoteWaitName);
            }
        }

        /// <summary>
        /// Will automatically end encounters in ACT when no enemies are nearby for a given amount of time.
        /// </summary>
        /// <returns></returns>
        private void ExecuteAutoEndActEncounters()
        {
            if(!WaitHelper.Instance.HasWait(AutoEndActWaitName))
                WaitHelper.Instance.AddWait(AutoEndActWaitName, new TimeSpan(0, 0, 0, 10));

            // Are we brawling?
            if (Core.Me.InCombat || PartyManager.VisibleMembers.Any(member => member.BattleCharacter.InCombat))
            {
                _wasFighting = true;
                WaitHelper.Instance.ResetWait(AutoEndActWaitName);
                return;
            }

            // Are we lazy?
            if(WaitHelper.Instance.IsFinished(AutoEndActWaitName))
            {
                // Only send the command when ACT is running, obviously...
                if(IsActRunning() && _wasFighting)
                {
                    _wasFighting = false;
                    ChatManager.SendChat(@"/echo end");
                }
            }
        }

        /// <summary>
        /// Will automatically sprint whenever available.
        /// </summary>
        /// <returns></returns>
        private bool ExecuteAutoSprint()
        {
            if(ActionManager.IsSprintReady && MovementManager.IsMoving)
            {
                ActionManager.Sprint();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Will automatically sync to a FATE if selected mob is part of a FATE.
        /// </summary>
        /// <returns></returns>
        private bool ExecuteAutoSyncFate()
        {
            // Because RebornBuddy can be whimsical with it's Core.Target...
            var target = Core.Target as BattleCharacter;

            // We have a target and it is part of a FATE.
            if (target != null && target.IsCharacter() && target.IsFate)
            {
                var fate = FateManager.GetFateById(target.FateId);

                // Sync us down, Scotty!
                if(fate != null && fate.Within2D(Core.Me.Location) && Core.Me.ClassLevel > fate.MaxLevel)
                {
                    LogHelper.Instance.Log(Resources.Localization.Msg_AutoSyncFate, fate.Name);
                    Core.Me.SyncToFate();
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Will skip cutscenes by trying to exit out of them or advance the dialogue.
        /// </summary>
        /// <returns></returns>
        private bool ExecuteSkipCutscene()
        {
            // Try to skip the cutscene
            AgentCutScene.Instance.PromptSkip();

            if (AgentCutScene.Instance.CanSkip && SelectString.IsOpen)
            {
                SelectString.ClickSlot(0);
                return true;
            }

            // If that is not an option, at least try to forward it as fast as possible...
            if (Talk.DialogOpen)
            {
                Talk.Next();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Checks whether the Advanced Combat Tracker process can be found in the running task list.
        /// </summary>
        /// <returns></returns>
        private bool IsActRunning()
        {
            return Process.GetProcessesByName(@"Advanced Combat Tracker").Any();
        }
    }
}