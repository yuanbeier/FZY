using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;

namespace Topevery.FMP.ObjectModel
{
    [Serializable]
    public class LogicFileInfoItemData : BaseEntity<Guid>
    {
        #region Fields
        private Guid _physicalFileID;
        private int _version = 0;
        private bool _withPhysicalFileInfo = true;
        #endregion

        #region Properties
        /// <summary>
        /// �����ļ�ID
        /// </summary>
        public Guid PhysicalFileID
        {
            get
            {
                return this._physicalFileID;
            }
            set
            {
                this._physicalFileID = value;
            }
        }

        /// <summary>
        /// �ļ��汾
        /// </summary>
        [DefaultValue(0)]
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
        /// �Ƿ���������ļ���Ϣ
        /// </summary>
        [DefaultValue(true)]
        public bool WithPhysicalFileInfo
        {
            get
            {
                return this._withPhysicalFileInfo;
            }
            set
            {
                this._withPhysicalFileInfo = value;
            }
        }
        #endregion
    }
}
