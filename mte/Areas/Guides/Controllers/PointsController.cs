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
    public class PointsController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/Points
        public async Task<ActionResult> Index()
        {
            var points = db.Points.Include(p => p.PointTypes);
            return View(await points.ToListAsync());
        }

        // GET: Guides/Points/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Points points = await db.Points.FindAsync(id);
            if (points == null)
            {
                return HttpNotFound();
            }
            return View(points);
        }

        // GET: Guides/Points/Create
        public ActionResult Create()
        {
            ViewBag.PointTypesId = new SelectList(db.PointTypes, "Id", "Name");
            return View();
        }

        // POST: Guides/Points/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,PointTypesId")] Points points)
        {
            if (ModelState.IsValid)
            {
                db.Points.Add(points);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PointTypesId = new SelectList(db.PointTypes, "Id", "Name", points.PointTypesId);
            return View(points);
        }

        // GET: Guides/Points/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Points points = await db.Points.FindAsync(id);
            if (points == null)
            {
                return HttpNotFound();
            }
            ViewBag.PointTypesId = new SelectList(db.PointTypes, "Id", "Name", points.PointTypesId);
            return View(points);
        }

        // POST: Guides/Points/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,PointTypesId")] Points points)
        {
            if (ModelState.IsValid)
            {
                db.Entry(points).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PointTypesId = new SelectList(db.PointTypes, "Id", "Name", points.PointTypesId);
            return View(points);
        }

        // GET: Guides/Points/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Points points = await db.Points.FindAsync(id);
            if (points == null)
            {
                return HttpNotFound();
            }
            return View(points);
        }

        // POST: Guides/Points/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Points points = await db.Points.FindAsync(id);
            db.Points.Remove(points);
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
