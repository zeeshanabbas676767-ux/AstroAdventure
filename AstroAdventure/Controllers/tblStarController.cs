using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AstroAdventure.Models;

namespace AstroAdventure.Controllers
{
    public class tblStarController : Controller
    {
        private AstroSpace db = new AstroSpace();

        // GET: tblStar
        public ActionResult Index()
        {
            var stars = db.tblStars
                          .OrderBy(s => s.Name)
                          .ToList();
            return View(stars);
        }

        // GET: tblStar/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblStar star = db.tblStars
                             .Include(s => s.tblImages)
                             .FirstOrDefault(s => s.StarID == id);

            if (star == null)
                return HttpNotFound();

            return View(star);
        }

        // GET: tblStar/Create
        public ActionResult Create()
        {
            return View(new tblStar());
        }

        // POST: tblStar/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Type,DistanceFromEarth,Mass,Radius")] tblStar star)
        {
            if (ModelState.IsValid)
            {
                db.tblStars.Add(star);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(star);
        }

        // GET: tblStar/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblStar star = db.tblStars.Find(id);
            if (star == null)
                return HttpNotFound();

            return View(star);
        }

        // POST: tblStar/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "StarID,Name,Type,DistanceFromEarth,Mass,Radius")] tblStar star)
        {
            if (ModelState.IsValid)
            {
                db.Entry(star).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(star);
        }

        // GET: tblStar/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblStar star = db.tblStars
                             .Include(s => s.tblImages)
                             .FirstOrDefault(s => s.StarID == id);

            if (star == null)
                return HttpNotFound();

            return View(star);
        }

        // POST: tblStar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblStar star = db.tblStars.Find(id);
            if (star != null)
            {
                // Optional: remove related images manually or via cascade delete
                db.tblStars.Remove(star);
                db.SaveChanges();
            }
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
