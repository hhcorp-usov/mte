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
    public class MonthTablesController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: aWayBills/MonthTables
        public async Task<ActionResult> Index()
        {
            return View(await db.MonthTables.ToListAsync());
        }

        // GET: aWayBills/MonthTables/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthTables monthTables = await db.MonthTables.FindAsync(id);
            if (monthTables == null)
            {
                return HttpNotFound();
            }
            return View(monthTables);
        }

        // GET: aWayBills/MonthTables/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: aWayBills/MonthTables/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,GlobalContainersId,Name,Month,DateBegin,DateEnd")] MonthTables monthTables)
        {
            if (ModelState.IsValid)
            {
                db.MonthTables.Add(monthTables);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(monthTables);
        }

        // GET: aWayBills/MonthTables/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthTables monthTables = await db.MonthTables.FindAsync(id);
            if (monthTables == null)
            {
                return HttpNotFound();
            }
            return View(monthTables);
        }

        // POST: aWayBills/MonthTables/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,GlobalContainersId,Name,Month,DateBegin,DateEnd")] MonthTables monthTables)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthTables).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(monthTables);
        }

        // GET: aWayBills/MonthTables/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthTables monthTables = await db.MonthTables.FindAsync(id);
            if (monthTables == null)
            {
                return HttpNotFound();
            }
            return View(monthTables);
        }

        // POST: aWayBills/MonthTables/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MonthTables monthTables = await db.MonthTables.FindAsync(id);
            db.MonthTables.Remove(monthTables);
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
