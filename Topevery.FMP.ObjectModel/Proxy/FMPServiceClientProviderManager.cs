using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.ServiceModel.Proxy;

namespace Topevery.FMP.ObjectModel.Proxy
{
    public static class FMPServiceClientProviderManager
    {
        

        #region Methods
        public static void Refresh()
        {
            ServiceClientProviderManager.Refresh(FMPUtility.DefaultGroupName);
        }
        
        public static IServiceClientProvider CreateDefaultProvider()
        {
            return ServiceClientProviderManager.CreateProvider(FMPUtility.DefaultGroupName);
        }
        
        public static ServiceClientProvider<UserNameToken>  CreateDefaultProxyProviderByUserNameToken()
        {
            return CreateDefaultProvider() as ServiceClientProvider<UserNameToken>;
        }
        #endregion

        #region Properties
        public static IServiceClientProvider DefaultProxyProvider
        {
            get
            {
                return ServiceClientProviderManager.GetProvider(FMPUtility.DefaultGroupName);
            }
        }
        
        public static ServiceClientProvider<UserNameToken> DefaultProxyProviderByUserNameToken
        {
            get
            {
                return DefaultProxyProvider as ServiceClientProvider<UserNameToken>;
            }
        }
        #endregion
    }
}
