using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Abp.AutoMapper;
using FZY.WebSite.Dto;

namespace FZY.Web.Models.WebSite
{
    [AutoMap(typeof(CategoryOutput))]

    public class CategoryModel
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
        /// 创建时间
        /// </summary>
        public string CreationTime { set; get; }


        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { set; get; }
    }
}