using System;
using System.Collections.Generic;
using System.Text;

namespace Topevery.FMP.ObjectModel
{
    /// <summary>
    /// 保存文件更新模式
    /// </summary>
    public enum UpdateMode
    {
        /// <summary>
        /// 缺省模式，根据配置值，如果没有配置值，则为覆盖模式(Override)
        /// </summary>
        None,

        /// <summary>
        /// 每次更新创建新版本
        /// </summary>
        NewVersion,

        /// <summary>
        /// 覆盖原来文件
        /// </summary>
        Override
    }
}
