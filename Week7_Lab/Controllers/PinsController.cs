using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
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
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult List()
        {
            var results = db.Pins.Include(x => x.WhoPinned).Select(p => new { p.ImageLink, p.Note, p.PinId, p.WhoPinned.Email, ImageUrl = "/Pins/GetImage/" + "?PinId=" + p.PinId}).ToList();
            return Json(results, JsonRequestBehavior.AllowGet);
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
        //[ValidateAntiForgeryToken]
        [Authorize]
        public ActionResult Create(string URL, string Notes, /*HttpPostedFileBase Image*/ string Image)
        {
            if (ModelState.IsValid)
            {
                var image = Migrations.Configuration.GetImageByteArray(Image);
                var currentUser = db.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefault();
                var p = new Pin() { Image = image, ImageLink = URL, Note = Notes, WhoPinned = currentUser };
                db.Pins.Add(p);
                db.SaveChanges();
                return Json(true);
            }

            return Json(false);
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
