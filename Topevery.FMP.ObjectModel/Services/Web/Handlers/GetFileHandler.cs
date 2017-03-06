using System;
using System.Web;
using System.IO;

namespace Topevery.FMP.ObjectModel.Web.Handlers
{
	/// <summary>
	/// Summary description for GetFileHandler.
	/// </summary>
	public class GetFileHandler : GetFileHandlerBase
	{
		#region Methods
		protected override System.IO.Stream GetFileStream(System.Web.HttpContext context)
		{
            string fileID = this.FileID;
            if (!string.IsNullOrEmpty(fileID))
			{
                try
                {

                    Guid id = new Guid(fileID);
                    Stream stream = FileManager.OpenReadFile(id);
                    return stream;
                }
                catch { }
			}			
			return null;
		}

        protected override void AddHeader(System.Web.HttpContext context, System.IO.Stream stream, long startPosition)
        {
            if (stream != null)
            {
                HttpResponse response = context.Response;
                long dataToRead = stream.Length;

                if (startPosition != 0)
                {
                    response.AddHeader("Content-Range", "bytes " + startPosition.ToString() + "-" + ((long)(dataToRead - 1)).ToString() + "/" + dataToRead.ToString());
                }
                response.AddHeader("Content-Length", ((dataToRead - startPosition)).ToString());                
                response.ContentEncoding = System.Text.Encoding.UTF8;
                string fileName = this.GetFileName(context);
                fileName = EncodeFileName(fileName);
                response.ContentType = this.GetContentType(fileName);
                response.AddHeader("Content-Disposition", "inline; filename=" + fileName);
                SetResponseHeader(context);
            }
        }
        private void SetResponseHeader(HttpContext context)
        {
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetAllowResponseInBrowserHistory(true);
            context.Response.Cache.SetExpires(DateTime.Now.AddYears(1));
        }
        private string EncodeFileName(string fileName)
        {
            if (!string.IsNullOrEmpty(fileName))
            {
                //Replace '&' as '&&'
                fileName = fileName.Replace("&", "&&");

                //Encoding fileName.
                fileName = HttpUtility.UrlEncode(fileName);

                //Replace '+' as ' '.
                fileName = fileName.Replace("+", "%20");
            }
            return fileName;
        }

        private string GetFileName(HttpContext context)
        {
            string filename = string.Empty;
            string temp = this.ClientFileID;
            if (!string.IsNullOrEmpty(temp))
            {
                filename = temp;
            }
            else
            {
                temp = this.FileID;
                if (!string.IsNullOrEmpty(temp))
                {
                    filename = temp;
                }
            }
            return filename;
        }

		/*protected override void AddHeader(System.Web.HttpContext context, System.IO.Stream stream, long startPosition)
		{
			if(stream != null)
			{
				HttpResponse response = context.Response;
				long dataToRead = stream.Length;

				if(startPosition != 0)
				{
					response.AddHeader("Content-Range","bytes " + startPosition.ToString() + "-" + ((long)(dataToRead - 1)).ToString() + "/" + dataToRead.ToString());                    
				}
				response.AddHeader("Content-Length",((dataToRead-startPosition)).ToString());
				
				string fileName = this.GetFileName(context);
				response.ContentType = this.GetContentType(fileName);
//				response.Charset = "gb2312";

				response.AddHeader("Content-Disposition", "inline; filename=" + HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(fileName)));				
			}
		}

        private string GetFileName(HttpContext context)
        {
            string filename = string.Empty;
            string temp = this.ClientFileID;
            if (!string.IsNullOrEmpty(temp))
            {
                filename = temp;
            }
            else
            {
                temp = this.FileID;
                if (!string.IsNullOrEmpty(temp))
                {
                    filename = temp;
                }
            }
            return filename;
        }*/

		protected virtual string GetContentType(string filename)
		{
			return Mime.GetContenType(Path.GetExtension(filename));
		}

		#endregion

        #region Properties
        
        #endregion
	}
}
