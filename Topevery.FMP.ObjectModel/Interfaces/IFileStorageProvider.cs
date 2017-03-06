using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel.Configuration;
using System.IO;

namespace Topevery.FMP.ObjectModel
{
    /// <summary>
    /// 本地文件操作接口
    /// </summary>
    public interface IFileStorageProvider :IDisposable
    {
        void Initialize(ConfigSettingData config, Guid fileId, string extensionName);
        int Read(byte[] buffer, int offset, int count);
        void Write(byte[] buffer, int offset, int count);
        long Seek(long offset, SeekOrigin origin);
        void Close(); 
        void Delete();
        void SetLength(long value);
        long Length
        {
            get;
        }
        ConfigSettingData ConfigSetting
        {
            get;
        }
    }
}
