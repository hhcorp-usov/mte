using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mte.Areas.aWayBills.Controllers
{
    public class HomeController : Controller
    {
        // GET: WayBills/Home
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }
    }
}