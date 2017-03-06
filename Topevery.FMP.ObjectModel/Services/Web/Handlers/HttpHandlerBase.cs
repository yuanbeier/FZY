using System;
using System.Web;

namespace Topevery.FMP.ObjectModel.Web.Handlers
{
	/// <summary>
	/// Summary description for HttpHandleBase.
	/// </summary>
	public abstract class HttpHandlerBase : IHttpHandler
    {
        #region Fields
        HttpContext _context;
        #endregion
       
        public virtual void ProcessRequest(HttpContext context)
        {
            _context = context;
            this.ProcessRequestCore(context);
        }

        protected abstract void ProcessRequestCore(HttpContext context);
        
        void IHttpHandler.ProcessRequest(HttpContext context)
        {
            this.ProcessRequest(context);
        }

		public virtual bool IsReusable
		{
			get { return false;}
		}

        protected virtual HttpContext Context
        {
            get
            {
                return _context;
            }
        }
	}
}
