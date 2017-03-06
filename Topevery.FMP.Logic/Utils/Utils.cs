using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel;
using System.Collections;

namespace Topevery.FMP.Logic
{
    internal static class Utils
    {
        public const int DefaultErrorCode = -1;

        public static void BuilderExecuteResult<T>(BaseExecuteResult<T> result, Exception e)
        {
            if (result != null && e != null)
            {
                result.ExecuteResult = default(T);
                result.InnerException.ErrorCode = Utils.DefaultErrorCode;
                result.InnerException.ErrorMessage = e.Message;
            }
        }

        public static bool IsEmpty(ICollection cols)
        {
            return cols == null || cols.Count == 0;
        }

        public static bool IsEmpty(string text)
        {
            return string.IsNullOrEmpty(text);
        }
    }
}
