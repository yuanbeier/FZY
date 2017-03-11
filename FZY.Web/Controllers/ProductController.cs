using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.AutoMapper;
using Abp.Threading;
using FZY.Web.Models.WebSite;
using FZY.WebSite;

namespace FZY.Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly IWebSiteAppServer _webSiteAppServer;

        public ProductController(IWebSiteAppServer webSiteAppServer)
        {
            _webSiteAppServer = webSiteAppServer;
        }


        // GET: Product
        public ActionResult Index(int id)
        {
            ViewBag.ProductId = id;
            var obj = AsyncHelper.RunSync(async () => await _webSiteAppServer.GetProductByIdAsync(id)).MapTo<ProductModel>();
            return View(obj);
        }

        // GET: Product
        public ActionResult List()
        {
            ViewBag.CategoryId = Request.QueryString["categoryId"];
            return View();
        }
    }
}