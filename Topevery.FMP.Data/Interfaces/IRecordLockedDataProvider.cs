using System;
using Topevery.Framework.Data;
using Topevery.FMP.ObjectModel;

namespace Topevery.FMP.Data
{
    public interface IRecordLockedDataProvider : IDataProvider
    {
        RecordLockedDataCollection GetLockedInfo(RecordLockedFetchParameter fetchParam);
        RecordLockedData CreateLockInfo(RecordLockedData lockedData);
        void UnLock(Guid id);
        void UpdateLockInfo(Guid id, DateTime expireDateTime);
    }
}
