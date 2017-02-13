using Abp.AutoMapper;
using Abp.Domain.Entities;
using Abp.Domain.Entities.Auditing;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZY.WebSite.Dto
{
    /// <summary>
    /// 首页图片
    /// </summary>
    [AutoMap(typeof(HomePic))]
    public class HomePicInput
    {
        /// <summary>
        /// 图片标题
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

        /// <summary>
        /// 图片地址
        /// </summary>
        [MaxLength(200)]
        public string ImageUrl { set; get; }


        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { set; get; }
    }
}
