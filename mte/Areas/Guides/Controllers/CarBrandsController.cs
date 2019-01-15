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
    public class CarBrandsController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/CarBrands
        public async Task<ActionResult> Index()
        {
            return View(await db.CarBrands.ToListAsync());
        }

        // GET: Guides/CarBrands/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarBrands carBrands = await db.CarBrands.FindAsync(id);
            if (carBrands == null)
            {
                return HttpNotFound();
            }
            return View(carBrands);
        }

        // GET: Guides/CarBrands/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guides/CarBrands/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] CarBrands carBrands)
        {
            if (ModelState.IsValid)
            {
                db.CarBrands.Add(carBrands);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(carBrands);
        }

        // GET: Guides/CarBrands/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarBrands carBrands = await db.CarBrands.FindAsync(id);
            if (carBrands == null)
            {
                return HttpNotFound();
            }
            return View(carBrands);
        }

        // POST: Guides/CarBrands/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] CarBrands carBrands)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carBrands).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(carBrands);
        }

        // GET: Guides/CarBrands/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarBrands carBrands = await db.CarBrands.FindAsync(id);
            if (carBrands == null)
            {
                return HttpNotFound();
            }
            return View(carBrands);
        }

        // POST: Guides/CarBrands/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CarBrands carBrands = await db.CarBrands.FindAsync(id);
            db.CarBrands.Remove(carBrands);
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
