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

namespace mte.Areas.Smena.Controllers
{
    public class SmenesController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Smena/Smenes
        public async Task<ActionResult> Index()
        {
            var smenes = db.Smenes.Include(s => s.ControlerEmployers).Include(s => s.DispEmployers);
            return View(await smenes.ToListAsync());
        }

        // GET: Smena/Smenes/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smenes smenes = await db.Smenes.FindAsync(id);
            if (smenes == null)
            {
                return HttpNotFound();
            }
            return View(smenes);
        }

        // GET: Smena/Smenes/Create
        public ActionResult Create()
        {
            ViewBag.ControlerEmployersId = new SelectList(db.Employers.Where(p => p.Posts.IsControler == true).OrderBy(o => o.FirstName), "Id", "Name");
            ViewBag.DispEmployersId = new SelectList(db.Employers.Where(p => p.Posts.IsDisp == true).OrderBy(o => o.FirstName), "Id", "Name");
            return View();
        }

        // POST: Smena/Smenes/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,SmenaDate,DispEmployersId,ControlerEmployersId")] Smenes smenes)
        {
            if (ModelState.IsValid)
            {
                db.Smenes.Add(smenes);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ControlerEmployersId = new SelectList(db.Employers, "Id", "Name", smenes.ControlerEmployersId);
            ViewBag.DispEmployersId = new SelectList(db.Employers, "Id", "Name", smenes.DispEmployersId);
            return View(smenes);
        }

        // GET: Smena/Smenes/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smenes smenes = await db.Smenes.FindAsync(id);
            if (smenes == null)
            {
                return HttpNotFound();
            }
            ViewBag.ControlerEmployersId = new SelectList(db.Employers, "Id", "Name", smenes.ControlerEmployersId);
            ViewBag.DispEmployersId = new SelectList(db.Employers, "Id", "Name", smenes.DispEmployersId);
            return View(smenes);
        }

        // POST: Smena/Smenes/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,SmenaDate,DispEmployersId,ControlerEmployersId")] Smenes smenes)
        {
            if (ModelState.IsValid)
            {
                db.Entry(smenes).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ControlerEmployersId = new SelectList(db.Employers, "Id", "Name", smenes.ControlerEmployersId);
            ViewBag.DispEmployersId = new SelectList(db.Employers, "Id", "Name", smenes.DispEmployersId);
            return View(smenes);
        }

        // GET: Smena/Smenes/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Smenes smenes = await db.Smenes.FindAsync(id);
            if (smenes == null)
            {
                return HttpNotFound();
            }
            return View(smenes);
        }

        // POST: Smena/Smenes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Smenes smenes = await db.Smenes.FindAsync(id);
            db.Smenes.Remove(smenes);
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
