using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using Topevery.Framework.Data;
using Topevery.Framework.Ioc;
using Topevery.FMP.ObjectModel;

namespace Topevery.FMP.Data
{
    public abstract class BaseMetaWithTransDataProvider<TData, TCollection> : BaseMetaDataProvider 
            where TData : BaseOrderDataEntity<Guid>
            where TCollection : ICollection<TData>
    {
        protected void CreateDataCollectionWithTransaction(BaseExecuteResult<TCollection> result, BaseUpdateParameter<TCollection> updateParam)
        {
            if (updateParam != null && updateParam.InputData.Count > 0)
            {
                DbConnection dbConn = this.Database.CreateConnection();
                dbConn.Open();
                DbTransaction trans = dbConn.BeginTransaction();
                try
                {
                    bool returnValue = updateParam.ReturnValue;
                    if (returnValue)
                    {
                        //result.ExecuteResult = this.CreateDataCollection();
                        AddDataCollection(result);
                    }
                    TData cloneData;
                    foreach (TData data in updateParam.InputData)
                    {
                        cloneData = (TData)data.Clone();
                        this.CreateDataCore(cloneData, trans);
                        if (returnValue)
                        {
                            result.ExecuteResult.Add(cloneData);
                        }
                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    Utils.BuilderExecuteResult(result, e);
                }
                finally
                {
                    if (dbConn.State == ConnectionState.Open)
                    {
                        dbConn.Close();
                    }
                }

            }
        }


        protected void UpdateDataCollectionWithTransaction(BaseExecuteResult<TCollection> result, BaseUpdateParameter<TCollection> updateParam)
        {
            if (updateParam != null && updateParam.InputData.Count > 0)
            {
                DbConnection dbConn = this.Database.CreateConnection();
                dbConn.Open();
                DbTransaction trans = dbConn.BeginTransaction();
                try
                {
                    bool returnValue = updateParam.ReturnValue;
                    if (returnValue)
                    {
                        //result.ExecuteResult = this.CreateDataCollection();
                        AddDataCollection(result);
                    }
                    TData cloneData;
                    foreach (TData data in updateParam.InputData)
                    {
                        cloneData = (TData)data.Clone();
                        this.UpdateDataCore(cloneData, trans);
                        if (returnValue)
                        {
                            result.ExecuteResult.Add(cloneData);
                        }
                    }
                    trans.Commit();
                }
                catch (Exception e)
                {
                    trans.Rollback();
                    Utils.BuilderExecuteResult(result, e);
                }
                finally
                {
                    if (dbConn.State == ConnectionState.Open)
                    {
                        dbConn.Close();
                    }
                }

            }
        }

        protected abstract void CreateDataCore(TData data, DbTransaction transaction);

        protected abstract void UpdateDataCore(TData data, DbTransaction transaction);

        protected virtual void AddDataCollection(BaseExecuteResult<TCollection> result)
        {
            result.ExecuteResult = ActivatorEx.CreateInstance<TCollection>();
        }

        protected virtual GuidEntityWrapper<TData> CreateWrapperData(TData data, bool created)
        {
            GuidEntityWrapper<TData> wrapper = new GuidEntityWrapper<TData>(data);
            if(created && data.ID.Equals(Guid.Empty))
            {
                data.ID = CombineGuid.NewComboGuid();
            }
            data.UpdatedDateTime = DateTime.UtcNow;
            wrapper.CreatedDate = data.UpdatedDateTime;
            wrapper.LastUpdatedDate = data.UpdatedDateTime;
            wrapper.CreatedUserID = data.CurrentUserID;
            wrapper.LastUpdatedUserID = data.CurrentUserID;
            return wrapper;
        }
    }

    
}
