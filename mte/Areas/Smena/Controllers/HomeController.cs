using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace mte.Areas.Smena.Controllers
{
    public class HomeController : Controller
    {
        // GET: Smena/Home
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.PageTitle = "Смена";
            return View();
        }
    }
}