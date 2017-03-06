using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class ReadFileItemData : ReadWriteFileItemDataBase
    {
        #region Fields
        private int _readCount;
        #endregion

        #region Properties
        public int ReadCount
        {
            get
            {
                return this._readCount;
            }
            set
            {
                this._readCount = value;
            }
        }
        #endregion
    }
}
