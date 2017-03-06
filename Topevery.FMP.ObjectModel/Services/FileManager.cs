using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel.Proxy;
using System.IO;

namespace Topevery.FMP.ObjectModel
{
    /// <summary>
    /// 文件操作的基础接口
    /// </summary>
    public static class FileManager
    {

        //public static DeleteFileResult DeleteFile(DeleteFileParameter deleteFileParam)
        //{
        //    throw new Exception("The method or operation is not implemented.");
        //}

        #region OpenFile
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="openFileItem"></param>
        /// <returns></returns>
        public static OpenFileResultItemData OpenFile(OpenFileItemData openFileItem)
        {
            return OpenFile(openFileItem, null);
        }

        public static OpenFileResultItemData OpenFile(OpenFileItemData openFileItem, IRemoteFileStorage proxy)
        {
            OpenFileResultItemData result = null;
            ManagerHelper.CheckNullReference(openFileItem, "openFileItem");
            OpenFileParameter param = new OpenFileParameter();
            param.InputData.Add(openFileItem);
            OpenFileResult itemResult = OpenFile(param, proxy);
            if (itemResult.RecordCount > 0)
            {
                result = itemResult.ExecuteResult[0];
            }
            return result;
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="openFileParam"></param>
        /// <returns></returns>
        public static OpenFileResult OpenFile(OpenFileParameter openFileParam)
        {
            return OpenFile(openFileParam, null);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="openFileParam"></param>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static　OpenFileResult OpenFile(OpenFileParameter openFileParam,IRemoteFileStorage proxy)
        {
            ManagerHelper.CheckNullReference(openFileParam, "openFileParam");
            if (proxy == null)
            {
                proxy = ServiceProxy;
            }
            OpenFileResult result = proxy.OpenFile(openFileParam);
            ManagerHelper.CheckUpdateResult(result);
            return result;
        }
        #endregion

        #region CloseFile
        public static bool CloseFileByLockID(Guid lockID)
        {
            return CloseFileByLockID(lockID, null);
        }

        /// <summary>
        /// 关闭文件
        /// </summary>
        /// <param name="lockID"></param>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static bool CloseFileByLockID(Guid lockID, IRemoteFileStorage proxy)
        {
            if (lockID != Guid.Empty)
            {
                CloseFileItemData item = new CloseFileItemData();
                item.LockID = lockID;
                CloseFileResultItemData itemResult = CloseFile(item, proxy);
                if (itemResult != null)
                {
                    return itemResult.Succeed;
                }
            }
            return false;
        }

        /// <summary>
        /// 关闭文件
        /// </summary>
        /// <param name="closeFileItem"></param>
        /// <returns></returns>
        public static CloseFileResultItemData CloseFile(CloseFileItemData closeFileItem)
        {
            return CloseFile(closeFileItem, null);
        }

        /// <summary>
        /// 关闭文件
        /// </summary>
        /// <param name="closeFileItem"></param>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static CloseFileResultItemData CloseFile(CloseFileItemData closeFileItem, IRemoteFileStorage proxy)
        {
            CloseFileResultItemData result = null;
            ManagerHelper.CheckNullReference(closeFileItem, "closeFileItem");
            CloseFileParameter param = new CloseFileParameter();
            param.InputData.Add(closeFileItem);
            CloseFileResult itemResult = CloseFile(param, proxy);
            if (itemResult.RecordCount > 0)
            {
                result = itemResult.ExecuteResult[0];
            }
            return result;
        }

        /// <summary>
        /// 关闭文件
        /// </summary>
        /// <param name="closeFileParam"></param>
        /// <returns></returns>
        public static CloseFileResult CloseFile(CloseFileParameter closeFileParam)
        {
            return CloseFile(closeFileParam, null);
        }

        /// <summary>
        /// 关闭文件
        /// </summary>
        /// <param name="closeFileParam"></param>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static CloseFileResult CloseFile(CloseFileParameter closeFileParam,IRemoteFileStorage proxy)
        {
            ManagerHelper.CheckNullReference(closeFileParam, "closeFileParam");
            if (proxy == null)
            {
                proxy = ServiceProxy;
            }
            CloseFileResult result = proxy.CloseFile(closeFileParam);
            ManagerHelper.CheckUpdateResult(result);
            return result;
        }
        #endregion

        #region ReadFile
        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileItem"></param>
        /// <returns></returns>
        public static ReadFileResultItemData ReadFile(ReadFileItemData fileItem)
        {
            return ReadFile(fileItem, null);
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="fileItem"></param>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static ReadFileResultItemData ReadFile(ReadFileItemData fileItem, IRemoteFileStorage proxy)
        {
            ReadFileResultItemData result = null;
            ManagerHelper.CheckNullReference(fileItem, "fileItem");
            if (fileItem.ReadCount > 0)
            {
                ReadFileParameter param = new ReadFileParameter();
                param.InputData.Add(fileItem);
                ReadFileResult itemResult = ReadFile(param, proxy);
                if (itemResult.RecordCount > 0)
                {
                    result = itemResult.ExecuteResult[0];
                }
            }
            return result;
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="readFileParam"></param>
        /// <returns></returns>
        public static ReadFileResult ReadFile(ReadFileParameter readFileParam)
        {
            return ReadFile(readFileParam, null);
        }

        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="readFileParam"></param>
        /// <returns></returns>
        public static ReadFileResult ReadFile(ReadFileParameter readFileParam, IRemoteFileStorage proxy)
        {
            ManagerHelper.CheckNullReference(readFileParam, "readFileParam");
            if (proxy == null)
            {
                proxy = ServiceProxy;
            }
            ReadFileResult result = proxy.ReadFile(readFileParam);
            ManagerHelper.CheckUpdateResult(result);
            return result;
        }
        #endregion

        #region WriteFile
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="fileItem"></param>
        /// <returns></returns>
        public static WriteFileResultItemData WriteFile(WriteFileItemData fileItem)
        {
            return WriteFile(fileItem, null);
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="fileItem"></param>
        /// <param name="proxy"></param>
        /// <returns></returns>
        public static WriteFileResultItemData WriteFile(WriteFileItemData fileItem, IRemoteFileStorage proxy)        
        {
            WriteFileResultItemData result = null;
            ManagerHelper.CheckNullReference(fileItem, "fileItem");
            WriteFileParameter param = new WriteFileParameter();
            param.InputData.Add(fileItem);
            WriteFileResult itemResult = WriteFile(param, proxy);
            if (itemResult.RecordCount > 0)
            {
                result = itemResult.ExecuteResult[0];
            }
            return result;
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="writeFileParam"></param>
        /// <returns></returns>
        public static WriteFileResult WriteFile(WriteFileParameter writeFileParam)
        {
            return WriteFile(writeFileParam, null);
        }

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="writeFileParam"></param>
        /// <returns></returns>
        public static WriteFileResult WriteFile(WriteFileParameter writeFileParam, IRemoteFileStorage proxy)
        {
            ManagerHelper.CheckNullReference(writeFileParam,"writeFileParam");
            if (proxy == null)
            {
                proxy = ServiceProxy;
            }
            WriteFileResult result = proxy.WriteFile(writeFileParam);
            ManagerHelper.CheckUpdateResult(result);
            return result;
        }
        #endregion

        #region Exsits File
        /// <summary>
        /// 判断是否存在对应的文件
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <returns></returns>
        public static bool FileExists(Guid logicFileID)
        {
            return FileExists(logicFileID, 0, null);
        }

        /// <summary>
        /// 判断是否存在对应的文件
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <returns></returns>
        public static bool FileExists(Guid logicFileID, IRemoteFileStorage proxy)
        {
            return FileExists(logicFileID, 0, proxy);
        }

        /// <summary>
        /// 判断是否存在对应的文件。
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool FileExists(Guid logicFileID, int version)
        {
            return FileExists(logicFileID, version, null);
        }

        /// <summary>
        /// 判断是否存在对应的文件。
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static bool FileExists(Guid logicFileID, int version, IRemoteFileStorage proxy)
        {
            LogicFileInfoData logicFileInfo = GetFileInfo(logicFileID, version, proxy);
            return logicFileInfo != null;
        }


        #endregion

        #region FetchFileInfo
        /// <summary>
        /// 检索文件信息
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <returns></returns>
        public static LogicFileInfoData GetFileInfo(Guid logicFileID)
        {
            return GetFileInfo(logicFileID, null);
        }

        /// <summary>
        /// 检索文件信息
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <returns></returns>
        public static LogicFileInfoData GetFileInfo(Guid logicFileID, IRemoteFileStorage proxy)
        {
            return GetFileInfo(logicFileID, 0, proxy);
        }

        /// <summary>
        /// 检索文件信息
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static LogicFileInfoData GetFileInfo(Guid logicFileID, int version)
        {
            return GetFileInfo(logicFileID, version, null);
        }

        /// <summary>
        /// 检索文件信息
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="version"></param>
        /// <returns></returns>
        public static LogicFileInfoData GetFileInfo(Guid logicFileID, int version, IRemoteFileStorage proxy)
        {
            LogicFileInfoItemData item = new LogicFileInfoItemData();
            item.ID = logicFileID;
            item.Version = version;
            return GetFileInfo(item, proxy);
        }

        /// <summary>
        /// 检索文件信息
        /// </summary>
        /// <param name="logicFileInfo"></param>
        /// <returns></returns>
        public static LogicFileInfoData GetFileInfo(LogicFileInfoItemData logicFileInfo)
        {
            return GetFileInfo(logicFileInfo, null);
        }

        /// <summary>
        /// 检索文件信息
        /// </summary>
        /// <param name="logicFileInfo"></param>
        /// <returns></returns>
        public static LogicFileInfoData GetFileInfo(LogicFileInfoItemData logicFileInfo, IRemoteFileStorage proxy)
        {
            ManagerHelper.CheckNullReference(logicFileInfo, "logicFileInfo");
            FetchFileInfoParameter fetchParam = new FetchFileInfoParameter();
            fetchParam.InputData.Add(logicFileInfo);
            FetchFileInfoResult result = GetFileInfo(fetchParam, proxy);
            if (result.RecordCount > 0)
            {
                LogicFileResultItemData item = result.ExecuteResult[0];
                if(!item.LogicFileID.Equals(Guid.Empty))
                {
                    return item.LogicFileInfo;
                }
            }
            return null;
        }

        /// <summary>
        /// 检索文件信息
        /// </summary>
        /// <param name="fetchFileInfoParam"></param>
        /// <returns></returns>
        public static FetchFileInfoResult GetFileInfo(FetchFileInfoParameter fetchFileInfoParam)
        {
            return GetFileInfo(fetchFileInfoParam, null);
        }

        /// <summary>
        /// 检索文件信息
        /// </summary>
        /// <param name="fetchFileInfoParam"></param>
        /// <returns></returns>
        public static FetchFileInfoResult GetFileInfo(FetchFileInfoParameter fetchFileInfoParam, IRemoteFileStorage proxy)
        {
            ManagerHelper.CheckNullReference(fetchFileInfoParam, "fetchFileInfo");
            if (proxy == null)
            {
                proxy = ServiceProxy;
            }
            FetchFileInfoResult result = proxy.FetchFileInfo(fetchFileInfoParam);
            ManagerHelper.CheckFetchResult(result);
            return result;
        }
        #endregion


        #region File, RemoteStream Helper Function

        #region CreateFile
        /// <summary>
        /// 创建文件流
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="clientFileName"></param>
        /// <returns></returns>
        public static Stream CreateFile(Guid logicFileID, string clientFileName)
        {
            return CreateFile(null, logicFileID, clientFileName);
        }

        /// <summary>
        /// 创建文件流
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="clientFileName"></param>
        /// <returns></returns>
        public static Stream CreateFile(string serviceGroup, Guid logicFileID, string clientFileName)
        {
            return CreateFile(serviceGroup, logicFileID, UpdateMode.None, clientFileName);
        }

        /// <summary>
        /// 创建文件流
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="updateMode"></param>
        /// <param name="clientFileName"></param>
        /// <returns></returns>
        public static Stream CreateFile(Guid logicFileID, UpdateMode updateMode, string clientFileName)
        {
            return CreateFile(null, logicFileID, updateMode, clientFileName);
        }

        /// <summary>
        /// 创建文件流
        /// </summary>
        /// <param name="serviceGroup"></param>
        /// <param name="logicFileID"></param>
        /// <param name="updateMode"></param>
        /// <param name="clientFileName"></param>
        /// <returns></returns>
        public static Stream CreateFile(string serviceGroup, Guid logicFileID, UpdateMode updateMode, string clientFileName)
        {
            RemoteStreamContext context = new RemoteStreamContext();
            context.ServiceGroup = serviceGroup;
            context.FileID = logicFileID;
            context.UpdateMode = updateMode;
            context.FileAccess = FileAccess.ReadWrite;
            context.FileMode = FileMode.Create;
            context.ClientFileName = clientFileName;
            return new RemoteStream(context);
        }
        #endregion

        #region OpenFile
        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="serviceGroup"></param>
        /// <param name="logicFileID"></param>
        /// <param name="mode"></param>
        /// <param name="access"></param>
        /// <param name="updateMode"></param>
        /// <param name="clientFileName"></param>
        /// <returns></returns>
        public static Stream OpenFile(string serviceGroup, Guid logicFileID, FileMode mode, FileAccess access, UpdateMode updateMode,string clientFileName)
        {
            RemoteStreamContext context = new RemoteStreamContext();
            context.ServiceGroup = serviceGroup;
            context.FileID = logicFileID;
            context.FileMode = mode;
            context.FileAccess = access;
            context.UpdateMode = updateMode;
            context.ClientFileName = clientFileName;
            return new RemoteStream(context);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="mode"></param>
        /// <param name="access"></param>
        /// <param name="updateMode"></param>
        /// <param name="clientFileName"></param>
        /// <returns></returns>
        public static Stream OpenFile(Guid logicFileID, FileMode mode, FileAccess access, UpdateMode updateMode, string clientFileName)
        {
            return OpenFile(null, logicFileID, mode, access, updateMode,clientFileName);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="serviceGroup"></param>
        /// <param name="logicFileID"></param>
        /// <param name="mode"></param>
        /// <param name="updateMode"></param>
        /// <param name="clientFileName"></param>
        /// <returns></returns>
        public static Stream OpenFile(string serviceGroup, Guid logicFileID, FileMode mode, UpdateMode updateMode, string clientFileName)
        {
            return OpenFile(serviceGroup, logicFileID, mode, (mode != FileMode.Append) ? FileAccess.ReadWrite : FileAccess.Write, updateMode, clientFileName);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="mode"></param>
        /// <param name="updateMode"></param>
        /// <param name="clientFileName"></param>
        /// <returns></returns>
        public static Stream OpenFile(Guid logicFileID, FileMode mode, UpdateMode updateMode, string clientFileName)
        {
            return OpenFile(logicFileID, mode, (mode != FileMode.Append) ? FileAccess.ReadWrite : FileAccess.Write, updateMode,clientFileName);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="mode"></param>
        /// <param name="clientFileName"></param>
        /// <returns></returns>
        public static Stream OpenFile(string serviceGroup, Guid logicFileID, FileMode mode, string clientFileName)
        {
            return OpenFile(serviceGroup, logicFileID, mode, UpdateMode.None, clientFileName);
        }

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="logicFileID"></param>
        /// <param name="mode"></param>
        /// <param name="clientFileName"></param>
        /// <returns></returns>
        public static Stream OpenFile(Guid logicFileID, FileMode mode,  string clientFileName)
        {
            return OpenFile(logicFileID, mode, (mode != FileMode.Append) ? FileAccess.ReadWrite : FileAccess.Write, UpdateMode.None, clientFileName);
        }
        #endregion

        #region OpenReadFile
        public static Stream OpenReadFile(Guid logicFileID)
        {
            return OpenReadFile(null,logicFileID);
        }

        public static Stream OpenReadFile(string serverGroup, Guid logicFileID)
        {
            RemoteStreamContext context = new RemoteStreamContext();
            context.ServiceGroup = serverGroup;
            context.FileID = logicFileID;
            context.FileMode = FileMode.Open;
            context.FileAccess = FileAccess.Read;
            return new RemoteStream(context);
        }
        #endregion

        #region OpenWriteFile
        public static Stream OpenWriteFile(Guid logicFileID)
        {
            return OpenWriteFile(logicFileID, UpdateMode.None, null);
        }

        public static Stream OpenWriteFile(Guid logicFileID, string clientFileName)
        {
            return OpenWriteFile(logicFileID, UpdateMode.None, clientFileName);
        }

        public static Stream OpenWriteFile(Guid logicFileID, UpdateMode updateMode, string clientFileName)
        {
            return OpenWriteFile(null, logicFileID, updateMode, clientFileName);
        }

        public static Stream OpenWriteFile(string serverGroup, Guid logicFileID, UpdateMode updateMode, string clientFileName)
        {
            RemoteStreamContext context = new RemoteStreamContext();
            context.ServiceGroup = serverGroup;
            context.FileID = logicFileID;
            context.FileMode = FileMode.OpenOrCreate;
            context.FileAccess = FileAccess.ReadWrite;
            context.UpdateMode = updateMode;
            context.ClientFileName = clientFileName;
            return new RemoteStream(context);
        }
        #endregion
        #endregion

        #region Properties
        private static IRemoteFileStorage ServiceProxy
        {
            get
            {
                return FMPServiceProxyProviderManager.GetRemoteFileStorage();
            }
        }
        #endregion
    }
}
