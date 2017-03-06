using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Web;

namespace Topevery.FMP.ObjectModel.Web.Handlers
{
    /// <summary>
    /// Summary description for ThumbImageHandler.
    /// </summary>
    public class ThumbImageHandler : HttpHandlerBase
    {
        protected override void ProcessRequestCore(HttpContext context)
        {
            if (CheckAccessRight(context))
            {
                try
                {
                    this.ThumbImage(context);
                }
                catch (Exception ex)
                {
                    ExecuteError(context, ex);
                }
                finally
                {
                    context.ApplicationInstance.CompleteRequest();
                }
            }
        }

        protected virtual bool CheckAccessRight(HttpContext context)
        {
            return true;
        }

        protected virtual void ThumbImage(HttpContext context)
        {
            string fileName = this.FileID;
            if (!string.IsNullOrEmpty(fileName))
            {
                int width = 0;
                int height = 0;
                GetImageSize(out width, out height);
                Guid fileID = FileGuid;

                SetResponseContentType(context);
                SetResponseHeader(context);

                Stream imageStream = null;

                LogicFileInfoData fileInfo =  FileManager.GetFileInfo(fileID);
                //先看缓存目录里有没有
                string thumbImageDirPath = context.Server.MapPath("~/ThumbImage");
                string thumbImagePath = Path.Combine(thumbImageDirPath, string.Format("{0}_{1}_{2}{3}", FileGuid, width, height, fileInfo.LogicFileExt));
                if (!Directory.Exists(thumbImageDirPath))
                {
                    Directory.CreateDirectory(thumbImageDirPath);
                }
                if (File.Exists(thumbImagePath))
                {
                    try
                    {
                        imageStream = File.OpenRead(thumbImagePath);
                        OutputStream(context, imageStream);
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (imageStream != null)
                        {
                            imageStream.Dispose();
                        }
                    }
                }
                else
                {
                    try
                    {
                        using (Stream stream = FileManager.OpenReadFile(fileID))
                        {
                            imageStream = new MemoryStream();
                            ThumbImageViewer.CopyStream(imageStream, stream);
                        }

                        if (width <= 0 || height <= 0)
                        {
                            OutputStream(context, imageStream);
                        }
                        else
                        {
                            using (MemoryStream bufferOutput = new MemoryStream())
                            {
                                ThumbImageViewer.OutputImage(bufferOutput, imageStream, this.ClientFileID, width, height, img =>
                                {
                                    //保存缩列图缓存
                                    if (img != null)
                                    {
                                        try
                                        {
                                            img.Save(thumbImagePath);
                                        }
                                        catch (Exception ex)
                                        {
                                            throw ex;
                                        }
                                    }
                                });
                                if (bufferOutput.Length > 0)
                                {
                                    bufferOutput.Position = 0;
                                    OutputStream(context, bufferOutput);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                    finally
                    {
                        if (imageStream != null)
                        {
                            imageStream.Dispose();
                        }
                        
                    }
                }
            }
        }

        protected virtual void ExecuteError(HttpContext context, Exception ex)
        {
            context.Response.Clear();
            context.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;
            context.Response.Flush();
        }

        private void OutputStream(HttpContext context, Stream inStream)
        {
            HttpResponse response = context.Response;
            ThumbImageViewer.CopyStream(response.OutputStream, inStream);
        }

        private void SetResponseContentType(HttpContext context)
        {
            string clientFileName = this.ClientFileID;
            string ext = Path.GetExtension(clientFileName);
            string contentType = Mime.GetContentTypeNotDefault(ext);
            if (!string.IsNullOrEmpty(contentType))
            {
                context.Response.ContentType = contentType;
            }
            else
            {
                context.Response.ContentType = "image/jpeg";
            }
        }

        private void SetResponseHeader(HttpContext context)
        {
            context.Response.Cache.SetCacheability(HttpCacheability.Public);
            context.Response.Cache.SetAllowResponseInBrowserHistory(true);
            context.Response.Cache.SetExpires(DateTime.Now.AddYears(1));
        }

        private void GetImageSize(out int width, out int height)
        {
            if (!(int.TryParse(this.Width, out width) && int.TryParse(this.Height, out height)))
            {
                width = 0;
                height = 0;
            }
            if (width < 0)
            {
                width = 0;
            }
            if (height < 0)
            {
                height = 0;
            }
        }

        protected virtual string PassportID
        {
            get
            {
                string result = string.Empty;
                if (Context != null)
                {
                    result = Context.Request.QueryString[HandlerStrings.PassportID];
                }
                return result;
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

        private Guid FileGuid
        {
            get
            {
                Guid result = Guid.Empty;
                if (!string.IsNullOrEmpty(FileID))
                {
                    result = new Guid(FileID);
                }
                return result;
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

        protected virtual string Width
        {
            get
            {
                if (Context != null)
                {
                    return Context.Request.QueryString[HandlerStrings.Width];
                }
                return string.Empty;
            }
        }

        protected virtual string Height
        {
            get
            {
                if (Context != null)
                {
                    return Context.Request.QueryString[HandlerStrings.Height];
                }
                return string.Empty;
            }
        }

        public override bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
