using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AstroAdventure.Models;

namespace AstroAdventure.Controllers
{
    public class tblCategoryController : Controller
    {
        private AstroSpace db = new AstroSpace();

        // GET: tblCategory
        public ActionResult Index()
        {
            var categories = db.tblCategories.ToList();
            return View(categories);
        }

        // GET: tblCategory/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblCategory category = db.tblCategories.Find(id);
            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        // GET: tblCategory/Create
        public ActionResult Create()
        {
            return View(new tblCategory());
        }

        // POST: tblCategory/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CategoryID,CategoryName,Description")] tblCategory category)
        {
            if (ModelState.IsValid)
            {
                db.tblCategories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: tblCategory/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblCategory category = db.tblCategories.Find(id);
            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        // POST: tblCategory/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CategoryID,CategoryName,Description")] tblCategory category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // GET: tblCategory/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblCategory category = db.tblCategories.Find(id);
            if (category == null)
                return HttpNotFound();

            return View(category);
        }

        // POST: tblCategory/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblCategory category = db.tblCategories.Find(id);

            if (category != null)
            {
                // Optional: Remove related articles or handle cascading deletes carefully
                db.tblCategories.Remove(category);
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
