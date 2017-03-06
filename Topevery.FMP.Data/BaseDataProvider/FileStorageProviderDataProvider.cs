using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel;
using System.Data;
using System.Data.Common;
using Microsoft.Practices.EnterpriseLibrary.Data;
using System.Collections;

namespace Topevery.FMP.Data
{
    public abstract class FileStorageProviderDataProvider : BaseMetaDataProvider, IFileStorageProviderDataProvider
    {
        #region Fields

        private DbCommand _fetchFileInfoCommand;
        private DbCommand _createFileInfoCommand;
        private DbCommand _updateFileInfoCommand;
        private DbCommand _updateFileStatusCommand;
        private DbCommand _createPhysicalFileInfoCommand;
        private DbCommand _updatePhysicalFileInfoCommand;
        private DbCommand _updatePhysicalFileLengthCommand;
        #endregion

        #region Methods
        #region IFileStorageProviderDataProvider Members

        public abstract StoreModeData SaveStoreMode(StoreModeData configData);

        public abstract StoreModeData GetStoreModeByID(Guid id);

        public abstract StoreModeData GetStoreModeByFileID(Guid fileID);

        public abstract LogicFileInfoData FetchFileInfo(LogicFileInfoItemData fetchParam, DbTransaction trans);

        public abstract LogicFileInfoData CreateFileInfo(LogicFileInfoData logicFileInfo, DbTransaction trans);

        public abstract LogicFileInfoData UpdateFileInfo(LogicFileInfoData updateFileInfo, DbTransaction trans);

        public abstract PhysicalFileInfoData CreatePhysicalFileInfo(PhysicalFileInfoData physicalFileInfo, DbTransaction trans);

        public abstract PhysicalFileInfoData UpdatePhyscialFileInfo(PhysicalFileInfoData physicalFileInfo, DbTransaction trans);

        public abstract void UpdateFileStatus(Guid id, LogicFileStatus status, DbTransaction trans);

        public abstract void UpdatePhysicalFileLength(Guid physicalFileID, long length, DateTime lastUpdateTime, Guid lastUpdateUserID, DbTransaction trans);
        #endregion



        #region IFileStorageProviderDataProvider Members


        LogicFileInfoData IFileStorageProviderDataProvider.FetchFileInfo(LogicFileInfoItemData fetchParam, IDbTransaction trans)
        {
            return this.FetchFileInfo(fetchParam, trans as DbTransaction);
        }

        LogicFileInfoData IFileStorageProviderDataProvider.CreateFileInfo(LogicFileInfoData logicFileInfo, IDbTransaction trans)
        {
            return this.CreateFileInfo(logicFileInfo, trans as DbTransaction);
        }

        LogicFileInfoData IFileStorageProviderDataProvider.UpdateFileInfo(LogicFileInfoData updateFileInfo, IDbTransaction trans)
        {
            return this.UpdateFileInfo(updateFileInfo, trans as DbTransaction);
        }

        PhysicalFileInfoData IFileStorageProviderDataProvider.CreatePhysicalFileInfo(PhysicalFileInfoData physicalFileInfo, IDbTransaction trans)
        {
            return this.CreatePhysicalFileInfo(physicalFileInfo, trans as DbTransaction);
        }

        PhysicalFileInfoData IFileStorageProviderDataProvider.UpdatePhyscialFileInfo(PhysicalFileInfoData physicalFileInfo, IDbTransaction trans)
        {
            return this.UpdatePhyscialFileInfo(physicalFileInfo, trans as DbTransaction);
        }

        
        void IFileStorageProviderDataProvider.UpdateFileStatus(Guid id, LogicFileStatus status, IDbTransaction trans)
        {
            this.UpdateFileStatus(id, status, trans as DbTransaction);
        }

        void IFileStorageProviderDataProvider.UpdatePhysicalFileLength(Guid physicalFileID, long length, DateTime lastUpdateTime, Guid lastUpdateUserID, IDbTransaction trans)
        {
            this.UpdatePhysicalFileLength(physicalFileID, length, lastUpdateTime, lastUpdateUserID, trans as DbTransaction);
        }
        #endregion

        #region Create Command
        protected abstract DbCommand CreateFetchFileInfoCommand();
        protected abstract DbCommand CreateCreateFileInfoCommand();
        protected abstract DbCommand CreateUpdateFileInfoCommand();
        protected abstract DbCommand CreateUpdateFileStatusCommand();
        protected abstract DbCommand CreateCreatePhysicalFileInfoCommand();
        protected abstract DbCommand CreateUpdatePhysicalFileInfoCommand();
        protected abstract DbCommand CreateUpdatePhysicalFileLengthCommand();
        #endregion

        #endregion


        #region Properties
        protected virtual DbCommand FetchFileInfoCommand
        {
            get
            {
                if (_fetchFileInfoCommand == null)
                {
                    _fetchFileInfoCommand = this.CreateFetchFileInfoCommand();
                }
                return _fetchFileInfoCommand;
            }
        }
        protected virtual DbCommand CreateFileInfoCommand
        {
            get
            {
                if (_createFileInfoCommand == null)
                {
                    _createFileInfoCommand = this.CreateCreateFileInfoCommand();
                }
                return _createFileInfoCommand;
            }
        }
        protected virtual DbCommand UpdateFileInfoCommand
        {
            get
            {
                if (_updateFileInfoCommand == null)
                {
                    _updateFileInfoCommand = this.CreateUpdateFileInfoCommand();
                }
                return _updateFileInfoCommand;
            }
        }

        protected virtual DbCommand UpdateFileStatusCommand
        {
            get
            {
                if (_updateFileStatusCommand == null)
                {
                    _updateFileStatusCommand = this.CreateUpdateFileStatusCommand();
                }
                return _updateFileStatusCommand;
            }
        }

        protected virtual DbCommand CreatePhysicalFileInfoCommand
        {
            get
            {
                if (_createPhysicalFileInfoCommand == null)
                {
                    _createPhysicalFileInfoCommand = CreateCreatePhysicalFileInfoCommand();
                }
                return _createPhysicalFileInfoCommand;
            }

        }
        protected virtual DbCommand UpdatePhysicalFileInfoCommand
        {
            get
            {
                if (_updatePhysicalFileInfoCommand == null)
                {
                    _updatePhysicalFileInfoCommand = CreateUpdatePhysicalFileInfoCommand();
                }
                return _updatePhysicalFileInfoCommand;
            }
        }

        protected virtual DbCommand UpdatePhysicalFileLengthCommand
        {
            get
            {
                if (_updatePhysicalFileLengthCommand == null)
                {
                    _updatePhysicalFileLengthCommand = CreateUpdatePhysicalFileLengthCommand();
                }
                return _updatePhysicalFileLengthCommand;
            }
        }
        #endregion

        #region Properties

        #endregion

    }
}
