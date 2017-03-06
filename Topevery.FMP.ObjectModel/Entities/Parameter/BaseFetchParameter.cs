using System;
using System.Collections.Generic;
using System.Text;
#if WCF
using System.Runtime.Serialization;
#endif

namespace Topevery.FMP.ObjectModel
{
#if WCF
    [DataContract(Namespace = FMPUtility.NamespaceURI)]
#endif
    [Serializable]
    public abstract class BaseFetchParameter : BaseParameter
    {
        #region Fields
        public const int DefaultPageSize = -1;
        public const int DefaultPageIndex = 0;
        private int _pageSize = DefaultPageSize;
        private int _pageIndex = DefaultPageIndex;
        #endregion

        #region Constructor
        protected BaseFetchParameter()
        {
            
        }
        #endregion

        #region Properties
#if WCF
        [DataMember()]
#endif
        public virtual int PageSize
        {
            get
            {
                return this._pageSize;
            }
            set
            {
                this._pageSize = value;
            }
        }

#if WCF
        [DataMember()]
#endif
        public virtual int PageIndex
        {
            get
            {
                return this._pageIndex;
            }
            set
            {
                this._pageIndex = value;
            }
        }
        #endregion
    }

    
}
