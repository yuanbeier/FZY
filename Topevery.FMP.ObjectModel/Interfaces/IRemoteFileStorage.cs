using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    /// <summary>
    /// 远程文件操作接口
    /// </summary>
    public interface IRemoteFileStorage
    {
        ///// <summary>
        ///// 创建文件
        ///// </summary>
        ///// <param name="createFileRequest"></param>
        ///// <returns></returns>
        //CreateFileResult CreateFile(CreateFileParameter createFileParam);

        /// <summary>
        /// 删除文件
        /// </summary>
        /// <param name="deleteFileRequest"></param>
        /// <returns></returns>
        DeleteFileResult DeleteFile(DeleteFileParameter deleteFileParam);

        /// <summary>
        /// 打开文件
        /// </summary>
        /// <param name="openFileRequest"></param>
        /// <returns></returns>
        OpenFileResult OpenFile(OpenFileParameter openFileParam);

        /// <summary>
        /// 关闭文件
        /// </summary>
        /// <param name="closeFileRequest"></param>
        /// <returns></returns>
        CloseFileResult CloseFile(CloseFileParameter closeFileParam);


        /// <summary>
        /// 读取文件
        /// </summary>
        /// <param name="readFileRequest"></param>
        /// <returns></returns>
        ReadFileResult ReadFile(ReadFileParameter readFileRequest);

        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="writeFileRequest"></param>
        /// <returns></returns>
        WriteFileResult WriteFile(WriteFileParameter writeFileParam);

        /// <summary>
        /// 获取文件相关信息
        /// </summary>
        /// <param name="fetchFileInfoRequest"></param>
        /// <returns></returns>
        FetchFileInfoResult FetchFileInfo(FetchFileInfoParameter fetchFileInfoParam);

        /// <summary>
        /// 更新文件相关信息
        /// </summary>
        /// <param name="updateFileInfoRequest"></param>
        /// <returns></returns>
        UpdateFileInfoResult UpdateFileInfo(UpdateFileInfoParameter updateFileInfoParam);
    }
}
