using Abp.Application.Services;
using FZY.WebSite;
using FZY.WebSite.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FZY.Page;

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

        /// <summary>
        /// 删除首页图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteHomePicAsync(int id);

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        Task AddProductAsync(ProducInput input);


        /// <summary>
        /// 获取产品列表
        /// </summary>
        /// <returns></returns>
        Task<PagedResultOutputDto<ProducOutput>> GetProductListAsync(GetProductListInput input);


        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteProductAsync(int id);
    }
}
