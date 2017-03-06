using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace Topevery.FMP.ObjectModel
{

    public class RemoveEventArgs : EventArgs
    {
        private int _index;
        public RemoveEventArgs(int index)
        {
            _index = index;
        }

        #region Properties
        public int Index
        {
            get
            {
                return _index;
            }
        }
        #endregion
    }

    public class ChangedEventArgs<TItem> : RemoveEventArgs
    {
        private TItem _item;
        public ChangedEventArgs(int index, TItem item)
            : base(index)
        {
            _item = item;
        }

        #region Properties
        public TItem Item
        {
            get
            {
                return _item;
            }
        }
        #endregion
    }
    [Serializable]
    public class ListBaseWithEvent<T> : Collection<T>
    {
        public EventHandler<RemoveEventArgs> Removed;
        public EventHandler<ChangedEventArgs<T>> InsertItemed;
        public EventHandler<ChangedEventArgs<T>> SetItemed;
        public EventHandler Cleared;

        public ListBaseWithEvent()
        {
        }

        #region Methods
        public virtual void AddRange(IEnumerable<T> items)
        {
            if (items != null)
            {
                foreach (T item in items)
                {
                    this.Add(item);
                }
            }
        }
        #endregion

        #region Fields
        #region Protected
        protected virtual void OnRemoved(RemoveEventArgs e)
        {
            if (Removed != null)
            {
                this.Removed(this, e);
            }
        }

        protected virtual void OnInsertItemed(ChangedEventArgs<T> e)
        {
            if (InsertItemed != null)
            {
                this.InsertItemed(this, e);
            }
        }

        protected virtual void OnSetItemed(ChangedEventArgs<T> e)
        {
            if (SetItemed != null)
            {
                this.SetItemed(this, e);
            }
        }

        protected virtual void OnCleared(EventArgs e)
        {
            if (Cleared != null)
            {
                this.Cleared(this, e);
            }
        }
        #endregion

        protected override void InsertItem(int index, T item)
        {
            base.InsertItem(index, item);
            ChangedEventArgs<T> e = new ChangedEventArgs<T>(index, item);
            this.OnInsertItemed(e);
        }

        protected override void RemoveItem(int index)
        {
            base.RemoveItem(index);
            RemoveEventArgs e = new RemoveEventArgs(index);
            this.OnRemoved(e);
        }

        protected override void SetItem(int index, T item)
        {
            base.SetItem(index, item);
            ChangedEventArgs<T> e = new ChangedEventArgs<T>(index, item);
            this.OnSetItemed(e);
        }

        protected override void ClearItems()
        {
            base.ClearItems();
            this.OnCleared(EventArgs.Empty);
        }
        #endregion
    }
}
