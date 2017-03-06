using System;
using System.Text;
using System.Net;
using System.IO;
using System.Net.Cache;
using System.Reflection;

namespace Topevery.FMP.ObjectModel
{
    public class HttpRuntimeClient
    {
        #region Fields
        private static RequestCachePolicy _bypassCache;
        private string _url;
        #endregion

        #region Constructor
        static HttpRuntimeClient()
        {
            ServicePointManager.DefaultConnectionLimit = 32;
        }
        #endregion

        #region Methods
        public Uri CombinePath(string serverUrl, string relativeUrl)
        {
            if (Uri.IsWellFormedUriString(serverUrl, UriKind.Absolute))
            {
                Uri uri = new Uri(serverUrl, UriKind.Absolute);
                return new Uri(uri, relativeUrl);
            }
            return null;
        }

        public InvokeContext Invoke(string url, InvokeContext context)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    url = this.Url;
                }
                if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
                {
                    return Invoke(new Uri(url), context);
                }
                else
                {
                    context.InnerException = new Exception("无效的Url");
                }
            }
            catch (Exception e)
            {
                context.InnerException = e;
            }
            return context;
        }

        public InvokeContext Invoke(Uri uri, InvokeContext context)
        {
            try
            {
                if (uri != null)
                {
                    //MemoryStream m = new MemoryStream();
                    //InvokeContext.Serialize(m, context);
                    HttpWebRequest request = CreateWebRequest(uri, context);

                    HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                    if (response != null && response.StatusCode == HttpStatusCode.OK && response.ContentType == HttpRuntimeService.ContentType)
                    {
                        try
                        {
                            using (Stream output = response.GetResponseStream())
                            {
                                context = InvokeContext.Deserialize(output);
                            }
                        }
                        catch (Exception ex)
                        {
                            context.InnerException = ex;
                        }
                    }
                    response.Close();
                }
                else
                {
                    context.InnerException = new Exception("无效的Url");
                }
            }
            catch (Exception e)
            {
                context.InnerException = e;
            }
            return context;
        }

        private static HttpWebRequest CreateWebRequest(Uri url, InvokeContext context)
        {
            HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
            request.Timeout = 60 * 1000;
            request.ContentType = HttpRuntimeService.ContentType;
            //request.CachePolicy = BypassCache;
            request.AllowWriteStreamBuffering = true;
            request.SendChunked = false;
            request.KeepAlive = true;
            request.Method = "POST";
            //request.ContentLength = m.Length;
            using (Stream s = request.GetRequestStream())
            {
                InvokeContext.Serialize(s, context);
                s.Close();
            }
            return request;
        }
        #endregion

        #region Properties
        public string Url
        {
            get
            {
                return this._url;
            }
            set
            {
                this._url = value;
            }
        }

        internal static RequestCachePolicy BypassCache
        {
            get
            {
                if (_bypassCache == null)
                {
                    _bypassCache = new RequestCachePolicy(RequestCacheLevel.BypassCache);
                }
                return _bypassCache;
            }
        }

        #endregion
    }
}
