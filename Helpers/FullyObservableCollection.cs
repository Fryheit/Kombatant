using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using Clio.Utilities;

namespace Kombatant.Helpers
{
    /// <summary>
    /// Surprisingly, the default ObservableCollection doesnt handle changed property events on objects within collection
    /// </summary>
    public class FullyObservableCollection<T> : ObservableCollection<T>
    {
        public FullyObservableCollection()
        {
        }

        public FullyObservableCollection(IEnumerable<T> items, bool clone = false)
        {
            foreach (var item in items)
            {
                if (clone && item is ICloneable)
                {
                    Add((T)(item as ICloneable).Clone());
                }
                else
                {
                    Add(item);
                }
            }
        }

        public delegate void ChildElementPropertyChangedEventHandler(ChildElementPropertyChangedEventArgs e);

        /// <summary>
        /// Occurs when objects within the collection raise a PropertyChanged event
        /// </summary>
        public event ChildElementPropertyChangedEventHandler ChildElementPropertyChanged;

        private void OnChildElementPropertyChanged(object childelement, PropertyChangedEventArgs propertyChangedEventArgs)
        {
            ChildElementPropertyChanged?.Invoke(new ChildElementPropertyChangedEventArgs(childelement, propertyChangedEventArgs));
        }

        protected override void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            base.OnCollectionChanged(e);
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                foreach (T item in e.NewItems)
                {
                    if (item is INotifyPropertyChanged convertedItem)
                    {
                        convertedItem.PropertyChanged -= convertedItem_PropertyChanged;
                        convertedItem.PropertyChanged += convertedItem_PropertyChanged;
                    }
                }
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                foreach (T item in e.OldItems)
                {
                    if (item is INotifyPropertyChanged convertedItem)
                        convertedItem.PropertyChanged -= convertedItem_PropertyChanged;
                }
            }
        }

        public bool ForceResetOnChildChanges = false;

        private void convertedItem_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnChildElementPropertyChanged(sender, e);

            if (ForceResetOnChildChanges)
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        protected override void ClearItems()
        {
            foreach (T unknown in Items)
            {
                var item = (INotifyPropertyChanged) unknown;
                item.PropertyChanged -= convertedItem_PropertyChanged;
            }

            base.ClearItems();
        }

        public void CopyTo(FullyObservableCollection<T> that)
        {
            that.Clear();
            this.ForEach(that.Add);
        }

        public void Reset()
        {
            Clear();
        }
    }

    public class ChildElementPropertyChangedEventArgs : EventArgs
    {
        public ChildElementPropertyChangedEventArgs(object item, PropertyChangedEventArgs e)
        {
            ChildElement = item;
            PropertyName = e.PropertyName;
        }

        public object ChildElement { get; set; }
        public object PropertyName { get; set; }
    }
}