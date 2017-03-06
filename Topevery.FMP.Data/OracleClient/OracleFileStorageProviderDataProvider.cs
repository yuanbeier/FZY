using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using Topevery.Framework.Data;
using Topevery.FMP.ObjectModel;
using Topevery.Framework.Data.SqlClient;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Data;

namespace Topevery.FMP.Data.OracleClient
{
    public class OracleFileStorageProviderDataProvider : FileStorageProviderDataProvider
    {
        private const string PACKAGE_NAME = "PKG_FMP_BAS.";
        #region Methods
        public override StoreModeData SaveStoreMode(StoreModeData configData)
        {
            StoreModeData result = null;
            if (configData != null)
            {
                StoreModeData data = (StoreModeData)configData.Clone();
                if (data.ID == Guid.Empty)
                {
                    data.ID = CombineGuid.NewComboGuid();
                }
                DbCommand dbCmd = this.InsertCommand;
                Utils.PrepareParameter(dbCmd);
                this.Adapter.SetParametersValue(dbCmd.Parameters, data);
                Utils.ResetParameter(dbCmd);
                this.Database.ExecuteNonQuery(dbCmd);
                result = data;
            }
            return result;
        }

        public override StoreModeData GetStoreModeByID(Guid id)
        {            
            return GetStoreMode(id, Guid.Empty);
        }

        public override StoreModeData GetStoreModeByFileID(Guid fileID)
        {
            return GetStoreMode(Guid.Empty, fileID);
        }

        #region LogicFile
        public override LogicFileInfoData FetchFileInfo(LogicFileInfoItemData fetchParam, DbTransaction trans)
        {
            LogicFileInfoData result = null;
            DbCommand dbCmd = this.FetchFileInfoCommand;
            
            this.Database.SetParameterValue(dbCmd, "p_logic_file_id", fetchParam.ID.ToString().ToUpper());
            this.Database.SetParameterValue(dbCmd, "p_physical_file_id", fetchParam.PhysicalFileID.ToString().ToUpper());
            if (fetchParam.Version > 0)
            {
                this.Database.SetParameterValue(dbCmd, "p_VERSION", fetchParam.Version);
            }
            this.Database.SetParameterValue(dbCmd, "p_with_physical_file", fetchParam.WithPhysicalFileInfo);
            IDataReader reader = null;
            if (trans != null)
            {
                reader = this.Database.ExecuteReader(dbCmd, trans);
            }
            else
            {
                reader = this.Database.ExecuteReader(dbCmd);
            }
            if (reader != null)
            {
                try
                {
                    if (reader.Read())
                    {
                        result = new LogicFileInfoData();
                        FieldLookup looup = this.EntityFactory.CreateFieldLookup(reader);
                        this.Adapter.SetEntityValue(result, reader, looup);
                        if (fetchParam.WithPhysicalFileInfo && reader.NextResult())
                        {
                            this.EntityFactory.BuildEntityCollection(result.PhysicalFileInfos, reader);
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            return result;
        }

        public override LogicFileInfoData CreateFileInfo(LogicFileInfoData logicFileInfo, DbTransaction trans)
        {
            LogicFileInfoData result = (LogicFileInfoData)logicFileInfo.Clone();
            DbCommand dbCmd = this.CreateFileInfoCommand;
            GuidEntityWrapper<LogicFileInfoData> wrapper = Utils.CreateWrapperData<LogicFileInfoData>(result, true);
            Utils.PrepareParameter(dbCmd);
            this.Adapter.SetParametersValue(dbCmd.Parameters, wrapper);
            Utils.ResetParameter(dbCmd);
            if (trans != null)
            {
                this.Database.ExecuteNonQuery(dbCmd, trans);
            }
            else
            {
                this.Database.ExecuteNonQuery(dbCmd);
            }
            return result;
        }

        public override LogicFileInfoData UpdateFileInfo(LogicFileInfoData updateFileInfo, DbTransaction trans)
        {
            LogicFileInfoData result = (LogicFileInfoData)updateFileInfo.Clone();
            DbCommand dbCmd = this.UpdateFileInfoCommand;
            GuidEntityWrapper<LogicFileInfoData> wrapper = Utils.CreateWrapperData<LogicFileInfoData>(result, false);
            Utils.PrepareParameter(dbCmd);
            this.Adapter.SetParametersValue(dbCmd.Parameters, wrapper);
            Utils.ResetParameter(dbCmd);
            if (trans != null)
            {
                this.Database.ExecuteNonQuery(dbCmd, trans);
            }
            else
            {
                this.Database.ExecuteNonQuery(dbCmd);
            }
            return result;
        }

        public override void UpdateFileStatus(Guid id, LogicFileStatus status, DbTransaction trans)
        {
            DbCommand dbCmd = this.UpdateFileInfoCommand;
            this.Database.SetParameterValue(dbCmd, "p_logic_file_id", id.ToString().ToUpper());
            this.Database.SetParameterValue(dbCmd, "p_logic_file_status", status);
            if (trans != null)
            {
                this.Database.ExecuteNonQuery(dbCmd, trans);
            }
            else
            {
                this.Database.ExecuteNonQuery(dbCmd);
            }            
        }

        public override PhysicalFileInfoData CreatePhysicalFileInfo(PhysicalFileInfoData physicalFileInfo, DbTransaction trans)
        {
            PhysicalFileInfoData result = (PhysicalFileInfoData)physicalFileInfo.Clone();
            DbCommand dbCmd = this.CreatePhysicalFileInfoCommand;
            GuidEntityWrapper<PhysicalFileInfoData> wrapper = Utils.CreateWrapperData<PhysicalFileInfoData>(result, false);
            Utils.PrepareParameter(dbCmd);
            this.Adapter.SetParametersValue(dbCmd.Parameters, wrapper);
            Utils.ResetParameter(dbCmd);
            if (trans != null)
            {
                this.Database.ExecuteNonQuery(dbCmd, trans);
            }
            else
            {
                this.Database.ExecuteNonQuery(dbCmd);
            }
            return result;
        }

        public override PhysicalFileInfoData UpdatePhyscialFileInfo(PhysicalFileInfoData physicalFileInfo, DbTransaction trans)
        {
            PhysicalFileInfoData result = (PhysicalFileInfoData)physicalFileInfo.Clone();
            DbCommand dbCmd = this.UpdatePhysicalFileInfoCommand;
            GuidEntityWrapper<PhysicalFileInfoData> wrapper = Utils.CreateWrapperData<PhysicalFileInfoData>(result, false);
            Utils.PrepareParameter(dbCmd);
            this.Adapter.SetParametersValue(dbCmd.Parameters, wrapper);
            Utils.ResetParameter(dbCmd);
            if (trans != null)
            {
                this.Database.ExecuteNonQuery(dbCmd, trans);
            }
            else
            {
                this.Database.ExecuteNonQuery(dbCmd);
            }
            return result;
        }

        public override void UpdatePhysicalFileLength(Guid physicalFileID, long length, DateTime lastUpdateTime, Guid lastUpdateUserID, DbTransaction trans)
        {
            DbCommand dbCmd = this.UpdatePhysicalFileLengthCommand;

            this.Database.SetParameterValue(dbCmd, "p_physical_file_id", physicalFileID.ToString().ToUpper());
            this.Database.SetParameterValue(dbCmd, "p_file_length", length);
            this.Database.SetParameterValue(dbCmd, "p_db_last_updated_date", lastUpdateTime);
            this.Database.SetParameterValue(dbCmd, "p_db_last_updated_id", lastUpdateUserID.ToString().ToUpper());
            if (trans != null)
            {
                this.Database.ExecuteNonQuery(dbCmd, trans);
            }
            else
            {
                this.Database.ExecuteNonQuery(dbCmd);
            }      
        }
        #endregion

        private DbCommand GetNewStoreMode()
        {
            const string SqlText = @"
 SELECT t1.store_mode_id, t1.store_mode_name, t1.store_params
  FROM (SELECT ROWNUM rn, t.store_mode_id, t.store_mode_name, t.store_params
          FROM ty_fmp_store_mode t) t1
 WHERE t1.rn = 1
 ORDER BY t1.store_mode_id";
            return Database.GetSqlStringCommand(SqlText);
        }

        protected virtual StoreModeData GetStoreMode(Guid id, Guid fileID)
        {
            StoreModeData result = null;
            DbCommand dbCmd = null;
            if (id != Guid.Empty || fileID != Guid.Empty)
            {
                dbCmd = this.SelectCommand;
                Database.SetParameterValue(dbCmd, "p_store_mode_id", id.ToString().ToUpper());
                Database.SetParameterValue(dbCmd, "p_physical_file_id", fileID.ToString().ToUpper());
            }
            else
            {
                dbCmd = GetNewStoreMode();
            }
            using (IDataReader reader = Database.ExecuteReader(dbCmd))
            {
                if (reader.Read())
                {
                    result = new StoreModeData();
                    FieldLookup lookup = this.EntityFactory.CreateFieldLookup(reader);
                    this.Adapter.SetEntityValue(result, reader, lookup);
                }
            }
            return result;
        }

        protected override DbCommand CreateInsertCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_insert_store_mode";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_store_mode_id", DbType.String);
            db.AddInParameter(dbCmd, "p_store_mode_name", DbType.String);
            db.AddInParameter(dbCmd, "p_store_params", DbType.String);
            return dbCmd;
        }

        protected override System.Data.Common.DbCommand CreateUpdateCommand()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        protected override System.Data.Common.DbCommand CreateSelectCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_fetch_store_mode";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_store_mode_id", DbType.String);
            db.AddInParameter(dbCmd, "p_physical_file_id", DbType.String);
            return dbCmd;
        }

        protected override DbCommand CreateFetchFileInfoCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_fetch_logic_file";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_logic_file_id", DbType.String);
            db.AddInParameter(dbCmd, "p_physical_file_id", DbType.String);
            db.AddInParameter(dbCmd, "p_version", DbType.Int32);
            db.AddInParameter(dbCmd, "p_with_physical_file", DbType.Boolean);

            string cur_OUT = db.BuildParameterName("cur_out");
            dbCmd.Parameters.Add(Common.GetCursorParameter(cur_OUT));

            string cur_OUT_Ext0 = db.BuildParameterName("cur_out1");
            dbCmd.Parameters.Add(Common.GetCursorParameter(cur_OUT_Ext0));

            return dbCmd;
        }

        protected override DbCommand CreateCreateFileInfoCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_insert_logic_file";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_logic_file_id", DbType.String);
            db.AddInParameter(dbCmd, "p_logic_file_name", DbType.String);
            db.AddInParameter(dbCmd, "p_logic_file_ext", DbType.String);
            db.AddInParameter(dbCmd, "p_latest_physical_file_id", DbType.String);
            db.AddInParameter(dbCmd, "p_is_read_only", DbType.Boolean);
            db.AddInParameter(dbCmd, "p_description", DbType.String);
            db.AddInParameter(dbCmd, "p_logic_file_status", DbType.Int32);
            db.AddInParameter(dbCmd, "p_order_num", DbType.Int64);
            db.AddInParameter(dbCmd, "p_db_status", DbType.Int32);
            db.AddInParameter(dbCmd, "p_db_created_date", DbType.DateTime);
            db.AddInParameter(dbCmd, "p_db_created_id", DbType.String);
            db.AddInParameter(dbCmd, "p_db_last_updated_date", DbType.DateTime);
            db.AddInParameter(dbCmd, "p_db_last_updated_id", DbType.String);
            return dbCmd;
        }

        protected override DbCommand CreateUpdateFileInfoCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_update_logic_file";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_logic_file_id", DbType.String);
            db.AddInParameter(dbCmd, "p_logic_file_name", DbType.String);
            db.AddInParameter(dbCmd, "p_logic_file_ext", DbType.String);
            db.AddInParameter(dbCmd, "p_latest_physical_file_id", DbType.String);
            db.AddInParameter(dbCmd, "p_is_read_only", DbType.Boolean);
            db.AddInParameter(dbCmd, "p_description", DbType.String);
            db.AddInParameter(dbCmd, "p_logic_file_status", DbType.Int32);
            db.AddInParameter(dbCmd, "p_order_num", DbType.Int64);
            db.AddInParameter(dbCmd, "p_db_status", DbType.Int32);
            db.AddInParameter(dbCmd, "p_db_created_date", DbType.DateTime);
            db.AddInParameter(dbCmd, "p_db_created_id", DbType.String);
            db.AddInParameter(dbCmd, "p_db_last_updated_date", DbType.DateTime);
            db.AddInParameter(dbCmd, "p_db_last_updated_id", DbType.String);
            return dbCmd;
        }

        protected override DbCommand CreateCreatePhysicalFileInfoCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_insert_physical_file";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_physical_file_id", DbType.String);
            db.AddInParameter(dbCmd, "p_logic_file_id", DbType.String);
            db.AddInParameter(dbCmd, "p_VERSION", DbType.Int32);
            db.AddInParameter(dbCmd, "p_file_length", DbType.Int64);
            db.AddInParameter(dbCmd, "p_compress_mode_id", DbType.String);
            db.AddInParameter(dbCmd, "p_encrypt_mode_id", DbType.String);
            db.AddInParameter(dbCmd, "p_order_num", DbType.Int64);
            db.AddInParameter(dbCmd, "p_store_mode_id", DbType.String);
            db.AddInParameter(dbCmd, "p_db_status", DbType.Int32);
            db.AddInParameter(dbCmd, "p_db_created_date", DbType.DateTime);
            db.AddInParameter(dbCmd, "p_db_created_id", DbType.String);
            db.AddInParameter(dbCmd, "p_db_last_updated_date", DbType.DateTime);
            db.AddInParameter(dbCmd, "p_db_last_updated_id", DbType.String);
            return dbCmd;
        }

        protected override DbCommand CreateUpdatePhysicalFileInfoCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_update_physical_file";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_physical_file_id", DbType.String);
            db.AddInParameter(dbCmd, "p_logic_file_id", DbType.String);
            db.AddInParameter(dbCmd, "p_VERSION", DbType.Int32);
            db.AddInParameter(dbCmd, "p_file_length", DbType.Int64);
            db.AddInParameter(dbCmd, "p_compress_mode_id", DbType.String);
            db.AddInParameter(dbCmd, "p_encrypt_mode_id", DbType.String);
            db.AddInParameter(dbCmd, "p_order_num", DbType.Int64);
            db.AddInParameter(dbCmd, "p_db_status", DbType.Int32);
            db.AddInParameter(dbCmd, "p_db_last_updated_date", DbType.DateTime);
            db.AddInParameter(dbCmd, "p_db_last_updated_id", DbType.String);
            return dbCmd;
        }        

        protected override DbCommand CreateUpdateFileStatusCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_update_logic_file_state";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_logic_file_id", DbType.String);
            db.AddInParameter(dbCmd, "p_logic_file_status", DbType.Int32);
            return dbCmd;
        }

        

        protected override DbCommand CreateUpdatePhysicalFileLengthCommand()
        {
            const string ProcName = PACKAGE_NAME + "ty_fmp_update_phy_file_len";
            Database db = this.Database;
            DbCommand dbCmd = db.GetStoredProcCommand(ProcName);
            db.AddInParameter(dbCmd, "p_physical_file_id", DbType.String);
            db.AddInParameter(dbCmd, "p_file_length", DbType.Int64);
            db.AddInParameter(dbCmd, "p_db_last_updated_date", DbType.DateTime);
            db.AddInParameter(dbCmd, "p_db_last_updated_id", DbType.String);
            return dbCmd;
        }

        protected override EntityFactory CreateEntityFactory()
        {
            return new Topevery.Framework.Data.OraClient.OraEntityFactory();
        }
        #endregion

        #region Properties

        #endregion

    }
}
