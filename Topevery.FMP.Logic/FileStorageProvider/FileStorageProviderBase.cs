using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel;
using System.IO;

namespace Topevery.FMP.Logic
{
    public abstract class FileStorageProviderBase : IFileStorageProvider
    {
        #region Fields
        private ConfigSettingData _configSetting;
        private Guid _fileID;
        private string _extensionName;
        #endregion

        #region Constructor
        protected FileStorageProviderBase()
        {
        }

        ~FileStorageProviderBase()
        {
            this.Dispose(false);
        }
        #endregion

        #region IFileStorageProvider Members

        public virtual void Initialize(ConfigSettingData config, Guid fileId, string extensionName)
        {
            _configSetting = config;
            _fileID = fileId;
            if (!string.IsNullOrEmpty(extensionName) && !extensionName.StartsWith("."))
            {
                extensionName = "." + extensionName;
            }
            _extensionName = extensionName;
            this.InitializeCore(config, fileId, extensionName);
        }

        protected abstract void InitializeCore(ConfigSettingData config, Guid fileId, string extensionName);

        public abstract int Read(byte[] buffer, int offset, int count);

        public abstract void Write(byte[] buffer, int offset, int count);

        public abstract long Seek(long offset, SeekOrigin origin);

        public virtual void Close()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
            
        }

        public abstract void Delete();

        public abstract void SetLength(long value);

        protected virtual void Dispose(bool disposing)
        {
        }
        #endregion

        #region IDisposable Members
        public void Dispose()
        {
            this.Close();
        }
        #endregion

        #region Properties
        public abstract long Length
        {
            get;
        }

        public virtual ConfigSettingData ConfigSetting
        {
            get
            {
                return this._configSetting;
            }
        }

        public virtual FileStorageData FileStorage
        {
            get
            {
                if (ConfigSetting != null)
                {
                    return ConfigSetting.FileStorage;
                }
                return null;
            }
        }

        protected virtual Guid FileID
        {
            get
            {
                return this._fileID;
            }
        }

        protected virtual string ExtendsionName
        {
            get
            {
                return this._extensionName;
            }
        }
        #endregion
    }
}
