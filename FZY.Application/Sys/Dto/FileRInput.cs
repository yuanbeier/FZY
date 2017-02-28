namespace FZY.Sys.Dto
{
    /// <summary>
    /// 文件输入Id
    /// </summary>
    public class FileRInput
    {
        /// <summary>
        /// 模块类型
        /// </summary>
        public ModuleType? ModuleType { set; get; }

        /// <summary>
        /// 标识Id
        /// </summary>
        public int? KeyId { set; get; }

        /// <summary>
        /// 环节实例Id
        /// </summary>
        public long? ActivityIntanceId { set; get; }
    }
}
