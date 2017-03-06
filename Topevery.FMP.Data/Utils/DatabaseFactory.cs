using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.Data;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Data.Sql;

using Microsoft.Practices.EnterpriseLibrary.Data.Oracle;
using System.Data.Common;
using Topevery.Framework.Ioc;

namespace Topevery.FMP.Data
{
    public static class  DatabaseFactory
    {
        #region Fields
        public const string DefaultSqlProviderName = "System.Data.SqlClient";
        /// <summary>
        /// Default name for the Oracle managed provider.
        /// </summary>
        public const string DefaultOracleProviderName = "System.Data.OracleClient";
        #endregion

        #region Methods
        public static Database CreateDatabase()
        {
            return Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase();
        }

        public static Database CreateDatabase(string name)
        {
            return Microsoft.Practices.EnterpriseLibrary.Data.DatabaseFactory.CreateDatabase(name);
        }

        public static Database CreateDatabase(string connectionString, string providerType)
        {
            if (string.Compare(DefaultSqlProviderName, providerType, true) == 0)
            {
                return new SqlDatabase(connectionString);
            }
            else if (string.Compare(DefaultOracleProviderName, providerType, true) == 0)
            {
                return new OracleDatabase(connectionString);
            }
            else
            {
                Type type = ActivatorEx.GetType(providerType);
                if (type != null)
                {
                    Database result = null;
                    if (type.IsSubclassOf(typeof(Database)))
                    {
                        result = ActivatorEx.CreateInstance(type, new Type[] { typeof(string) }, new object[] { connectionString }) as Database;
                    }
                    else
                    {
                        DbProviderFactory fac = ActivatorEx.CreateInstance(providerType) as DbProviderFactory;
                        if (fac != null)
                        {
                            result = new GenericDatabase(connectionString, fac);
                        }
                    }
                    return result;
                }
            }
            return null;
        }
        #endregion
    }
}
