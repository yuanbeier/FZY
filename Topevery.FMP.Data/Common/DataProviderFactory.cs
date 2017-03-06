using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.Data;
using Topevery.FMP.ObjectModel.Configuration;
using Topevery.FMP.ObjectModel.Runtime;
using DataProviderFactory = Topevery.Framework.Data.DataProviderFactory;

namespace Topevery.FMP.Data
{
    public static class FMPDataProviderFactory
    {
        #region Fields
        public const string GroupName = "topevery.fmp";
        
        [ThreadStatic]
        private static IDictionary<string, IDataProvider> _cache = null;
        #endregion
        
        #region Methods
        public static IDataProvider CreateDataProvider(string name)
        {
            return DataProviderFactory.CreateProvider(GroupName, name);
        }

        public static IDataProvider GetProvider(string name)
        {
            IDataProvider result = null;
            if (!DataProviders.TryGetValue(name, out result))
            {
                result = CreateDataProvider(name);
                if (result != null)
                {
                    DataProviders[name] = result;
                }
            }
            return result;
        }
        #endregion

        #region Fields
        private static IDictionary<string, IDataProvider> DataProviders
        {
            get
            {
                if (_cache == null)
                {
                    _cache = new Dictionary<string, IDataProvider>(StringComparer.InvariantCultureIgnoreCase);
                }
                return _cache;
            }
        }
        #endregion
    }
}
