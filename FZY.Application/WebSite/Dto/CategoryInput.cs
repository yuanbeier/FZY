using System.ComponentModel.DataAnnotations;
using Abp.AutoMapper;
using Abp.Domain.Entities.Auditing;

namespace FZY.WebSite.Dto
{
    /// <summary>
    /// 产品类别
    /// </summary>
    [AutoMap(typeof(Category))]
    public class CategoryInput
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
        /// 产品图片
        /// </summary>
        public string ProductImage { set; get; }

        /// <summary>
        /// 样式图片
        /// </summary>
        public string StyleImage { set; get; }



        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { set; get; }
    }
}
