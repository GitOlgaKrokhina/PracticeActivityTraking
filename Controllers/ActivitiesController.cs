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
    public class ActivitiesController : Controller
    {
        private ActivityTrackingEntities1 db = new ActivityTrackingEntities1();

        // GET: Activities
        public ActionResult Index()
        {
            var activities = db.Activities.Include(a => a.CodifierActivity).Include(a => a.CodifierStatusActivity).Include(a => a.Event);
            return View(activities.ToList());
        }
        // GET: Activities
        public ActionResult IndexUser()
        {
            var activities = db.Activities.Include(a => a.CodifierActivity).Include(a => a.CodifierStatusActivity).Include(a => a.Event);
            return View(activities.ToList());
        }

        // GET: Activities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // GET: Activities/Create
        public ActionResult Create()
        {
            ViewBag.ActivityTypeID = new SelectList(db.CodifierActivities, "ActivityTypeID", "ActivityName");
            ViewBag.ActivityStatusID = new SelectList(db.CodifierStatusActivities, "ActivityStatusID", "ActivityStatusName");
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName");
            return View();
        }

        // POST: Activities/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ActivityID,EventID,ActivityTypeID,Date,ActivityStatusID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Activities.Add(activity);
                db.SaveChanges();
                return View("ForAddingEmp");
                //return RedirectToAction("Index");
            }

            ViewBag.ActivityTypeID = new SelectList(db.CodifierActivities, "ActivityTypeID", "ActivityName", activity.ActivityTypeID);
            ViewBag.ActivityStatusID = new SelectList(db.CodifierStatusActivities, "ActivityStatusID", "ActivityStatusName", activity.ActivityStatusID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", activity.EventID);
            return View(activity);
        }

        // GET: Activities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            ViewBag.ActivityTypeID = new SelectList(db.CodifierActivities, "ActivityTypeID", "ActivityName", activity.ActivityTypeID);
            ViewBag.ActivityStatusID = new SelectList(db.CodifierStatusActivities, "ActivityStatusID", "ActivityStatusName", activity.ActivityStatusID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", activity.EventID);
            return View(activity);
        }

        // POST: Activities/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ActivityID,EventID,ActivityTypeID,Date,ActivityStatusID")] Activity activity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(activity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ActivityTypeID = new SelectList(db.CodifierActivities, "ActivityTypeID", "ActivityName", activity.ActivityTypeID);
            ViewBag.ActivityStatusID = new SelectList(db.CodifierStatusActivities, "ActivityStatusID", "ActivityStatusName", activity.ActivityStatusID);
            ViewBag.EventID = new SelectList(db.Events, "EventID", "EventName", activity.EventID);
            return View(activity);
        }

        // GET: Activities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Activity activity = db.Activities.Find(id);
            if (activity == null)
            {
                return HttpNotFound();
            }
            return View(activity);
        }

        // POST: Activities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Activity activity = db.Activities.Find(id);
            db.Activities.Remove(activity);
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
