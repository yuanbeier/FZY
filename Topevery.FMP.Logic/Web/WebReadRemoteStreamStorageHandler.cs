using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.IO;
using Topevery.FMP.ObjectModel;

namespace Topevery.FMP.Logic.Web
{
    public class WebReadRemoteStreamStorageHandler : BaseHandler
    {
        #region Fields
        
        #endregion

        #region IHttpHandler Members

        public override void ProcessRequest(HttpContext context)
        {
            Guid fileID = FileID;
            Guid lockID = LockID;
            string clientFileName = this.ClientFileName;
            int startPos = StartPosition;
            int bufferCount = BufferCount;
            HttpResponse response = context.Response;
            try
            {
                if (CheckParameter(fileID, startPos, bufferCount))
                {
                    response.ContentType = "application/octet-stream";
                    string error;
                    byte[] buffer = ReadFile(fileID, lockID, clientFileName, startPos, bufferCount, out error);
                    
                    if (buffer != null && buffer.Length > 0)
                    {
                        response.AddHeader("content-length", buffer.Length.ToString());
                        Stream s = response.OutputStream;
                        s.Write(buffer, 0, buffer.Length);
                        s.Flush();
                    }
                    else
                    {
                        WriteError(response, error);
                    }
                }
                else
                {
                    WriteError(response, "无效的参数");
                }
            }
            catch (Exception e)
            {
                WriteError(response, e.Message);
            }
            finally
            {
                context.ApplicationInstance.CompleteRequest();
            }
        }

        protected void WriteError(HttpResponse response, string error)
        {
            response.AddHeader("Exception", error);
            response.StatusCode = 500;
        }

        protected bool CheckParameter(Guid fileID,int startPos, int bufferCount)
        {
            if (fileID == Guid.Empty)
                return false;
            if (startPos < 0)
                return false;
            if (bufferCount <= 0)
                return false;
            return true;
        }

        protected byte[] ReadFile(Guid fileID, Guid lockID, string clientFileName,int startPos, int bufferCount, out string error)
        {
            RemoteFileStorageService provider = new RemoteFileStorageService();
            ReadFileParameter para = new ReadFileParameter();
            ReadFileItemData item = new ReadFileItemData();
            para.InputData.Add(item);
            item.PhysicalFileID = fileID;
            item.ClientFileName = clientFileName;
            item.LockID = lockID;
            item.Position = startPos;
            item.ReadCount = bufferCount;
            ReadFileResult result = provider.ReadFile(para);

            error = null;
            if (result != null)
            {
                if (result.Succeed)
                {                    
                    if (result.ExecuteResult.Count > 0)
                    {
                        ReadFileResultItemData itemResult = result.ExecuteResult[0];
                        if (itemResult != null)
                        {
                            return itemResult.ReadData;
                        }
                    }
                    error = string.Format("不存在ID为:{0}的文件.", fileID);
                }
                else
                {
                    error = result.InnerException.ErrorMessage;
                }
            }
            else
            {
                error = "读取文件失败.";
            }
            return null;
        }
        #endregion
    }
}
