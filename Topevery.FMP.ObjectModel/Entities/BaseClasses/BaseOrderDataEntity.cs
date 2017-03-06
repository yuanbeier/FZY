using System;
using System.Collections.Generic;
using System.Text;
using Topevery.Framework.Data;
#if WCF
using System.Runtime.Serialization;
#endif

namespace Topevery.FMP.ObjectModel
{
#if WCF
	[DataContract(Namespace = FMPUtility.NamespaceURI)]
#endif
    [Serializable]
    public class BaseOrderDataEntity<T> : BaseEntity<T>
    {
    #region Fields
        private long _orderNum;
        private T _currentID;
        private DateTime _updatedDateTime;
    #endregion

        #region Methods
        public override void CopyFrom(EntityBase entity)
        {
            base.CopyFrom(entity);
            BaseOrderDataEntity<T> sourceEntiy = entity as BaseOrderDataEntity<T>;
            if(sourceEntiy != null)
            {
                this.OrderNum = sourceEntiy.OrderNum;
                this.CurrentUserID = sourceEntiy.CurrentUserID;
                this.UpdatedDateTime = sourceEntiy.UpdatedDateTime;
            }
        }
        #endregion

        #region Properties
#if WCF
		[DataMember()]
#endif
        [DataField(BaseOrderDataEntity<T>.FieldNames.OrderNum)]
        public virtual long OrderNum
        {
            get
            {
                return _orderNum;
            }
            set
            {
                _orderNum = value;
            }
        }

#if WCF
		[DataMember()]
#endif
        public virtual T CurrentUserID
        {
            get
            {
                return _currentID;
            }
            set
            {
                _currentID = value;
            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(BaseOrderDataEntity<T>.FieldNames.UpdatedDateTime)]
        public virtual DateTime UpdatedDateTime
        {
            get
            {
                return _updatedDateTime;
            }
            set
            {
                _updatedDateTime = value;
            }
        }
        #region FieldName
        public static class FieldNames
        {
            public const string OrderNum = "order_num";
            public const string UpdatedDateTime = "db_last_updated_date";
        }
        #endregion
    #endregion
    }
}
