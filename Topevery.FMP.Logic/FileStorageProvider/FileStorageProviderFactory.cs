using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel;
using Topevery.FMP.ObjectModel.Configuration;
using Topevery.FMP.Data;
using Topevery.Framework.Data;

namespace Topevery.FMP.Logic
{
    internal static class FileStorageProviderFactory
    {
        

        #region Methods



        public static IFileStorageProvider CreateFileStorageProvider(IFileStorageProviderDataProvider dataProvider, Guid physicalFileID, string extendsionName)
        {
            return CreateFileStorageProvider(dataProvider, null, physicalFileID, extendsionName);
        }

        public static IFileStorageProvider CreateFileStorageProvider(IFileStorageProviderDataProvider dataProvider, ConfigSettingData configData, Guid physicalFileID, string extendsionName)
        {
            IFileStorageProvider provider = null;
            if (configData == null)
            {
                configData = FileStorageManager.GetConfigSetting(dataProvider, physicalFileID);
            }
            if (configData != null && configData.FileStorage != null)
            {
                provider = ObjectFactory.CreateObject(configData.FileStorage.Provider) as IFileStorageProvider;
                if (provider != null)
                {
                    provider.Initialize(configData, physicalFileID, extendsionName);
                }
            }
            return provider;
        }

        
        #endregion

        #region Properties
        

        
        #endregion
    }
}
