﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using FZY.WebSite.Dto;

namespace FZY.WebSite
{
    /// <summary>
    /// 网站服务
    /// </summary>
    public class WebSiteAppServer:FZYAppServiceBase,IWebSiteAppServer
    {
        private readonly IRepository<HomePic> _homePicRepository;

        public WebSiteAppServer(IRepository<HomePic> homePicRepository)
        {
            _homePicRepository = homePicRepository;
        }


        /// <summary>
        /// 新增首页图片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddHomePicAsync(HomePicInput input)
        {
            var homePic = input.MapTo<HomePic>();
            await _homePicRepository.InsertAsync(homePic);
        }

        /// <summary>
        /// 获取首页图片列表
        /// </summary>
        /// <returns></returns>
        public async Task<List<HomePicOutput>> GetHomePicListAsync()
        {
            var list = await _homePicRepository.GetAllListAsync();
            return list.MapTo<List<HomePicOutput>>();
        }
    }
}
