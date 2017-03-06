using System;
using System.IO;
using System.Web;

namespace Topevery.FMP.ObjectModel.Web.Handlers
{
    ///// <summary>
    ///// Summary description for InceptFileHandler.
    ///// </summary>
    //public class InceptFileHandler : HttpHandlerBase
    //{
    //    public override void ProcessRequest(System.Web.HttpContext context)
    //    {
    //        if(!CheckAccessRight(context))
    //        {
    //            context.Response.Write("CheckUserAccessRight is failed");
    //        }
    //        string extension = ".zip";

    //        if(context.Request.QueryString["Extension"] != null && context.Request.QueryString["Extension"].Length > 0)
    //        {
    //            extension = context.Request.QueryString["Extension"];
    //        }
    //        Stream fileStream = context.Request.InputStream;
    //        if(fileStream.Length > 0)
    //        {
    //            FileData fileData = ZipFileSaver.SaveZipFile(fileStream, extension);
    //            context.Response.Write(FileData.ToBase64String(fileData));
    //        }
    //        else
    //        {
    //            if(context.Request.QueryString["Test"] != null && context.Request.QueryString["Test"].Length > 0)
    //            {
    //                fileStream = new FileStream(@"d:\desktop.zip",FileMode.Open);
    //                FileData fileData = ZipFileSaver.SaveZipFile(fileStream, extension);
    //                context.Response.Write(FileData.ToBase64String(fileData));

    //            }
    //            else
    //                context.Response.Write("not find upload file data");
    //        }
    //    }

    //    protected virtual bool CheckAccessRight(HttpContext context)
    //    {
    //        if(context.Request.QueryString["PID"] != null && context.Request.QueryString["PID"].Length != 0)
    //        {
    //            return	AccessRight.CheckUserAccessRight(context.Request.QueryString["PID"]);
    //        }					
    //        else
    //            return true;
    //    }
    //}
}
