using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZY.WebSite
{
    /// <summary>
    /// 产品类别
    /// </summary>
    public class Product: FullAuditedEntity
    {
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
        /// 排序
        /// </summary>
        public int Sort { set; get; }

        /// <summary>
        /// 产品类别
        /// </summary>
        public Category Category { set; get; }

        /// <summary>
        /// 产品类别Id
        /// </summary>
        public int CategoryId { set; get; }
    }
}
