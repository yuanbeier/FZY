using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FZY.Web.Controllers
{
    public class ContractController : FZYControllerBase
    {
        // GET: Contract
        public ActionResult Index()
        {
            return View();
        }
    }
}