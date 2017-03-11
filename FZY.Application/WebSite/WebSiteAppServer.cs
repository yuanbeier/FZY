using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Abp.AutoMapper;
using Abp.Domain.Repositories;
using FZY.WebSite.Dto;
using Abp.Authorization;
using Abp.Linq.Extensions;
using FZY.Page;

namespace FZY.WebSite
{
    /// <summary>
    /// 网站服务
    /// </summary>
 
    public class WebSiteAppServer:FZYAppServiceBase,IWebSiteAppServer
    {
        private readonly IRepository<HomePic> _homePicRepository;

        private readonly IRepository<Product> _productRepository;

        public WebSiteAppServer(IRepository<HomePic> homePicRepository, IRepository<Product> productRepository)
        {
            _homePicRepository = homePicRepository;
            _productRepository = productRepository;
        }


        /// <summary>
        /// 新增首页图片
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddHomePicAsync(HomePicInput input)
        {
            var homePic = input.MapTo<HomePic>();
            homePic.ImageUrl = homePic.ImageUrl.Split(',')[0];
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

        /// <summary>
        /// 删除首页图片
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteHomePicAsync(int id)
        {
            await _homePicRepository.DeleteAsync(id);
        }

        /// <summary>
        /// 添加产品
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task AddProductAsync(ProductInput input)
        {
            var product = input.MapTo<Product>();
            product = _productRepository.Insert(product);
            CurrentUnitOfWork.SaveChanges();
            await AddFileRelationAsync(input.ProductImage, product.Id, ModuleType.ProductMan);
            await AddFileRelationAsync(input.StyleImage, product.Id, ModuleType.ProductDetail);

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultOutputDto<ProductOutput>> GetProductListAsync(GetProductListInput input)
        {
            var query = _productRepository.GetAll().
               WhereIf(!string.IsNullOrEmpty(input.Name), x => x.Name == input.Name);
            var count = query.Count();
            var result = await query.OrderByDescending(x => x.CreationTime).Skip(input.SkipCount).Take(input.PageCount).ToListAsync();
            var reusltOut = result.MapTo<List<ProductOutput>>();
            foreach (var producOutput in reusltOut)
            {
                var firstOrDefault
                    = FileRelationRepository
                    .GetAll().FirstOrDefault
                    (x => x.KeyId == producOutput.Id
                          && x.ModuleId == ModuleType.ProductMan);
                if (firstOrDefault != null)
                    producOutput.FileId = firstOrDefault
                        .FileId;
            }
            return new PagedResultOutputDto<ProductOutput>(count, reusltOut);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ProductOutput> GetProductByIdAsync(int id)
        {
            return (await _productRepository.GetAsync(id)).MapTo<ProductOutput>();
        }

        /// <summary>
        /// 删除产品
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteProductAsync(int id)
        {
            await _productRepository.DeleteAsync(id);
        }
    }
}
