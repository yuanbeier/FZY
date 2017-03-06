using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.Data;
using Topevery.FMP.Data;
using Topevery.FMP.ObjectModel;

namespace Topevery.FMP.Logic
{
    public class RecordLocked : ServiceBase,IRecordLocked
    {
        #region Fields        
        public static TimeSpan DefaultExpireTimeSpan = TimeSpan.FromMinutes(1);
        #endregion

        #region Methods
        #region IRecordLocked Members

        public virtual RecordLockedData Lock(RecordLockedData lockedData)
        {            
            RecordLockedData result = null;            
            RecordLockedFetchParameter fetchParam = CreateFetchParameter(lockedData);
            RecordLockedData data = this.GetLockedInfoData(fetchParam);
            if (data != null)
            {
                //if (lockedData.FormID != Guid.Empty
                //    && lockedData.FormUniqueID != Guid.Empty
                //    && data.FormID != Guid.Empty
                //    && data.FormUniqueID != Guid.Empty
                //    && data.FormID != lockedData.FormID
                //    && data.FormUniqueID != lockedData.FormUniqueID)
                //{
                //    __Error.RecordHasLocked(data.FormID, data.FormUniqueID);
                //}
                if (lockedData.FormID != Guid.Empty
                    && lockedData.FormUniqueID != Guid.Empty
                    && data.FormID != Guid.Empty
                    && data.FormUniqueID != Guid.Empty
                    )
                {
                    if ((data.FormID != lockedData.FormID
                        || data.FormUniqueID != lockedData.FormUniqueID)
                        && data.ExpireTime > DateTime.UtcNow
                    )
                    {
                        __Error.RecordHasLocked(data.FormID, data.FormUniqueID);
                    }
                    else if (data.FormID == lockedData.FormID
                        && data.FormUniqueID == lockedData.FormUniqueID
                        && data.ID != lockedData.ID
                        && data.ExpireTime > DateTime.UtcNow)
                    {
                        __Error.RecordHasLocked(data.FormID, data.FormUniqueID);
                    }
                }
            }
            else
            {
                data = (RecordLockedData)lockedData.Clone();
            }
            this.PrepareLockData(data);
            result = this.DataProvider.CreateLockInfo(data);            
            return result;
        }

        public virtual void UnLock(Guid id)
        {
            this.DataProvider.UnLock(id);
        }

        public virtual void UpdateLockInfo(Guid id, DateTime expireDateTime)
        {
            this.DataProvider.UpdateLockInfo(id, expireDateTime);
        }

        public virtual RecordLockedDataCollection GetLockedInfo(RecordLockedFetchParameter fetchParam)
        {
            return this.DataProvider.GetLockedInfo(fetchParam);
        }

        
        #endregion        
    
        protected virtual void PrepareLockData(RecordLockedData lockedData)
        {
            if (lockedData.ID == Guid.Empty)
            {
                lockedData.ID = CombineGuid.NewComboGuid();
            }
            if (lockedData.LockTime == DateTime.MinValue)
            {
                lockedData.LockTime = DateTime.UtcNow;
            }

            if (lockedData.ExpireTime < lockedData.LockTime)
            {
                lockedData.ExpireTime = lockedData.LockTime.Add(DefaultExpireTimeSpan);
            }
        }

        protected RecordLockedData GetLockedInfoData(RecordLockedFetchParameter fetchParam)
        {
            RecordLockedData result = null;
            RecordLockedDataCollection datas = this.GetLockedInfo(fetchParam);
            if (datas != null && datas.Count > 0)
            {
                result = datas[0];
            }
            return result;
        }

        protected RecordLockedFetchParameter CreateFetchParameter(RecordLockedData lockedData)
        {
            RecordLockedFetchParameter fetchParam = new RecordLockedFetchParameter();
            if (lockedData.ID != Guid.Empty)
            {
                fetchParam.LockID = lockedData.ID;
            }
            else if (lockedData.FormID != Guid.Empty && lockedData.FormUniqueID != Guid.Empty)
            {
                fetchParam.FormID = lockedData.FormID;
                fetchParam.FormUniqueID = lockedData.FormUniqueID;
            }
            return fetchParam;
        }

        protected override IDataProvider CreateDataProviderCore()
        {
            return DataProviderHelper.GetRecordLockedProvider();
        }

        
        #endregion

        #region Properties
        protected IRecordLockedDataProvider DataProvider
        {
            get
            {
                return this.InnerDataProvider as IRecordLockedDataProvider;
            }
        }
        #endregion

        #region IRecordLocked ≥…‘±

        public void SetDataProvider(IRecordLockedDataProvider dataProvider)
        {
            this.InnerDataProvider = dataProvider;
        }

        #endregion
    }
}
