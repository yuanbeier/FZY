using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Abp.AutoMapper;
using Abp.Threading;
using FZY.Web.Models.WebSite;
using FZY.WebSite;

namespace FZY.Web.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IWebSiteAppServer _webSiteAppServer;

        public CategoryController(IWebSiteAppServer webSiteAppServer)
        {
            _webSiteAppServer = webSiteAppServer;
        }

        // GET: Admin/Category
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }

        /// <summary>
        /// 编辑
        /// </summary>
        /// <returns></returns>
        public ActionResult Edit(int id)
        {
            var obj = AsyncHelper.RunSync(async () => await _webSiteAppServer.GetCategoryByIdAsync(id)).MapTo<CategoryModel>();
            return View(obj);
        }
    }
}