using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel;
using Topevery.Framework.Data;
using System.Data.Common;

namespace Topevery.FMP.Data
{
    internal static class Utils
    {
        public const int DefaultErrorCode = -1;


        public static void BuilderExecuteResult<T>(BaseExecuteResult<T> result, Exception e)
        {
            if(result != null && e != null)
            {
                result.ExecuteResult = default(T);
                result.InnerException.ErrorCode = Utils.DefaultErrorCode;
                result.InnerException.ErrorMessage = e.Message;
            }
        }
         
        public static object GetGuidParameterValue(Guid parentID)
        {
            if (parentID.Equals(Guid.Empty))
            {
                return Guid.Empty;
            }
            return parentID;
        }

        public static GuidEntityWrapper<TData> CreateWrapperData<TData>(TData data, bool created) where TData : BaseOrderDataEntity<Guid>
        {
            GuidEntityWrapper<TData> wrapper = new GuidEntityWrapper<TData>(data);
            if (created && data.ID.Equals(Guid.Empty))
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
        public static void PrepareParameter(DbCommand dbCmd)
        {
            foreach (DbParameter p in dbCmd.Parameters)
            {
                if (!string.IsNullOrEmpty(p.ParameterName) && (p.ParameterName.StartsWith("p_")
                    || p.ParameterName.StartsWith("P_")))
                {
                    p.ParameterName = p.ParameterName.Substring(2);
                }
            }
        }

        public static void ResetParameter(DbCommand dbCmd)
        {
            foreach (DbParameter p in dbCmd.Parameters)
            {
                if (!string.IsNullOrEmpty(p.ParameterName) && (!p.ParameterName.StartsWith("p_")
                    && !p.ParameterName.StartsWith("P_")))
                {
                    p.ParameterName = "p_" + p.ParameterName;
                }
            }
        }
    }
}
