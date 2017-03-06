using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration;

namespace Topevery.FMP.ObjectModel.Configuration
{
    public class FileStorageElement : ConfigurationElement
    {
        #region Properties
        [ConfigurationProperty(ConfigurationStrings.ProviderKey, DefaultValue = null)]
        public string Provider
        {
            get
            {
                return (string)this[ConfigurationStrings.ProviderKey];
            }
            set
            {
                this[ConfigurationStrings.ProviderKey] = value;
            }
        }

        [ConfigurationProperty(ConfigurationStrings.ServerKey, DefaultValue = null)]
        public string Server
        {
            get
            {
                return (string)this[ConfigurationStrings.ServerKey];
            }
            set
            {
                this[ConfigurationStrings.ServerKey] = value;
            }
        }

        [ConfigurationProperty(ConfigurationStrings.PortKey, DefaultValue = 0)]
        public int Port
        {
            get
            {
                return (int)this[ConfigurationStrings.PortKey];
            }
            set
            {
                this[ConfigurationStrings.PortKey] = value;
            }
        }

        [ConfigurationProperty(ConfigurationStrings.RootFolderKey, DefaultValue = null)]
        public string RootFolder
        {
            get
            {
                return (string)this[ConfigurationStrings.RootFolderKey];
            }
            set
            {
                this[ConfigurationStrings.RootFolderKey] = value;
            }
        }

        [ConfigurationProperty(ConfigurationStrings.UserNameKey, DefaultValue = null)]
        public string UserName
        {
            get
            {
                return (string)this[ConfigurationStrings.UserNameKey];
            }
            set
            {
                this[ConfigurationStrings.UserNameKey] = value;
            }
        }

        [ConfigurationProperty(ConfigurationStrings.PasswordKey, DefaultValue = null)]
        public string Password
        {
            get
            {
                return (string)this[ConfigurationStrings.PasswordKey];
            }
            set
            {
                this[ConfigurationStrings.PasswordKey] = value;
            }
        }

        [ConfigurationProperty(ConfigurationStrings.DatabaseKey, DefaultValue = null)]
        public string Database
        {
            get
            {
                return (string)this[ConfigurationStrings.DatabaseKey];
            }
            set
            {
                this[ConfigurationStrings.DatabaseKey] = value;
            }
        }

        #endregion
    }
}
