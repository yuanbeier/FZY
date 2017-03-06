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
    public abstract class BaseGuidTreeEntity : BaseOrderDataEntity<Guid>
    {
        #region Fields
        private Guid _parentId;
        #endregion

        #region Methods
        public override void CopyFrom(EntityBase entity)
        {
            base.CopyFrom(entity);
            BaseGuidTreeEntity sourceEntity = entity as BaseGuidTreeEntity;
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
        [DataField("parent_id", IsNullable = false)]
        [DefaultValue(typeof(Guid), FMPUtility.DefaultValues.DefaultGuidEmptyString)]
        public virtual Guid ParentID
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
