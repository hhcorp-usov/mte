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
    public class RoutesController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/Routes
        public async Task<ActionResult> Index()
        {
            var routes = db.Routes.Include(r => r.Enterprises).Include(r => r.PointStart).Include(r => r.PointStop).Include(r => r.RouteTypes);
            return View(await routes.ToListAsync());
        }

        // GET: Guides/Routes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = await db.Routes.FindAsync(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // GET: Guides/Routes/Create
        public ActionResult Create()
        {
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name");
            ViewBag.PointStartId = new SelectList(db.Points, "Id", "Name");
            ViewBag.PointStopId = new SelectList(db.Points, "Id", "Name");
            ViewBag.RouteTypesId = new SelectList(db.RouteTypes, "Id", "Name");
            return View();
        }

        // POST: Guides/Routes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,BackName,RNumber,EnterprisesId,RouteTypesId,PointStartId,PointStopId")] Routes routes)
        {
            if (ModelState.IsValid)
            {
                db.Routes.Add(routes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", routes.EnterprisesId);
            ViewBag.PointStartId = new SelectList(db.Points, "Id", "Name", routes.PointStartId);
            ViewBag.PointStopId = new SelectList(db.Points, "Id", "Name", routes.PointStopId);
            ViewBag.RouteTypesId = new SelectList(db.RouteTypes, "Id", "Name", routes.RouteTypesId);
            return View(routes);
        }

        // GET: Guides/Routes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = await db.Routes.FindAsync(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", routes.EnterprisesId);
            ViewBag.PointStartId = new SelectList(db.Points, "Id", "Name", routes.PointStartId);
            ViewBag.PointStopId = new SelectList(db.Points, "Id", "Name", routes.PointStopId);
            ViewBag.RouteTypesId = new SelectList(db.RouteTypes, "Id", "Name", routes.RouteTypesId);
            return View(routes);
        }

        // POST: Guides/Routes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,BackName,RNumber,EnterprisesId,RouteTypesId,PointStartId,PointStopId")] Routes routes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(routes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", routes.EnterprisesId);
            ViewBag.PointStartId = new SelectList(db.Points, "Id", "Name", routes.PointStartId);
            ViewBag.PointStopId = new SelectList(db.Points, "Id", "Name", routes.PointStopId);
            ViewBag.RouteTypesId = new SelectList(db.RouteTypes, "Id", "Name", routes.RouteTypesId);
            return View(routes);
        }

        // GET: Guides/Routes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Routes routes = await db.Routes.FindAsync(id);
            if (routes == null)
            {
                return HttpNotFound();
            }
            return View(routes);
        }

        // POST: Guides/Routes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Routes routes = await db.Routes.FindAsync(id);
            db.Routes.Remove(routes);
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
