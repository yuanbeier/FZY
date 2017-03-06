using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel;
using System.IO;
using Topevery.Web.Services;

namespace Topevery.FMP.Logic
{
    public class LocalFileStorageProvider : FileStorageProviderBase
    {
        #region Fields
        private Stream _innerStream;
        #endregion

        #region Constructor
        protected LocalFileStorageProvider()
        {
        }
        #endregion

        #region Methods
        protected override void InitializeCore(ConfigSettingData config, Guid fileId, string extendsionName)
        {
            string log = string.Empty;
            string filePath = GetFilePath();
            log = string.Format("LogicFileID='{0}' | FilePath='{1}'", fileId, filePath);
            LogHelper.Log.Debug(log);
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            this.PrepareStream();
            return this.InnerStream.Read(buffer, offset, count);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            this.PrepareStream();
            this.InnerStream.Write(buffer, offset, count);
        }

        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            this.PrepareStream();
            return this.InnerStream.Seek(offset, origin);
        }

        public override void Delete()
        {
            string fileName = this.GetFileName();
            if (File.Exists(fileName))
            {
                File.Delete(fileName);
            }
        }

        public override void SetLength(long value)
        {
            this.PrepareStream();
            this.InnerStream.SetLength(value);
        }

        public override long Length
        {
            get
            {
                this.PrepareStream();
                return this.InnerStream.Length;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (InnerStream != null)
            {
                InnerStream.Close();
            }
            base.Dispose(disposing);
        }

        protected virtual void PrepareStream()
        {
            if (_innerStream == null)
            {
                string fileName = this.GetFilePath();
                string path = Path.GetDirectoryName(fileName);
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                _innerStream = new FileStream(fileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.ReadWrite, System.IO.FileShare.ReadWrite);
            }
        }

        public virtual string GetFileName()
        {
            string result = this.FileID.ToString() + this.ExtendsionName;
            return result;
        }

        protected virtual string GetFilePath()
        {
            string result = GetFileName();
            string path = RootPath;
            if (!string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(result))
            {
                result = PathHelper.CombineIOPath(path, result);

            }
            return result;
        }
        #endregion

        #region Properties
        protected virtual Stream InnerStream
        {
            get
            {
                return _innerStream;
            }            
        }

        protected virtual string RootPath
        {
            get
            {
                string result = null;
                if (this.FileStorage != null)
                {
                    result = PathHelper.ResolvePath(this.FileStorage.RootFolder);
                }
                return result;
            }
        }
        #endregion
    }
}
