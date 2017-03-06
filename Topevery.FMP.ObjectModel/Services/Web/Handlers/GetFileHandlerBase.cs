using System;
using System.IO;
using System.Web;


namespace Topevery.FMP.ObjectModel.Web.Handlers
{
	/// <summary>
	/// Summary description for GetFileHandleBase.
	/// </summary>
	public abstract class GetFileHandlerBase : HttpHandlerBase
    {
        #region Fields
        protected const int BufferLen = 16 * 1024;
        #endregion

        protected GetFileHandlerBase()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Methods
        protected override void ProcessRequestCore(System.Web.HttpContext context)
		{
			HttpResponse response = context.Response;			
			response.Clear();
			try
			{
				if(this.CheckAccessRight(context))
				{
                    MemoryStream mStream = null;
					using(Stream stream = this.GetFileStream(context))
					{
                        ThumbImageViewer.CopyStream(mStream, stream);
					}
                    long startPosition = this.GetStartPosition(context);
                    this.AddHeader(context, mStream, startPosition);
                    this.ResponseStream(context, mStream, startPosition);
				}
			}
			finally
			{
				
                //response.End();
			}
		}

		#region abstract methods
		protected abstract Stream GetFileStream(HttpContext context);
		protected abstract void AddHeader(HttpContext context,Stream stream, long startPosition);
		#endregion

		protected virtual bool CheckAccessRight(HttpContext context)
		{
			/*if(context.Request.QueryString["PID"] != null && context.Request.QueryString["PID"].Length != 0)
			{
				return	AccessRight.CheckUserAccessRight(context.Request.QueryString["PID"]);
			}					
			else*/
				return true;
		}

		protected virtual long GetStartPosition(HttpContext context)
		{
			HttpRequest request = context.Request;
			HttpResponse response = context.Response;
			long retPosition = 0;
			if(request.Headers["Range"]!=null)
			{
				response.StatusCode = 206;
				retPosition = long.Parse( request.Headers["Range"].Replace("bytes=","").Replace("-",""));
			}
			return retPosition;
		}

		
		

		protected virtual void ResponseStream(HttpContext context,Stream iStream,long startPosition)
		{
			HttpResponse response = context.Response;

			// Buffer to read 10K bytes in chunk:
            

			// Length of the file:
			int length;

			// Total bytes to read:
			long dataToRead;

			try
			{
				// Total bytes to read:
				dataToRead = iStream.Length;

				
				iStream.Position = startPosition;
				dataToRead = dataToRead - startPosition;
				// Read the bytes.
				while (dataToRead > 0)
				{
					// Verify that the client is connected.
					if (response.IsClientConnected) 
					{
                        byte[] buffer = new Byte[BufferLen];
						// Read the data in buffer.
                        length = iStream.Read(buffer, 0, BufferLen);
                        if (length == 0)
                        {
                            break;
                        }
						// Write the data to the current output stream.
						response.OutputStream.Write(buffer, 0, length);

						// Flush the data to the HTML output.
						response.Flush();
						dataToRead = dataToRead - length;
					}
					else
					{
						//prevent infinite loop if user disconnects
						dataToRead = -1;
					}
				}
			}
			catch (Exception ex) 
			{
				// Trap the error, if any.
				response.Write("Error : " + ex.Message);
			}
			finally
			{
				if (iStream != null) 
				{
					//Close the file.
					iStream.Close();
				}
			}
		}
		#endregion

        #region Properties
        protected virtual string PassportID
        {
            get
            {
                if (Context != null)
                {
                    return Context.Request.QueryString[HandlerStrings.PassportID];
                }
                return string.Empty;
            }
        }

        protected virtual string FileID
        {
            get
            {
                if (Context != null)
                {
                    return Context.Request.QueryString[HandlerStrings.FileID];
                }
                return string.Empty;
            }
        }

        protected virtual string ClientFileID
        {
            get
            {
                if (Context != null)
                {
                    return Context.Request.QueryString[HandlerStrings.ClientFileID];
                }
                return string.Empty;
            }
        }
        #endregion
    }
}
