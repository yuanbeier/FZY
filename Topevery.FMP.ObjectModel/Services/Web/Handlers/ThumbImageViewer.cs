using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Topevery.FMP.ObjectModel.Web.Handlers
{
    public static class ThumbImageViewer
    {
        #region Fields
        private static Bitmap _EmptyImage = DrawEmptyImage();
        [ThreadStatic]
        private static byte[] _cacheBuffer;
        #endregion

        public static void OutputImage(Stream output, Stream inStream, string clientFileName, int width, int height)
        {
            OutputImage(output, inStream, clientFileName, width, height);
        }

        public static void OutputImage(Stream output, Stream inStream, string clientFileName, int width, int height, Action<Image> afterDrawImage)
        {
            Stream stream = inStream;
            if (stream == null || stream.Length == 0)
            {
                OutputEmptyImage(output, clientFileName, stream);
                return;
            }
            Image image = null;
            try
            {
                image = Image.FromStream(stream, false, false);
                double zoomX = (double)image.Width / (double)width;
                double zoomY = (double)image.Height / (double)height;
                double zoom = Math.Max(zoomX, zoomY);
                width = (int)Math.Round(image.Width / zoom);
                height = (int)Math.Round(image.Height / zoom);

                if (image.Width <= width && image.Height <= height)
                {
                    CopyStream(output, inStream);
                    return;
                }
                using (Bitmap bmp = DrawImage(width, height, image))
                {
                    if (afterDrawImage != null)
                    {
                        afterDrawImage(bmp);
                    }

                    OutputZoomImage(output, clientFileName, stream, bmp);
                }
            }
            finally
            {
                if (image != null)
                {
                    image.Dispose();
                }
            }
        }

        private static void OutputZoomImage(Stream output, string clientFileName, Stream stream, Bitmap bmp)
        {
            if (bmp != null)
            {
                try
                {
                    bmp.Save(output, GetImageFormat(clientFileName));
                }
                finally
                {
                    //bmp.Dispose();
                    stream.Dispose();
                }
            }

        }

        //private static void OutputNormalImage(Stream output, string clientFileName, MemoryStream stream, Image image)
        //{
        //    try
        //    {
        //        //MemoryStream outStream = new MemoryStream();
        //        try
        //        {
        //            image.Save(output, GetImageFormat(clientFileName));
        //            //CopyStream(output, outStream);
        //        }
        //        finally
        //        {
        //            //outStream.Dispose();
        //        }
        //    }
        //    finally
        //    {
        //        //image.Dispose();
        //        stream.Dispose();
        //    }

        //}

        private static void OutputEmptyImage(Stream output, string clientFileName, Stream stream)
        {
            Bitmap emptyBmp = _EmptyImage;
            try
            {
                emptyBmp.Save(output, GetImageFormat(clientFileName));
            }
            finally
            {
                stream.Dispose();
            }

        }

        public static void CopyStream(Stream ouputStream, Stream inStream)
        {
            const int BufferSize = 32 * 1024;
            byte[] buffer = GetCacheBuffer(BufferSize);
            if (inStream.CanSeek)
            {
                inStream.Seek(0, SeekOrigin.Begin);
            }
            int readLen = inStream.Read(buffer, 0, BufferSize);
            while (readLen > 0)
            {
                ouputStream.Write(buffer, 0, readLen);
                readLen = inStream.Read(buffer, 0, BufferSize);
            }
        }

        private static byte[] GetCacheBuffer(int bufferSize)
        {
            if (_cacheBuffer != null && _cacheBuffer.Length != bufferSize)
            {
                _cacheBuffer = null;
            }
            if (_cacheBuffer == null)
            {
                _cacheBuffer = new byte[bufferSize];
            }
            return _cacheBuffer;
        }

        private static ImageFormat GetImageFormat(string clientFileName)
        {
            string ext = Path.GetExtension(clientFileName);
            if (string.Compare(ext, ".gif", true) == 0)
            {
                return ImageFormat.Gif;
            }
            else if (string.Compare(ext, ".png", true) == 0)
            {
                return ImageFormat.Png;
            }
            else
            {
                return ImageFormat.Jpeg;
            }
        }

        private static Bitmap DrawImage(int width, int height, Image image)
        {
            Bitmap bmp = new Bitmap(width, height);
            Graphics g = Graphics.FromImage(bmp);
            try
            {
                g.Clear(Color.Transparent);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.DrawImage(image, 0, 0, width, height);
            }
            finally
            {
                g.Dispose();
            }
            return bmp;
        }

        private static Bitmap DrawEmptyImage()
        {
            Bitmap bmp = new Bitmap(1, 1);
            Graphics g = Graphics.FromImage(bmp);
            try
            {
                g.Clear(Color.Transparent);
                g.FillRectangle(new SolidBrush(Color.White), new Rectangle(0, 0, 1, 1));
            }
            finally
            {
                g.Dispose();
            }
            return bmp;
        }
    }
}
