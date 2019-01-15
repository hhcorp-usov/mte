using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using mte.Models;

namespace mte.Areas.aWayBills.Controllers
{
    public class WayBillTeamsController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: WayBills/WayBillTeams
        public async Task<ActionResult> Index()
        {
            var wayBillTeams = db.WayBillTeams.Include(w => w.Employers).Include(w => w.WayBills);
            return View(await wayBillTeams.ToListAsync());
        }

        // GET: WayBills/WayBillTeams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBillTeams wayBillTeams = await db.WayBillTeams.FindAsync(id);
            if (wayBillTeams == null)
            {
                return HttpNotFound();
            }
            return View(wayBillTeams);
        }

        // GET: WayBills/WayBillTeams/Create
        public ActionResult Create()
        {
            ViewBag.EmployersId = new SelectList(db.Employers, "Id", "Name");
            ViewBag.WayBillsId = new SelectList(db.WayBills, "Id", "WNumber");
            return View();
        }

        // POST: WayBills/WayBillTeams/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,WayBillsId,EmployersId,NumberShift")] WayBillTeams wayBillTeams)
        {
            if (ModelState.IsValid)
            {
                db.WayBillTeams.Add(wayBillTeams);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployersId = new SelectList(db.Employers, "Id", "Name", wayBillTeams.EmployersId);
            ViewBag.WayBillsId = new SelectList(db.WayBills, "Id", "WNumber", wayBillTeams.WayBillsId);
            return View(wayBillTeams);
        }

        // GET: WayBills/WayBillTeams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBillTeams wayBillTeams = await db.WayBillTeams.FindAsync(id);
            if (wayBillTeams == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployersId = new SelectList(db.Employers, "Id", "Name", wayBillTeams.EmployersId);
            ViewBag.WayBillsId = new SelectList(db.WayBills, "Id", "WNumber", wayBillTeams.WayBillsId);
            return View(wayBillTeams);
        }

        // POST: WayBills/WayBillTeams/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,WayBillsId,EmployersId,NumberShift")] WayBillTeams wayBillTeams)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wayBillTeams).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EmployersId = new SelectList(db.Employers, "Id", "Name", wayBillTeams.EmployersId);
            ViewBag.WayBillsId = new SelectList(db.WayBills, "Id", "WNumber", wayBillTeams.WayBillsId);
            return View(wayBillTeams);
        }

        // GET: WayBills/WayBillTeams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBillTeams wayBillTeams = await db.WayBillTeams.FindAsync(id);
            if (wayBillTeams == null)
            {
                return HttpNotFound();
            }
            return View(wayBillTeams);
        }

        // POST: WayBills/WayBillTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WayBillTeams wayBillTeams = await db.WayBillTeams.FindAsync(id);
            db.WayBillTeams.Remove(wayBillTeams);
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
