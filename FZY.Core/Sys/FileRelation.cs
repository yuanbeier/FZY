using System;
using System.ComponentModel.DataAnnotations;
using Abp.Domain.Entities;
using FZY.Common;

namespace FZY.Sys
{

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
        /// 文件Id
        /// </summary>
        public Guid FileId { get; set; }

        /// <summary>
        /// 图片名称
        /// </summary>
        [MaxLength(200)]
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
    }
}
