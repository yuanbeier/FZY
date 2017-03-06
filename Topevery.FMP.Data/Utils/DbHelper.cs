using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Topevery.FMP.Data
{
    internal static class DbHelper
    {
        #region Fields
        //private static readonly object _lockObj = new object();
        
        [ThreadStatic]
        private static IDictionary<string, Database> _cache = null;
        #endregion
        
        #region Methods
        public static Database CreateDatabase(string dbName)
        {
            return DatabaseFactory.CreateDatabase(dbName);
        }

        public static Database GetDatabase(string name)
        {
           // lock (Databases)
            {
                Database result = null;
                if (!Databases.ContainsKey(name))
                {
                    result = CreateDatabase(name);
                    
                    Databases[name] = result;
                }
                else
                {
                    result = Databases[name];
                }
                return result;
            }
        }
        #endregion

        #region Properties
        private static IDictionary<string, Database> Databases
        {
            get
            {
                //lock (_lockObj)
                {
                    if (_cache == null)
                    {
                        _cache = new Dictionary<string, Database>(StringComparer.InvariantCultureIgnoreCase);
                        //Console.WriteLine("Create Instance.");
                    }
                    return _cache;
                }
            }
        }

        //public static Database DefaultDatabase
        //{
        //    get
        //    {
        //        return GetDatabase(DefaultDatabaseName);
        //    }
        //}
        #endregion
    }
}
