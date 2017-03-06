using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace Topevery.FMP.Data
{
    public abstract class BaseMetaDataProvider : BaseDataProvider
    {
        #region Fields
        private DbCommand _dbCmdInsert;
        private DbCommand _dbCmdUpdate;
        private DbCommand _dbCmdSelect;
        #endregion

        #region Methods

        protected abstract DbCommand CreateInsertCommand();
        protected abstract DbCommand CreateUpdateCommand();
        protected abstract DbCommand CreateSelectCommand();
        #endregion

        #region Properties
        protected DbCommand InsertCommand
        {
            get
            {
                if(!IsInsertCommandCreated)
                {
                    this._dbCmdInsert = this.CreateInsertCommand();
                }
                return this._dbCmdInsert;
            }
        }

        protected bool IsInsertCommandCreated
        {
            get
            {
                return this._dbCmdInsert != null;
            }
        }

        protected DbCommand UpdateCommand
        {
            get
            {
                if(!IsUpdateCommandCreated)
                {
                    this._dbCmdUpdate = this.CreateUpdateCommand();
                }
                return this._dbCmdUpdate;
            }
        }

        protected bool IsUpdateCommandCreated
        {
            get
            {
                return this._dbCmdUpdate != null;
            }
        }

        protected DbCommand SelectCommand
        {
            get
            {
                if(!this.IsSelectCommandCreated)
                {
                    this._dbCmdSelect = this.CreateSelectCommand();
                }
                return this._dbCmdSelect;
            }
        }

        protected bool IsSelectCommandCreated
        {
            get
            {
                return this._dbCmdSelect != null;
            }
        }
        #endregion
    }
}
