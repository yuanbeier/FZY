using Abp.AutoMapper;
using FZY.WebSite.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FZY.Web.Models.WebSite
{
    [AutoMap(typeof(HomePicInput))]
    public class HomePicModel
    {
        /// 图片名称
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { set; get; }

        /// <summary>
        /// 排序
        /// </summary>
        public int Sort { set; get; }

        /// <summary>
        /// 图片地址
        /// </summary>
        public string ImageUrl { set; get; }

        /// <summary>
        /// 链接地址
        /// </summary>
        public string Url { set; get; }
    }
}