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
    public class PointTypesController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/PointTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.PointTypes.ToListAsync());
        }

        // GET: Guides/PointTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PointTypes pointTypes = await db.PointTypes.FindAsync(id);
            if (pointTypes == null)
            {
                return HttpNotFound();
            }
            return View(pointTypes);
        }

        // GET: Guides/PointTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guides/PointTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,ShortName")] PointTypes pointTypes)
        {
            if (ModelState.IsValid)
            {
                db.PointTypes.Add(pointTypes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(pointTypes);
        }

        // GET: Guides/PointTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PointTypes pointTypes = await db.PointTypes.FindAsync(id);
            if (pointTypes == null)
            {
                return HttpNotFound();
            }
            return View(pointTypes);
        }

        // POST: Guides/PointTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ShortName")] PointTypes pointTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pointTypes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(pointTypes);
        }

        // GET: Guides/PointTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PointTypes pointTypes = await db.PointTypes.FindAsync(id);
            if (pointTypes == null)
            {
                return HttpNotFound();
            }
            return View(pointTypes);
        }

        // POST: Guides/PointTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            PointTypes pointTypes = await db.PointTypes.FindAsync(id);
            db.PointTypes.Remove(pointTypes);
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
