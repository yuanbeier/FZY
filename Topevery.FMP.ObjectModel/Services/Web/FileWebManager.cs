using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Topevery.FMP.ObjectModel.Web.Handlers;
using System.IO;

namespace Topevery.FMP.ObjectModel.Web
{
    public static class FileWebManager
    {
        #region Fields
        private const string DownloadFileUrlParam =
            "{0}" + HandlerStrings.DownloadHanderPath + "?"
            + HandlerStrings.FileID + "={1}&"            
            + HandlerStrings.PassportID + "={2}&"
            + HandlerStrings.ClientFileID + "={3}";

        private const string GetFileUrlParam =
            "{0}" + HandlerStrings.GetFileHandlerPath + "?"
             + HandlerStrings.FileID + "={1}&"
            + HandlerStrings.PassportID + "={2}&"
            + HandlerStrings.ClientFileID + "={3}";

        private const string ThumbImageUrlParam =
            "{0}" + HandlerStrings.ThumbImageHandlerPath + "?"
            + HandlerStrings.FileID + "={1}&"
            + HandlerStrings.PassportID + "={2}&"
            + HandlerStrings.ClientFileID + "={3}&"
            + HandlerStrings.Width + "={4}&"
            + HandlerStrings.Height + "={5}";
        #endregion

        #region DownloadFileHanlderUrl
        /// <summary>
        /// 获取文件下载的URL
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <returns></returns>
        public static string GetDownloadFileUrl(Guid logicFileID)
        {
            return GetDownloadFileUrl(logicFileID, null);
        }

        /// <summary>
        /// 获取文件下载的URL
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="passportID"></param>
        /// <returns></returns>
        public static string GetDownloadFileUrl(Guid logicFileID, string passportID)
        {
            string result = null;
            LogicFileInfoData logicFileInfo = GetFileInfo(logicFileID);
            if (logicFileInfo != null)
            {
                string pid = string.Empty;                
                result = string.Format(DownloadFileUrlParam, ApplicationPath, logicFileID, HttpUtility.UrlEncode(passportID), HttpUtility.UrlEncode(Path.GetFileName(logicFileInfo.LogicFileName)));
            }
            return result;
        }
        #endregion

        #region GetFileHandlerUrl
        /// <summary>
        /// 获取文件的URL
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <returns></returns>
        public static string GetFileUrl(Guid logicFileID)
        {
            return GetFileUrl(logicFileID, null);
        }

        /// <summary>
        /// 获取链接文件的URL
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="passportID"></param>
        /// <returns></returns>
        public static string GetFileUrl(Guid logicFileID, string passportID)
        {
            string result = null;
            LogicFileInfoData logicFileInfo = GetFileInfo(logicFileID);
            if (logicFileInfo != null)
            {
                string pid = string.Empty;
                result = string.Format(GetFileUrlParam, ApplicationPath, logicFileID, HttpUtility.UrlEncode(passportID), HttpUtility.UrlEncode(Path.GetFileName(logicFileInfo.LogicFileName)));
            }
            return result;
        }


        /// <summary>
        /// 获取图片的Url
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string ThumbImageUrl(Guid logicFileID, int width, int height)
        {
            return ThumbImageUrl(logicFileID, null, width, height);
        }

        /// <summary>
        ///  获取图片的Url
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="passportID"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <returns></returns>
        public static string ThumbImageUrl(Guid logicFileID, string passportID, int width, int height)
        {
            string result = null;
            LogicFileInfoData logicFileInfo = GetFileInfo(logicFileID);
            if (logicFileInfo != null)
            {
                result = string.Format(ThumbImageUrlParam, ApplicationPath, logicFileID,
                    HttpUtility.UrlEncode(passportID),
                    HttpUtility.UrlEncode(Path.GetFileName(logicFileInfo.LogicFileName)),
                    width, height);
            }
            return result;
        }
        #endregion

        #region FetchFileInfo
        public static LogicFileInfoData GetFileInfo(Guid logicFileID)
        {
            return FileManager.GetFileInfo(logicFileID);
        }
        #endregion

        #region Properties
        public static string ApplicationPath
        {
            get
            {
                string result = string.Empty;
                HttpRequest request = Request;
                if (request != null)
                {
                    result = request.ApplicationPath;
                    if (string.IsNullOrEmpty(result))
                    {
                        result = "/";
                    }
                    else
                    {
                        if (!result.EndsWith("/"))
                        {
                            result += "/";
                        }
                    }
                }
                return result;
            }
        }
        public static HttpContext Context
        {
            get
            {
                return HttpContext.Current;
            }
        }

        public static HttpRequest Request
        {
            get
            {
                HttpContext context = Context;
                if (context != null)
                {
                    return context.Request;
                }
                return null;
            }
        }
        #endregion

    }
}
