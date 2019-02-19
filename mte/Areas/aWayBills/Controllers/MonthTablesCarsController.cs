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
    public class MonthTablesCarsController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: aWayBills/MonthTablesCars
        public async Task<ActionResult> Index()
        {
            var monthTablesCars = db.MonthTablesCars.Include(m => m.Cars).Include(m => m.MonthTables);
            return View(await monthTablesCars.ToListAsync());
        }

        // GET: aWayBills/MonthTablesCars/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthTablesCars monthTablesCars = await db.MonthTablesCars.FindAsync(id);
            if (monthTablesCars == null)
            {
                return HttpNotFound();
            }
            return View(monthTablesCars);
        }

        // GET: aWayBills/MonthTablesCars/Create
        public ActionResult Create()
        {
            ViewBag.CarsId = new SelectList(db.Cars, "Id", "Name");
            ViewBag.MonthTablesId = new SelectList(db.MonthTables, "Id", "Name");
            return View();
        }

        // POST: aWayBills/MonthTablesCars/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,GlobalContainersId,MonthTablesId,CarsId")] MonthTablesCars monthTablesCars)
        {
            if (ModelState.IsValid)
            {
                db.MonthTablesCars.Add(monthTablesCars);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CarsId = new SelectList(db.Cars, "Id", "Name", monthTablesCars.CarsId);
            ViewBag.MonthTablesId = new SelectList(db.MonthTables, "Id", "Name", monthTablesCars.MonthTablesId);
            return View(monthTablesCars);
        }

        // GET: aWayBills/MonthTablesCars/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthTablesCars monthTablesCars = await db.MonthTablesCars.FindAsync(id);
            if (monthTablesCars == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarsId = new SelectList(db.Cars, "Id", "Name", monthTablesCars.CarsId);
            ViewBag.MonthTablesId = new SelectList(db.MonthTables, "Id", "Name", monthTablesCars.MonthTablesId);
            return View(monthTablesCars);
        }

        // POST: aWayBills/MonthTablesCars/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,GlobalContainersId,MonthTablesId,CarsId")] MonthTablesCars monthTablesCars)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthTablesCars).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CarsId = new SelectList(db.Cars, "Id", "Name", monthTablesCars.CarsId);
            ViewBag.MonthTablesId = new SelectList(db.MonthTables, "Id", "Name", monthTablesCars.MonthTablesId);
            return View(monthTablesCars);
        }

        // GET: aWayBills/MonthTablesCars/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthTablesCars monthTablesCars = await db.MonthTablesCars.FindAsync(id);
            if (monthTablesCars == null)
            {
                return HttpNotFound();
            }
            return View(monthTablesCars);
        }

        // POST: aWayBills/MonthTablesCars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MonthTablesCars monthTablesCars = await db.MonthTablesCars.FindAsync(id);
            db.MonthTablesCars.Remove(monthTablesCars);
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
