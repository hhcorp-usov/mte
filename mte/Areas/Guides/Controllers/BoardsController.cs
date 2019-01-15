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
    public class BoardsController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/Boards
        public async Task<ActionResult> Index()
        {
            var boards = db.Boards.Include(b => b.Enterprises);
            return View(await boards.ToListAsync());
        }

        // GET: Guides/Boards/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boards boards = await db.Boards.FindAsync(id);
            if (boards == null)
            {
                return HttpNotFound();
            }
            return View(boards);
        }

        // GET: Guides/Boards/Create
        public ActionResult Create()
        {
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name");
            return View();
        }

        // POST: Guides/Boards/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,DateBegin,DateEnd,WeekDayWorks,EnterprisesId")] Boards boards)
        {
            if (ModelState.IsValid)
            {
                db.Boards.Add(boards);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", boards.EnterprisesId);
            return View(boards);
        }

        // GET: Guides/Boards/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boards boards = await db.Boards.FindAsync(id);
            if (boards == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", boards.EnterprisesId);
            return View(boards);
        }

        // POST: Guides/Boards/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,DateBegin,DateEnd,WeekDayWorks,EnterprisesId")] Boards boards)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boards).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", boards.EnterprisesId);
            return View(boards);
        }

        // GET: Guides/Boards/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Boards boards = await db.Boards.FindAsync(id);
            if (boards == null)
            {
                return HttpNotFound();
            }
            return View(boards);
        }

        // POST: Guides/Boards/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Boards boards = await db.Boards.FindAsync(id);
            db.Boards.Remove(boards);
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
