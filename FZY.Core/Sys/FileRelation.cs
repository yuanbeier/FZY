using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities;
using FZY.FileHandle;

namespace FZY.WebSite.Sys
{
    /// <summary>
    /// 文件关系实体
    /// 刘珠明
    /// 20160905
    /// </summary>
    public class FileRelation : Entity<int>
    {
        /// <summary>
        /// 模块Id
        /// </summary>
        public ModuleType ModuleId { get; set; }

        /// <summary>
        /// 标识Id
        /// </summary>
        public int KeyId { get; set; }

        /// <summary>
        /// 文件路径
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public  long? CreatorUserId { get; set; }

        /// <summary>
        /// 创建人编号
        /// </summary>
        public DateTime? CreationTime { get; set; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType? FileType { set; get; }

        /// <summary>
        /// 环节实例Id
        /// </summary>
        public long? ActivityInstanceId { set; get; }
    }
}
