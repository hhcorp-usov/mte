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
    public class WayBillStatusesController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/WayBillStatuses
        public async Task<ActionResult> Index()
        {
            return View(await db.WayBillStatuses.ToListAsync());
        }

        // GET: Guides/WayBillStatuses/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBillStatuses wayBillStatuses = await db.WayBillStatuses.FindAsync(id);
            if (wayBillStatuses == null)
            {
                return HttpNotFound();
            }
            return View(wayBillStatuses);
        }

        // GET: Guides/WayBillStatuses/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guides/WayBillStatuses/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Step")] WayBillStatuses wayBillStatuses)
        {
            if (ModelState.IsValid)
            {
                db.WayBillStatuses.Add(wayBillStatuses);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(wayBillStatuses);
        }

        // GET: Guides/WayBillStatuses/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBillStatuses wayBillStatuses = await db.WayBillStatuses.FindAsync(id);
            if (wayBillStatuses == null)
            {
                return HttpNotFound();
            }
            return View(wayBillStatuses);
        }

        // POST: Guides/WayBillStatuses/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Step")] WayBillStatuses wayBillStatuses)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wayBillStatuses).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(wayBillStatuses);
        }

        // GET: Guides/WayBillStatuses/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBillStatuses wayBillStatuses = await db.WayBillStatuses.FindAsync(id);
            if (wayBillStatuses == null)
            {
                return HttpNotFound();
            }
            return View(wayBillStatuses);
        }

        // POST: Guides/WayBillStatuses/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WayBillStatuses wayBillStatuses = await db.WayBillStatuses.FindAsync(id);
            db.WayBillStatuses.Remove(wayBillStatuses);
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
