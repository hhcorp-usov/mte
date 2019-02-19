using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mte.Areas.Guides.Controllers
{
    public class HomeController : Controller
    {
        // GET: Guides/Home
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Справочники";
            return View();
        }
    }
}