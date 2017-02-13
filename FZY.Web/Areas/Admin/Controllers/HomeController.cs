using Abp.Web.Mvc.Authorization;
using FZY.Web.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FZY.Web.Areas.Admin.Controllers
{
    [AbpMvcAuthorize]
    public class HomeController : FZYControllerBase
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }
    }
}