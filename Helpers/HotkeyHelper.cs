using System.Collections.Generic;
using System.Linq;
using System.Windows.Media;
using ff14bot.Managers;
using Kombatant.Resources;
using Kombatant.Settings;
using Hotkey = Kombatant.Constants.Hotkey;

namespace Kombatant.Helpers
{
    /// <summary>
    /// Helper class to handle all the registering and unregistering of hotkeys in the botbase.
    /// </summary>
    internal class HotkeyHelper
    {
        #region Singleton

        private static HotkeyHelper _hotkeyHelper;
        internal static HotkeyHelper Instance => _hotkeyHelper ?? (_hotkeyHelper = new HotkeyHelper());

        #endregion

        #region Predefined Colors for enable/disable toasts

        private readonly Color _fontColorEnable    = Color.FromRgb(65, 245, 65);
        private readonly Color _shadowColorEnable  = Color.FromRgb(30, 110, 30);
        private readonly Color _fontColorDisable   = Color.FromRgb(245, 65, 65);
        private readonly Color _shadowColorDisable = Color.FromRgb(110, 30, 30);

        #endregion

        /// <summary>
        /// <para>Contains a list of all dynamic hotkeys registered through this class.</para>
        /// <para>The contents of this list are used to deny chaining multiple dynamic hotkeys together.</para>
        /// </summary>
        private readonly HashSet<string> _dynamicHotkeys = new HashSet<string>();

        /// <summary>
        /// Registers all Kombatant hotkeys.
        /// </summary>
        // ReSharper disable once MemberCanBePrivate.Global
        internal void RegisterHotkeys()
        {
            RegisterPauseToggleHotkey();
            RegisterAutoFaceTargetToggleHotkey();
            RegisterAutonomousToggleHotkey();
            RegisterAutoTargetToggleHotkey();
            RegisterAvoidanceToggleHotkey();
            RegisterDynamicHotkeys();
        }

        /// <summary>
        /// Unregisters all Kombatant hotkeys.
        /// </summary>
        internal void UnregisterHotkeys()
        {
            HotkeyManager.Unregister(Hotkey.HotkeyPauseToggle);
            HotkeyManager.Unregister(Hotkey.HotkeyAutoFaceToggle);
            HotkeyManager.Unregister(Hotkey.HotkeyAutonomousToggle);
            HotkeyManager.Unregister(Hotkey.HotkeyAutoTargetToggle);
            HotkeyManager.Unregister(Hotkey.HotkeyAvoidanceToggle);
            UnregisterDynamicHotkeys();
        }

        /// <summary>
        /// Unregisters/Registers hotkeys.
        /// </summary>
        internal void ReloadHotkeys()
        {
            UnregisterHotkeys();
            RegisterHotkeys();
        }

        /// <summary>
        /// Returns whether a given hotkey name is known to be a dynamic hotkey.
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsDynamicHotkey(string name)
        {
            return _dynamicHotkeys.Any(hk => hk == name);
        }

        /// <summary>
        /// <para>Returns whether a given hotkey name is already in use.</para>
        /// <para>Only available names can be registered to ensure nothing funky is going on.</para>
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private bool IsHotkeyNameInUse(string name)
        {
            return HotkeyManager.RegisteredHotkeys.Any(hotkey => hotkey.Name == name);
        }

        /// <summary>
        /// Registers all dynamic hotkeys.
        /// </summary>
        private void RegisterDynamicHotkeys()
        {
            LogHelper.Instance.Log("Initializing dynamic hotkeys.");

            if(Hotkeys.Instance.DynamicHotkeys.Any())
            {
                // Make sure we do not start with a tainted list of hotkeys.
                _dynamicHotkeys.Clear();

                foreach (var hotkey in Hotkeys.Instance.DynamicHotkeys)
                {
                    // Specified name for the dynamic hotkey already in use?
                    if (IsHotkeyNameInUse(hotkey.Name))
                    {
                        LogHelper.Instance.Log("Hotkey name \"{0}\" already in use, skipping...", hotkey.Name);
                        continue;
                    }

                    HotkeyManager.Register(hotkey.Name, hotkey.Hotkey, hotkey.HotkeyModifier, dhk =>
                    {
                        // Iterate through all available hotkeys in RebornBuddy that match the names we want to trigger.
                        // But make sure we do not chain multiple dynamic hotkeys together. You know, loops and stuff...
                        // This is an intentional design choice. Because I do not trust you, you crazy carbuncles!
                        foreach(var hk in HotkeyManager.RegisteredHotkeys
                            .Where(k => hotkey.HotkeysToCall.Contains(k.Name) && !IsDynamicHotkey(k.Name)))
                        {
                            var hotkeyAction = hk.Callback;
                            var hotkeyObject = hk;

                            hotkeyAction.Invoke(hotkeyObject);
                        }
                    });

                    // Add the hotkey name to the index, so we can query and remove it later on.
                    _dynamicHotkeys.Add(hotkey.Name);

                    LogHelper.Instance.Log("Registered dynamic hotkey: {0}", hotkey.Name);
                }

                LogHelper.Instance.Log("All dynamic hotkeys initialized.");
            }
            else
            {
                LogHelper.Instance.Log("No dynamic hotkeys defined.");
            }
        }

        /// <summary>
        /// Unregisters all dynamic hotkeys in the index.
        /// </summary>
        private void UnregisterDynamicHotkeys()
        {
            LogHelper.Instance.Log(@"Shutting down dynamic hotkeys.");

            foreach (var hotkeyName in _dynamicHotkeys)
            {
                LogHelper.Instance.Log(@"Removing dynamic hotkey: {0}", hotkeyName);
                HotkeyManager.Unregister(hotkeyName);
                _dynamicHotkeys.Remove(hotkeyName);
            }

            LogHelper.Instance.Log(@"All dynamic hotkeys removed.");
        }

        /// <summary>
        /// Registers the "Pause" hotkey.
        /// </summary>
        private void RegisterPauseToggleHotkey()
        {
            HotkeyManager.Register(Hotkey.HotkeyPauseToggle, Hotkeys.Instance.PauseKey, Hotkeys.Instance.PauseKeyModifier, hk =>
            {
                BotBase.Instance.IsPaused = !BotBase.Instance.IsPaused;

                var fontColor   = BotBase.Instance.IsPaused ? _fontColorDisable : _fontColorEnable;
                var shadowColor = BotBase.Instance.IsPaused ? _shadowColorDisable : _shadowColorEnable;
                var statusMsg   = BotBase.Instance.IsPaused ? Localization.Msg_Paused : Localization.Msg_Unpaused;

                OverlayHelper.Instance.AddToast(statusMsg, fontColor, shadowColor);
                LogHelper.Instance.Log(statusMsg);
            });
        }

        /// <summary>
        /// Registers the "Auto Face Target" hotkey.
        /// </summary>
        private void RegisterAutoFaceTargetToggleHotkey()
        {
            HotkeyManager.Register(Hotkey.HotkeyAutoFaceToggle, Hotkeys.Instance.AutoFaceToggleKey, Hotkeys.Instance.AutoFaceToggleModifierKey, hk =>
            {
                BotBase.Instance.AutoFaceTarget = !BotBase.Instance.AutoFaceTarget;

                var fontColor   = BotBase.Instance.AutoFaceTarget ? _fontColorEnable : _fontColorDisable;
                var shadowColor = BotBase.Instance.AutoFaceTarget ? _shadowColorEnable : _shadowColorDisable;
                var statusMsg   = BotBase.Instance.AutoFaceTarget ? Localization.Msg_AutoFaceTargetOn : Localization.Msg_AutoFaceTargetOff;

                if (BotBase.Instance.AutoFaceTarget)
                    WaitHelper.Instance.ResetWait(@"Target.AutoFace");

                OverlayHelper.Instance.AddToast(statusMsg, fontColor, shadowColor);
                LogHelper.Instance.Log(statusMsg);
            });
        }

        /// <summary>
        /// Registers the "Auto Target" hotkey.
        /// </summary>
        private void RegisterAutoTargetToggleHotkey()
        {
            HotkeyManager.Register(Hotkey.HotkeyAutoTargetToggle, Hotkeys.Instance.AutoTargetToggleKey, Hotkeys.Instance.AutoTargetToggleModifierKey, hk =>
            {
                BotBase.Instance.AutoTarget = !BotBase.Instance.AutoTarget;

                var fontColor   = BotBase.Instance.AutoTarget ? _fontColorEnable : _fontColorDisable;
                var shadowColor = BotBase.Instance.AutoTarget ? _shadowColorEnable : _shadowColorDisable;
                var statusMsg   = BotBase.Instance.AutoTarget ? Localization.Msg_AutoTargetOn : Localization.Msg_AutoTargetOff;

                if (BotBase.Instance.AutoTarget)
                    WaitHelper.Instance.ResetWait(@"Target.AutoTarget");

                OverlayHelper.Instance.AddToast(statusMsg, fontColor, shadowColor);
                LogHelper.Instance.Log(statusMsg);
            });
        }

        /// <summary>
        /// Registers the "Autonomous" hotkey.
        /// </summary>
        private void RegisterAutonomousToggleHotkey()
        {
            HotkeyManager.Register(Hotkey.HotkeyAutonomousToggle, Hotkeys.Instance.ToggleAutonomousKey, Hotkeys.Instance.ToggleAutonomousModifierKey, hk =>
            {
                BotBase.Instance.IsAutonomous = !BotBase.Instance.IsAutonomous;

                var fontColor   = BotBase.Instance.IsAutonomous ? _fontColorEnable : _fontColorDisable;
                var shadowColor = BotBase.Instance.IsAutonomous ? _shadowColorEnable : _shadowColorDisable;
                var statusMsg   = BotBase.Instance.IsAutonomous ? Localization.Msg_AutonomousOn : Localization.Msg_AutonomousOff;

                OverlayHelper.Instance.AddToast(statusMsg, fontColor, shadowColor);
                LogHelper.Instance.Log(statusMsg);
            });
        }

        /// <summary>
        /// Registers the "Avoidance" hotkey.
        /// </summary>
        private void RegisterAvoidanceToggleHotkey()
        {
            HotkeyManager.Register(Hotkey.HotkeyAvoidanceToggle, Hotkeys.Instance.AvoidanceToggleKey, Hotkeys.Instance.AvoidanceToggleModifierKey, hk =>
            {
                BotBase.Instance.EnableAvoidance = !BotBase.Instance.EnableAvoidance;

                var fontColor   = BotBase.Instance.EnableAvoidance ? _fontColorEnable : _fontColorDisable;
                var shadowColor = BotBase.Instance.EnableAvoidance ? _shadowColorEnable : _shadowColorDisable;
                var statusMsg   = BotBase.Instance.EnableAvoidance ? Localization.Msg_AvoidanceOn : Localization.Msg_AvoidanceOff;

                OverlayHelper.Instance.AddToast(statusMsg, fontColor, shadowColor);
                LogHelper.Instance.Log(statusMsg);
            });
        }
    }
}