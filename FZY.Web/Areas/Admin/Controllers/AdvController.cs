using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FZY.Web.Areas.Admin.Controllers
{
    /// <summary>
    /// 首页轮播
    /// </summary>
    public class AdvController : Controller
    {
        // GET: Admin/Adv
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Add()
        {
            return View();
        }
    }
}