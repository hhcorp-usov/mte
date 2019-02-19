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
    public class MonthTablesTeamsController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: aWayBills/MonthTablesTeams
        public async Task<ActionResult> Index()
        {
            var monthTablesTeams = db.MonthTablesTeams.Include(m => m.Employers).Include(m => m.MonthTablesCars);
            return View(await monthTablesTeams.ToListAsync());
        }

        // GET: aWayBills/MonthTablesTeams/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthTablesTeams monthTablesTeams = await db.MonthTablesTeams.FindAsync(id);
            if (monthTablesTeams == null)
            {
                return HttpNotFound();
            }
            return View(monthTablesTeams);
        }

        // GET: aWayBills/MonthTablesTeams/Create
        public ActionResult Create()
        {
            ViewBag.EmployersId = new SelectList(db.Employers, "Id", "Name");
            ViewBag.MonthTablesCarsId = new SelectList(db.MonthTablesCars, "Id", "Id");
            return View();
        }

        // POST: aWayBills/MonthTablesTeams/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,GlobalContainersId,MonthTablesCarsId,EmployersId,NumberShift,D1_RIdent,D1_REIdent,D2_RIdent,D2_REIdent,D3_RIdent,D3_REIdent,D4_RIdent,D4_REIdent,D5_RIdent,D5_REIdent,D6_RIdent,D6_REIdent,D7_RIdent,D7_REIdent,D8_RIdent,D8_REIdent,D9_RIdent,D9_REIdent,D10_RIdent,D10_REIdent,D11_RIdent,D11_REIdent,D12_RIdent,D12_REIdent,D13_RIdent,D13_REIdent,D14_RIdent,D14_REIdent,D15_RIdent,D15_REIdent,D16_RIdent,D16_REIdent,D17_RIdent,D17_REIdent,D18_RIdent,D18_REIdent,D19_RIdent,D19_REIdent,D20_RIdent,D20_REIdent,D21_RIdent,D21_REIdent,D22_RIdent,D22_REIdent,D23_RIdent,D23_REIdent,D24_RIdent,D24_REIdent,D25_RIdent,D25_REIdent,D26_RIdent,D26_REIdent,D27_RIdent,D27_REIdent,D28_RIdent,D28_REIdent,D29_RIdent,D29_REIdent,D30_RIdent,D30_REIdent,D31_RIdent,D31_REIdent")] MonthTablesTeams monthTablesTeams)
        {
            if (ModelState.IsValid)
            {
                db.MonthTablesTeams.Add(monthTablesTeams);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EmployersId = new SelectList(db.Employers, "Id", "Name", monthTablesTeams.EmployersId);
            ViewBag.MonthTablesCarsId = new SelectList(db.MonthTablesCars, "Id", "Id", monthTablesTeams.MonthTablesCarsId);
            return View(monthTablesTeams);
        }

        // GET: aWayBills/MonthTablesTeams/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthTablesTeams monthTablesTeams = await db.MonthTablesTeams.FindAsync(id);
            if (monthTablesTeams == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployersId = new SelectList(db.Employers, "Id", "Name", monthTablesTeams.EmployersId);
            ViewBag.MonthTablesCarsId = new SelectList(db.MonthTablesCars, "Id", "Id", monthTablesTeams.MonthTablesCarsId);
            return View(monthTablesTeams);
        }

        // POST: aWayBills/MonthTablesTeams/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,GlobalContainersId,MonthTablesCarsId,EmployersId,NumberShift,D1_RIdent,D1_REIdent,D2_RIdent,D2_REIdent,D3_RIdent,D3_REIdent,D4_RIdent,D4_REIdent,D5_RIdent,D5_REIdent,D6_RIdent,D6_REIdent,D7_RIdent,D7_REIdent,D8_RIdent,D8_REIdent,D9_RIdent,D9_REIdent,D10_RIdent,D10_REIdent,D11_RIdent,D11_REIdent,D12_RIdent,D12_REIdent,D13_RIdent,D13_REIdent,D14_RIdent,D14_REIdent,D15_RIdent,D15_REIdent,D16_RIdent,D16_REIdent,D17_RIdent,D17_REIdent,D18_RIdent,D18_REIdent,D19_RIdent,D19_REIdent,D20_RIdent,D20_REIdent,D21_RIdent,D21_REIdent,D22_RIdent,D22_REIdent,D23_RIdent,D23_REIdent,D24_RIdent,D24_REIdent,D25_RIdent,D25_REIdent,D26_RIdent,D26_REIdent,D27_RIdent,D27_REIdent,D28_RIdent,D28_REIdent,D29_RIdent,D29_REIdent,D30_RIdent,D30_REIdent,D31_RIdent,D31_REIdent")] MonthTablesTeams monthTablesTeams)
        {
            if (ModelState.IsValid)
            {
                db.Entry(monthTablesTeams).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EmployersId = new SelectList(db.Employers, "Id", "Name", monthTablesTeams.EmployersId);
            ViewBag.MonthTablesCarsId = new SelectList(db.MonthTablesCars, "Id", "Id", monthTablesTeams.MonthTablesCarsId);
            return View(monthTablesTeams);
        }

        // GET: aWayBills/MonthTablesTeams/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MonthTablesTeams monthTablesTeams = await db.MonthTablesTeams.FindAsync(id);
            if (monthTablesTeams == null)
            {
                return HttpNotFound();
            }
            return View(monthTablesTeams);
        }

        // POST: aWayBills/MonthTablesTeams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            MonthTablesTeams monthTablesTeams = await db.MonthTablesTeams.FindAsync(id);
            db.MonthTablesTeams.Remove(monthTablesTeams);
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
