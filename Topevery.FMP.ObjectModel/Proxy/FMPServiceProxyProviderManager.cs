using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.ServiceModel.Proxy;
using System.Diagnostics;

namespace Topevery.FMP.ObjectModel.Proxy
{
    public static class FMPServiceProxyProviderManager
    {
        [ThreadStatic]
        private static IDictionary<string, IServiceProxyProvider> _cache;

        public const string RemoteFileStorageServiceName = "RemoteFileStorage";

        public static IServiceProxyProvider CreateInstance(string providerName)
        {
            return ServiceProxyProviderFactory.CreateProvider(FMPUtility.DefaultGroupName, providerName);
        }

        public static IServiceProxyProvider GetInstance(string providerName)
        {
            IServiceProxyProvider result = null;
            if (!Cache.TryGetValue(providerName, out result))
            {
                result = CreateInstance(providerName);
                if (result != null)
                {
                    Cache[providerName] = result;
                }
            }
            /*if (Cache.ContainsKey(providerName))
            {
                result = Cache[providerName];              
            }
            if (result == null)
            {
                 result = CreateInstance( providerName); 
                if(result != null)
                {
                    Cache[providerName] = result;
                }
            }*/
            return result;

        }

        public static void Refresh(string providerName)
        {
            ServiceProxyProviderFactory.Refresh(FMPUtility.DefaultGroupName, providerName);
        }
        
        public static void Refresh()
        {
            ServiceProxyProviderFactory.Refresh(FMPUtility.DefaultGroupName);
        }

        public static IRemoteFileStorage CreateRemoteFileStorage()
        {
            return CreateInstance(RemoteFileStorageServiceName) as IRemoteFileStorage;
        }

        public static IRemoteFileStorage GetRemoteFileStorage()
        {
            return GetInstance(RemoteFileStorageServiceName) as IRemoteFileStorage;
        }

        #region Properties
        public static IDictionary<string, IServiceProxyProvider> Cache
        {
            get
            {
                if (_cache == null)
                {
                    _cache = new Dictionary<string, IServiceProxyProvider>(StringComparer.InvariantCultureIgnoreCase);
                    //Debug.WriteLine("Create ServiceProxyProvider Container");
                }
                return _cache;
            }
        }
        #endregion
    }
}
