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

namespace mte.Areas.Guides.Controllers
{
    public class CarsController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/Cars
        public async Task<ActionResult> Index()
        {
            var cars = db.Cars.Include(c => c.CarBrands).Include(c => c.CarTypes).Include(c => c.Enterprises);
            return View(await cars.ToListAsync());
        }

        // GET: Guides/Cars/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = await db.Cars.FindAsync(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // GET: Guides/Cars/Create
        public ActionResult Create()
        {
            ViewBag.CarBrandsId = new SelectList(db.CarBrands, "Id", "Name");
            ViewBag.CarTypesId = new SelectList(db.CarTypes, "Id", "Name");
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name");
            return View();
        }

        // POST: Guides/Cars/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,PIdent,BIdent,EnterprisesId,CarBrandsId,CarTypesId")] Cars cars)
        {
            if (ModelState.IsValid)
            {
                db.Cars.Add(cars);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CarBrandsId = new SelectList(db.CarBrands, "Id", "Name", cars.CarBrandsId);
            ViewBag.CarTypesId = new SelectList(db.CarTypes, "Id", "Name", cars.CarTypesId);
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", cars.EnterprisesId);
            return View(cars);
        }

        // GET: Guides/Cars/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = await db.Cars.FindAsync(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarBrandsId = new SelectList(db.CarBrands, "Id", "Name", cars.CarBrandsId);
            ViewBag.CarTypesId = new SelectList(db.CarTypes, "Id", "Name", cars.CarTypesId);
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", cars.EnterprisesId);
            return View(cars);
        }

        // POST: Guides/Cars/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,PIdent,BIdent,EnterprisesId,CarBrandsId,CarTypesId")] Cars cars)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cars).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CarBrandsId = new SelectList(db.CarBrands, "Id", "Name", cars.CarBrandsId);
            ViewBag.CarTypesId = new SelectList(db.CarTypes, "Id", "Name", cars.CarTypesId);
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", cars.EnterprisesId);
            return View(cars);
        }

        // GET: Guides/Cars/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cars cars = await db.Cars.FindAsync(id);
            if (cars == null)
            {
                return HttpNotFound();
            }
            return View(cars);
        }

        // POST: Guides/Cars/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Cars cars = await db.Cars.FindAsync(id);
            db.Cars.Remove(cars);
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
