using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using System.Windows.Forms;
using System.Windows.Input;
using ff14bot.Helpers;
using Kombatant.Annotations;
using Kombatant.Settings.Models;
using Newtonsoft.Json;

namespace Kombatant.Settings
{
    /// <summary>
    /// Settings class for the botbase hotkey configuration.
    /// </summary>
    public class Hotkeys : JsonSettings, INotifyPropertyChanged
    {
        private static Hotkeys _hotkeys;
        public static Hotkeys Instance => _hotkeys ?? (_hotkeys = new Hotkeys("Hotkeys"));

        // ReSharper disable once MemberCanBePrivate.Global
        public Hotkeys(string filename) : base(
            Path.Combine(CharacterSettingsDirectory, "Kombatant", filename + ".json"))
        {

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null, bool save = false)
        {
            if(save)
                Save();

            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Reloads the settings.
        /// </summary>
        public static void Reload()
        {
            Instance.Save();
            _hotkeys = new Hotkeys("Hotkeys");
        }

        /// <summary>
        /// Overwrites the current Instance with the given new version.
        /// </summary>
        /// <param name="settings"></param>
        public static void Overwrite(Hotkeys settings)
        {
            _hotkeys = settings;
        }

        #region --- Dynamic Hotkeys

        private HashSet<DynamicHotkey> _dynamicHotkeys;

        [Description("List of dynamic hotkeys")]
        [JsonProperty("DynamicHotkeys")]
        public HashSet<DynamicHotkey> DynamicHotkeys
        {
            get
            {
                if(_dynamicHotkeys == null)
                    _dynamicHotkeys = new HashSet<DynamicHotkey>();

                return _dynamicHotkeys;
            }

            set
            {
                if (_dynamicHotkeys.Equals(value))
                    return;

                _dynamicHotkeys = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region --- Pause/Unpause

        private Keys _pauseKey;
        private ModifierKeys _pauseModifier;

        [DefaultValue(Keys.F)]
        [Description("Hotkey to pause/unpause Kombatant")]
        [JsonProperty("PauseKey")]
        public Keys PauseKey
        {
            get => _pauseKey;
            set
            {
                if(!_pauseKey.Equals(value))
                {
                    _pauseKey = value;
                    OnPropertyChanged();
                }
            }
        }

        [DefaultValue(ModifierKeys.Control)]
        [Description("Modifier for the Pause/Unpause hotkey")]
        [JsonProperty("PauseKeyModifier")]
        public ModifierKeys PauseKeyModifier
        {
            get => _pauseModifier;
            set
            {
                if(!_pauseModifier.Equals(value))
                {
                    _pauseModifier = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region --- Enable/Disable Autonomous Mode

        private Keys _autonomousKey;
        private ModifierKeys _autonomousModifier;

        [DefaultValue(Keys.T)]
        [Description("Hotkey to toggle autonomous mode")]
        [JsonProperty("ToggleAutonomousKey")]
        public Keys ToggleAutonomousKey
        {
            get => _autonomousKey;
            set
            {
                if(!_autonomousKey.Equals(value))
                {
                    _autonomousKey = value;
                    OnPropertyChanged();
                }
            }
        }

        [DefaultValue(ModifierKeys.Control)]
        [Description("Modifier for the autonomous toggle")]
        [JsonProperty("ToggleAutonomousModifierKey")]
        public ModifierKeys ToggleAutonomousModifierKey
        {
            get => _autonomousModifier;
            set
            {
                if(!_autonomousModifier.Equals(value))
                {
                    _autonomousModifier = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region --- Enable/Disable Auto Face

        private Keys _autofaceKey;
        private ModifierKeys _autofaceModifier;

        [DefaultValue(Keys.V)]
        [Description("Hotkey to toggle auto face")]
        [JsonProperty("AutoFaceToggleKey")]
        public Keys AutoFaceToggleKey
        {
            get => _autofaceKey;
            set
            {
                if(!_autofaceKey.Equals(value))
                {
                    _autofaceKey = value;
                    OnPropertyChanged();
                }
            }
        }

        [DefaultValue(ModifierKeys.Control)]
        [Description("Modifier for the auto face hotkey")]
        [JsonProperty("AutoFaceToggleModifierKey")]
        public ModifierKeys AutoFaceToggleModifierKey
        {
            get => _autofaceModifier;
            set
            {
                if(!_autofaceModifier.Equals(value))
                {
                    _autofaceModifier = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region --- Enable/Disable Auto Targeting

        private Keys _autotargetKey;
        private ModifierKeys _autotargetModifier;

        [DefaultValue(Keys.G)]
        [Description("Hotkey to toggle auto target selection")]
        [JsonProperty("AutoTargetToggleKey")]
        public Keys AutoTargetToggleKey
        {
            get => _autotargetKey;
            set
            {
                if(!_autotargetKey.Equals(value))
                {
                    _autotargetKey = value;
                    OnPropertyChanged();
                }
            }
        }

        [DefaultValue(ModifierKeys.Control)]
        [Description("Modifier for the auto target selection hotkey")]
        [JsonProperty("AutoTargetToggleModifierKey")]
        public ModifierKeys AutoTargetToggleModifierKey
        {
            get => _autotargetModifier;
            set
            {
                if(!_autotargetModifier.Equals(value))
                {
                    _autotargetModifier = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion

        #region --- Enable/Disable Avoidance

        private Keys _avoidanceKey;
        private ModifierKeys _avoidanceModifier;

        [DefaultValue(Keys.T)]
        [Description("Hotkey to toggle avoidance")]
        [JsonProperty("AvoidanceToggleKey")]
        public Keys AvoidanceToggleKey
        {
            get => _avoidanceKey;
            set
            {
                if(!_avoidanceKey.Equals(value))
                {
                    _avoidanceKey = value;
                    OnPropertyChanged();
                }
            }
        }

        [DefaultValue(ModifierKeys.Shift)]
        [Description("Modifier for the avoidance toggle hotkey")]
        [JsonProperty("AvoidanceToggleModifierKey")]
        public ModifierKeys AvoidanceToggleModifierKey
        {
            get => _avoidanceModifier;
            set
            {
                if(!_avoidanceModifier.Equals(value))
                {
                    _avoidanceModifier = value;
                    OnPropertyChanged();
                }
            }
        }

        #endregion
    }
}