using System;
using System.Collections.Generic;
using System.Text;
#if WCF
using System.Runtime.Serialization;
#endif
using Topevery.Framework.Data;
using Topevery.FMP.ObjectModel.Exceptions;

namespace Topevery.FMP.ObjectModel
{
#if WCF
	[DataContract(Namespace = FMPUtility.NamespaceURI)]
#endif
    [Serializable]
    public abstract class BaseSetReadOnly : ISetReadOnly
    {
        #region Fields
        private bool _readonly = false;
        #endregion

        #region Constructor
        protected BaseSetReadOnly()
        {
            
        }
        #endregion
        
        #region Methods
        protected virtual internal void SetReadOnly(bool readOnly)
        {
            this._readonly = readOnly;
        }

        protected virtual void CheckReadOnly(string propertyName)
        {
            if (ReadOnly)
            {
                __Error.CheckPropertyReadOnly(propertyName);
            }
        }

        void ISetReadOnly.SetReadOnly(bool readOnly)
        {
            this.SetReadOnly(readOnly);
        }
        #endregion

        #region Properties
#if WCF
		[DataMember()]
#endif
        public virtual bool ReadOnly
        {
            get
            {
                return this._readonly;
            }
        }
        #endregion
    }
}
