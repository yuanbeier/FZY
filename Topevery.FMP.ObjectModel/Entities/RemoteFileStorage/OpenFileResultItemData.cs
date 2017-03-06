using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class OpenFileResultItemData : LogicFileResultItemData
    {
        #region Fields
        private RecordLockedData _lockData;        
        #endregion

        #region Properties
        public Guid LockedID
        {
            get
            {
                if (this._lockData != null)
                {
                    return this._lockData.ID;
                }
                return Guid.Empty;
            }
        }

        public RecordLockedData LockedData
        {
            get
            {
                return this._lockData;
            }
            set
            {
                this._lockData = value;
            }
        }
        #endregion
    }
}
