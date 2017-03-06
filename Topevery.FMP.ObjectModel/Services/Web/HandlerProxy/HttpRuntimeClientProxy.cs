using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.ServiceModel.Proxy;
using Topevery.Framework.ServiceModel.Configuration;

namespace Topevery.FMP.ObjectModel
{
    public class HttpRuntimeClientProxy : HttpRuntimeClient, IServiceProxyProvider
    {
        #region Fields
        private string _providerName;
        private ServiceGroupElement _groupData;
        #endregion

        #region IServiceProxyProvider Members

        public void Initialize(string providerName, object contextData)
        {
            _providerName = providerName;
            _groupData = contextData as ServiceGroupElement;
        }

        public string Name
        {
            get
            {
                return _providerName;
            }
        }

        protected string ServerUrl
        {
            get
            {
                if (this.ConfigurationGroupData != null)
                {
                    return this.ConfigurationGroupData.Client.BaseAddress;
                }
                return null;
            }
        }

        protected ServiceGroupElement ConfigurationGroupData
        {
            get
            {
                return this._groupData;
            }
        }

        protected ClientElement ConfigurationClientData
        {
            get
            {
                if (ConfigurationGroupData != null && ConfigurationGroupData.Client != null)
                {
                    return ConfigurationGroupData.Client.Services[Name];
                }
                return null;
            }
        }
        #endregion
    }
}
