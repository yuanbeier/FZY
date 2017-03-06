using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel.Exceptions
{
    public static class __Error
    {
        public static void CheckNullReference(object checkValue, string propertyName)
        {
            if(checkValue == null)
            {
                throw new NullReferenceException(propertyName);
            }
        }

        public static void CheckPropertyReadOnly(string propertyName)
        {
            string text = string.Format(SR.PropertyReadonlyError, propertyName);
            throw new ArgumentException(text);
        }
        
        public static void CheckFetchExecuteResult(BaseExecuteResult fetchresult)
        {
            if(fetchresult != null && fetchresult.Failed)
            {
                ThrowExecuteResult(fetchresult.InnerException.ErrorMessage);
            }
        }

        public static void CheckUpdateExecuteResult(BaseExecuteResult updateResult)
        {
            if (updateResult == null || updateResult.Failed)
            {
                string errorMsg;
                if(updateResult == null)
                {
                    errorMsg = SR.NullReturnValue;
                }
                else
                {
                    errorMsg = updateResult.InnerException.ErrorMessage;
                }
                string text = string.Format(SR.UpdateExecuteResultError, errorMsg);
                throw new ExecuteResultException(text);
            }
        }
        public static void ThrowExecuteResult(string errorMsg)
        {
            string text = string.Format(SR.FetchExecuteResultError, errorMsg);
            throw new ExecuteResultException(text);
        }
        // by jerry.zeng
        public static void ThrowException(string message)
        {
            if (message != null && message.Length > 0)
            {
                throw new Exception(message);
            }
        }


        #region RemoteStream
        public static void InvalidateFileID()
        {
            string text = SR.InvalidateFileID;
            throw new ArgumentException(text);
        }

        public static void InvalidateFileMode()
        {
            string text = SR.InvalidateFileMode;
            throw new ArgumentException(text);
        }

        public static void ExistsFileID(Guid id)
        {
            string text = SR.ExsitsFileID;
            string.Format(text, id);
            throw new ArgumentException(text);
        }

        public static void FileReadOnly(Guid id)
        {
            string text = SR.FileReadOnly;
            string.Format(text, id);
            throw new ArgumentException(text);
        }

        public static void NotExistsFileID(Guid id)
        {
            string text = SR.NotExsitsFileID;
            string.Format(text, id);
            throw new ArgumentException(text);
        }

        public static void FileIsClosed()
        {
            string text = SR.FileIsClosed;
            throw new ObjectDisposedException(null, text);
        }
        #endregion
    }
}
