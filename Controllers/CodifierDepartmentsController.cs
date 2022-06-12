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
    public class CodifierDepartmentsController : Controller
    {
        private ActivityTrackingEntities1 db = new ActivityTrackingEntities1();

        // GET: CodifierDepartments
        public ActionResult Index()
        {
            return View(db.CodifierDepartments.ToList());
        }

        // GET: CodifierDepartments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodifierDepartment codifierDepartment = db.CodifierDepartments.Find(id);
            if (codifierDepartment == null)
            {
                return HttpNotFound();
            }
            return View(codifierDepartment);
        }

        // GET: CodifierDepartments/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CodifierDepartments/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DepartmentID,DepartmentName")] CodifierDepartment codifierDepartment)
        {
            if (ModelState.IsValid)
            {
                db.CodifierDepartments.Add(codifierDepartment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(codifierDepartment);
        }

        // GET: CodifierDepartments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodifierDepartment codifierDepartment = db.CodifierDepartments.Find(id);
            if (codifierDepartment == null)
            {
                return HttpNotFound();
            }
            return View(codifierDepartment);
        }

        // POST: CodifierDepartments/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DepartmentID,DepartmentName")] CodifierDepartment codifierDepartment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codifierDepartment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(codifierDepartment);
        }

        // GET: CodifierDepartments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodifierDepartment codifierDepartment = db.CodifierDepartments.Find(id);
            if (codifierDepartment == null)
            {
                return HttpNotFound();
            }
            return View(codifierDepartment);
        }

        // POST: CodifierDepartments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodifierDepartment codifierDepartment = db.CodifierDepartments.Find(id);
            db.CodifierDepartments.Remove(codifierDepartment);
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
