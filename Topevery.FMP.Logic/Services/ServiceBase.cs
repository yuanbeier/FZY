using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.Data;
using Topevery.Framework.ServiceModel;

namespace Topevery.FMP.Logic
{
    public abstract class ServiceBase : ServiceProxyProviderBase
    {
        #region Fields
        private IDataProvider _dataProvider;
        #endregion
        
        #region Constructor
        protected ServiceBase()
        {
            
        }
        #endregion

        #region Methods
        protected virtual IDataProvider CreateDataProvider()
        {
            IDataProvider result = null;
            if (ConfigurationClientData != null && ConfigurationGroupData != null
                && !string.IsNullOrEmpty(ConfigurationGroupData.DataGroup))
            {
                result = DataProviderFactory.CreateProvider(ConfigurationGroupData.DataGroup, this.ConfigurationClientData.DataProvider);
            }

            if(result == null)
            {
                return CreateDataProviderCore();
            }
            return result;
        }
        protected abstract IDataProvider CreateDataProviderCore();
        #endregion

        #region Properties
        protected virtual IDataProvider InnerDataProvider
        {
            get
            {
                if(!this.IsDataProviderCreated)
                {
                    this._dataProvider = this.CreateDataProvider();
                }
                return _dataProvider;
            }
            set
            {
                _dataProvider = value;
            }
        }
        
        protected bool IsDataProviderCreated
        {
            get
            {
                return _dataProvider != null;
            }
        }
        #endregion
    }
}
