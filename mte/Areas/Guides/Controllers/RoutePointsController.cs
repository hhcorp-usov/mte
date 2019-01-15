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
    public class RoutePointsController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/RoutePoints
        public async Task<ActionResult> Index()
        {
            var routePoints = db.RoutePoints.Include(r => r.Points).Include(r => r.Routes);
            return View(await routePoints.ToListAsync());
        }

        // GET: Guides/RoutePoints/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoutePoints routePoints = await db.RoutePoints.FindAsync(id);
            if (routePoints == null)
            {
                return HttpNotFound();
            }
            return View(routePoints);
        }

        // GET: Guides/RoutePoints/Create
        public ActionResult Create()
        {
            ViewBag.PointsId = new SelectList(db.Points, "Id", "Name");
            ViewBag.RoutesId = new SelectList(db.Routes, "Id", "Name");
            return View();
        }

        // POST: Guides/RoutePoints/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,RoutesId,PointsId,RLength,RTime,IsBack")] RoutePoints routePoints)
        {
            if (ModelState.IsValid)
            {
                db.RoutePoints.Add(routePoints);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.PointsId = new SelectList(db.Points, "Id", "Name", routePoints.PointsId);
            ViewBag.RoutesId = new SelectList(db.Routes, "Id", "Name", routePoints.RoutesId);
            return View(routePoints);
        }

        // GET: Guides/RoutePoints/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoutePoints routePoints = await db.RoutePoints.FindAsync(id);
            if (routePoints == null)
            {
                return HttpNotFound();
            }
            ViewBag.PointsId = new SelectList(db.Points, "Id", "Name", routePoints.PointsId);
            ViewBag.RoutesId = new SelectList(db.Routes, "Id", "Name", routePoints.RoutesId);
            return View(routePoints);
        }

        // POST: Guides/RoutePoints/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,RoutesId,PointsId,RLength,RTime,IsBack")] RoutePoints routePoints)
        {
            if (ModelState.IsValid)
            {
                db.Entry(routePoints).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.PointsId = new SelectList(db.Points, "Id", "Name", routePoints.PointsId);
            ViewBag.RoutesId = new SelectList(db.Routes, "Id", "Name", routePoints.RoutesId);
            return View(routePoints);
        }

        // GET: Guides/RoutePoints/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RoutePoints routePoints = await db.RoutePoints.FindAsync(id);
            if (routePoints == null)
            {
                return HttpNotFound();
            }
            return View(routePoints);
        }

        // POST: Guides/RoutePoints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RoutePoints routePoints = await db.RoutePoints.FindAsync(id);
            db.RoutePoints.Remove(routePoints);
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
