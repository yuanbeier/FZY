
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
    public class PhysicalFileInfoData : BaseOrderDataEntity<Guid>
    {
        #region Fields
        private System.Guid _logicFileID;
        private int _version;
        private long _fileLength;
        private System.Guid _compressModeID;
        private System.Guid _encryptModeID;
        private LogicFileInfoData _logicFileInfo;
        private Guid _storeModeID;
        #endregion

        #region Methods
        protected override EntityBase CreateInstance()
        {
            return new PhysicalFileInfoData();
        }

        public override void CopyFrom(EntityBase entity)
        {
            base.CopyFrom(entity);
            PhysicalFileInfoData sourceEntity = entity as PhysicalFileInfoData;
            if (sourceEntity != null)
            {
                this.LogicFileID = sourceEntity.LogicFileID;
                this.Version = sourceEntity.Version;
                this.FileLength = sourceEntity.FileLength;
                this.CompressModeID = sourceEntity.CompressModeID;
                this.EncryptModeID = sourceEntity.EncryptModeID;
                this.StoreModeID = sourceEntity.StoreModeID;
            }
        }

        protected internal void SetParent(LogicFileInfoData logicFileInfo)
        {
            _logicFileInfo = logicFileInfo;
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
        [DataField(FieldNames.LogicFileID, IsNullable = false)]
        public virtual System.Guid LogicFileID
        {
            get
            {
                return _logicFileID;

            }
            set
            {
                _logicFileID = value;

            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.Version, IsNullable = false)]
        public virtual int Version
        {
            get
            {
                return _version;

            }
            set
            {
                _version = value;

            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.FileLength, IsNullable = false)]
        public virtual long FileLength
        {
            get
            {
                return _fileLength;

            }
            set
            {
                _fileLength = value;

            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.CompressModeID, IsNullable = true)]
        public virtual System.Guid CompressModeID
        {
            get
            {
                return _compressModeID;

            }
            set
            {
                _compressModeID = value;

            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.EncryptModeID, IsNullable = true)]
        public virtual System.Guid EncryptModeID
        {
            get
            {
                return _encryptModeID;

            }
            set
            {
                _encryptModeID = value;

            }
        }

#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.StoreModeID, IsNullable = false)]
        public virtual Guid StoreModeID
        {
            get
            {
                return this._storeModeID;
            }
            set
            {
                this._storeModeID = value;
            }
        }

        public virtual LogicFileInfoData LogicFileInfo
        {
            get
            {
                return this._logicFileInfo;
            }
        }
        #endregion

        #region FieldNames Class
        public new static class FieldNames
        {
            public const string ID = "physical_file_id";
            public const string LogicFileID = "logic_file_id";
            public const string Version = "version";
            public const string FileLength = "file_length";
            public const string CompressModeID = "compress_mode_id";
            public const string EncryptModeID = "encrypt_mode_id";
            public const string OrderNum = "order_num";
            public const string StoreModeID = "store_mode_id";
        }
        #endregion
    }
}

