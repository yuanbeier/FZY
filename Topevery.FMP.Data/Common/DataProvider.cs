using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Topevery.Framework.Data;
using Topevery.Framework.Data.Configuration;
using System.Data;
using System.Data.Common;

namespace Topevery.FMP.Data
{
    public abstract class DataProvider : DataProviderBase
    {
        #region Fields
        public const string DefaultDatabaseName = "fmpDatabase";
        
        private Database _database;
        #endregion

        #region Constructor
        protected DataProvider()
        {
            
        }
        #endregion

        #region Methods
        
        protected override void InitializeCore(object contextData)
        {
            DataGroupElement groupData = this.DataGroupData;
            DataProviderElement element = this.DataProviderData;
            if(element != null && groupData != null)
            {
                string databaseName = element.Database;
                if(string.IsNullOrEmpty(databaseName))
                {
                    databaseName = groupData.Database;
                }
                if(string.IsNullOrEmpty(databaseName))
                {
                    databaseName = DefaultDatabaseName;
                }
                _database = CreateDatabase(databaseName);
            }
        }

        protected virtual Database CreateDatabase(string dbName)
        {
            return DbHelper.GetDatabase(dbName);
        }

        public override DbTransaction CreateTransaction()
        {
            DbTransaction result = null;
            if (this.Database != null)
            {
                DbConnection dbConn = this.Database.CreateConnection();
                dbConn.Open();
                result = dbConn.BeginTransaction();
            }
            return result;
        }

        public override void DisposeTransaction(DbTransaction trans)
        {
            if (trans != null)
            {
                IDbConnection con = trans.Connection;
                if (con != null && con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }
        }
        #endregion

        #region Properties
        protected virtual Database Database
        {
            get
            {
                return _database;
            }
            set
            {
                this._database = value;
            }
        }
        #endregion
    }
}
