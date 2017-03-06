using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.ComponentModel;
using Topevery.FMP.ObjectModel.Configuration;

namespace Topevery.FMP.ObjectModel
{
    [XmlRoot(ConfigurationStrings.FileStorageKey)]
    [Serializable]
    public class FileStorageData : ICloneable
    {
        #region Fields
        private string _provider;
        private string _server;
        private int _port;
        private string _rootFolder;
        private string _userName;
        private string _passWord;
        private string _database;
        #endregion

        #region Methods
        public virtual FileStorageData Clone()
        {
            FileStorageData result = new FileStorageData();
            result.Provider = this.Provider;
            result.Server = this.Server;
            result.Port = this.Port;
            result.RootFolder = this.RootFolder;
            result.UserName = this.UserName;
            result.Password = this.Password;
            result.Database = this.Database;
            return result;
        }

        object ICloneable.Clone()
        {
            return this.Clone();
        }
        #endregion

        #region Propreties
        [DefaultValue(null)]
        [XmlAttribute(ConfigurationStrings.ProviderKey)]
        public string Provider
        {
            get
            {
                return this._provider;
            }
            set
            {
                this._provider = value;
            }
        }

        [DefaultValue(null)]
        [XmlAttribute(ConfigurationStrings.ServerKey)]        
        public string Server
        {
            get
            {
                return this._server;
            }
            set
            {
                this._server = value;
            }
        }

        [XmlAttribute(ConfigurationStrings.PortKey)]
        [DefaultValue(0)]
        public int Port
        {
            get
            {
                return this._port;
            }
            set
            {
                this._port = value;
            }
        }

        [XmlAttribute(ConfigurationStrings.RootFolderKey)]
        [DefaultValue(null)]
        public string RootFolder
        {
            get
            {
                return this._rootFolder;
            }
            set
            {
                this._rootFolder = value;
            }
        }

        [DefaultValue(null)]
        [XmlAttribute(ConfigurationStrings.UserNameKey)]
        public string UserName
        {
            get
            {
                return this._userName;
            }
            set
            {
                this._userName = value;
            }
        }

        [DefaultValue(null)]
        [XmlAttribute(ConfigurationStrings.PasswordKey)]
        public string Password
        {
            get
            {
                return this._passWord;
            }
            set
            {
                this._passWord = value;
            }
        }

        [DefaultValue(null)]
        [XmlAttribute(ConfigurationStrings.DatabaseKey)]
        public string Database
        {
            get
            {
                return this._database;
            }
            set
            {
                this._database = value;
            }
        }
        #endregion
    }
}
