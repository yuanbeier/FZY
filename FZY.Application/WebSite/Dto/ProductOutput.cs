using System;
using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace FZY.WebSite.Dto
{
    /// <summary>
    /// 产品类别
    /// </summary>
    [AutoMap(typeof(Product))]
    public class ProductOutput
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { set; get; }
        /// <summary>
        /// 产品名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 产品描述
        /// </summary>
        [MaxLength(500)]
        public string Description { set; get; }

        /// <summary>
        /// 货号
        /// </summary>
        [MaxLength(100)]
        public string Style { set; get; }

        /// <summary>
        /// 尺寸
        /// </summary>
        [MaxLength(100)]
        public string Size { set; get; }

        /// <summary>
        /// 包装
        /// </summary>
        [MaxLength(200)]
        public string Package { set; get;}

        /// <summary>
        /// 创建时间
        /// </summary>
        public string CreationTime { set; get; }


        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { set; get; }

        /// <summary>
        /// 文件Id
        /// </summary>
        public Guid FileId { set; get; }

        /// <summary>
        /// 产品类别Id
        /// </summary>
        public int CategoryId { set; get; }

        /// <summary>
        /// 产品类别Id
        /// </summary>
        public string CategoryName { set; get; }
    }
}
