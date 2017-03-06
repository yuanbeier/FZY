using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;



namespace Topevery.FMP.ObjectModel
{
    /// <summary>
    /// 操作远程文件的上下文
    /// </summary>
    [Serializable]
    public class RemoteStreamContext : OpenFileItemData
    {
        #region Fields
        public const int DefaultBufferSize = 256 * 1024;
        //private string _server;  
        private string _serviceGroup;

        
        private int _bufferSize = DefaultBufferSize;        
        #endregion

        #region Constructor
        #endregion

        #region Properties
        /// <summary>
        /// 
        /// </summary>
        /*public string Server
        {
            get
            {
                return this._server;
            }
            set
            {
                this._server = value;
            }
        }*/
        /// <summary>
        /// 对应ServiceGroup节点名称
        /// </summary>
        public string ServiceGroup
        {
            get { return _serviceGroup; }
            set { _serviceGroup = value; }
        }

        /// <summary>
        /// 缓存大小
        /// </summary>
        [DefaultValue(DefaultBufferSize)]
        public int BufferSize
        {
            get
            {
                return this._bufferSize;
            }
            set
            {
                this._bufferSize = value;
            }
        }

        
        #endregion
    }
}
