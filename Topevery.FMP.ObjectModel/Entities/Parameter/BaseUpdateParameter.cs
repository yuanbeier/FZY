using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.Ioc;
using System.Collections;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public abstract class BaseUpdateParameter : BaseParameter
    {
        #region Fields
        private  bool _returnValue;
        private Guid _currentUserID;
        #endregion

        #region Constructor
        protected BaseUpdateParameter(bool returnValue)
        {
            _returnValue = returnValue;
        }
        #endregion

        #region Properties
        public bool ReturnValue
        {
            get
            {
                return _returnValue;
            }
            set
            {
                this._returnValue = value;
            }
        }

        public Guid CurrentUserID
        {
            get
            {
                return this._currentUserID;
            }
            set
            {
                this._currentUserID = value;
            }
        }
        #endregion
    }

    [Serializable]
    public abstract class BaseUpdateObjectParameter<TData> : BaseUpdateParameter
    {
        #region Fields
        private TData _inputData;
        #endregion 

        #region Constructor
        protected BaseUpdateObjectParameter()
            : this(false)
        {

        }
        protected BaseUpdateObjectParameter(bool returnValue) : base(returnValue)
        {
            
        }
        #endregion

       

        #region Properties
        public TData InputData
        {
            get
            {
                return this._inputData;
            }
            set
            {
                this._inputData = value;
            }
        }
        #endregion
    }

    [Serializable]
    public abstract class BaseUpdateParameter<TCollection> : BaseUpdateParameter
    {
        #region Fields
        private TCollection _items;
        #endregion

        #region Constructor
        protected BaseUpdateParameter() : base(false)
        {
            
        }

        protected BaseUpdateParameter(bool returnValue)
            : base(returnValue)
        {

        }

        
        #endregion

        #region Methods
        protected virtual TCollection CreateInputData()
        {
            return (TCollection)ActivatorEx.CreateInstance(typeof(TCollection));
        }
        #endregion

        #region Properties


        public virtual TCollection InputData
        {
            get
            {
                if(!IsInputDataCreated)
                {
                    _items = this.CreateInputData();
                }
                return _items;
            }
        }

        public bool IsInputDataCreated
        {
            get
            {
                return _items != null;
            }
        }
        #endregion
    }
}
