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
    public class EmployersController : Controller
    {
        private MteDataContexts db = new MteDataContexts();

        // GET: Guides/Employers
        public async Task<ActionResult> Index()
        {
            var employers = db.Employers.Include(e => e.Enterprises).Include(e => e.Posts);
            return View(await employers.ToListAsync());
        }

        // GET: Guides/Employers/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employers employers = await db.Employers.FindAsync(id);
            if (employers == null)
            {
                return HttpNotFound();
            }
            return View(employers);
        }

        // GET: Guides/Employers/Create
        public ActionResult Create()
        {
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name");
            ViewBag.PostsId = new SelectList(db.Posts, "Id", "Name");
            return View();
        }

        // POST: Guides/Employers/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,FirstName,SurName,EnterprisesId,PostsId")] Employers employers)
        {
            if (ModelState.IsValid)
            {
                db.Employers.Add(employers);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", employers.EnterprisesId);
            ViewBag.PostsId = new SelectList(db.Posts, "Id", "Name", employers.PostsId);
            return View(employers);
        }

        // GET: Guides/Employers/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employers employers = await db.Employers.FindAsync(id);
            if (employers == null)
            {
                return HttpNotFound();
            }
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", employers.EnterprisesId);
            ViewBag.PostsId = new SelectList(db.Posts, "Id", "Name", employers.PostsId);
            return View(employers);
        }

        // POST: Guides/Employers/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,FirstName,SurName,EnterprisesId,PostsId")] Employers employers)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employers).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.EnterprisesId = new SelectList(db.Enterprises, "Id", "Name", employers.EnterprisesId);
            ViewBag.PostsId = new SelectList(db.Posts, "Id", "Name", employers.PostsId);
            return View(employers);
        }

        // GET: Guides/Employers/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employers employers = await db.Employers.FindAsync(id);
            if (employers == null)
            {
                return HttpNotFound();
            }
            return View(employers);
        }

        // POST: Guides/Employers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Employers employers = await db.Employers.FindAsync(id);
            db.Employers.Remove(employers);
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
