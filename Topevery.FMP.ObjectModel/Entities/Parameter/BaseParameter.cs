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
    public abstract class BaseParameter : BaseSetReadOnly
    {
        #region Constructor
        protected BaseParameter()
        {
            
        }
        #endregion
    }
}
