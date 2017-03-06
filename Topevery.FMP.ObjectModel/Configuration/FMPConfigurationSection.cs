using System;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using Configuration_ConfigurationProperty=System.Configuration.ConfigurationProperty;

namespace Topevery.FMP.ObjectModel.Configuration
{
    public class FMPConfigurationSection : ConfigurationSection
    {
        #region Fields
        
        #endregion
        
        #region Properties
        [ConfigurationProperty(ConfigurationStrings.FileStorageKey)]
        public FileStorageElement FileStorage
        {
            get{
                return (FileStorageElement)this[ConfigurationStrings.FileStorageKey];
            }
        }
        #endregion
    }
}
