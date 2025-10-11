using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AstroAdventure.Models;

namespace AstroAdventure.Controllers
{
    public class tblMissionController : Controller
    {
        private AstroSpace db = new AstroSpace();

        // GET: tblMission
        public ActionResult Index()
        {
            var missions = db.tblMissions
                             .Include(m => m.tblPlanet)
                             .OrderByDescending(m => m.LaunchDate)
                             .ToList();
            return View(missions);
        }

        // GET: tblMission/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblMission mission = db.tblMissions
                                   .Include(m => m.tblPlanet)
                                   .FirstOrDefault(m => m.MissionID == id);

            if (mission == null)
                return HttpNotFound();

            return View(mission);
        }

        // GET: tblMission/Create
        public ActionResult Create()
        {
            ViewBag.RelatedPlanetID = new SelectList(db.tblPlanets, "PlanetID", "Name");
            return View(new tblMission());
        }

        // POST: tblMission/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MissionName,LaunchDate,EndDate,Objective,RelatedPlanetID")] tblMission mission)
        {
            if (ModelState.IsValid)
            {
                db.tblMissions.Add(mission);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RelatedPlanetID = new SelectList(db.tblPlanets, "PlanetID", "Name", mission.RelatedPlanetID);
            return View(mission);
        }

        // GET: tblMission/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblMission mission = db.tblMissions.Find(id);
            if (mission == null)
                return HttpNotFound();

            ViewBag.RelatedPlanetID = new SelectList(db.tblPlanets, "PlanetID", "Name", mission.RelatedPlanetID);
            return View(mission);
        }

        // POST: tblMission/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MissionID,MissionName,LaunchDate,EndDate,Objective,RelatedPlanetID")] tblMission mission)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mission).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.RelatedPlanetID = new SelectList(db.tblPlanets, "PlanetID", "Name", mission.RelatedPlanetID);
            return View(mission);
        }

        // GET: tblMission/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblMission mission = db.tblMissions
                                   .Include(m => m.tblPlanet)
                                   .FirstOrDefault(m => m.MissionID == id);

            if (mission == null)
                return HttpNotFound();

            return View(mission);
        }

        // POST: tblMission/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblMission mission = db.tblMissions.Find(id);
            if (mission != null)
            {
                db.tblMissions.Remove(mission);
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
