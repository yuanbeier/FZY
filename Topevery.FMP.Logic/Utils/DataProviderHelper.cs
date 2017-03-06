using System;
using System.Collections.Generic;
using System.Text;
using Topevery.FMP.Data;
using Topevery.FMP.ObjectModel;

namespace Topevery.FMP.Logic
{
    internal static class DataProviderHelper
    {
        #region Fields
        public const string RecordLockedName = "RecordLocked";
        public const string FileStorageProviderName = "FileStorageProvider";
        #endregion

        #region Methods
        public static IRecordLockedDataProvider GetRecordLockedProvider()
        {
            IRecordLockedDataProvider result = FMPDataProviderFactory.GetProvider(RecordLockedName) as IRecordLockedDataProvider;
            return result;
        }

        public static IRecordLockedDataProvider CreateRecordLockedProvider()
        {
            IRecordLockedDataProvider result = FMPDataProviderFactory.CreateDataProvider(RecordLockedName) as IRecordLockedDataProvider;
            return result;
        }


        public static IFileStorageProviderDataProvider GetFileStorageProvider()
        {
            IFileStorageProviderDataProvider result = FMPDataProviderFactory.GetProvider(FileStorageProviderName) as IFileStorageProviderDataProvider;
            return result;
        }

        public static IFileStorageProviderDataProvider CreateFileStorageProvider()
        {
            IFileStorageProviderDataProvider result = FMPDataProviderFactory.CreateDataProvider(FileStorageProviderName) as IFileStorageProviderDataProvider;
            return result;
        }      
        #endregion
    }
}
