namespace FZY.Web.Models.File
{
    /// <summary>
    /// 文件上传Dto
    /// 袁贝尔
    /// 20161031
    /// </summary>
    public class UploadFileDto
    {
        /// <summary>
        /// 文件上传控件Id
        /// </summary>
        public string UploadId { set; get; }

        /// <summary>
        /// 模块Id
        /// </summary>
        public ModuleType? ModuleId { set; get; }

        /// <summary>
        /// 相关主体Id（业务Id）
        /// </summary>
        public int? KeyId { set; get; }

        /// <summary>
        /// 环节实例Id
        /// </summary>
        public int? ActivityInstanceId { set; get; }

        /// <summary>
        /// 上传类型
        /// </summary>
        public string UploadContentType { set; get; }
    }
}