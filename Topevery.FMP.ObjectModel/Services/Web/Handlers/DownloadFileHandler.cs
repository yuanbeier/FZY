using System;
using System.IO;
using System.Web;

namespace Topevery.FMP.ObjectModel.Web.Handlers
{
	/// <summary>
	/// Summary description for GetFileHandler.
	/// </summary>
	public class DownloadFileHandler : GetFileHandlerBase
	{
		public DownloadFileHandler()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Methods
		protected override System.IO.Stream GetFileStream(System.Web.HttpContext context)
		{
			string filename = string.Empty;
			if(!string.IsNullOrEmpty(this.FileID))
			{
                filename = this.FileID;
                try
                {
                    Guid id = new Guid(filename);
                    Stream stream = FileManager.OpenReadFile(id);
                    return stream;
                }
                catch
                {
                }
				/*if(FileManager.Exists(filename))
				{
					//return  FileManager.GetStream(filename);
					//Edit by Tim
					return FileCacheUtility.GetStreamByFileName(filename);
				}
				else
				{
					string tempPath = RSUtility.GetTempPath();
					filename = RSUtility.CombineUrl(tempPath, filename);
					if(File.Exists(filename))
					{
						return new FileStream(filename, FileMode.Open, FileAccess.Read);
					}
				}*/
			}			
			return null;
		}

		protected override void AddHeader(System.Web.HttpContext context, System.IO.Stream stream, long startPosition)
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
				response.ContentType = "application/octet-stream";
				response.ContentEncoding = System.Text.Encoding.UTF8;
				string fileName = this.GetFileName(context);
				fileName = EncodeFileName(fileName);
				//response.AddHeader("Content-Disposition", "attachment; filename=" + System.Web.HttpUtility.UrlEncode(System.Text.Encoding.UTF8.GetBytes(fileName)));
				response.AddHeader("Content-Disposition", "attachment; filename=" + fileName);

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
			if(!string.IsNullOrEmpty(fileName))
			{
				//Replace '&' as '&&'
				fileName = fileName.Replace("&", "&&");

				//Encoding fileName.
				fileName = HttpUtility.UrlEncode(fileName);

				//Replace '+' as ' '.
				fileName = fileName.Replace("+","%20");
			}
			return fileName;
		}

		private string GetFileName(HttpContext context)
		{
			string filename = string.Empty;
            string temp = this.ClientFileID;
			if(!string.IsNullOrEmpty(temp))
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

		

		#endregion
	}
}
