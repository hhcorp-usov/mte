using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using mte.Models;
using Microsoft.AspNet.Identity;

namespace mte.Areas.Guides.Controllers
{
    public class EnterprisesController : Controller
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

        [Authorize]
        public async Task<ActionResult> Index(int page = 1, string search = null, string sort_filter = null, string sort_order = null)
        {
            ViewBag.PageTitle = "Справочники / Организации";

            var list_count = 0;
            var list = from b in db.Enterprises select b;
            int cguid = GetUserIdentity();

            sort_filter = string.IsNullOrEmpty(sort_filter) ? "inn" : sort_filter;
            sort_order = string.IsNullOrEmpty(sort_order) ? "asc" : sort_order;

            list = list.Where(w => w._deleted != true).Where(w => w.GlobalContainersId == cguid);

            if (!string.IsNullOrEmpty(search))
            {
                list = list.Where(
                    w => w.Inn.ToUpper().Contains(search.ToUpper()) ||
                    w.Kpp.ToUpper().Contains(search.ToUpper()) ||
                    w.Name.ToUpper().Contains(search.ToUpper())
                    );
            }

            switch (sort_filter.ToLower())
            {
                case "inn":
                    switch (sort_order.ToLower())
                    {
                        case "asc":
                            list = list.OrderBy(o => o.Inn);
                            break;

                        case "desc":
                            list = list.OrderByDescending(o => o.Inn);
                            break;
                    }
                    break;

                case "kpp":
                    switch (sort_order.ToLower())
                    {
                        case "asc":
                            list = list.OrderBy(o => o.Kpp);
                            break;

                        case "desc":
                            list = list.OrderByDescending(o => o.Kpp);
                            break;
                    }
                    break;

                case "name":
                    switch (sort_order.ToLower())
                    {
                        case "asc":
                            list = list.OrderBy(o => o.Name);
                            break;

                        case "desc":
                            list = list.OrderByDescending(o => o.Name);
                            break;
                    }
                    break;
            }

            list_count = await list.CountAsync();
            list = list.Skip((page - 1) * bs.bs_itemsPerPage).Take(bs.bs_itemsPerPage);

            EnterprisesView model = new EnterprisesView
            {
                Enterprises = await list.ToListAsync(),
                PageInfo = new PagingInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = bs.bs_itemsPerPage,
                    Search = search,
                    Sort_order = sort_order,
                    Sort_filter = sort_filter,
                    TotalItems = list_count
                }
            };

            return View(model);
        }

        [Authorize]
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int cguid = GetUserIdentity();
            Enterprises enterprises = await db.Enterprises
                .Where(w => w.GlobalContainersId == cguid)
                .Where(w => w.Id == id)
                .FirstAsync();
            if (enterprises == null)
            {
                return HttpNotFound();
            }
            return PartialView(enterprises);
        }

        [Authorize]
        public ActionResult Create()
        {
            return PartialView();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Inn,Kpp,Ogrn,FAdress,YAdress,Phones")] Enterprises enterprises)
        {
            if (ModelState.IsValid)
            {
                int cguid = GetUserIdentity();
                enterprises.GlobalContainersId = cguid;
                db.Enterprises.Add(enterprises);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(enterprises);
        }

        // GET: Guides/Enterprises/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int cguid = GetUserIdentity();
            Enterprises enterprises = await db.Enterprises.Where(w => w.GlobalContainersId == cguid || w.Id == id).FirstAsync();
            if (enterprises == null)
            {
                return HttpNotFound();
            }
            return View(enterprises);
        }

        // POST: Guides/Enterprises/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Inn,Kpp,Ogrn,FAdress,YAdress,Phones")] Enterprises enterprises)
        {
            if (ModelState.IsValid)
            {
                int cguid = GetUserIdentity();
                enterprises.GlobalContainersId = cguid;
                db.Entry(enterprises).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(enterprises);
        }

        // GET: Guides/Enterprises/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            int cguid = GetUserIdentity();
            Enterprises enterprises = await db.Enterprises.Where(w => w.GlobalContainersId == cguid || w.Id == id).FirstAsync();
            if (enterprises == null)
            {
                return HttpNotFound();
            }
            return View(enterprises);
        }

        // POST: Guides/Enterprises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            int cguid = GetUserIdentity();
            Enterprises enterprises = await db.Enterprises.Where(w => w.GlobalContainersId == cguid || w.Id == id).FirstAsync();
            enterprises._deleted = true;
            db.Entry(enterprises).State = EntityState.Modified;
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                dbu.Dispose();
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
