using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Topevery.Framework.Data;
using Topevery.Framework.Data.SqlClient;
using Topevery.FMP.ObjectModel;

namespace Topevery.FMP.Data.SqlClient
{
    public class SqlRecordLockedDataProvider : RecordLockedDataProvider
    {
        #region Constructor
        private SqlRecordLockedDataProvider()
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
                this.Adapter.SetParametersValue(dbCmd.Parameters, result, false);
                
                this.Database.ExecuteNonQuery(dbCmd);
            }
            return result;
        }

        public override void UnLock(Guid id)
        {
            DbCommand dbCmd = this.UnlockCommand;
            this.Database.SetParameterValue(dbCmd, "@id", id);
            this.Database.ExecuteNonQuery(dbCmd);
        }

        public override void UpdateLockInfo(Guid id, DateTime expireDateTime)
        {
            DbCommand dbCmd = this.UpdateCommand;
            this.Database.SetParameterValue(dbCmd, "@id", id);            
            this.Database.SetParameterValue(dbCmd, "@expire_time", expireDateTime);
            this.Database.ExecuteNonQuery(dbCmd);
        }

        public override RecordLockedDataCollection GetLockedInfo(RecordLockedFetchParameter fetchParam)
        {
            RecordLockedDataCollection result = new RecordLockedDataCollection();
            DbCommand dbCmd = this.SelectCommand;
            this.Database.SetParameterValue(dbCmd, "@lock_id", fetchParam.LockID);
            this.Database.SetParameterValue(dbCmd, "@form_id", fetchParam.FormID);
            this.Database.SetParameterValue(dbCmd, "@form_unique_id", fetchParam.FormUniqueID);
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
            const string ProcName = "ty_fmp_insert_record_lock";
            
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "@id", DbType.Guid);
            db.AddInParameter(dbCmd, "@form_id", DbType.Guid);
            db.AddInParameter(dbCmd, "@form_unique_id", DbType.Guid);
            db.AddInParameter(dbCmd, "@lock_time", DbType.DateTime);
            db.AddInParameter(dbCmd, "@expire_time", DbType.DateTime);
            db.AddInParameter(dbCmd, "@lock_user_id", DbType.Guid);
            return dbCmd;
        }

        protected override DbCommand CreateUpdateCommand()
        {
            const string ProcName = "ty_fmp_update_record_lock_expire_time";            
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "@id", DbType.Guid);
            db.AddInParameter(dbCmd, "@expire_time", DbType.DateTime);
            return dbCmd;
        }

        protected override DbCommand CreateSelectCommand()
        {
            const string ProcName = "ty_fmp_fetch_record_lock";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "@lock_id", DbType.Guid);
            db.AddInParameter(dbCmd, "@form_id", DbType.Guid);
            db.AddInParameter(dbCmd, "@form_unique_id", DbType.Guid);
            return dbCmd;
        }

        protected override DbCommand CreateUnlockCommand()
        {
            const string ProcName = "ty_fmp_delete_record_lock";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "@id", DbType.Guid);
            return dbCmd;
        }

        #endregion

        protected override EntityFactory CreateEntityFactory()
        {
            return new SqlEntityFactory();
        }
    }
}
