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
    public class EmployeeToActivitiesController : Controller
    {
        private ActivityTrackingEntities1 db = new ActivityTrackingEntities1();

        // GET: EmployeeToActivities
        public ActionResult IndexID(int id)
        {
            var employeeToActivities = db.EmployeeToActivities.Include(e => e.Activity).Include(e => e.Employee).Select(x => x).Where(x => x.ActivityID == id);
            return View(employeeToActivities.ToList());
        }
        // GET: EmployeeToActivities
        public ActionResult IndexIDUser(int id)
        {
            var employeeToActivities = db.EmployeeToActivities.Include(e => e.Activity).Include(e => e.Employee).Select(x => x).Where(x => x.ActivityID == id);
            return View(employeeToActivities.ToList());
        }

        // GET: EmployeeToActivities
        public ActionResult Index()
        {
            var employeeToActivities = db.EmployeeToActivities.Include(e => e.Activity).Include(e => e.Employee);
            return View(employeeToActivities.ToList());
        }

        // GET: EmployeeToActivities/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeToActivity employeeToActivity = db.EmployeeToActivities.Find(id);
            if (employeeToActivity == null)
            {
                return HttpNotFound();
            }
            return View(employeeToActivity);
        }

        // GET: EmployeeToActivities/Create
        public ActionResult Create()
        {
            actIdToList();
            passIdToList();
            //ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "EventID");
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name");
            return View();
        }

        // POST: EmployeeToActivities/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ConnectID,PassportID,ActivityID")] EmployeeToActivity employeeToActivity)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeToActivities.Add(employeeToActivity);
                db.SaveChanges();
                return View("ForAddingEmp");
                //return RedirectToAction("Index");
            }
            actIdToList();
            passIdToList();
            //ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "EventID", employeeToActivity.ActivityID);
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name", employeeToActivity.PassportID);
            return View(employeeToActivity);
        }

        // GET: EmployeeToActivities/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeToActivity employeeToActivity = db.EmployeeToActivities.Find(id);
            if (employeeToActivity == null)
            {
                return HttpNotFound();
            }
            actIdToList();
            passIdToList();
            //ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "EventID", employeeToActivity.ActivityID);
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name", employeeToActivity.PassportID);
            return View(employeeToActivity);
        }

        // POST: EmployeeToActivities/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ConnectID,PassportID,ActivityID")] EmployeeToActivity employeeToActivity)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employeeToActivity).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            actIdToList();
            passIdToList();
            //ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "EventID", employeeToActivity.ActivityID);
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name", employeeToActivity.PassportID);
            return View(employeeToActivity);
        }

        // GET: EmployeeToActivities/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            EmployeeToActivity employeeToActivity = db.EmployeeToActivities.Find(id);
            if (employeeToActivity == null)
            {
                return HttpNotFound();
            }
            return View(employeeToActivity);
        }

        // POST: EmployeeToActivities/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            EmployeeToActivity employeeToActivity = db.EmployeeToActivities.Find(id);
            db.EmployeeToActivities.Remove(employeeToActivity);
            db.SaveChanges();
            return RedirectToAction("Index", "Activities", null);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
        // GET: EmployeeToActivities/Create
        public ActionResult CreateUser()
        {
            actIdToList();
            passIdToList();
            return View();
        }
        // POST: EmployeeToActivities/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateUser([Bind(Include = "ConnectID,PassportID,ActivityID")] EmployeeToActivity employeeToActivity)
        {
            if (ModelState.IsValid)
            {
                db.EmployeeToActivities.Add(employeeToActivity);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //ViewBag.ActivityID = new SelectList(db.Activities, "ActivityID", "EventID", employeeToActivity.ActivityID);
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name", employeeToActivity.PassportID);
            actIdToList();
            passIdToList();
            return View(employeeToActivity);
        }

        public void passIdToList()
        {
            var employees = db.Employees.GroupBy(x => x.Name + " " + x.Surname + " " + x.Patronymic);
            List<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var employee in employees)
            {
                var optionGroup = new SelectListGroup() { Name = employee.Key };
                foreach (var item in employee)
                {
                    selectListItems.Add(new SelectListItem() { Value = item.PassportID.ToString(), Text = item.PassportID.ToString(), Group = optionGroup });
                }
            }

            ViewBag.PassportID = selectListItems;
        }
        public void actIdToList()
        {
            var activities = db.Activities.GroupBy(x => x.Event.EventName + " " + x.CodifierActivity.ActivityName + " " + x.Date + " id=" + x.ActivityID);
            List<SelectListItem> selectListItem = new List<SelectListItem>();
            foreach (var act in activities)
            {
                var optionGroupp = new SelectListGroup() { Name = act.Key };
                foreach (var itemm in act)
                {
                    selectListItem.Add(new SelectListItem() { Value = itemm.ActivityID.ToString(), Text = itemm.ActivityID.ToString(), Group = optionGroupp });
                }
            }

            ViewBag.ActivityID = selectListItem;
        }
    }
}
