using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Topevery.Web.Ashx
{
    /// <summary>
    /// ThumbImage 的摘要说明
    /// </summary>
    public class DownloadFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
             new Topevery.FMP.ObjectModel.Web.Handlers.DownloadFileHandler().ProcessRequest(context);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}