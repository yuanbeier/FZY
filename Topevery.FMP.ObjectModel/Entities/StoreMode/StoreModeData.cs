
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
    public class StoreModeData : BaseEntity<Guid>
    {
        #region Fields
        private string _storeParams;
        private string _name;
        #endregion

        #region Methods
        protected override EntityBase CreateInstance()
        {
            return new StoreModeData();
        }

        public override void CopyFrom(EntityBase entity)
        {
            base.CopyFrom(entity);
            StoreModeData sourceEntity = entity as StoreModeData;
            if (sourceEntity != null)
            {
                this.Name = sourceEntity.Name;
                this.StoreParams = sourceEntity.StoreParams;
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
        [DataField(FieldNames.Name, IsNullable = true)]
        public virtual string Name
        {
            get
            {
                return this._name;

            }
            set
            {
                _name = value;

            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.StoreParams, IsNullable = false)]
        public virtual string StoreParams
        {
            get
            {
                return _storeParams;

            }
            set
            {
                _storeParams = value;

            }
        }

        #endregion

        #region FieldNames Class
        public static class FieldNames
        {
            public const string ID = "store_mode_id";
            public const string Name = "store_mode_name";
            public const string StoreParams = "store_params";
        }
        #endregion
    }
}

