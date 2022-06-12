using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PracticeActivityTraking;
using PracticeActivityTraking.Models;

namespace PracticeActivityTraking.Controllers
{
    public class RequestHelpsController : Controller
    {
        private ActivityTrackingEntities1 db = new ActivityTrackingEntities1();

        // GET: RequestHelps
        public ActionResult Index(int? status)
        {
            //var requestHelps = db.RequestHelps.Include(r => r.CodifierRequestHelp).Include(r => r.Employee);
            //return View(requestHelps.ToList());

            var requestHelps = db.RequestHelps.Include(r => r.CodifierRequestHelp).Include(r => r.Employee);

            if (status != null && status != 0)
            {
                requestHelps = requestHelps.Where(p => p.StatusID == status);
            }
            List<CodifierRequestHelp> statusts = db.CodifierRequestHelps.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            statusts.Insert(0, new CodifierRequestHelp { StatusName = "Все", StatusID = 0 });

            ClassReq clasReq = new ClassReq
            {
                Requests = requestHelps.ToList(),
                Status = new SelectList(statusts, "StatusID", "StatusName")
            };

            return View(clasReq);
        }

        // GET: RequestHelps/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestHelp requestHelp = db.RequestHelps.Find(id);
            if (requestHelp == null)
            {
                return HttpNotFound();
            }
            return View(requestHelp);
        }

        // GET: RequestHelps/Create
        public ActionResult Create()
        {
            ViewBag.StatusID = new SelectList(db.CodifierRequestHelps, "StatusID", "StatusName");
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name");
            passIdToList();
            return View();
        }

        // POST: RequestHelps/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "RequestID,PassportID,Description,StatusID")] RequestHelp requestHelp)
        {
            if (ModelState.IsValid)
            {
                db.RequestHelps.Add(requestHelp);
                db.SaveChanges();
                return RedirectToAction("Index","Home");
            }

            ViewBag.StatusID = new SelectList(db.CodifierRequestHelps, "StatusID", "StatusName", requestHelp.StatusID);
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name", requestHelp.PassportID);
            passIdToList();
            return View(requestHelp);
        }

        // GET: RequestHelps/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RequestHelp requestHelp = db.RequestHelps.Find(id);
            if (requestHelp == null)
            {
                return HttpNotFound();
            }
            ViewBag.StatusID = new SelectList(db.CodifierRequestHelps, "StatusID", "StatusName", requestHelp.StatusID);
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name", requestHelp.PassportID);
            passIdToList();
            return View(requestHelp);
        }

        // POST: RequestHelps/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "RequestID,PassportID,Description,StatusID")] RequestHelp requestHelp)
        {
            if (ModelState.IsValid)
            {
                db.Entry(requestHelp).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.StatusID = new SelectList(db.CodifierRequestHelps, "StatusID", "StatusName", requestHelp.StatusID);
            //ViewBag.PassportID = new SelectList(db.Employees, "PassportID", "Name", requestHelp.PassportID);
            passIdToList();
            return View(requestHelp);
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
