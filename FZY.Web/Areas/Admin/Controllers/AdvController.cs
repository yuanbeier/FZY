using Abp.AutoMapper;
using Abp.Web.Mvc.Authorization;
using FZY.Web.Controllers;
using FZY.Web.Models.WebSite;
using FZY.WebSite;
using FZY.WebSite.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace FZY.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 首页轮播
    /// </summary>
    [AbpMvcAuthorize]
    public class AdvController : FZYControllerBase
    {
        private readonly IWebSiteAppServer _iWebSiteAppServer;

        public AdvController(IWebSiteAppServer iWebSiteAppServer)
        {
            _iWebSiteAppServer = iWebSiteAppServer;
        }
        // GET: Admin/Adv
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
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> Add(HomePicModel model)
        {
            await _iWebSiteAppServer.AddHomePicAsync(model.MapTo<HomePicInput>());
            return RedirectToAction("Index");
        }
    }
}