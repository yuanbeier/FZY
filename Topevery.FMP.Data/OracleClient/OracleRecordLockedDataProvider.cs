using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Topevery.Framework.Data;
using Topevery.Framework.Data.SqlClient;
using Topevery.FMP.ObjectModel;

namespace Topevery.FMP.Data.OracleClient
{
    public class OracleRecordLockedDataProvider : RecordLockedDataProvider
    {
        private const string PACKAGE_NAME = "PKG_FMP_BAS.";

        #region Constructor
        private OracleRecordLockedDataProvider()
        {
        }
        #endregion

        #region Logic
        public override RecordLockedData CreateLockInfo(RecordLockedData lockedData)
        {
            RecordLockedData result = null;
            if (lockedData != null)
            {
                result = (RecordLockedData)lockedData.Clone();
                if (result.ID == Guid.Empty)
                {
                    result.ID = CombineGuid.NewComboGuid();
                }

                if (result.LockTime == DateTime.MinValue)
                {
                    result.LockTime = DateTime.UtcNow;
                }

                DbCommand dbCmd = this.InsertCommand;
                Utils.PrepareParameter(dbCmd);
                this.Adapter.SetParametersValue(dbCmd.Parameters, result, false);
                Utils.ResetParameter(dbCmd);
                
                this.Database.ExecuteNonQuery(dbCmd);
            }
            return result;
        }

        public override void UnLock(Guid id)
        {
            DbCommand dbCmd = this.UnlockCommand;
            this.Database.SetParameterValue(dbCmd, "p_id", id.ToString().ToUpper());
            this.Database.ExecuteNonQuery(dbCmd);
        }

        public override void UpdateLockInfo(Guid id, DateTime expireDateTime)
        {
            DbCommand dbCmd = this.UpdateCommand;
            this.Database.SetParameterValue(dbCmd, "p_id", id.ToString().ToUpper());
            this.Database.SetParameterValue(dbCmd, "p_expire_time", expireDateTime);
            this.Database.ExecuteNonQuery(dbCmd);
        }

        public override RecordLockedDataCollection GetLockedInfo(RecordLockedFetchParameter fetchParam)
        {
            RecordLockedDataCollection result = new RecordLockedDataCollection();
            DbCommand dbCmd = this.SelectCommand;
            this.Database.SetParameterValue(dbCmd, "p_lock_id", fetchParam.LockID.ToString().ToUpper());
            this.Database.SetParameterValue(dbCmd, "p_form_id", fetchParam.FormID.ToString().ToUpper());
            this.Database.SetParameterValue(dbCmd, "p_form_unique_id", fetchParam.FormUniqueID.ToString().ToUpper());
            using (IDataReader reader = this.Database.ExecuteReader(dbCmd))
            {
                this.EntityFactory.BuildEntityCollection(result, reader);
            }
            return result;
        }
        #endregion

        #region Create DbCommand
        protected override DbCommand CreateInsertCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_insert_record_lock";
            
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_id", DbType.String);
            db.AddInParameter(dbCmd, "p_form_id", DbType.String);
            db.AddInParameter(dbCmd, "p_form_unique_id", DbType.String);
            db.AddInParameter(dbCmd, "p_lock_time", DbType.DateTime);
            db.AddInParameter(dbCmd, "p_expire_time", DbType.DateTime);
            db.AddInParameter(dbCmd, "p_lock_user_id", DbType.String);
            return dbCmd;
        }

        protected override DbCommand CreateUpdateCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_update_record_lock_exp";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_id", DbType.String);
            db.AddInParameter(dbCmd, "p_expire_time", DbType.DateTime);
            return dbCmd;
        }

        protected override DbCommand CreateSelectCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_fetch_record_lock";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_lock_id", DbType.String);
            db.AddInParameter(dbCmd, "p_form_id", DbType.String);
            db.AddInParameter(dbCmd, "p_form_unique_id", DbType.String);
            return dbCmd;
        }

        protected override DbCommand CreateUnlockCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_delete_record_lock";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_id", DbType.String);
            return dbCmd;
        }
        #endregion

        protected override EntityFactory CreateEntityFactory()
        {
            return new Topevery.Framework.Data.OraClient.OraEntityFactory();
        }
    }
}
