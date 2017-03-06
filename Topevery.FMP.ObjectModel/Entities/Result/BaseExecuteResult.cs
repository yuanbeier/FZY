using System;
using System.Collections.Generic;
using System.Text;
#if WCF
using System.Runtime.Serialization;
#endif
using Topevery.Framework.Data;
using Topevery.FMP.ObjectModel.Exceptions;
using Topevery.Framework.Ioc;

namespace Topevery.FMP.ObjectModel
{
#if WCF
    [DataContract(Namespace = FMPUtility.NamespaceURI)]
#endif
    [Serializable]
    public abstract class BaseExecuteResult : BaseSetReadOnly
    {
        #region Fields
        
        private ExceptionResult _exception;
        #endregion

        #region Methods

        #endregion
        
        #region Properties
#if WCF
        [DataMember()]
#endif
        public ExceptionResult InnerException
        {
            get
            {
                if(_exception == null)
                {
                    _exception = new ExceptionResult();
                }
                return _exception;
            }
            set
            {
                CheckReadOnly("FetchData");
                this._exception = value;
            }
        }

        public virtual bool Succeed
        {
            get
            {
                return this.InnerException != null && this.InnerException.ErrorCode == 0;
            }
        }
        
        public bool Failed
        {
            get
            {
                return !this.Succeed;
            }
        }
        
        #endregion
    }

#if WCF
    [DataContract(Namespace = FMPUtility.NamespaceURI)]
#endif
    [Serializable]
    public abstract class BaseExecuteResult<TData> : BaseExecuteResult
    {
        #region Fields
        private TData _result;
        #endregion

        #region Methods
        protected internal override void SetReadOnly(bool readOnly)
        {
            base.SetReadOnly(readOnly);
            ISetReadOnly setReadOnlyObj = this.ExecuteResult as ISetReadOnly;
            if (setReadOnlyObj != null)
            {
                setReadOnlyObj.SetReadOnly(readOnly);
            }
        }

        protected virtual TData CreateExecuteResult()
        {
            return (TData)ActivatorEx.CreateInstance(typeof(TData));
        }
        #endregion

        #region Properties
#if WCF
        [DataMember()]
#endif
        public TData ExecuteResult
        {
            get
            {
                if(this._result == null)
                {
                    this._result = this.CreateExecuteResult();
                }
                return _result;
            }
            set
            {
                CheckReadOnly("ExecuteResult");
                this._result = value;
                
            }
        }
        #endregion
    }
}
