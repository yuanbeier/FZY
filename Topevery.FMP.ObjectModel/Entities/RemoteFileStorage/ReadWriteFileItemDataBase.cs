using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class ReadWriteFileItemDataBase
    {
        #region Fields
        private Guid _lockID;
        private Guid _phyiscalFileID;
        private string _clientFileName;
        private long _position;
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

        public Guid PhysicalFileID
        {
            get
            {
                return this._phyiscalFileID;
            }
            set
            {
                this._phyiscalFileID = value;
            }
        }

        public string ClientFileName
        {
            get
            {
                return this._clientFileName;
            }
            set
            {
                this._clientFileName = value;
            }
        }

        public long Position
        {
            get
            {
                return this._position;
            }
            set
            {
                this._position = value;
            }
        }
        #endregion
    }
}
