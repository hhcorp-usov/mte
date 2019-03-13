using Microsoft.AspNet.Identity;
using mte.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace mte.Areas.Smena.Controllers
{
    public class HomeController : Controller
    {
        private MteDataContexts db = new MteDataContexts();
        private ApplicationDbContext dbu = new ApplicationDbContext();
        private BaseSettings bs = new BaseSettings();

        public int GetUserIdentity()
        {
            var uid = User.Identity.GetUserId();
            ApplicationUser u = dbu.Users.FirstOrDefault(x => x.Id == uid);
            return u.AdditionalUserInfo.GlobalContainersId;
        }

        // GET: Smena/Home
        [Authorize]
        public async Task<ActionResult> Index()
        {
            var ucid = GetUserIdentity();
            var model = db.Smenes.FirstOrDefault(w => w.GlobalContainersId == ucid && w.SmenaDate <= DateTime.Now);

            ViewBag.PageTitle = "Текущая смена";
            return View(model);
        }
    }
}