using System;
using System.Text;
using System.Web;
using System.IO;

namespace Topevery.FMP.ObjectModel
{
    public class HttpRuntimeService : IHttpHandler
    {
        #region Fields
        private const string ContentLength = "conent-length";
        internal const string ContentType = "x-tyrmo";
        private static DynamicProxyExecutor _executor = new DynamicProxyExecutor();
        #endregion

        #region Methods
        public void ProcessRequest(HttpContext context)
        {
            HttpRequest request = context.Request;            
            HttpResponse response = context.Response;
            InvokeContext invokeContext = null;
            try
            {
                if (request.ContentType == ContentType)
                {
                    invokeContext = ReadInvokeContext(context);
                    _executor.Execute(invokeContext);
                    OutputStream(invokeContext, response, true);
                }
                else
                {
                    WriteError(context, invokeContext, new Exception("无效请求类型"));
                }
            }
            catch (Exception e)
            {
                WriteError(context, invokeContext, e);
            }
            finally
            {                
                context.ApplicationInstance.CompleteRequest();
            }
        }

        private static void OutputStream(InvokeContext invokeContext, HttpResponse response, bool raiseError)
        {
            try
            {
                if (response.IsClientConnected)
                {
                    response.ContentType = ContentType;
                    Stream output = response.OutputStream;
                    InvokeContext.Serialize(output, invokeContext);
                }
            }
            catch
            {
                if (raiseError)
                {
                    throw;
                }
            }
        }

        private static InvokeContext ReadInvokeContext(HttpContext context)
        {
            HttpRequest request = context.Request;
            Stream s = request.InputStream;
            return InvokeContext.Deserialize(s);
        }

        private static void WriteError(HttpContext context, InvokeContext invokeContext, Exception e)
        {
            HttpResponse response = context.Response;
            if (invokeContext == null)
            {
                invokeContext = new InvokeContext();                
            }
            invokeContext.InnerException = e;
            if (invokeContext != null)
            {
                response.Clear();
                Stream output = response.OutputStream;
                InvokeContext.Serialize(output, invokeContext);
            }
        }

        private static int GetHeaderInt(HttpContext context, string name, int defaultValue)
        {
            string text = GetHeaderString(context, name);
            if (!string.IsNullOrEmpty(text))
            {
                int result;
                if (int.TryParse(text, out result))
                {
                    return result;
                }
            }
            return defaultValue;
        }

        private static string GetHeaderString(HttpContext context, string name)
        {
            if (context != null)
            {
                HttpRequest request = context.Request;
                string text = request.Headers[name];
                return text;
            }
            return null;
        }
        #endregion

        #region Properties
        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
        #endregion
    }
}
