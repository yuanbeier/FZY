using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class CloseFileItemData
    {
        #region Fields
        private Guid _lockID;
        private Guid _fileID;
        #endregion

        #region Propreties
        public Guid FileID
        {
            get
            {
                return this._fileID;
            }
            set
            {
                this._fileID = value;
            }
        }

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
        #endregion
    }
}
