using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AstroAdventure.Models;

namespace AstroAdventure.Controllers
{
    public class tblPlanetController : Controller
    {
        private AstroSpace db = new AstroSpace();

        // GET: tblPlanet
        public ActionResult Index()
        {
            var planets = db.tblPlanets
                            .Include(p => p.tblCategory)
                            .OrderBy(p => p.Name)
                            .ToList();
            return View(planets);
        }

        // GET: tblPlanet/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblPlanet planet = db.tblPlanets
                                 .Include(p => p.tblCategory)
                                 .Include(p => p.tblComments)
                                 .Include(p => p.tblImages)
                                 .Include(p => p.tblMissions)
                                 .FirstOrDefault(p => p.PlanetID == id);

            if (planet == null)
                return HttpNotFound();

            return View(planet);
        }

        // GET: tblPlanet/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.tblCategories, "CategoryID", "CategoryName");
            return View(new tblPlanet());
        }

        // POST: tblPlanet/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,CategoryID,DistanceFromSun,Radius,Mass")] tblPlanet planet)
        {
            if (ModelState.IsValid)
            {
                db.tblPlanets.Add(planet);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.tblCategories, "CategoryID", "CategoryName", planet.CategoryID);
            return View(planet);
        }

        // GET: tblPlanet/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblPlanet planet = db.tblPlanets.Find(id);
            if (planet == null)
                return HttpNotFound();

            ViewBag.CategoryID = new SelectList(db.tblCategories, "CategoryID", "CategoryName", planet.CategoryID);
            return View(planet);
        }

        // POST: tblPlanet/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "PlanetID,Name,Description,CategoryID,DistanceFromSun,Radius,Mass")] tblPlanet planet)
        {
            if (ModelState.IsValid)
            {
                db.Entry(planet).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.tblCategories, "CategoryID", "CategoryName", planet.CategoryID);
            return View(planet);
        }

        // GET: tblPlanet/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblPlanet planet = db.tblPlanets
                                 .Include(p => p.tblCategory)
                                 .FirstOrDefault(p => p.PlanetID == id);

            if (planet == null)
                return HttpNotFound();

            return View(planet);
        }

        // POST: tblPlanet/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblPlanet planet = db.tblPlanets.Find(id);
            if (planet != null)
            {
                // Optional: remove related comments, images, missions carefully or cascade delete in DB
                db.tblPlanets.Remove(planet);
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
