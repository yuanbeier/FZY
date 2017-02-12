using System.Web.Mvc;
using Abp.Web.Mvc.Authorization;

namespace FZY.Web.Controllers
{
    public class HomeController : FZYControllerBase
    {
        public ActionResult Index()
        {
            return View();
        }
	}
}