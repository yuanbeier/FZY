using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
#if WCF
using System.Runtime.Serialization;
#endif
using Topevery.FMP.ObjectModel.Exceptions;

namespace Topevery.FMP.ObjectModel
{
#if WCF
	[DataContract(Namespace = FMPUtility.NamespaceURI)]
#endif
    [Serializable]
    public class ExceptionResult : BaseSetReadOnly
    {
        #region Fields
        private int _errorCode;
        private string _errorMsg;
        private Exception _innerException;
        #endregion

        #region Methods
        
        #endregion

        #region Properties
#if WCF
		[DataMember()]
#endif
        public virtual int ErrorCode
        {
            get
            {
                return this._errorCode;
            }
            set
            {
                CheckReadOnly("ErrorCode");
                this._errorCode = value;
            }
        }

#if WCF
		[DataMember()]
#endif
        public virtual string ErrorMessage
        {
            get
            {
                return this._errorMsg;
            }
            set
            {
                CheckReadOnly("ErrorMessage");
                this._errorMsg = value;
            }
        }

        [XmlIgnore]
        public virtual Exception Exception
        {
            get
            {
                return this._innerException;
            }
            set
            {
                CheckReadOnly("Exception");
                this._innerException = value;
            }
        }
        
        #endregion
    }
}
