using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel;
using System.Data.Common;

namespace Topevery.FMP.Data
{
    public abstract class RecordLockedDataProvider : BaseMetaDataProvider, IRecordLockedDataProvider
    {
        #region Fields
        private DbCommand _unlockCmd;

        #endregion

        #region Constructor
        protected RecordLockedDataProvider()
        {
        }

        #endregion
        #region IRecordLocked Members
        public abstract RecordLockedData CreateLockInfo(RecordLockedData lockedData);
        public abstract void UnLock(Guid id);
        public abstract void UpdateLockInfo(Guid id, DateTime expireDateTime);
        public abstract RecordLockedDataCollection GetLockedInfo(RecordLockedFetchParameter fetchParam);

        protected abstract DbCommand CreateUnlockCommand();
        #endregion

        #region Properties
        protected DbCommand UnlockCommand
        {
            get
            {                
                if (this._unlockCmd == null)
                {
                    this._unlockCmd = this.CreateUnlockCommand();
                }
                return this._unlockCmd;
            }
        }
        #endregion
    }
}
