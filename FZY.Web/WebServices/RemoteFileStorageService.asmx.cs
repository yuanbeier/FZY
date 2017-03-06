using System;
using System.Data;
using System.Web;
using System.Collections;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.ComponentModel;
using Topevery.Web.Services;
using Topevery.FMP.ObjectModel;

namespace Topevery.FMP.Web.WebServices
{
    [WebService(Namespace = Topevery.FMP.ObjectModel.FMPUtility.NamespaceURI)]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [ToolboxItem(false)]
    public class RemoteFileStorageService : Topevery.Web.Services.WebServiceBase, IRemoteFileStorage
    {
        #region Fields
        Topevery.FMP.Logic.RemoteFileStorageService provider = new Topevery.FMP.Logic.RemoteFileStorageService();
        #endregion

        #region Methods

        [WebMethod()]
        [SoapExtensionEx()]
        public Topevery.FMP.ObjectModel.DeleteFileResult DeleteFile(Topevery.FMP.ObjectModel.DeleteFileParameter deleteFileParam)
        {
            return provider.DeleteFile(deleteFileParam);
        }

        [WebMethod()]
        [SoapExtensionEx()]
        public Topevery.FMP.ObjectModel.OpenFileResult OpenFile(Topevery.FMP.ObjectModel.OpenFileParameter openFileParam)
        {
            return provider.OpenFile(openFileParam);
        }

        [WebMethod()]
        [SoapExtensionEx()]
        public Topevery.FMP.ObjectModel.CloseFileResult CloseFile(Topevery.FMP.ObjectModel.CloseFileParameter closeFileParam)
        {
            return provider.CloseFile(closeFileParam);
        }

        [WebMethod()]
        [SoapExtensionEx()]
        public Topevery.FMP.ObjectModel.ReadFileResult ReadFile(Topevery.FMP.ObjectModel.ReadFileParameter readFileRequest)
        {
            return provider.ReadFile(readFileRequest);
        }

        [WebMethod()]
        [SoapExtensionEx()]
        public Topevery.FMP.ObjectModel.WriteFileResult WriteFile(Topevery.FMP.ObjectModel.WriteFileParameter writeFileParam)
        {
            return provider.WriteFile(writeFileParam);
        }

        [WebMethod()]
        [SoapExtensionEx()]
        public Topevery.FMP.ObjectModel.FetchFileInfoResult FetchFileInfo(Topevery.FMP.ObjectModel.FetchFileInfoParameter fetchFileInfoParam)
        {
            return provider.FetchFileInfo(fetchFileInfoParam);
        }

        [WebMethod()]
        [SoapExtensionEx()]
        public Topevery.FMP.ObjectModel.UpdateFileInfoResult UpdateFileInfo(Topevery.FMP.ObjectModel.UpdateFileInfoParameter updateFileInfoParam)
        {
            return provider.UpdateFileInfo(updateFileInfoParam);
        }
        #endregion
    }
}