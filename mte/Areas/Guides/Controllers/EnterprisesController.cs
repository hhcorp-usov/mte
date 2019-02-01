using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using Microsoft.AspNet.Identity;
using System.Web.Mvc;
using mte.Models;

namespace mte.Areas.Guides.Controllers
{
    public class EnterprisesController : Controller
    {
        private MteDataContexts db = new MteDataContexts();
        private ApplicationDbContext dbu = new ApplicationDbContext();

        // GET: Guides/Enterprises
        public async Task<ActionResult> Index()
        {
            var currentUserId = User.Identity.GetUserId();
            ApplicationUser u = dbu.Users.FirstOrDefault(x => x.Id == currentUserId);
            return View(await db.Enterprises.Where(w => w.GlobalContainersId == u.AdditionalUserInfo.GlobalContainersId).ToListAsync());
        }

        // GET: Guides/Enterprises/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enterprises enterprises = await db.Enterprises.FindAsync(id);
            if (enterprises == null)
            {
                return HttpNotFound();
            }
            return View(enterprises);
        }

        // GET: Guides/Enterprises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Guides/Enterprises/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Name,Inn,Kpp,Ogrn,FAdress,YAdress")] Enterprises enterprises)
        {
            if (ModelState.IsValid)
            {
                db.Enterprises.Add(enterprises);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(enterprises);
        }

        // GET: Guides/Enterprises/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enterprises enterprises = await db.Enterprises.FindAsync(id);
            if (enterprises == null)
            {
                return HttpNotFound();
            }
            return View(enterprises);
        }

        // POST: Guides/Enterprises/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Name,Inn,Kpp,Ogrn,FAdress,YAdress")] Enterprises enterprises)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enterprises).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(enterprises);
        }

        // GET: Guides/Enterprises/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enterprises enterprises = await db.Enterprises.FindAsync(id);
            if (enterprises == null)
            {
                return HttpNotFound();
            }
            return View(enterprises);
        }

        // POST: Guides/Enterprises/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Enterprises enterprises = await db.Enterprises.FindAsync(id);
            db.Enterprises.Remove(enterprises);
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
