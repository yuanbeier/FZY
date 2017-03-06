
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
#if WCF
using System.Runtime.Serialization;
#endif
using Topevery.Framework.Data;
using Topevery.Framework.ServiceModel;


namespace Topevery.FMP.ObjectModel
{
#if WCF
	[DataContract(Namespace = FMPUtility.NamespaceURI)]
#endif
    [Serializable]
    public class RecordLockedData : BaseEntity<Guid>
    {
        #region Fields
        private System.Guid _formID;
        private System.Guid _fromUniqueID;
        private System.DateTime _lockTime;
        private System.DateTime _expireTime;
        private System.Guid _lockUserID;
        #endregion

        #region Methods
        protected override EntityBase CreateInstance()
        {
            return new RecordLockedData();
        }

        public override void CopyFrom(EntityBase entity)
        {
            base.CopyFrom(entity);
            RecordLockedData sourceEntity = entity as RecordLockedData;
            if (sourceEntity != null)
            {
                this.FormID = sourceEntity.FormID;
                this.FormUniqueID = sourceEntity.FormUniqueID;
                this.LockTime = sourceEntity.LockTime;
                this.ExpireTime = sourceEntity.ExpireTime;
                this.LockUserID = sourceEntity.LockUserID;
            }
        }
        #endregion

        #region Properties
#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.ID, IsNullable = false)]
        public override System.Guid ID
        {
            get
            {
                return base.ID;
            }
            set
            {
                base.ID = value;
            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.FormID, IsNullable = false)]
        public virtual System.Guid FormID
        {
            get
            {
                return _formID;

            }
            set
            {
                _formID = value;

            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.FormUniqueID, IsNullable = false)]
        public virtual System.Guid FormUniqueID
        {
            get
            {
                return _fromUniqueID;

            }
            set
            {
                _fromUniqueID = value;

            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.LockTime, IsNullable = false)]
        public virtual System.DateTime LockTime
        {
            get
            {
                return _lockTime;

            }
            set
            {
                _lockTime = value;

            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.ExpireTime, IsNullable = true)]
        public virtual System.DateTime ExpireTime
        {
            get
            {
                return _expireTime;

            }
            set
            {
                _expireTime = value;

            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.LockUserID, IsNullable = false)]
        public virtual System.Guid LockUserID
        {
            get
            {
                return _lockUserID;

            }
            set
            {
                _lockUserID = value;

            }
        }

        #endregion

        #region FieldNames Class
        public static class FieldNames
        {
            public const string ID = "id";
            public const string FormID = "form_id";
            public const string FormUniqueID = "form_unique_id";
            public const string LockTime = "lock_time";
            public const string ExpireTime = "expire_time";
            public const string LockUserID = "lock_user_id";
        }
        #endregion
    }
}

