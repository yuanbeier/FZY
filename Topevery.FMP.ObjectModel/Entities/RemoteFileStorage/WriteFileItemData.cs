using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class WriteFileItemData : ReadFileItemData
    {
        #region Fields
        //private long _length;
        private byte[] _writeData;
        #endregion

        #region Public
        

        public byte[] WriteData
        {
            get
            {
                return this._writeData;
            }
            set
            {
                this._writeData = value;
            }
        }

        //public long Length
        //{
        //    get
        //    {
        //        return this._length;
        //    }
        //    set
        //    {
        //        this._length = value;
        //    }
        //}
        #endregion
    }
}
