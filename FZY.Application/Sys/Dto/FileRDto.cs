using System;
using Abp.AutoMapper;
using FZY.Common;

namespace FZY.Sys.Dto
{
    /// <summary>
    /// 文件Dto
    /// </summary>
    [AutoMap(typeof(FileRelation))]
    public class FileRDto
    {
        /// <summary>
        /// 文件Id
        /// </summary>
        public Guid FileId { set; get; }

        /// <summary>
        /// 文件名
        /// </summary>
        public string FileName { set; get; }


        /// <summary>
        /// 文件名不带后缀名
        /// </summary>
        public string FileNameWithoutExten { set; get; }

        /// <summary>
        /// 文件类型
        /// </summary>
        public FileType FileType { set; get; }


        /// <summary>
        /// 文件下载路径
        /// </summary>
        public string FileDownUrl { set; get; }

        /// <summary>
        /// 文件缩略图路径
        /// </summary>
        public string ImageShowUrl { set; get; }
    }
}
