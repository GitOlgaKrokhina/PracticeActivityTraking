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
using PracticeActivityTraking.Models;

namespace PracticeActivityTraking.Controllers
{
    public class EventsController : Controller
    {
        private ActivityTrackingEntities1 db = new ActivityTrackingEntities1();

        // GET: Events
        //public ActionResult Index()
        //{
        //    var events = db.Events.Include(s => s.CodifierFormatEvent).Include(s => s.CodifierStatusEvent).Include(s => s.CodifierSubtypeEvent);
        //    return View(events.ToList());
        //}
        public ActionResult Index(int? format, int? status, int? type)
        {
            var events = db.Events.Include(s => s.CodifierFormatEvent).Include(s => s.CodifierStatusEvent).Include(s => s.CodifierSubtypeEvent);
            if (format != null && format != 0)
            {
                events = events.Where(p => p.FormatID == format);
            }
            if (status != null && status != 0)
            {
                events = events.Where(p => p.EventStatusID == status);
            }
            if (type != null && type != 0)
            {
                events = events.Where(p => p.CodifierSubtypeEvent.TypeID == type);
            }
            List<CodifierFormatEvent> formats = db.CodifierFormatEvents.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            formats.Insert(0, new CodifierFormatEvent { FormatName = "Все", FormatID = 0 });
            List<CodifierStatusEvent> statusts = db.CodifierStatusEvents.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            statusts.Insert(0, new CodifierStatusEvent { EventStatusName = "Все", EventStatusID = 0 });
            List<CodifierTypeEvent> types = db.CodifierTypeEvents.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            types.Insert(0, new CodifierTypeEvent { TypeName = "Все", TypeID = 0 });

            ClassifiedEvent clasEv = new ClassifiedEvent
            {
                Events = events.ToList(),
                Formats = new SelectList(formats, "FormatID", "FormatName"),
                Status = new SelectList(statusts, "EventStatusID", "EventStatusName"),
                Types = new SelectList(types, "TypeID", "TypeName")
            };

            return View(clasEv);
        }
        public ActionResult IndexUser(int? format, int? status, int? type)
        {
            var events = db.Events.Include(s => s.CodifierFormatEvent).Include(s => s.CodifierStatusEvent).Include(s => s.CodifierSubtypeEvent);
            if (format != null && format != 0)
            {
                events = events.Where(p => p.FormatID == format);
            }
            if (status != null && status != 0)
            {
                events = events.Where(p => p.EventStatusID == status);
            }
            if (type != null && type != 0)
            {
                events = events.Where(p => p.CodifierSubtypeEvent.TypeID == type);
            }
            List<CodifierFormatEvent> formats = db.CodifierFormatEvents.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            formats.Insert(0, new CodifierFormatEvent { FormatName = "Все", FormatID = 0 });
            List<CodifierStatusEvent> statusts = db.CodifierStatusEvents.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            statusts.Insert(0, new CodifierStatusEvent { EventStatusName = "Все", EventStatusID = 0 });
            List<CodifierTypeEvent> types = db.CodifierTypeEvents.ToList();
            // устанавливаем начальный элемент, который позволит выбрать всех
            types.Insert(0, new CodifierTypeEvent { TypeName = "Все", TypeID = 0 });

            ClassifiedEvent clasEv = new ClassifiedEvent
            {
                Events = events.ToList(),
                Formats = new SelectList(formats, "FormatID", "FormatName"),
                Status = new SelectList(statusts, "EventStatusID", "EventStatusName"),
                Types = new SelectList(types, "TypeID", "TypeName")
            };

            return View(clasEv);
        }
        // GET: Events
        //public ActionResult IndexUser()
        //{
        //    var events = db.Events.Include(s => s.CodifierFormatEvent).Include(s => s.CodifierStatusEvent).Include(s => s.CodifierSubtypeEvent);
        //    return View(events.ToList());
        //}

        // GET: Events/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            ViewBag.FormatID = new SelectList(db.CodifierFormatEvents, "FormatID", "FormatName");
            ViewBag.EventStatusID = new SelectList(db.CodifierStatusEvents, "EventStatusID", "EventStatusName");
            ViewBag.SubtypeEventID = new SelectList(db.CodifierSubtypeEvents, "SubtypeID", "SubtypeName");
            return View();
        }

        // POST: Events/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,EventName,FormatID,SubtypeEventID,Date,EventStatusID")] Event @event)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    db.Events.Add(@event);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                ViewBag.FormatID = new SelectList(db.CodifierFormatEvents, "FormatID", "FormatName", @event.FormatID);
                ViewBag.EventStatusID = new SelectList(db.CodifierStatusEvents, "EventStatusID", "EventStatusName", @event.EventStatusID);
                ViewBag.SubtypeEventID = new SelectList(db.CodifierSubtypeEvents, "SubtypeID", "SubtypeName", @event.SubtypeEventID);
                return View(@event);
            }
            catch (DbUpdateException ex)
            {
                SqlException innerException = ex.InnerException.InnerException as SqlException;
                if (innerException != null && (innerException.Number == 2627 || innerException.Number == 2601))
                {

                    return View("ErrorIdEvent");
                }
                else
                {
                    throw;
                }
            }
        }

        // GET: Events/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            ViewBag.FormatID = new SelectList(db.CodifierFormatEvents, "FormatID", "FormatName", @event.FormatID);
            ViewBag.EventStatusID = new SelectList(db.CodifierStatusEvents, "EventStatusID", "EventStatusName", @event.EventStatusID);
            ViewBag.SubtypeEventID = new SelectList(db.CodifierSubtypeEvents, "SubtypeID", "SubtypeName", @event.SubtypeEventID);
            return View(@event);
        }

        // POST: Events/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. 
        // Дополнительные сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,EventName,FormatID,SubtypeEventID,Date,EventStatusID")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FormatID = new SelectList(db.CodifierFormatEvents, "FormatID", "FormatName", @event.FormatID);
            ViewBag.EventStatusID = new SelectList(db.CodifierStatusEvents, "EventStatusID", "EventStatusName", @event.EventStatusID);
            ViewBag.SubtypeEventID = new SelectList(db.CodifierSubtypeEvents, "SubtypeID", "SubtypeName", @event.SubtypeEventID);
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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
