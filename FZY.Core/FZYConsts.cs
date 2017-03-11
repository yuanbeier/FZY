using System.ComponentModel;

namespace FZY
{
    public class FZYConsts
    {
        public const string LocalizationSourceName = "FZY";
    }

    /// <summary>
    /// 模块枚举
    /// </summary>
    public enum ModuleType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("产品")]
        ProductMan = 1,
        [Description("产品详细")]
        ProductDetail = 2,
        [Description("产品类别")]
        Category = 3
    }
}