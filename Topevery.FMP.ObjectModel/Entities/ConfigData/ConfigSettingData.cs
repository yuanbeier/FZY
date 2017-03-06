using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using Topevery.FMP.ObjectModel.Configuration;

namespace Topevery.FMP.ObjectModel
{
    [XmlRoot("configSettings")]
    [Serializable]
    public class ConfigSettingData : ICloneable
    {
        #region Fields
        private FileStorageData _fileStorage;
        #endregion

        #region Methods
        public static ConfigSettingData CreateSettings(FMPConfigurationSection configData)
        {
            ConfigSettingData result = null;
            if (configData != null)
            {
                result = new ConfigSettingData();
                FileStorageData fileStorage = new FileStorageData();
                result.FileStorage = fileStorage;
                FileStorageElement eFileStorage = configData.FileStorage;
                fileStorage.Provider = eFileStorage.Provider;
                fileStorage.Server = eFileStorage.Server;
                fileStorage.Port = eFileStorage.Port;
                fileStorage.RootFolder = eFileStorage.RootFolder;
                fileStorage.UserName = eFileStorage.UserName;
                fileStorage.Password = eFileStorage.Password;
                fileStorage.Database = eFileStorage.Database;
            }
            return result;
        }

        public virtual ConfigSettingData Clone()
        {
            ConfigSettingData result = new ConfigSettingData();
            if (this.FileStorage != null)
            {
                result.FileStorage = this.FileStorage.Clone();
            }
            return result;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion

        #region Properties
        [XmlElement("fileStorage")]
        public FileStorageData FileStorage
        {
            get
            {
                if (_fileStorage == null)
                {
                    _fileStorage = new FileStorageData();
                }
                return _fileStorage;
            }
            set
            {
                _fileStorage = value;
            }
        }
        #endregion
    }
}
