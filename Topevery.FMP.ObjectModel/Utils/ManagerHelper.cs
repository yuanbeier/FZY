using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.Data;
using Topevery.FMP.ObjectModel;
using Topevery.FMP.ObjectModel.Exceptions;
using System.Collections;

namespace Topevery.FMP.ObjectModel
{
    internal static class ManagerHelper
    {

        public static void CheckNullReference(object checkValue, string propertyName)
        {
            __Error.CheckNullReference(checkValue, propertyName);
        }

        public static void CheckUpdateResult(BaseExecuteResult result )
        {
            __Error.CheckUpdateExecuteResult(result);
            if (result != null)
            {
                ((ISetReadOnly)result).SetReadOnly(true);
            }
        }

        public static void CheckFetchResult(BaseExecuteResult result)
        {
            __Error.CheckFetchExecuteResult(result);
            if (result != null)
            {
                ((ISetReadOnly)result).SetReadOnly(true);
            }
        }
        
        public static bool IsEmpty(ICollection cols)
        {
            return cols == null || cols.Count == 0;
        }
    }
}
