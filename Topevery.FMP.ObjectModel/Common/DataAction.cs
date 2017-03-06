using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    /// <summary>
    /// 数据操作状态
    /// </summary>
    [Flags]
    public enum DataAction
    {
        /// <summary>
        /// 正常
        /// </summary>
        None = 0,
        
        /// <summary>
        /// 删除
        /// </summary>
        Delete = 1,
        
        /// <summary>
        /// 修改
        /// </summary>
        Change = 2,
        
        /// <summary>
        /// 添加
        /// </summary>
        Add    = 4,
        
        
        /// <summary>
        /// 提交
        /// </summary>
        Commit = 8,
        
        /// <summary>
        /// 回滚
        /// </summary>
        Rollback = 16,
        
        /// <summary>
        /// 未知
        /// </summary>
        Unknown = 2 ^ 30
    }
}
