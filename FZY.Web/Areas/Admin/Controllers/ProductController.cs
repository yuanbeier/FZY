using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Abp.AutoMapper;
using Abp.Threading;
using FZY.WebSite;
using FZY.Web.Models.WebSite;

namespace FZY.Web.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebSiteAppServer _webSiteAppServer;

        public ProductController(IWebSiteAppServer webSiteAppServer)
        {
            _webSiteAppServer = webSiteAppServer;
        }

        // GET: Admin/Product
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int　productId)
        { 
            var obj = AsyncHelper.RunSync(async () => await _webSiteAppServer.GetProductByIdAsync(productId)).MapTo<ProductModel>();
            return View(obj);
        }
    }
}