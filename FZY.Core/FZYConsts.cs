using System.ComponentModel;

namespace FZY
{
    public class FZYConsts
    {
        public const string LocalizationSourceName = "FZY";
    }

    /// <summary>
    /// </summary>
    public enum ModuleType
    {
        /// <summary>
        /// 
        /// </summary>
        [Description("产品")]
        ProductMan = 1,
        [Description("产品详细")]
        ProductDetail = 2
    }
}