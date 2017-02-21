using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Topevery.Web.Ashx
{
    /// <summary>
    /// ThumbImage 的摘要说明
    /// </summary>
    public class GetFile : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
             new Topevery.FMP.ObjectModel.Web.Handlers.GetFileHandler().ProcessRequest(context);
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