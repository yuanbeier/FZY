using System;
using System.Collections.Generic;
using System.Text;
#if WCF
using System.Runtime.Serialization;
#endif
using Topevery.Framework.Data;
using Topevery.Framework.ServiceModel;
using Topevery.FMP.ObjectModel.Exceptions;


namespace Topevery.FMP.ObjectModel
{
    #if WCF
	[DataContract(Namespace = FMPUtility.NamespaceURI)]
    #endif
    [Serializable]
    public abstract class BaseEntity<T> : EntityBase, ISetReadOnly
    {
        #region Fields
        private T _id;
        private bool _readonly = false;
        #endregion

        #region Override
        public override void CopyFrom(EntityBase entity)
        {
            bool readOnly = this.ReadOnly;
            if (readOnly)
            {
                this.SetReadOnly(false);
            }
            base.CopyFrom(entity);
            BaseEntity<T> newEntity = entity as BaseEntity<T>;
            if(newEntity != null)
            {
                this.ID = newEntity.ID;
            }
            if(readOnly)
            {
                this.SetReadOnly(true);
            }
        }

        protected virtual internal void SetReadOnly(bool readOnly)
        {
            this._readonly = true;
        }

        protected virtual void CheckReadOnly(string propertyName)
        {
            if (ReadOnly)
            {
                __Error.CheckPropertyReadOnly(propertyName);
            }
        }

        void ISetReadOnly.SetReadOnly(bool readOnly)
        {
            this.SetReadOnly(readOnly);
        }
        #endregion

        #region Properties
        #if WCF
		[DataMember()]
        #endif
        public virtual T ID
        {
            get
            {
                return _id;
            }
            set
            {
                this.CheckReadOnly("ID");
                _id = value;
            }
        }
        #endregion

        #region Properties
#if WCF
		[DataMember()]
#endif
        public virtual bool ReadOnly
        {
            get
            {
                return this._readonly;
            }
        }
        #endregion
    }    
}
