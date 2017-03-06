using System;
using System.Collections.Generic;
using System.Text;
#if WCF
using System.Runtime.Serialization;
#endif
using Topevery.Framework.Data;

namespace Topevery.FMP.ObjectModel
{
#if WCF
	[DataContract(Namespace = FMPUtility.NamespaceURI)]
#endif
    [Serializable]
    public class BaseMetaEntity<T> : BaseOrderDataEntity<T>
    {
        #region Fields
        private string _code;
        private string _name;
        private string _description;
        #endregion


        #region Methods
        public override void CopyFrom(EntityBase entity)
        {
            base.CopyFrom(entity);
            BaseMetaEntity<T> sourceEntiy = entity as BaseMetaEntity<T>;
            if(sourceEntiy != null)
            {
                this.BaseCode = sourceEntiy.BaseCode;
                this.Name = sourceEntiy.Name;
                this.Description = sourceEntiy.Description;
            }
        }
        #endregion

        #region Properties
        protected virtual string BaseCode
        {
            get
            {
                return _code;
            }
            set
            {
                _code = value;
            }
        }

#if WCF
		[DataMember()]
#endif
        public virtual string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(BaseMetaEntity<T>.FieldNames.Description, IsNullable = true)]
        public virtual string Description
        {
            get
            {
                return _description;
            }
            set
            {
                _description = value;
            }
        }
        #region FieldName
        public new static class FieldNames
        {
            public const string Description = "description";
        }
        #endregion
        #endregion
    }
}
