using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using Topevery.FMP.ObjectModel.Web.Handlers;

namespace Topevery.FMP.Logic.Web
{
    public abstract class BaseHandler : IHttpHandler
    {
        #region IHttpHandler Members

        public bool IsReusable
        {
            get { return true; }
        }

        public abstract void ProcessRequest(HttpContext context);

        private static Guid GetGuid(string text)
        {
            if (string.IsNullOrEmpty(text))
                return Guid.Empty;
            try
            {
                return new Guid(text);
            }
            catch { }
            return Guid.Empty;
        }

        private static Guid QueryGuid(string name)
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                return GetGuid(context.Request.QueryString[name]);
            }
            return Guid.Empty;
        }

        private static string QueryString(string name)
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                return context.Request.QueryString[name];
            }
            return null;
        }

        private static int QueryInt(string name, int defaultValue)
        {
            HttpContext context = HttpContext.Current;
            if (context != null)
            {
                string text = context.Request.QueryString[name];
                int result;
                if (int.TryParse(text, out result))
                {
                    return result;
                }
            }
            return defaultValue;
        }
        #endregion

        #region Properties
        protected virtual Guid PassportID
        {
            get
            {
                return QueryGuid(HandlerStrings.PassportID);
            }
        }

        protected virtual Guid FileID
        {
            get
            {
                return QueryGuid(HandlerStrings.FileID);
            }
        }

        protected virtual Guid LockID
        {
            get
            {
                return QueryGuid(HandlerStrings.LockID);
            }
        }

        public virtual string ClientFileName
        {
            get
            {
                return QueryString(HandlerStrings.ClientFileID);
            }
        }

        protected virtual int StartPosition
        {
            get
            {
                return QueryInt(HandlerStrings.StartPosition, -1);
            }
        }

        

        protected virtual int BufferCount
        {
            get
            {
                return QueryInt(HandlerStrings.BufferCount, 0);
            }
             
        }
        #endregion
    }
}
