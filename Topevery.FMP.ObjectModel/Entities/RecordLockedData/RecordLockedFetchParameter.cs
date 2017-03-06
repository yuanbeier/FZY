using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class RecordLockedFetchParameter : BaseFetchParameter
    {
        #region Fields
        private Guid _lockID;
        private Guid _formID;
        private Guid _formUniqueID;
        #endregion

        #region Properties
        public Guid LockID
        {
            get
            {
                return this._lockID;
            }
            set
            {
                this._lockID = value;
            }
        }

        public Guid FormID
        {
            get
            {
                return this._formID;
            }
            set
            {
                this._formID = value;
            }
        }

        public Guid FormUniqueID
        {
            get
            {
                return this._formUniqueID;
            }
            set
            {
                this._formUniqueID = value;
            }
        }
        #endregion
    }
}
