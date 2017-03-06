using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel;
using Topevery.FMP.Data;

namespace Topevery.FMP.Logic
{
    /// <summary>
    /// 记录锁操作接口
    /// </summary>
    public interface IRecordLocked
    {
        void SetDataProvider(IRecordLockedDataProvider dataProvider);
        RecordLockedData Lock(RecordLockedData lockedData);
        void UnLock(Guid id);
        void UpdateLockInfo(Guid id, DateTime expireDateTime);
        RecordLockedDataCollection GetLockedInfo(RecordLockedFetchParameter fetchParam);
    }
}
