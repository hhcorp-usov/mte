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
    public class RouteTypesController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/RouteTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.RouteTypes.ToListAsync());
        }

        // GET: Guides/RouteTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteTypes routeTypes = await db.RouteTypes.FindAsync(id);
            if (routeTypes == null)
            {
                return HttpNotFound();
            }
            return View(routeTypes);
        }

        // GET: Guides/RouteTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guides/RouteTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name")] RouteTypes routeTypes)
        {
            if (ModelState.IsValid)
            {
                db.RouteTypes.Add(routeTypes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(routeTypes);
        }

        // GET: Guides/RouteTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteTypes routeTypes = await db.RouteTypes.FindAsync(id);
            if (routeTypes == null)
            {
                return HttpNotFound();
            }
            return View(routeTypes);
        }

        // POST: Guides/RouteTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name")] RouteTypes routeTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(routeTypes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(routeTypes);
        }

        // GET: Guides/RouteTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RouteTypes routeTypes = await db.RouteTypes.FindAsync(id);
            if (routeTypes == null)
            {
                return HttpNotFound();
            }
            return View(routeTypes);
        }

        // POST: Guides/RouteTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            RouteTypes routeTypes = await db.RouteTypes.FindAsync(id);
            db.RouteTypes.Remove(routeTypes);
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
