using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.Office.Interop.Excel;
using PracticeActivityTraking;

namespace PracticeActivityTraking.Controllers
{
    public class EmployeesController : Controller
    {
        private ActivityTrackingEntities1 db = new ActivityTrackingEntities1();

        // GET: Employees
        public ActionResult Index()
        {
            var employees = db.Employees.Include(e => e.CodifierGender);
            return View(employees.ToList());
        }
        // GET: Employees
        public ActionResult IndexUser()
        {
            var employees = db.Employees.Include(e => e.CodifierGender);
            return View(employees.ToList());
        }

        // GET: Employees/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }
        public int GetScore(string id)
        {
            
            int score = 0;
            List<int> ball = db.EmployeeToActivities.Select(x => x).Where(x => x.PassportID == id).Select(x => x.ActivityID).ToList();
            foreach (var b in ball)
            {
                string scor1 = db.Activities.Select(x => x).Where(x => x.ActivityID == b).Select(x => x.CodifierActivity.Score).ToString();
                score += int.Parse(scor1);
            }
            return score;
        }

        // GET: Employees/Create
        public ActionResult Create()
        {
            ViewBag.GenderID = new SelectList(db.CodifierGenders, "GenderID", "GenderName");
            return View();
        }

        // POST: Employees/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PassportID,Name,Surname,Patronymic,GenderID,Birthdate,Login,Password,Phone")] Employee employee)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Employees.Add(employee);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.GenderID = new SelectList(db.CodifierGenders, "GenderID", "GenderName", employee.GenderID);
                return View(employee);
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {
                    
                    return View("ErrorID");
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Employees/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenderID = new SelectList(db.CodifierGenders, "GenderID", "GenderName", employee.GenderID);
            return View(employee);
        }

        // POST: Employees/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PassportID,Name,Surname,Patronymic,GenderID,Birthdate,Login,Password,Phone")] Employee employee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(employee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenderID = new SelectList(db.CodifierGenders, "GenderID", "GenderName", employee.GenderID);
            return View(employee);
        }

        // GET: Employees/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Employee employee = db.Employees.Find(id);
            if (employee == null)
            {
                return HttpNotFound();
            }
            return View(employee);
        }

        // POST: Employees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Employee employee = db.Employees.Find(id);
            db.Employees.Remove(employee);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        // POST: Employees/ErrorID
        [HttpPost, ActionName("ErrorID")]
        [ValidateAntiForgeryToken]
        public ActionResult ErrorID()
        {

            return View();
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
