using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Topevery.Framework.Data;
using Topevery.FMP.ObjectModel;

namespace Topevery.FMP.Data
{
    public class GuidEntityWrapper<TEntity> : EntityBase  
    {
        #region Fields
        private TEntity _wrapEntity;
        private DbRecordStatus _dbRecordStatus;
        private Guid _createdUserId;
        private DateTime _createdDate;
        private Guid _lastUpdatedUserId;
        private DateTime _lastUpdatedDate;
        #endregion

        #region Constructor
        public GuidEntityWrapper()
        {
            
        }
        
        public GuidEntityWrapper(TEntity entity)
        {
            this.WrapEntity = entity;
        }
        #endregion

        #region Properties
        public virtual TEntity WrapEntity
        {
            get
            {
                return this._wrapEntity;
            }
            set
            {
                _wrapEntity = value;
            }
        }
        
        [DataField("db_status")]
        [DefaultValue(ObjectModel.DbRecordStatus.Normal)]
        public virtual DbRecordStatus DbRecordStatus
        {
            get
            {
                return this._dbRecordStatus;
            }
            set
            {
                this._dbRecordStatus = value;
            }
        }
        
        [DataField("db_created_id")]
        public virtual Guid CreatedUserID
        {
            get
            {
                return this._createdUserId;
            }
            set
            {
                this._createdUserId = value;
            }
        }
        
        [DataField("db_created_date", IsNullable = true)]
        public virtual DateTime CreatedDate
        {
            get
            {
                return this._createdDate;
            }
            set
            {
                this._createdDate = value;
            }
        }

        [DataField("db_last_updated_id")]
        public virtual Guid LastUpdatedUserID
        {
            get
            {
                return this._lastUpdatedUserId;
            }
            set
            {
                this._lastUpdatedUserId = value;
            }
        }

        [DataField("db_last_updated_date")]
        public virtual DateTime LastUpdatedDate
        {
            get
            {
                return this._lastUpdatedDate;
            }
            set
            {
                this._lastUpdatedDate = value;
            }
        }
        #endregion
    }
}
