using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Buddy.Overlay.Commands;
using ff14bot;
using ff14bot.Managers;
using Kombatant.Annotations;
using Kombatant.Settings.Models;

namespace Kombatant.Forms.Models
{
    /// <summary>
    /// ViewModel for the settings window.
    /// </summary>
    public class SettingsFormModel : INotifyPropertyChanged
    {
        private static SettingsFormModel _settingsFormModel;
        internal static SettingsFormModel Instance => _settingsFormModel ?? (_settingsFormModel = new SettingsFormModel());

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private SettingsFormModel()
        {

        }

        /// <summary>
        /// Reference for the BotBase settings.
        /// Placed here, so we only have to deal with one model.
        /// </summary>
        public Settings.BotBase BotBase
        {
            get => Settings.BotBase.Instance;

            set
            {
                Settings.BotBase.Overwrite(value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Reference for the Hotkeys settings.
        /// Placed here, so we only have to deal with one model.
        /// </summary>
        public Settings.Hotkeys Hotkeys
        {
            get => Settings.Hotkeys.Instance;

            set
            {
                Settings.Hotkeys.Overwrite(value);
                OnPropertyChanged();
            }
        }

        /// <summary>
        /// Pops open the combat routine selector.
        /// </summary>
        public ICommand SelectCombatRoutine
        {
            get
            {
                return new RelayCommand(s =>
                {
                    RoutineManager.PreferedRoutine = @"";
                    RoutineManager.PickRoutine();
                });
            }
        }

        /// <summary>
        /// Adds the currently selected target to the targeting whitelist.
        /// </summary>
        public ICommand AddToTargetWhitelist
        {
            get
            {
                return new RelayCommand(s =>
                {
                    var to = new TargetObject(1, "Test");
                    /*
                    if (!Core.Me.HasTarget || BotBase.TargetWhitelist.Any(o => o.NpcId == Core.Target.NpcId) || !Core.Target.IsEnemy())
                        return;
                    */
                    BotBase.TargetWhitelist.Add(to);
                    //LogHelper.Instance.Log($"Adding target {Core.Target.Name} to whitelist...");
                });
            }
        }

        /// <summary>
        /// Removes the currently selected entry from the targeting whitelist.
        /// </summary>
        public ICommand RemoveFromTargetWhitelist
        {
            get
            {
                return new RelayCommand(s =>
                {
                    var to = new TargetObject(Core.Target.NpcId, Core.Target.Name);
                    /*if (!Core.Me.HasTarget || BotBase.TargetWhitelist.All(o => o.NpcId != Core.Target.NpcId) || !Core.Target.IsEnemy())
                        return;*/

                    //LogHelper.Instance.Log($"Removing target {Core.Target.Name} from whitelist...");
                    BotBase.TargetWhitelist.Remove(to);
                });
            }
        }
    }
}