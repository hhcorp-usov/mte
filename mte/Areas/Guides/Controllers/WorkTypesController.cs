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
    public class WorkTypesController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/WorkTypes
        public async Task<ActionResult> Index()
        {
            return View(await db.WorkTypes.ToListAsync());
        }

        // GET: Guides/WorkTypes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkTypes workTypes = await db.WorkTypes.FindAsync(id);
            if (workTypes == null)
            {
                return HttpNotFound();
            }
            return View(workTypes);
        }

        // GET: Guides/WorkTypes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guides/WorkTypes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,ShortName,IsNullRun,IsWork,IsDinner,IsBreak")] WorkTypes workTypes)
        {
            if (ModelState.IsValid)
            {
                db.WorkTypes.Add(workTypes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(workTypes);
        }

        // GET: Guides/WorkTypes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkTypes workTypes = await db.WorkTypes.FindAsync(id);
            if (workTypes == null)
            {
                return HttpNotFound();
            }
            return View(workTypes);
        }

        // POST: Guides/WorkTypes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,ShortName,IsNullRun,IsWork,IsDinner,IsBreak")] WorkTypes workTypes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(workTypes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(workTypes);
        }

        // GET: Guides/WorkTypes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            WorkTypes workTypes = await db.WorkTypes.FindAsync(id);
            if (workTypes == null)
            {
                return HttpNotFound();
            }
            return View(workTypes);
        }

        // POST: Guides/WorkTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            WorkTypes workTypes = await db.WorkTypes.FindAsync(id);
            db.WorkTypes.Remove(workTypes);
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
