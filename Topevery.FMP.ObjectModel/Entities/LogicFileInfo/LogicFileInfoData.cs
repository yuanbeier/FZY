
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
    public class LogicFileInfoData : BaseOrderDataEntity<Guid>
	{
		#region Fields
		private string _logicFileName = string.Empty;
        private string _logicFileExt = string.Empty;
		private System.Guid _latestPhysicalFileID;
		private bool _isReadOnly;
        private LogicFileStatus _logicFileStatus = LogicFileStatus.Normal;
        private PhysicalFileInfoDataCollection _physicalFileInfos;
		#endregion
		
		#region Methods
		protected override EntityBase CreateInstance()
        {
            return new LogicFileInfoData();
        }
		
		public override void CopyFrom(EntityBase entity)
		{
			base.CopyFrom(entity);
			LogicFileInfoData sourceEntity = entity as LogicFileInfoData;
			if(sourceEntity != null)
			{				
				this.LogicFileName = sourceEntity.LogicFileName;
				this.LogicFileExt = sourceEntity.LogicFileExt;
				this.LatestPhysicalFileID = sourceEntity.LatestPhysicalFileID;
				this.IsReadOnly = sourceEntity.IsReadOnly;
                this.LogicFileStatus = sourceEntity.LogicFileStatus;
			}
		}
		#endregion
		
		#region Properties
		#if WCF
		[DataMember()]
		#endif
		[DataField(FieldNames.ID,IsNullable=false)]
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
		[DataField(FieldNames.LogicFileName,IsNullable=false)]
        [DefaultValue("")]
		public virtual string LogicFileName
		{
			get
			{
				return _logicFileName;
				
			}
			set
			{
				_logicFileName = value;
				
			}
		}
		
		#if WCF
		[DataMember()]
		#endif
		[DataField(FieldNames.LogicFileExt,IsNullable=false)]
        [DefaultValue("")]
		public virtual string LogicFileExt
		{
			get
			{
				return _logicFileExt;
				
			}
			set
			{
				_logicFileExt = value;
				
			}
		}
		
		#if WCF
		[DataMember()]
		#endif
		[DataField(FieldNames.LatestPhysicalFileID,IsNullable=true)]
		public virtual System.Guid LatestPhysicalFileID
		{
			get
			{
				return _latestPhysicalFileID;
				
			}
			set
			{
				_latestPhysicalFileID = value;
				
			}
		}
		
		#if WCF
		[DataMember()]
		#endif
		[DataField(FieldNames.IsReadOnly,IsNullable=false)]
		public virtual bool IsReadOnly
		{
			get
			{
				return _isReadOnly;
				
			}
			set
			{
				_isReadOnly = value;
				
			}
        }

#if WCF
		[DataMember()]
#endif
        [DataField(FieldNames.LogicFileStatus, IsNullable = false)]
        [DefaultValue(LogicFileStatus.Normal)]
        public virtual LogicFileStatus LogicFileStatus
        {
            get
            {
                return _logicFileStatus;

            }
            set
            {
                _logicFileStatus = value;

            }
        }

        #if WCF
		[DataMember()]
        #endif
        public virtual PhysicalFileInfoDataCollection PhysicalFileInfos
        {
            get
            {
                if (_physicalFileInfos == null)
                {
                    _physicalFileInfos = new PhysicalFileInfoDataCollection(this);
                }
                return _physicalFileInfos;
            }
        }

        
		#endregion
		
		#region FieldNames Class
		public new static class FieldNames
		{
			public const string ID = "logic_file_id";
			public const string LogicFileName = "logic_file_name";
			public const string LogicFileExt = "logic_file_ext";
			public const string LatestPhysicalFileID = "latest_physical_file_id";
			public const string IsReadOnly = "is_read_only";
			public const string Description = "description";
            public const string LogicFileStatus = "logic_file_status";
			public const string OrderNum = "order_num";
		}
		#endregion
	}
}

