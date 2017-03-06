using Abp.Web.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Topevery.FMP.ObjectModel;
using Topevery.Framework.CommonModel.Utility;

namespace FZY.Web.Controllers
{
    public class FileController : FZYControllerBase
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 上传文件
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult UploadFile()
        {

            // 获取从MIME中读取出来的文件
            var listFile = Request.Files;
            Guid fid = Guid.Empty;
            string fileId2Return = "";
            string message = "";
            if (!string.IsNullOrEmpty(Request.QueryString["fileId"]))
            {
                fid = new Guid(Request.QueryString["fileId"]);
            }

            for (int n = 0; n < listFile.Count; n++)
            {
                Guid fileId = fid;
                if (fid == Guid.Empty)
                    fileId = Guid.NewGuid();
                HttpPostedFileBase file = listFile[n];
                if (!string.IsNullOrEmpty(file?.FileName))
                {
                    fileId2Return = fileId.ToString();
                    string fileName = Path.GetFileName(file.FileName);
                    //string FileNameNotExtension = Path.GetFileNameWithoutExtension(file.FileName);
                    //string FileExtensionName = Path.GetExtension(file.FileName);

                    try
                    {
                        using (Stream inStream = StaticFunction.StreamToMemoryStream(file.InputStream))
                        {
                            using (Stream fmpStream = FileManager.CreateFile(fileId, fileName))
                            {
                                StaticFunction.StreamSourceStreamToTargetStream(inStream, fmpStream);
                            }

                            //OpenFileItemData context = new OpenFileItemData();
                            //context.FileID = fileId;
                            //context.UpdateMode = UpdateMode.None;
                            //context.FileAccess = Topevery.FMP.ObjectModel.FileAccess.ReadWrite;
                            //context.FileMode = Topevery.FMP.ObjectModel.FileMode.Create;
                            //context.ClientFileName = fileName;
                        }
                    }
                    catch (Exception ex)
                    {
                        message = ex.ToString();
                        Logger.Error("FMP错误", ex);
                        throw ex;
                    }
                }
            }

            if (string.IsNullOrEmpty(message) && !string.IsNullOrEmpty(fileId2Return))
            {
                return Json(new AjaxResponse<string> { Success = true, Result = fileId2Return });
            }
            return Json(new AjaxResponse { Success = false, Error = new ErrorInfo(message) });

        }
    }
}