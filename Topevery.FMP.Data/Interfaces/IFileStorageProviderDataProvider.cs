using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.ObjectModel;
using Topevery.Framework.Data;
using System.Data;

namespace Topevery.FMP.Data
{
    public interface IFileStorageProviderDataProvider : IDataProvider
    {
        StoreModeData SaveStoreMode(StoreModeData configData);
        StoreModeData GetStoreModeByID(Guid id);
        StoreModeData GetStoreModeByFileID(Guid fileID);


        LogicFileInfoData FetchFileInfo(LogicFileInfoItemData fetchParam, IDbTransaction trans);
        LogicFileInfoData CreateFileInfo(LogicFileInfoData logicFileInfo, IDbTransaction trans);
        LogicFileInfoData UpdateFileInfo(LogicFileInfoData updateFileInfo, IDbTransaction trans);
        void UpdateFileStatus(Guid fileID, LogicFileStatus status, IDbTransaction trans);

        PhysicalFileInfoData CreatePhysicalFileInfo(PhysicalFileInfoData physicalFileInfo, IDbTransaction trans);
        PhysicalFileInfoData UpdatePhyscialFileInfo(PhysicalFileInfoData physicalFileInfo, IDbTransaction trans);

        void UpdatePhysicalFileLength(Guid physicalFileID, long length, DateTime lastUpdateTime, Guid lastUpdateUserID, IDbTransaction trans);
    }
}
