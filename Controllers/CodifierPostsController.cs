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
using PracticeActivityTraking;

namespace PracticeActivityTraking.Controllers
{
    public class CodifierPostsController : Controller
    {
        private ActivityTrackingEntities1 db = new ActivityTrackingEntities1();

        // GET: CodifierPosts
        public ActionResult Index()
        {
            return View(db.CodifierPosts.ToList());
        }

        // GET: CodifierPosts/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodifierPost codifierPost = db.CodifierPosts.Find(id);
            if (codifierPost == null)
            {
                return HttpNotFound();
            }
            return View(codifierPost);
        }

        // GET: CodifierPosts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CodifierPosts/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PostID,PostName")] CodifierPost codifierPost)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.CodifierPosts.Add(codifierPost);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(codifierPost);
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

        // GET: CodifierPosts/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodifierPost codifierPost = db.CodifierPosts.Find(id);
            if (codifierPost == null)
            {
                return HttpNotFound();
            }
            return View(codifierPost);
        }

        // POST: CodifierPosts/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PostID,PostName")] CodifierPost codifierPost)
        {
            if (ModelState.IsValid)
            {
                db.Entry(codifierPost).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(codifierPost);
        }

        // GET: CodifierPosts/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CodifierPost codifierPost = db.CodifierPosts.Find(id);
            if (codifierPost == null)
            {
                return HttpNotFound();
            }
            return View(codifierPost);
        }

        // POST: CodifierPosts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CodifierPost codifierPost = db.CodifierPosts.Find(id);
            db.CodifierPosts.Remove(codifierPost);
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
