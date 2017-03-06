using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class PhysicalFileInfoDataCollection : BaseGuidCollection<PhysicalFileInfoData>
    {
        #region Fields
        private LogicFileInfoData _owner;
        #endregion
        
        #region Constructor
        public PhysicalFileInfoDataCollection() 
        {
            
        }

        public PhysicalFileInfoDataCollection(LogicFileInfoData owner)            
        {
            _owner = owner;
        }
        #endregion

        #region Methods
        protected override void InsertItem(int index, PhysicalFileInfoData item)
        {
            if (item != null)
            {
                item.SetParent(_owner);
                base.InsertItem(index, item);
            }
        }

        protected override void SetItem(int index, PhysicalFileInfoData item)
        {
            if (item != null)
            {
                item.SetParent(_owner);
                base.SetItem(index, item);
            }
        }
        #endregion
    }
}
