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
    public class CarTypesController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/CarTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.CarTypes.ToListAsync());
        }

        // GET: Guides/CarTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarTypes carTypes = await db.CarTypes.FindAsync(id);
            if (carTypes == null)
            {
                return HttpNotFound();
            }
            return View(carTypes);
        }

        // GET: Guides/CarTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guides/CarTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] CarTypes carTypes)
        {
            if (ModelState.IsValid)
            {
                db.CarTypes.Add(carTypes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(carTypes);
        }

        // GET: Guides/CarTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarTypes carTypes = await db.CarTypes.FindAsync(id);
            if (carTypes == null)
            {
                return HttpNotFound();
            }
            return View(carTypes);
        }

        // POST: Guides/CarTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] CarTypes carTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(carTypes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(carTypes);
        }

        // GET: Guides/CarTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CarTypes carTypes = await db.CarTypes.FindAsync(id);
            if (carTypes == null)
            {
                return HttpNotFound();
            }
            return View(carTypes);
        }

        // POST: Guides/CarTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            CarTypes carTypes = await db.CarTypes.FindAsync(id);
            db.CarTypes.Remove(carTypes);
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
