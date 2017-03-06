using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    /// <summary>
    /// Զ���ļ������ӿ�
    /// </summary>
    public interface IRemoteFileStorage
    {
        ///// <summary>
        ///// �����ļ�
        ///// </summary>
        ///// <param name="createFileRequest"></param>
        ///// <returns></returns>
        //CreateFileResult CreateFile(CreateFileParameter createFileParam);

        /// <summary>
        /// ɾ���ļ�
        /// </summary>
        /// <param name="deleteFileRequest"></param>
        /// <returns></returns>
        DeleteFileResult DeleteFile(DeleteFileParameter deleteFileParam);

        /// <summary>
        /// ���ļ�
        /// </summary>
        /// <param name="openFileRequest"></param>
        /// <returns></returns>
        OpenFileResult OpenFile(OpenFileParameter openFileParam);

        /// <summary>
        /// �ر��ļ�
        /// </summary>
        /// <param name="closeFileRequest"></param>
        /// <returns></returns>
        CloseFileResult CloseFile(CloseFileParameter closeFileParam);


        /// <summary>
        /// ��ȡ�ļ�
        /// </summary>
        /// <param name="readFileRequest"></param>
        /// <returns></returns>
        ReadFileResult ReadFile(ReadFileParameter readFileRequest);

        /// <summary>
        /// д���ļ�
        /// </summary>
        /// <param name="writeFileRequest"></param>
        /// <returns></returns>
        WriteFileResult WriteFile(WriteFileParameter writeFileParam);

        /// <summary>
        /// ��ȡ�ļ������Ϣ
        /// </summary>
        /// <param name="fetchFileInfoRequest"></param>
        /// <returns></returns>
        FetchFileInfoResult FetchFileInfo(FetchFileInfoParameter fetchFileInfoParam);

        /// <summary>
        /// �����ļ������Ϣ
        /// </summary>
        /// <param name="updateFileInfoRequest"></param>
        /// <returns></returns>
        UpdateFileInfoResult UpdateFileInfo(UpdateFileInfoParameter updateFileInfoParam);
    }
}
