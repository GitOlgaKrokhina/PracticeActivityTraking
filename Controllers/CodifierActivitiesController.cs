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
    public class CodifierActivitiesController : Controller
    {
        private ActivityTrackingEntities1 db = new ActivityTrackingEntities1();

        // GET: CodifierActivities
        public ActionResult Index()
        {
            return View(db.CodifierActivities.ToList());
        }

        // GET: CodifierActivities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodifierActivity codifierActivity = db.CodifierActivities.Find(id);
            if (codifierActivity == null)
            {
                return HttpNotFound();
            }
            return View(codifierActivity);
        }

        // GET: CodifierActivities/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CodifierActivities/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityTypeID,ActivityName,Score")] CodifierActivity codifierActivity)
        {
            if (ModelState.IsValid)
            {
                db.CodifierActivities.Add(codifierActivity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(codifierActivity);
        }

        // GET: CodifierActivities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodifierActivity codifierActivity = db.CodifierActivities.Find(id);
            if (codifierActivity == null)
            {
                return HttpNotFound();
            }
            return View(codifierActivity);
        }

        // POST: CodifierActivities/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityTypeID,ActivityName,Score")] CodifierActivity codifierActivity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codifierActivity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(codifierActivity);
        }

        // GET: CodifierActivities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodifierActivity codifierActivity = db.CodifierActivities.Find(id);
            if (codifierActivity == null)
            {
                return HttpNotFound();
            }
            return View(codifierActivity);
        }

        // POST: CodifierActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodifierActivity codifierActivity = db.CodifierActivities.Find(id);
            db.CodifierActivities.Remove(codifierActivity);
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
