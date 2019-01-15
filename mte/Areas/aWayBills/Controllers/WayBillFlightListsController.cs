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
    public class WayBillFlightListsController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: WayBills/WayBillFlightLists
        public async Task<ActionResult> Index()
        {
            var wayBillFlightLists = db.WayBillFlightLists.Include(w => w.Routes).Include(w => w.WayBills).Include(w => w.WorkTypes);
            return View(await wayBillFlightLists.ToListAsync());
        }

        // GET: WayBills/WayBillFlightLists/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBillFlightLists wayBillFlightLists = await db.WayBillFlightLists.FindAsync(id);
            if (wayBillFlightLists == null)
            {
                return HttpNotFound();
            }
            return View(wayBillFlightLists);
        }

        // GET: WayBills/WayBillFlightLists/Create
        public ActionResult Create()
        {
            ViewBag.RoutesId = new SelectList(db.Routes, "Id", "Name");
            ViewBag.WayBillsId = new SelectList(db.WayBills, "Id", "WNumber");
            ViewBag.WorkTypesId = new SelectList(db.WorkTypes, "Id", "Name");
            return View();
        }

        // POST: WayBills/WayBillFlightLists/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,WayBillsId,NumberShift,TimeBegin,TimeEnd,WorkTypesId,RoutesId,RLength,IsBack")] WayBillFlightLists wayBillFlightLists)
        {
            if (ModelState.IsValid)
            {
                db.WayBillFlightLists.Add(wayBillFlightLists);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.RoutesId = new SelectList(db.Routes, "Id", "Name", wayBillFlightLists.RoutesId);
            ViewBag.WayBillsId = new SelectList(db.WayBills, "Id", "WNumber", wayBillFlightLists.WayBillsId);
            ViewBag.WorkTypesId = new SelectList(db.WorkTypes, "Id", "Name", wayBillFlightLists.WorkTypesId);
            return View(wayBillFlightLists);
        }

        // GET: WayBills/WayBillFlightLists/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBillFlightLists wayBillFlightLists = await db.WayBillFlightLists.FindAsync(id);
            if (wayBillFlightLists == null)
            {
                return HttpNotFound();
            }
            ViewBag.RoutesId = new SelectList(db.Routes, "Id", "Name", wayBillFlightLists.RoutesId);
            ViewBag.WayBillsId = new SelectList(db.WayBills, "Id", "WNumber", wayBillFlightLists.WayBillsId);
            ViewBag.WorkTypesId = new SelectList(db.WorkTypes, "Id", "Name", wayBillFlightLists.WorkTypesId);
            return View(wayBillFlightLists);
        }

        // POST: WayBills/WayBillFlightLists/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,WayBillsId,NumberShift,TimeBegin,TimeEnd,WorkTypesId,RoutesId,RLength,IsBack")] WayBillFlightLists wayBillFlightLists)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wayBillFlightLists).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.RoutesId = new SelectList(db.Routes, "Id", "Name", wayBillFlightLists.RoutesId);
            ViewBag.WayBillsId = new SelectList(db.WayBills, "Id", "WNumber", wayBillFlightLists.WayBillsId);
            ViewBag.WorkTypesId = new SelectList(db.WorkTypes, "Id", "Name", wayBillFlightLists.WorkTypesId);
            return View(wayBillFlightLists);
        }

        // GET: WayBills/WayBillFlightLists/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBillFlightLists wayBillFlightLists = await db.WayBillFlightLists.FindAsync(id);
            if (wayBillFlightLists == null)
            {
                return HttpNotFound();
            }
            return View(wayBillFlightLists);
        }

        // POST: WayBills/WayBillFlightLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WayBillFlightLists wayBillFlightLists = await db.WayBillFlightLists.FindAsync(id);
            db.WayBillFlightLists.Remove(wayBillFlightLists);
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
