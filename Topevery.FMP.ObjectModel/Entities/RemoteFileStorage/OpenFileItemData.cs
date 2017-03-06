using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Xml.Serialization;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class OpenFileItemData
    {
        #region Fields
        private Guid _fileID;
        private int _version = -1;
        private FileMode _fileMode = FileMode.None;
        private FileAccess _fileAccess = FileAccess.Read;
        private UpdateMode _updateMode = UpdateMode.None;
        private string _clientFileName;
        private Guid _lockID;
        private TimeSpan _lockExpiredTimeSpan;
        private long _lockExpiredTicks;
        #endregion

        [DefaultValue(typeof(Guid), FMPUtility.DefaultValues.DefaultGuidEmptyString)]
        public Guid LockID
        {
            get
            {
                return this._lockID;
            }
            set
            {
                this._lockID = value;
            }
        }

        [XmlIgnore]
        public TimeSpan LockExpiredtimeSpan
        {
            get
            {                
                return this._lockExpiredTimeSpan;
            }
            set
            {
                if (this._lockExpiredTimeSpan != value)
                {
                    this._lockExpiredTimeSpan = value;
                    this._lockExpiredTicks = value.Ticks;
                }
            }
        }

        public long LockExpiredTicks
        {
            get
            {
                return this._lockExpiredTicks;
            }
            set
            {
                if (this._lockExpiredTicks != value)
                {
                    this._lockExpiredTicks = value;
                    this._lockExpiredTimeSpan = TimeSpan.FromTicks(value);
                }
            }
        }
        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(typeof(Guid), FMPUtility.DefaultValues.DefaultGuidEmptyString)]
        public Guid FileID
        {
            get
            {
                return _fileID;
            }
            set
            {
                _fileID = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(-1)]
        public int Version
        {
            get
            {
                return this._version;
            }
            set
            {
                this._version = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(FileMode.None)]
        public FileMode FileMode
        {
            get
            {
                return this._fileMode;
            }
            set
            {
                this._fileMode = value;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        [DefaultValue(FileAccess.Read)]
        public FileAccess FileAccess
        {
            get
            {
                return this._fileAccess;
            }
            set
            {
                this._fileAccess = value;
            }
        }

        [DefaultValue(UpdateMode.None)]
        public UpdateMode UpdateMode
        {
            get
            {
                return this._updateMode;
            }
            set
            {
                this._updateMode = value;
            }
        }

        public string ClientFileName
        {
            get
            {
                return this._clientFileName;
            }
            set
            {
                this._clientFileName = value;
            }
        }
    }
}
