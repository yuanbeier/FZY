using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    /// <summary>
    /// 检索树型结构参数基类
    /// </summary>
    public abstract class BaseGuidTreeFetchParameter : BaseGuidFetchParameter
    {
        #region Fields
        private Guid _parentId;
        private int _maxChildLevel = -1;
        private bool _includeParent = false;
        #endregion

        #region Properties
        /// <summary>
        /// 父节点ID
        /// </summary>
        public Guid ParentID
        {
            get
            {
                return this._parentId;
            }
            set
            {
                this._parentId = value;
            }
        }

        /// <summary>
        /// 检索子结点最大级别
        /// </summary>
        public int MaxChildLevel
        {
            get
            {
                return this._maxChildLevel;
            }
            set
            {
                this._maxChildLevel = value;
            }
        }
        public bool IncludeParent
        { 
            get{
                return this._includeParent; 
            }
            set {
                this._includeParent = value;
            }
        }
        #endregion
    }
}
