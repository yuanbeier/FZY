using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.Data;

namespace Topevery.FMP.Data
{
    public abstract class BaseDataProvider : DataProvider
    {
        #region Fields
        private EntityFactory _factory;
        #endregion

        #region Constructor
        protected BaseDataProvider()
        {

        }
        #endregion

        #region Methods
        protected abstract EntityFactory CreateEntityFactory();
        #endregion

        #region Properties
        protected EntityFactory EntityFactory
        {
            get
            {
                if (!this.IsEntityFactoryCreated)
                {
                    this._factory = this.CreateEntityFactory();
                }
                return this._factory;
            }
        }

        protected bool IsEntityFactoryCreated
        {
            get
            {
                return _factory != null;
            }
        }

        protected EntityAdapter Adapter
        {
            get
            {
                return this.EntityFactory.Adapter;
            }
        }
        #endregion


    }
}
