using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web.Mvc;
using mte.Models;
using Microsoft.AspNet.Identity;

namespace mte.Areas.Smena.Controllers
{
    public class SmenesController : Controller
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

        // GET: Smena/Smenes
        public async Task<ActionResult> Index()
        {
            ViewBag.PageTitle = "Смена / Текущие";
            return View();
        }

        public async Task<ActionResult> GetDataList(int page = 1, string search = null, string sort_filter = null, string sort_order = null)
        {
            var list_count = 0;
            var list = from b in db.Smenes select b;
            int cguid = GetUserIdentity();

            sort_filter = string.IsNullOrEmpty(sort_filter) ? "date" : sort_filter;
            sort_order = string.IsNullOrEmpty(sort_order) ? "asc" : sort_order;

            if (!string.IsNullOrEmpty(search))
            {
                list = list.Where(
                    w => w.ControlerEmployers.Name.ToUpper().Contains(search.ToUpper()) ||
                    w.DispEmployers.Name.ToUpper().Contains(search.ToUpper())
                );
            }

            switch (sort_filter.ToLower())
            {
                case "date":
                    switch (sort_order.ToLower())
                    {
                        case "asc":
                            list = list.OrderBy(o => o.SmenaDate);
                            break;

                        case "desc":
                            list = list.OrderByDescending(o => o.SmenaDate);
                            break;
                    }
                    break;

                case "cname":
                    switch (sort_order.ToLower())
                    {
                        case "asc":
                            list = list.OrderBy(o => o.ControlerEmployers.Name);
                            break;

                        case "desc":
                            list = list.OrderByDescending(o => o.ControlerEmployers.Name);
                            break;
                    }
                    break;

                case "dname":
                    switch (sort_order.ToLower())
                    {
                        case "asc":
                            list = list.OrderBy(o => o.DispEmployers.Name);
                            break;

                        case "desc":
                            list = list.OrderByDescending(o => o.DispEmployers.Name);
                            break;
                    }
                    break;
            }

            list_count = await list.CountAsync();
            list = list.Skip((page - 1) * bs.bs_itemsPerPage).Take(bs.bs_itemsPerPage);

            SmenesView model = new SmenesView
            {
                DataItems = await list.ToListAsync(),
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

            var smenes = db.Smenes
                .Include(s => s.ControlerEmployers)
                .Include(s => s.DispEmployers)
                .Where(w => w.GlobalContainersId == cguid);

            return PartialView(model);
        }

        // GET: Smena/Smenes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smenes smenes = await db.Smenes.FindAsync(id);
            if (smenes == null)
            {
                return HttpNotFound();
            }
            return View(smenes);
        }

        // GET: Smena/Smenes/Create
        public ActionResult Create()
        {
            ViewBag.ControlerEmployersId = new SelectList(db.Employers.Where(p => p.Posts.IsControler == true).OrderBy(o => o.FirstName), "Id", "Name");
            ViewBag.DispEmployersId = new SelectList(db.Employers.Where(p => p.Posts.IsDisp == true).OrderBy(o => o.FirstName), "Id", "Name");
            return View();
        }

        // POST: Smena/Smenes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SmenaDate,DispEmployersId,ControlerEmployersId")] Smenes smenes)
        {
            if (ModelState.IsValid)
            {
                db.Smenes.Add(smenes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ControlerEmployersId = new SelectList(db.Employers, "Id", "Name", smenes.ControlerEmployersId);
            ViewBag.DispEmployersId = new SelectList(db.Employers, "Id", "Name", smenes.DispEmployersId);
            return View(smenes);
        }

        // GET: Smena/Smenes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smenes smenes = await db.Smenes.FindAsync(id);
            if (smenes == null)
            {
                return HttpNotFound();
            }
            ViewBag.ControlerEmployersId = new SelectList(db.Employers, "Id", "Name", smenes.ControlerEmployersId);
            ViewBag.DispEmployersId = new SelectList(db.Employers, "Id", "Name", smenes.DispEmployersId);
            return View(smenes);
        }

        // POST: Smena/Smenes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SmenaDate,DispEmployersId,ControlerEmployersId")] Smenes smenes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smenes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ControlerEmployersId = new SelectList(db.Employers, "Id", "Name", smenes.ControlerEmployersId);
            ViewBag.DispEmployersId = new SelectList(db.Employers, "Id", "Name", smenes.DispEmployersId);
            return View(smenes);
        }

        // GET: Smena/Smenes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smenes smenes = await db.Smenes.FindAsync(id);
            if (smenes == null)
            {
                return HttpNotFound();
            }
            return View(smenes);
        }

        // POST: Smena/Smenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Smenes smenes = await db.Smenes.FindAsync(id);
            db.Smenes.Remove(smenes);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
