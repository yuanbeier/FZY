using System;
using System.Collections.Generic;
using System.Text;
#if WCF
using System.Runtime.Serialization;
#endif
using Topevery.FMP.ObjectModel.Exceptions;
using System.Collections;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class BaseFetchResult<TData> : BaseExecuteResult<TData>
    {
        #region Fields
        private int _recordCount = 0;
       #endregion

        #region Methods
        
        #endregion
        
        #region Properties
#if WCF
        [DataMember()]
#endif
        public int RecordCount
        {
            get
            {
                if(this._recordCount <= 0)
                {
                    ICollection cols = this.ExecuteResult as ICollection;
                    if(cols != null)
                    {
                        this._recordCount = cols.Count;
                    }
                }
                return this._recordCount;
            }
            set
            {
                CheckReadOnly("RecordCount");
                this._recordCount = value;
            }
        }
        #endregion
    }
}
