using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel;

namespace Topevery.FMP.Logic
{
    public static class RecordLockedManager
    {
        #region Fields
        [ThreadStatic]
        private static IRecordLocked _serviceProxy;
        #endregion

        #region Methods
        public static RecordLockedData Lock(RecordLockedData lockedInfo)
        {
            if (lockedInfo != null)
            {
                RecordLockedData result = ServiceProxy.Lock(lockedInfo);
                return result;
            }
            return null;
        }

        public static void Unlock(Guid id)
        {
            if (id != Guid.Empty)
            {
                ServiceProxy.UnLock(id);
            }
        }

        public static void UpdateLockInfo(Guid id, DateTime expireDateTime)
        {
            if (id != Guid.Empty && expireDateTime > DateTime.UtcNow)
            {
                ServiceProxy.UpdateLockInfo(id, expireDateTime);
            }
        }

        public static RecordLockedDataCollection GetRecordLockedInfo(RecordLockedFetchParameter fetchParam)
        {
            if (fetchParam != null)
            {
                RecordLockedDataCollection result = ServiceProxy.GetLockedInfo(fetchParam);
                return result;
            }
            return null;
        }
        #endregion

        #region Properties
        public static IRecordLocked ServiceProxy
        {
            get
            {
                if (_serviceProxy == null)
                {
                    _serviceProxy = new RecordLocked();
                }
                return _serviceProxy;
            }
        }
        #endregion
    }
}
