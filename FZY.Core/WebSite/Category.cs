using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.Domain.Entities.Auditing;

namespace FZY.WebSite
{
    public class Category: FullAuditedEntity
    {
        /// <summary>
        /// 图片名称
        /// </summary>
        [MaxLength(50)]
        public string Name { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        [MaxLength(100)]
        public string Description { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { set; get; }

   
    }
}
