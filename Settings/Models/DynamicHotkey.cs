using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;

namespace Kombatant.Settings.Models
{
    /// <summary>
    /// Model that represents a single dynamic hotkey
    /// </summary>
    public class DynamicHotkey
    {
        private Keys _key;
        private ModifierKeys _modifier;
        private string _name;
        private HashSet<string> _hotkeyToCall;

        public DynamicHotkey()
        {
            _key = Keys.None;
            _modifier = ModifierKeys.None;
            _name = string.Empty;
            _hotkeyToCall = new HashSet<string>();
        }

        public DynamicHotkey(Keys key, ModifierKeys modifier, string name, HashSet<string> hotkeysToCall)
        {
            _key = key;
            _modifier = modifier;
            _name = name;
            _hotkeyToCall = hotkeysToCall;
        }

        /// <summary>
        /// Hotkey
        /// </summary>
        public Keys Hotkey
        {
            get => _key;
            set => _key = value;
        }

        /// <summary>
        /// Modifier for the hotkey
        /// </summary>
        public ModifierKeys HotkeyModifier
        {
            get => _modifier;
            set => _modifier = value;
        }

        /// <summary>
        /// Simple name for the hotkey
        /// </summary>
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        /// <summary>
        /// Names of the hotkeys within RebornBuddy that should be called when this dynamic hotkey is pressed.
        /// </summary>
        public HashSet<string> HotkeysToCall
        {
            get => _hotkeyToCall;
            set => _hotkeyToCall = value;
        }
    }
}