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
    public class BoardFlightListsController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/BoardFlightLists
        public async Task<ActionResult> Index()
        {
            var boardFlightLists = db.BoardFlightLists.Include(b => b.Boards).Include(b => b.Routes).Include(b => b.WorkTypes);
            return View(await boardFlightLists.ToListAsync());
        }

        // GET: Guides/BoardFlightLists/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardFlightLists boardFlightLists = await db.BoardFlightLists.FindAsync(id);
            if (boardFlightLists == null)
            {
                return HttpNotFound();
            }
            return View(boardFlightLists);
        }

        // GET: Guides/BoardFlightLists/Create
        public ActionResult Create()
        {
            ViewBag.BoardsId = new SelectList(db.Boards, "Id", "Name");
            ViewBag.RoutesId = new SelectList(db.Routes, "Id", "Name");
            ViewBag.WorkTypesId = new SelectList(db.WorkTypes, "Id", "Name");
            return View();
        }

        // POST: Guides/BoardFlightLists/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,BoardsId,NumberShift,TimeBegin,TimeEnd,WorkTypesId,RoutesId,RLength,IsBack")] BoardFlightLists boardFlightLists)
        {
            if (ModelState.IsValid)
            {
                db.BoardFlightLists.Add(boardFlightLists);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.BoardsId = new SelectList(db.Boards, "Id", "Name", boardFlightLists.BoardsId);
            ViewBag.RoutesId = new SelectList(db.Routes, "Id", "Name", boardFlightLists.RoutesId);
            ViewBag.WorkTypesId = new SelectList(db.WorkTypes, "Id", "Name", boardFlightLists.WorkTypesId);
            return View(boardFlightLists);
        }

        // GET: Guides/BoardFlightLists/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardFlightLists boardFlightLists = await db.BoardFlightLists.FindAsync(id);
            if (boardFlightLists == null)
            {
                return HttpNotFound();
            }
            ViewBag.BoardsId = new SelectList(db.Boards, "Id", "Name", boardFlightLists.BoardsId);
            ViewBag.RoutesId = new SelectList(db.Routes, "Id", "Name", boardFlightLists.RoutesId);
            ViewBag.WorkTypesId = new SelectList(db.WorkTypes, "Id", "Name", boardFlightLists.WorkTypesId);
            return View(boardFlightLists);
        }

        // POST: Guides/BoardFlightLists/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,BoardsId,NumberShift,TimeBegin,TimeEnd,WorkTypesId,RoutesId,RLength,IsBack")] BoardFlightLists boardFlightLists)
        {
            if (ModelState.IsValid)
            {
                db.Entry(boardFlightLists).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.BoardsId = new SelectList(db.Boards, "Id", "Name", boardFlightLists.BoardsId);
            ViewBag.RoutesId = new SelectList(db.Routes, "Id", "Name", boardFlightLists.RoutesId);
            ViewBag.WorkTypesId = new SelectList(db.WorkTypes, "Id", "Name", boardFlightLists.WorkTypesId);
            return View(boardFlightLists);
        }

        // GET: Guides/BoardFlightLists/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BoardFlightLists boardFlightLists = await db.BoardFlightLists.FindAsync(id);
            if (boardFlightLists == null)
            {
                return HttpNotFound();
            }
            return View(boardFlightLists);
        }

        // POST: Guides/BoardFlightLists/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            BoardFlightLists boardFlightLists = await db.BoardFlightLists.FindAsync(id);
            db.BoardFlightLists.Remove(boardFlightLists);
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
