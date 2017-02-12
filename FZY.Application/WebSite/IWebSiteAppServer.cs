using Abp.Application.Services;
using FZY.WebSite;
using FZY.WebSite.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FZY.WebSite
{
    /// <summary>
    /// 网站服务
    /// </summary>
    public interface IWebSiteAppServer: IApplicationService
    {
        /// <summary>
        /// 新增首页图片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddHomePicAsync(HomePicInput input);

        /// <summary>
        /// 获取首页图片列表
        /// </summary>
        /// <returns></returns>
        Task<List<HomePicOutput>> GetHomePicListAsync();
    }
}
