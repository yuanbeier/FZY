using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class ReadFileResultItemData : BaseExecuteResult
    {
        #region Fields
        private byte[] _readData;
        #endregion

        #region Properties
        public int ReadDataLength
        {
            get
            {
                if (ReadData != null)
                {
                    return ReadData.Length;
                }
                return 0;
            }
        }

        public byte[] ReadData
        {
            get
            {
                return this._readData;
            }
            set
            {
                this._readData = value;
            }
        }
        #endregion
    }
}
