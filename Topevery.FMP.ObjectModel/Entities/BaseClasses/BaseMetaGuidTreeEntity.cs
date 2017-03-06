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
    public class BaseMetaGuidTreeEntity : BaseMetaEntity<Guid>
    {
        #region Fields
        private Guid _parentID = Guid.Empty;
        #endregion


        #region Methods
        public override void CopyFrom(EntityBase entity)
        {
            base.CopyFrom(entity);
            BaseMetaGuidTreeEntity sourceEntiy = entity as BaseMetaGuidTreeEntity;
            if(sourceEntiy != null)
            {
                this.ParentID = sourceEntiy.ParentID;
            }
        }
        #endregion

        #region Properties
#if WCF
		[DataMember()]
#endif
        [DataField(BaseMetaGuidTreeEntity.FieldNames.ParentID, IsNullable = false)]
        [DefaultValue(typeof(Guid), FMPUtility.DefaultValues.DefaultGuidEmptyString)]
        public virtual Guid ParentID
        {
            get
            {
                return _parentID;
            }
            set
            {
                _parentID = value;
            }
        }
        #endregion
        #region fieldnames
        public new static class FieldNames
        {
            public const string ParentID = "parent_id";
        }
        #endregion
    }
}
