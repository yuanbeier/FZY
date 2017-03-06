using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel.Web.Handlers;
using System.Net;
using System.IO;
using System.Diagnostics;

namespace Topevery.FMP.ObjectModel.Web
{
    public class HttpRemoteFileStorageProxy : HttpRuntimeClientProxy, IRemoteFileStorage
    {
        #region Fields
        #endregion

        #region Methods
        public DeleteFileResult DeleteFile(DeleteFileParameter deleteFileParam)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public OpenFileResult OpenFile(OpenFileParameter openFileParam)
        {
            OpenFileResult result = new OpenFileResult();
            try
            {
                result = ExecuteRemoteResult("OpenFile", openFileParam, result) as OpenFileResult;   
            }
            catch (Exception e)
            {
                result.InnerException = CreateExceptionResult(result.InnerException, e.Message);
            }
            return result;
        }

        public CloseFileResult CloseFile(CloseFileParameter closeFileParam)
        {
            CloseFileResult result = new CloseFileResult();
            try
            {
                result = ExecuteRemoteResult("CloseFile", closeFileParam, result) as CloseFileResult;
            }
            catch (Exception e)
            {
                result.InnerException = CreateExceptionResult(result.InnerException, e.Message);

            }
            return result;
        }

        public WriteFileResult WriteFile(WriteFileParameter writeFileParam)
        {
            WriteFileResult result = new WriteFileResult();
            try
            {
                result = ExecuteRemoteResult("WriteFile", writeFileParam, result) as WriteFileResult;
            }
            catch (Exception e)
            {
                result.InnerException = CreateExceptionResult(result.InnerException, e.Message);

            }
            return result;
        }

        public ReadFileResult ReadFile(ReadFileParameter param)
        {
            ReadFileResult result = new ReadFileResult();
            try
            {
                result = ExecuteRemoteResult("ReadFile", param, result) as ReadFileResult;
            }
            catch (Exception e)
            {
                result.InnerException = CreateExceptionResult(result.InnerException, e.Message);

            }
            return result;
        }

        public FetchFileInfoResult FetchFileInfo(FetchFileInfoParameter fetchFileInfoParam)
        {
            FetchFileInfoResult result = new FetchFileInfoResult();
            try
            {
                result = ExecuteRemoteResult("GetFileInfo", fetchFileInfoParam, result) as FetchFileInfoResult;
            }
            catch (Exception e)
            {
                result.InnerException = CreateExceptionResult(result.InnerException, e.Message);

            }
            return result;
        }

        public UpdateFileInfoResult UpdateFileInfo(UpdateFileInfoParameter updateFileInfoParam)
        {
            throw new Exception("The method or operation is not implemented.");
            //UpdateFileInfoResult result = new UpdateFileInfoResult();
            //try
            //{
            //    result = ExecuteRemoteResult("UpdateFileInfo", updateFileInfoParam, result) as UpdateFileInfoResult;
            //}
            //catch (Exception e)
            //{
            //    result.InnerException = CreateExceptionResult(result.InnerException, e.Message);

            //}
            //return result;
        }

        private BaseExecuteResult ExecuteRemoteResult(string methodName, BaseParameter param, BaseExecuteResult result)
        {
            InvokeContext context = new InvokeContext();
            context.IsStatic = true;
            context.Arguments = new object[] { param };
            context.MethodName = methodName;
            context.TypeName = "Topevery.FMP.ObjectModel.FileManager,Topevery.FMP.ObjectModel";
            Uri uri = this.CombinePath(this.ServerUrl, HandlerStrings.RuntimeServicePath);
            context = this.Invoke(uri, context);
            if (context != null)
            {
                if (context.InnerException != null)
                {
                    result.InnerException = CreateExceptionResult(result.InnerException, context.InnerException.Message);                    
                }
                else
                {
                    result = context.ReturnValue as BaseExecuteResult;
                }
            }
            return result;
        }
       
        private static ExceptionResult CreateExceptionResult(ExceptionResult result, string error)
        {
            if (result == null)
            {
                result = new ExceptionResult();
            }
            result.ErrorCode = -1;
            result.ErrorMessage = error;
            return result;
        }
        #endregion
    }
}
