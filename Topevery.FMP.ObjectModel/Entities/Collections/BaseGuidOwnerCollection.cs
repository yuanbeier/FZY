using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.Data;
using Topevery.Framework.Data.Collections;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class BaseGuidOwnerCollection<TItem> : BaseGuidCollection<TItem> where TItem : BaseEntity<Guid>, ITreeEntity<TItem>
    {
        #region Fields

        private TItem _owner;
        private bool _setParent;
        #endregion

        #region Constructor
        public BaseGuidOwnerCollection(TItem owner)
        {
            _owner = owner;
            if (_owner != null)
            {
                this._setParent = true;
            }            
        }

        public BaseGuidOwnerCollection(TItem owner, bool setParent)
        {
            _owner = owner;
            _setParent = setParent;
        }
        #endregion

        #region Methods
       
        public override void CopyFrom(BaseKeyedCollection<Guid, TItem> items)
        {
            base.CopyFrom(items);
            BaseGuidOwnerCollection<TItem> sourceItem = items as BaseGuidOwnerCollection<TItem>;
            if(sourceItem != null)
            {
                this._owner = sourceItem._owner;
                this._setParent = sourceItem._setParent;
            }
        }
        protected override void InsertItem(int index, TItem item)
        {
            if (item != null)
            {
                if (_setParent)
                {
                    item.SetParent(_owner);
                }
                base.InsertItem(index, item);
            }
        }

        protected override void SetItem(int index, TItem item)
        {
            if (item != null)
            {
                if (_setParent)
                {
                    item.SetParent(_owner);
                }
                base.SetItem(index, item);
            }
        }
        #endregion
    }
}
