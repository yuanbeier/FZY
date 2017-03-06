using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel;
using Topevery.FMP.Data;
using Topevery.Framework.Data;
using Topevery.FMP.ObjectModel.Configuration;
using System.Diagnostics;

namespace Topevery.FMP.Logic
{
    internal static class FileStorageManager
    {
        #region Fields
        private static readonly object _lockObj = new object();
        private static readonly object _inializeLock = new object();
        private static readonly Dictionary<string, ConfigSettingData> _settingsCache = new Dictionary<string, ConfigSettingData>();
        private static readonly ConfigSettingCache _settingsCacheByID = new ConfigSettingCache();
        private const int DefaultBufferSize =256 * 1024;
        private static ConfigSettingData _currentConfigSettings;
        private static Guid _currentStoreModeID;
        private static DateTime _lastDate = DateTime.UtcNow.Date;
        [ThreadStatic]
        private static byte[] _cacheBuffer;
        #endregion

        #region Methods
        public static void Initialize(IFileStorageProviderDataProvider dataProvider)
        {
            if (_currentStoreModeID == Guid.Empty || _lastDate != DateTime.UtcNow.Date)
            {
                lock (_inializeLock)
                {
                    if (_currentStoreModeID == Guid.Empty || _lastDate != DateTime.UtcNow.Date)
                    {
                        _lastDate = DateTime.UtcNow.Date;
                        Refresh();
                        ConfigSettingData configData = CurrentConfigSetting;
                        if (configData != null)
                        {
                            if (dataProvider == null)
                            {
                                dataProvider = DataProvider;
                            }
                            StoreModeData oldData = dataProvider.GetStoreModeByID(Guid.Empty);

                            StoreModeData data = new StoreModeData();
                            data.StoreParams = ObjectFactory.XmlSerializerObject(configData);
                            if (oldData != null && oldData.StoreParams == data.StoreParams)
                            {
                                _currentStoreModeID = oldData.ID;
                                return;
                            }
                            Guid id = CombineGuid.NewComboGuid();
                            data.ID = id;
                            dataProvider.SaveStoreMode(data);
                            if (!_settingsCache.ContainsKey(data.StoreParams))
                            {
                                lock (_settingsCache)
                                {
                                    if (!_settingsCache.ContainsKey(data.StoreParams))
                                    {
                                        _settingsCache[data.StoreParams] = configData;                                        
                                    }
                                }
                            }
                            _currentStoreModeID = id;
                        }
                    }
                }
            }
        }


        public static ConfigSettingData GetConfigSetting(IFileStorageProviderDataProvider dataProvider, Guid physicalFileID)
        {
            ConfigSettingData result = _settingsCacheByID[physicalFileID];
            if (result != null)
                return result.Clone();
            lock (_settingsCacheByID)
            {
                result = _settingsCacheByID[physicalFileID];                
            }
            if (result != null)
            {
                return result.Clone();
            }
            if (dataProvider == null)
            {
                dataProvider = DataProvider;
            }
            StoreModeData storeData = dataProvider.GetStoreModeByFileID(physicalFileID);
            if (storeData != null)
            {
                if (!_settingsCache.TryGetValue(storeData.StoreParams, out result))
                {
                    lock (_settingsCache)
                    {
                        if (!_settingsCache.TryGetValue(storeData.StoreParams, out result))
                        {
                            result = ObjectFactory.XmlDeserializeObject(typeof(ConfigSettingData), storeData.StoreParams) as ConfigSettingData;
                            _settingsCache[storeData.StoreParams] = result;
                        }
                    }
                }
                if (result != null)
                {
                    lock (_settingsCacheByID)
                    {
                        _settingsCacheByID.Add(physicalFileID, result);
                    }
                    return result.Clone();
                }
            }
            else
            {
                result = CurrentConfigSetting.Clone();
            }
            return result;
        }

        #region FileHelper
        public static void SetFileLength(IFileStorageProviderDataProvider dataProvider, Guid physicalFileID, string extensionName, long length)
        {
            IFileStorageProvider provider = FileStorageProviderFactory.CreateFileStorageProvider(dataProvider, physicalFileID, extensionName);
            if (provider != null)
            {
                try
                {
                    provider.SetLength(length);
                }
                finally
                {
                    provider.Close();
                }
            }
        }

        public static void CreateNewFile(IFileStorageProviderDataProvider dataProvider, Guid physicalFileID, string extensionName)
        {
            IFileStorageProvider provider = FileStorageProviderFactory.CreateFileStorageProvider(dataProvider, CurrentConfigSetting, physicalFileID, extensionName);
            if (provider != null)
            {
                try
                {
                    provider.SetLength(0);
                }
                finally
                {
                    provider.Close();
                }
            }
        }


        public static long WriteFile(IFileStorageProviderDataProvider dataProvider, Guid physicalFileID, string extensionName, long position, byte[] writeData)
        {
            long result = 0;
            IFileStorageProvider provider = FileStorageProviderFactory.CreateFileStorageProvider(dataProvider, physicalFileID, extensionName);
            if (provider != null)
            {
                try
                {
                    int length = 0;
                    if(writeData != null)
                    {
                        length = writeData.Length;
                    }
                    if (length > 0)
                    {
                        provider.Seek(position, System.IO.SeekOrigin.Begin);
                        provider.Write(writeData, 0, length);
                        result = provider.Length;
                    }
                }
                finally
                {
                    provider.Close();
                }
            }
            return result;
        }

        public static byte[] ReadFile(IFileStorageProviderDataProvider dataProvider, Guid physicalFileID, string extensionName, long position, int readCount)
        {
            LogHelper.Log.ErrorFormat("FileStorageManager.ReadFile(IFileStorageProviderDataProvider dataProvider='{0}', Guid physicalFileID='{1}', string extensionName='{2}', long position='{3}', int readCount='{4}')", dataProvider, physicalFileID, extensionName, position, readCount);

            byte[] result = null;
            IFileStorageProvider provider = FileStorageProviderFactory.CreateFileStorageProvider(dataProvider, physicalFileID, extensionName);
            if (provider != null)
            {
                try
                {
                    if (readCount > 0)
                    {
                        provider.Seek(position, System.IO.SeekOrigin.Begin);
                        byte[] buffer = GetCacheBuffer(readCount);
                        int readLen = provider.Read(buffer, 0, readCount);
                        if (readLen > 0)
                        {
                            result = new byte[readLen];
                            Buffer.BlockCopy(buffer, 0, result, 0, readLen);
                        }
                    }
                }
                catch (Exception ex)
                {
                    LogHelper.Log.Error(ex.Message, ex);
                }
                finally
                {
                    provider.Close();
                }
            }
            LogHelper.Log.ErrorFormat("FileStorageManager.ReadFile Result Bytes Length='{0}'", result.Length);
            return result;
        }

        public static long CopyFile(IFileStorageProviderDataProvider dataProvider, Guid oldPhysicalFileID, Guid newPhysicalFileID, string extensionName)
        {
            long result = 0;
            IFileStorageProvider oldProvider = FileStorageProviderFactory.CreateFileStorageProvider(dataProvider, oldPhysicalFileID, extensionName);
            IFileStorageProvider newProvider = FileStorageProviderFactory.CreateFileStorageProvider(dataProvider, CurrentConfigSetting, newPhysicalFileID, extensionName);
            if (oldProvider != null && newProvider != null)
            {
                try
                {
                    byte[] buffer = GetCacheBuffer(DefaultBufferSize);

                    int readLen = oldProvider.Read(buffer, 0, DefaultBufferSize);
                    while(readLen > 0)
                    {
                        newProvider.Write(buffer, 0, readLen);
                        readLen = oldProvider.Read(buffer, 0, DefaultBufferSize);
                    }
                    result = newProvider.Length;
                }
                finally
                {
                    oldProvider.Close();
                    newProvider.Close();
                }
            }
            return result;
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
        #endregion

        #endregion

        #region Properties
        public static ConfigSettingData CurrentConfigSetting
        {
            get
            {
                if (_currentConfigSettings == null)
                {
                    lock (_lockObj)
                    {
                        if (_currentConfigSettings == null)
                        {
                            FMPConfigurationSection section = FMPConfiguration.Section;
                            if (section != null)
                            {
                                ConfigSettingData configData = ConfigSettingData.CreateSettings(section);                                
                                if (configData.FileStorage != null && !string.IsNullOrEmpty(configData.FileStorage.RootFolder))
                                {
                                    string path = configData.FileStorage.RootFolder;                                    
                                    path = PathHelper.CombineIOPath(path, _lastDate.Year.ToString(), _lastDate.Month.ToString(), _lastDate.Day.ToString());
                                    configData.FileStorage.RootFolder = path;
                                }
                                _currentConfigSettings = configData;
                            }
                        }
                    }
                }
                return _currentConfigSettings;
            }
        }

        public static Guid GetCurrentStoreModeID(IFileStorageProviderDataProvider dataProvider)
        {
            Initialize(dataProvider);
            return _currentStoreModeID;
        }

        private static IFileStorageProviderDataProvider DataProvider
        {
            get
            {
                return DataProviderHelper.GetFileStorageProvider();
            }
        }

        private static void Refresh()
        {
            _currentConfigSettings = null;
        }
        #endregion
    }
}
