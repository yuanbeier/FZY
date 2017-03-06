using System;
using System.Collections.Generic;
using System.Text;
using System.Data.Common;
using System.IO;
using Topevery.FMP.ObjectModel;
using Topevery.Framework.Data;
using Topevery.FMP.Data;

using FileAccess = Topevery.FMP.ObjectModel.FileAccess;
using FileMode = Topevery.FMP.ObjectModel.FileMode;
using System.Diagnostics;

namespace Topevery.FMP.Logic
{
    public class RemoteFileStorageService : ServiceBase, IRemoteFileStorage
    {
        #region Fields
        public static readonly Guid LogicFileFormID = new Guid("{383E3A77-5809-4295-BF13-8CBA16FA3FB1}");
        private IRecordLockedDataProvider _dataProvider;
        #endregion

        #region Constructor
        public RemoteFileStorageService()
        {
        }
        #endregion

        #region Methods
        #region IRemoteFileStorage Members

        public DeleteFileResult DeleteFile(DeleteFileParameter deleteFileParam)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public OpenFileResult OpenFile(OpenFileParameter openFileParam)
        {
            OpenFileResult result = new OpenFileResult();
            try
            {
                if (openFileParam != null)
                {
                    //Stopwatch w = new Stopwatch();
                    //w.Start();
                    OpenFileResultItemDataCollection items = result.ExecuteResult;
                    foreach (OpenFileItemData itemParam in openFileParam.InputData)
                    {
                        OpenFileResultItemData item = this.OpenFileItem(itemParam, openFileParam.CurrentUserID, null);
                        items.Add(item);
                    }
                    //w.Stop();
                    //Debug.WriteLine("OpenFile Executes times:" + w.ElapsedMilliseconds);
                    //DbTransaction dbTrans = this.DataProvider.CreateTransaction() as DbTransaction;
                    //if (dbTrans != null)
                    //{
                    //    try
                    //    {
                    //        foreach (OpenFileItemData itemParam in openFileParam.InputData)
                    //        {
                    //            OpenFileResultItemData item = this.OpenFileItem(itemParam, openFileParam.CurrentUserID, dbTrans);
                    //            items.Add(item);
                    //        }
                    //        dbTrans.Commit();
                    //    }
                    //    catch
                    //    {
                    //        dbTrans.Rollback();
                    //        throw;
                    //    }
                    //    finally
                    //    {
                    //        this.DataProvider.DisposeTransaction(dbTrans);
                    //    }
                    //}
                }
            }
            catch(Exception e)
            {
                Utils.BuilderExecuteResult(result, e);
            }
            return result;
        }

        public CloseFileResult CloseFile(CloseFileParameter closeFileParam)
        {
            CloseFileResult result = new CloseFileResult();
            try
            {
                if (closeFileParam != null)
                {
                    CloseFileResultItemDataCollection items = result.ExecuteResult;                   
                    foreach (CloseFileItemData itemParam in closeFileParam.InputData)
                    {
                        CloseFileResultItemData item = this.CloseFileItem(itemParam, closeFileParam.CurrentUserID, null);
                        items.Add(item);
                    }                            
                }
            }
            catch (Exception e)
            {
                Utils.BuilderExecuteResult(result, e);
            }
            return result;
        }

        public ReadFileResult ReadFile(ReadFileParameter readFileParam)
        {
            ReadFileResult result = new ReadFileResult();
            try
            {
                if (readFileParam != null)
                {
                    ReadFileResultItemDataCollection items = result.ExecuteResult;
                    foreach (ReadFileItemData itemParam in readFileParam.InputData)
                    {
                        ReadFileResultItemData item = this.ReadFileItem(itemParam, readFileParam.CurrentUserID, null);
                        items.Add(item);
                    }
                }
            }
            catch (Exception e)
            {
                Utils.BuilderExecuteResult(result, e);
            }
            return result;
        }

        public WriteFileResult WriteFile(WriteFileParameter writeFileParam)
        {
            WriteFileResult result = new WriteFileResult();
            try
            {
                if (writeFileParam != null)
                {
                    WriteFileResultItemDataCollection items = result.ExecuteResult;
                    foreach (WriteFileItemData itemParam in writeFileParam.InputData)
                    {
                        WriteFileResultItemData item = this.WriteFileItem(itemParam, writeFileParam.CurrentUserID, null);
                        items.Add(item);
                    }   
                }
            }
            catch (Exception e)
            {
                Utils.BuilderExecuteResult(result, e);
            }
            return result;
        }

        public FetchFileInfoResult FetchFileInfo(FetchFileInfoParameter fetchFileInfoParam)
        {
            FetchFileInfoResult result = new FetchFileInfoResult();
            try
            {
                LogicFileResultItemDataCollection items = result.ExecuteResult;
                foreach (LogicFileInfoItemData itemPara in fetchFileInfoParam.InputData)
                {
                    LogicFileInfoData info = this.DataProvider.FetchFileInfo(itemPara, null);
                    LogicFileResultItemData itemResult = new LogicFileResultItemData();
                    itemResult.LogicFileInfo = info;
                    items.Add(itemResult);
                }
            }
            catch (Exception e)
            {
                Utils.BuilderExecuteResult(result, e);
            }
            return result;
        }

        public UpdateFileInfoResult UpdateFileInfo(UpdateFileInfoParameter updateFileInfoParam)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region protected
        protected OpenFileResultItemData OpenFileItem(OpenFileItemData itemParam, Guid userID, DbTransaction trans)
        {
            OpenFileResultItemData item = new OpenFileResultItemData();
            LogicFileInfoData logicFileInfo = this.DataProvider.FetchFileInfo(CreateFetchItemParam(itemParam), trans);            
            item.LockedData = this.CreateLockData(itemParam,ref logicFileInfo, userID,trans);
            item.LogicFileInfo = logicFileInfo;
            return item;
        }

        protected CloseFileResultItemData CloseFileItem(CloseFileItemData itemParam, Guid userID, DbTransaction trans)
        {
            CloseFileResultItemData item = new CloseFileResultItemData();
            Guid lockID = Guid.Empty;
            if (itemParam.LockID != Guid.Empty)
            {
                lockID = itemParam.LockID;
            }
            else if (itemParam.FileID != Guid.Empty)
            {
                lockID = GetLockID(itemParam.FileID);
            }
            if (lockID != Guid.Empty)
            {
                RecordLockedManager.ServiceProxy.SetDataProvider(RecordLockedDataProvider);
                RecordLockedManager.Unlock(lockID);
                //item.Succeed = true;
            }
            return item;
        }

        protected ReadFileResultItemData ReadFileItem(ReadFileItemData itemParam, Guid userID, DbTransaction trans)
        {
            ReadFileResultItemData item = new ReadFileResultItemData();
            IRecordLockedDataProvider dataProvider = RecordLockedDataProvider;
            if (dataProvider != null)
            {
                RecordLockedManager.ServiceProxy.SetDataProvider(dataProvider);
            }
            if (itemParam.LockID != Guid.Empty)
            {
                RecordLockedManager.UpdateLockInfo(itemParam.LockID, DateTime.UtcNow.Add(RecordLocked.DefaultExpireTimeSpan));
            }
            string extendsionName = string.Empty;
            if (!string.IsNullOrEmpty(itemParam.ClientFileName))
            {
                extendsionName = Path.GetExtension(itemParam.ClientFileName);
            }

            item.ReadData = FileStorageManager.ReadFile(this.DataProvider, itemParam.PhysicalFileID, extendsionName, itemParam.Position, itemParam.ReadCount);
            return item;
        }


        protected WriteFileResultItemData WriteFileItem(WriteFileItemData itemParam, Guid userID, DbTransaction trans)
        {
            WriteFileResultItemData item = new WriteFileResultItemData();
            RecordLockedManager.ServiceProxy.SetDataProvider(RecordLockedDataProvider);
            RecordLockedManager.UpdateLockInfo(itemParam.LockID, DateTime.UtcNow.Add(RecordLocked.DefaultExpireTimeSpan));
            string extendsionName = string.Empty;
            if (!string.IsNullOrEmpty(itemParam.ClientFileName))
            {
                extendsionName = Path.GetExtension(itemParam.ClientFileName);
            }

            long length = FileStorageManager.WriteFile(this.DataProvider, itemParam.PhysicalFileID, extendsionName, itemParam.Position, itemParam.WriteData);
            this.DataProvider.UpdatePhysicalFileLength(itemParam.PhysicalFileID, length, DateTime.UtcNow, userID, trans);
            return item;
        }

        protected override IDataProvider CreateDataProviderCore()
        {
            return DataProviderHelper.GetFileStorageProvider();
        }

        protected Guid GetLockID(Guid fileID)
        {
            if (fileID != Guid.Empty)
            {
                RecordLockedFetchParameter fetchParam = new RecordLockedFetchParameter();
                fetchParam.FormID = LogicFileFormID;
                fetchParam.FormUniqueID = fileID;

                RecordLockedManager.ServiceProxy.SetDataProvider(RecordLockedDataProvider);
                RecordLockedDataCollection lockResult = RecordLockedManager.GetRecordLockedInfo(fetchParam);
                if (lockResult != null && lockResult.Count > 0)
                {
                    return lockResult[0].ID;
                }
            }
            return Guid.Empty;
        }
        #endregion

        #region Private
        private LogicFileInfoItemData CreateFetchItemParam(OpenFileItemData itemParam)
        {
            LogicFileInfoItemData result = new LogicFileInfoItemData();
            result.ID = itemParam.FileID;
            result.Version = itemParam.Version;
            result.WithPhysicalFileInfo = true;
            return result;
        }

        private RecordLockedData CreateLockData(OpenFileItemData itemParam, ref LogicFileInfoData logicFileInfo, Guid userID, DbTransaction trans)
        {
            RecordLockedData result = null;
            FileAccess access = itemParam.FileAccess;
            FileMode mode = itemParam.FileMode;
            long fileLength = -1;
            if (mode == FileMode.Truncate)
            {
                access |= FileAccess.Write;
                fileLength = 0;
            }
            bool canWrite = (access & FileAccess.Write) != 0;
            
            if (canWrite)
            {
                if (logicFileInfo == null)
                {
                    logicFileInfo = CreateNewFile(itemParam,userID, trans);
                }
                else
                {
                    logicFileInfo = UpdateFile(itemParam, logicFileInfo, fileLength, userID, trans);
                }
                if (logicFileInfo != null)
                {
                    result = Lock(logicFileInfo.ID, userID);                    
                }
            }
            
            return result;
        }

        private LogicFileInfoData UpdateFile(OpenFileItemData itemParam, LogicFileInfoData logicFileInfo, long fileLength, Guid userID, DbTransaction trans)
        {
            LogicFileInfoData result = logicFileInfo;
            UpdateMode updateMode = itemParam.UpdateMode;
            FileMode mode = itemParam.FileMode;
            long length = 0;
            if (updateMode == UpdateMode.None)
            {
                updateMode = UpdateMode.Override;
            }
            if (updateMode == UpdateMode.NewVersion)
            {
                PhysicalFileInfoData oldFileData = logicFileInfo.PhysicalFileInfos[logicFileInfo.PhysicalFileInfos.Count - 1];
                Guid physcicalFileID = CombineGuid.NewComboGuid();     
                switch (mode)
                {
                    case FileMode.Truncate:
                    case FileMode.Create:
                        FileStorageManager.SetFileLength(this.DataProvider, physcicalFileID, result.LogicFileExt, 0);
                        break;
                    default:
                        length = FileStorageManager.CopyFile(this.DataProvider, oldFileData.ID, physcicalFileID, logicFileInfo.LogicFileExt);
                        break;
                }
                PhysicalFileInfoData fileData = new PhysicalFileInfoData();
                fileData.ID = physcicalFileID;
                fileData.LogicFileID = result.ID;
                fileData.FileLength = length;
                fileData.Version = GetFileVersion(logicFileInfo, trans);
                fileData.StoreModeID = FileStorageManager.GetCurrentStoreModeID(this.DataProvider);
                this.DataProvider.CreatePhysicalFileInfo(fileData, trans);
                result.PhysicalFileInfos.Add(fileData);
                result.LatestPhysicalFileID = physcicalFileID;
                this.DataProvider.UpdateFileInfo(logicFileInfo, trans);
            }
            else if (updateMode == UpdateMode.Override)
            {
                switch (mode)
                {
                    case FileMode.Truncate:
                    case FileMode.Create:
                        FileStorageManager.SetFileLength(this.DataProvider, logicFileInfo.LatestPhysicalFileID, result.LogicFileExt, 0);
                        break;
                }
            }
            return result;
        }

        private LogicFileInfoData CreateNewFile(OpenFileItemData itemParam, Guid userID, DbTransaction trans)
        {
            LogicFileInfoData result = new LogicFileInfoData();
            Guid physcicalFileID = CombineGuid.NewComboGuid();
            if (itemParam.FileID != Guid.Empty)
            {
                result.ID = itemParam.FileID;
            }
            else
            {
                result.ID = CombineGuid.NewComboGuid();
            }
            if (!string.IsNullOrEmpty(itemParam.ClientFileName))
            {
                result.LogicFileName = Path.GetFileName(itemParam.ClientFileName);
                result.LogicFileExt = Path.GetExtension(itemParam.ClientFileName);
            }
            result.LogicFileStatus = LogicFileStatus.Normal;
            result.IsReadOnly= false;
            result.LatestPhysicalFileID = physcicalFileID;
            result.CurrentUserID = userID;
            this.DataProvider.CreateFileInfo(result, trans);

            PhysicalFileInfoData fileData = new PhysicalFileInfoData();
            fileData.ID = physcicalFileID;
            fileData.LogicFileID = result.ID;
            fileData.FileLength = 0;
            fileData.Version = 1;
            fileData.StoreModeID = FileStorageManager.GetCurrentStoreModeID(this.DataProvider);
            this.DataProvider.CreatePhysicalFileInfo(fileData, trans);
            result.PhysicalFileInfos.Add(fileData);
            FileStorageManager.CreateNewFile(this.DataProvider, physcicalFileID, result.LogicFileExt);
            return result;
        }

        private RecordLockedData Lock(Guid fileID, Guid userID)
        {
            RecordLockedData result = null;
            RecordLockedData data = new RecordLockedData();
            data.ID = CombineGuid.NewComboGuid();
            data.FormID = LogicFileFormID;
            data.FormUniqueID = fileID;
            data.LockUserID = userID;

            RecordLockedManager.ServiceProxy.SetDataProvider(RecordLockedDataProvider);
            result = RecordLockedManager.Lock(data);
            return result;
        }

        private int GetFileVersion(LogicFileInfoData logicFileInfo, DbTransaction trans)
        {
            int result = 1;
            int count = logicFileInfo.PhysicalFileInfos.Count;
            if (count > 0)
            {
                PhysicalFileInfoData fileData = logicFileInfo.PhysicalFileInfos[count - 1];
                if (fileData.Version != count )
                {
                    LogicFileInfoItemData itemData = new LogicFileInfoItemData();
                    itemData.ID = logicFileInfo.ID;
                    LogicFileInfoData newLogicFileInfo = this.DataProvider.FetchFileInfo(itemData, trans);
                    if (newLogicFileInfo != null)
                    {
                        count = newLogicFileInfo.PhysicalFileInfos.Count;
                        fileData = newLogicFileInfo.PhysicalFileInfos[count - 1];
                    }
                }
                if(fileData != null)
                {
                    result = fileData.Version + 1;
                }
            }
            return result;
        }
        #endregion

        #endregion

        #region Properties
        protected IFileStorageProviderDataProvider DataProvider
        {
            get
            {
                return this.InnerDataProvider as IFileStorageProviderDataProvider;
            }
        }
        
        protected virtual IRecordLockedDataProvider RecordLockedDataProvider
        {
            get
            {
                if (_dataProvider == null
                    && ConfigurationClientData != null
                    && ConfigurationGroupData != null
                    && !string.IsNullOrEmpty(ConfigurationGroupData.DataGroup))
                {
                    _dataProvider = DataProviderFactory.CreateProvider(
                        ConfigurationGroupData.DataGroup,
                        DataProviderHelper.RecordLockedName) as IRecordLockedDataProvider;
                }
                return _dataProvider;
            }
        }
        #endregion
    }
}
