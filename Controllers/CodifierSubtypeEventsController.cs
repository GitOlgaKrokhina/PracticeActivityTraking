using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracticeActivityTraking;

namespace PracticeActivityTraking.Controllers
{
    public class CodifierSubtypeEventsController : Controller
    {
        private ActivityTrackingEntities1 db = new ActivityTrackingEntities1();

        // GET: CodifierSubtypeEvents
        public ActionResult Index()
        {
            var codifierSubtypeEvents = db.CodifierSubtypeEvents.Include(c => c.CodifierTypeEvent);
            return View(codifierSubtypeEvents.ToList());
        }

        // GET: CodifierSubtypeEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodifierSubtypeEvent codifierSubtypeEvent = db.CodifierSubtypeEvents.Find(id);
            if (codifierSubtypeEvent == null)
            {
                return HttpNotFound();
            }
            return View(codifierSubtypeEvent);
        }

        // GET: CodifierSubtypeEvents/Create
        public ActionResult Create()
        {
            ViewBag.TypeID = new SelectList(db.CodifierTypeEvents, "TypeID", "TypeName");
            return View();
        }

        // POST: CodifierSubtypeEvents/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SubtypeID,TypeID,SubtypeName")] CodifierSubtypeEvent codifierSubtypeEvent)
        {
            if (ModelState.IsValid)
            {
                db.CodifierSubtypeEvents.Add(codifierSubtypeEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TypeID = new SelectList(db.CodifierTypeEvents, "TypeID", "TypeName", codifierSubtypeEvent.TypeID);
            return View(codifierSubtypeEvent);
        }

        // GET: CodifierSubtypeEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodifierSubtypeEvent codifierSubtypeEvent = db.CodifierSubtypeEvents.Find(id);
            if (codifierSubtypeEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.TypeID = new SelectList(db.CodifierTypeEvents, "TypeID", "TypeName", codifierSubtypeEvent.TypeID);
            return View(codifierSubtypeEvent);
        }

        // POST: CodifierSubtypeEvents/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SubtypeID,TypeID,SubtypeName")] CodifierSubtypeEvent codifierSubtypeEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codifierSubtypeEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TypeID = new SelectList(db.CodifierTypeEvents, "TypeID", "TypeName", codifierSubtypeEvent.TypeID);
            return View(codifierSubtypeEvent);
        }

        // GET: CodifierSubtypeEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodifierSubtypeEvent codifierSubtypeEvent = db.CodifierSubtypeEvents.Find(id);
            if (codifierSubtypeEvent == null)
            {
                return HttpNotFound();
            }
            return View(codifierSubtypeEvent);
        }

        // POST: CodifierSubtypeEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodifierSubtypeEvent codifierSubtypeEvent = db.CodifierSubtypeEvents.Find(id);
            db.CodifierSubtypeEvents.Remove(codifierSubtypeEvent);
            db.SaveChanges();
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
