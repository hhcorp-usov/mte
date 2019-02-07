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
    public class WayBillsController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: aWayBills/WayBills
        public async Task<ActionResult> Index()
        {
            var wayBills = db.WayBills.Include(w => w.Cars).Include(w => w.Enterprises).Include(w => w.WayBillStatuses);
            return View(await wayBills.ToListAsync());
        }

        // GET: aWayBills/WayBills/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBills wayBills = await db.WayBills.FindAsync(id);
            if (wayBills == null)
            {
                return HttpNotFound();
            }
            return View(wayBills);
        }

        // GET: aWayBills/WayBills/Create
        public ActionResult Create()
        {
            ViewBag.CarsId = new SelectList(db.Cars, "Id", "Name");
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name");
            ViewBag.WayBillStatusesId = new SelectList(db.WayBillStatuses, "Id", "Name");
            return View();
        }

        // POST: aWayBills/WayBills/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,WNumber,DateAdd,DateClose,EnterprisesId,WayBillStatusesId,CarsId,OdometrStart,OdometrStop")] WayBills wayBills)
        {
            if (ModelState.IsValid)
            {
                db.WayBills.Add(wayBills);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.CarsId = new SelectList(db.Cars, "Id", "Name", wayBills.CarsId);
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", wayBills.EnterprisesId);
            ViewBag.WayBillStatusesId = new SelectList(db.WayBillStatuses, "Id", "Name", wayBills.WayBillStatusesId);
            return View(wayBills);
        }

        // GET: aWayBills/WayBills/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBills wayBills = await db.WayBills.FindAsync(id);
            if (wayBills == null)
            {
                return HttpNotFound();
            }
            ViewBag.CarsId = new SelectList(db.Cars, "Id", "Name", wayBills.CarsId);
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", wayBills.EnterprisesId);
            ViewBag.WayBillStatusesId = new SelectList(db.WayBillStatuses, "Id", "Name", wayBills.WayBillStatusesId);
            return View(wayBills);
        }

        // POST: aWayBills/WayBills/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,WNumber,DateAdd,DateClose,EnterprisesId,WayBillStatusesId,CarsId,OdometrStart,OdometrStop")] WayBills wayBills)
        {
            if (ModelState.IsValid)
            {
                db.Entry(wayBills).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.CarsId = new SelectList(db.Cars, "Id", "Name", wayBills.CarsId);
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", wayBills.EnterprisesId);
            ViewBag.WayBillStatusesId = new SelectList(db.WayBillStatuses, "Id", "Name", wayBills.WayBillStatusesId);
            return View(wayBills);
        }

        // GET: aWayBills/WayBills/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBills wayBills = await db.WayBills.FindAsync(id);
            if (wayBills == null)
            {
                return HttpNotFound();
            }
            return View(wayBills);
        }

        // POST: aWayBills/WayBills/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WayBills wayBills = await db.WayBills.FindAsync(id);
            db.WayBills.Remove(wayBills);
            await db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        // GET: aWayBills/WayBills/Print/5
        public async Task<ActionResult> Print(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WayBills wayBills = await db.WayBills.FindAsync(id);
            if (wayBills == null)
            {
                return HttpNotFound();
            }
            ViewBag.CurrentSmena = db.Smenes.Where(w => w.SmenaDate <= DateTime.Now).First();
            ViewBag.CarsId = new SelectList(db.Cars, "Id", "Name", wayBills.CarsId);
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", wayBills.EnterprisesId);
            ViewBag.WayBillStatusesId = new SelectList(db.WayBillStatuses, "Id", "Name", wayBills.WayBillStatusesId);

            //return View(wayBills);

            return new Rotativa.ViewAsPdf(wayBills)
            {
                PageMargins = new Rotativa.Options.Margins(5, 5, 5, 5),
                PageSize = Rotativa.Options.Size.A4,
                PageOrientation = Rotativa.Options.Orientation.Landscape
            };
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