using System.ComponentModel;
using System.Runtime.CompilerServices;
using Kombatant.Annotations;

namespace Kombatant.Settings.Models
{
    public class TargetObject : INotifyPropertyChanged
    {
        private uint _npcId;

        public uint NpcId
        {
            get => _npcId;
            set
            {
                if (_npcId != value)
                {
                    _npcId = value;
                    OnPropertyChanged();
                }
            }
        }

        private string _name;

        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged();
                }
            }
        }

        internal TargetObject(uint npcId, string name)
        {
            NpcId = npcId;
            Name = name;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}