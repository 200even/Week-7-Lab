using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Week7_Lab.Models;

namespace Week7_Lab.Controllers
{
    public class PinsController : Controller
    {
        private PinterestDbContext db = new PinterestDbContext();

        public ActionResult GetImage(int PinId)
        {
            var pin = db.Pins.Find(PinId);
            return File(pin.Image,"image");
        }
        // GET: Pins
        public ActionResult Index()
        {
            return View(db.Pins.ToList());
        }

        // GET: Pins/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pin pin = db.Pins.Find(id);
            if (pin == null)
            {
                return HttpNotFound();
            }
            return View(pin);
        }

        // GET: Pins/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "PinId,Image,ImageLink,Note")] Pin pin)
        {
            if (ModelState.IsValid)
            {
                db.Pins.Add(pin);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(pin);
        }

        // GET: Pins/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pin pin = db.Pins.Find(id);
            if (pin == null)
            {
                return HttpNotFound();
            }
            return View(pin);
        }

        // POST: Pins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PinId,Image,ImageLink,Note")] Pin pin)
        {
            if (ModelState.IsValid)
            {
                db.Entry(pin).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(pin);
        }

        // GET: Pins/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Pin pin = db.Pins.Find(id);
            if (pin == null)
            {
                return HttpNotFound();
            }
            return View(pin);
        }

        // POST: Pins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Pin pin = db.Pins.Find(id);
            db.Pins.Remove(pin);
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
