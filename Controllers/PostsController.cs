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
    public class PostsController : Controller
    {
        private ActivityTrackingEntities1 db = new ActivityTrackingEntities1();

        // GET: Posts
        public ActionResult Index()
        {
            var posts = db.Posts.Include(p => p.CodifierDepartment).Include(p => p.CodifierPost).Include(p => p.Employee);
            return View(posts.ToList());
        }
        // GET: Posts by ID
        public ActionResult IndexID(string id)
        {
            var posts = db.Posts.Include(p => p.CodifierDepartment).Include(p => p.CodifierPost).Include(p => p.Employee).Select(x => x).Where(x => x.PassportID == id);
            return View(posts.ToList());
        }
        // GET: Posts
        public ActionResult IndexUser()
        {
            var posts = db.Posts.Include(p => p.CodifierDepartment).Include(p => p.CodifierPost).Include(p => p.Employee);
            return View(posts.ToList());
        }

        // GET: Posts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // GET: Posts/Create
        public ActionResult Create()
        {
            ViewBag.DepartmentID = new SelectList(db.CodifierDepartments, "DepartmentID", "DepartmentName");
            ViewBag.PostID = new SelectList(db.CodifierPosts, "PostID", "PostName");
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name");
            passIdToList();
            return View();
        }

        // POST: Posts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EmployeeNumberID,PassportID,PostID,DepartmentID,DateEmployment,DateDismissal")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Posts.Add(post);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.DepartmentID = new SelectList(db.CodifierDepartments, "DepartmentID", "DepartmentName", post.DepartmentID);
            ViewBag.PostID = new SelectList(db.CodifierPosts, "PostID", "PostName", post.PostID);

            passIdToList();
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name", post.PassportID);
            return View(post);
        }

        // GET: Posts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            ViewBag.DepartmentID = new SelectList(db.CodifierDepartments, "DepartmentID", "DepartmentName", post.DepartmentID);
            ViewBag.PostID = new SelectList(db.CodifierPosts, "PostID", "PostName", post.PostID);
            passIdToList();
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name", post.PassportID);
            return View(post);
        }

        // POST: Posts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EmployeeNumberID,PassportID,PostID,DepartmentID,DateEmployment,DateDismissal")] Post post)
        {
            if (ModelState.IsValid)
            {
                db.Entry(post).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.DepartmentID = new SelectList(db.CodifierDepartments, "DepartmentID", "DepartmentName", post.DepartmentID);
            ViewBag.PostID = new SelectList(db.CodifierPosts, "PostID", "PostName", post.PostID);
            passIdToList();
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name", post.PassportID);
            return View(post);
        }

        // GET: Posts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Post post = db.Posts.Find(id);
            if (post == null)
            {
                return HttpNotFound();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Post post = db.Posts.Find(id);
            db.Posts.Remove(post);
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
    }
}
