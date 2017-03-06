using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    public class BaseMetaInt32TreeEntity : BaseMetaEntity<int >
    {
        #region Fields
        private int _parentId;
        #endregion

        #region Methods
        public override void CopyFrom(EntityBase entity)
        {
            base.CopyFrom(entity);
            BaseMetaInt32TreeEntity sourceEntity = entity as BaseMetaInt32TreeEntity;
            if(sourceEntity != null)
            {
                this.ParentID = sourceEntity.ParentID;
            }
        }
        #endregion

        #region Properties
#if WCF
		[DataMember()]
#endif
        [DataField("parent_id", IsNullable = true)]
        [DefaultValue(0)]
        public virtual int ParentID
        {
            get
            {
                return _parentId;
            }
            set
            {
                _parentId = value;
            }
        }
        #endregion
    }
}
