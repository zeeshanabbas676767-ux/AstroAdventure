using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AstroAdventure.Models;

namespace AstroAdventure.Controllers
{
    public class tblImageController : Controller
    {
        private AstroSpace db = new AstroSpace();

        // GET: tblImage
        public ActionResult Index()
        {
            var images = db.tblImages
                           .Include(i => i.tblArticle)
                           .Include(i => i.tblPlanet)
                           .Include(i => i.tblStar)
                           .ToList();
            return View(images);
        }

        // GET: tblImage/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblImage image = db.tblImages
                               .Include(i => i.tblArticle)
                               .Include(i => i.tblPlanet)
                               .Include(i => i.tblStar)
                               .FirstOrDefault(i => i.ImageID == id);

            if (image == null)
                return HttpNotFound();

            return View(image);
        }

        // GET: tblImage/Create
        public ActionResult Create()
        {
            ViewBag.ArticleID = new SelectList(db.tblArticles, "ArticleID", "Title");
            ViewBag.PlanetID = new SelectList(db.tblPlanets, "PlanetID", "Name");
            ViewBag.StarID = new SelectList(db.tblStars, "StarID", "Name");
            return View(new tblImage());
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(tblImage tblImage, HttpPostedFileBase ImageFile)
        {
            if (ImageFile == null || ImageFile.ContentLength == 0)
            {
                ModelState.AddModelError("ImageFile", "Please select an image to upload.");
            }
            if (ModelState.IsValid)
            {
                string fileName = System.IO.Path.GetFileName(ImageFile.FileName);
                string uploadPath = Server.MapPath("~/Uploads/" + fileName);

                // Save file
                ImageFile.SaveAs(uploadPath);

                // Save relative path to DB
                tblImage.Url = "/Uploads/" + fileName;

                db.tblImages.Add(tblImage);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            ViewBag.ArticleID = new SelectList(db.tblArticles, "ArticleID", "Title", tblImage.ArticleID);
            ViewBag.PlanetID = new SelectList(db.tblPlanets, "PlanetID", "Name", tblImage.PlanetID);
            ViewBag.StarID = new SelectList(db.tblStars, "StarID", "Name", tblImage.StarID);

            return View(tblImage);
        }

        // GET: tblImage/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblImage image = db.tblImages.Find(id);
            if (image == null)
                return HttpNotFound();

            ViewBag.ArticleID = new SelectList(db.tblArticles, "ArticleID", "Title", image.ArticleID);
            ViewBag.PlanetID = new SelectList(db.tblPlanets, "PlanetID", "Name", image.PlanetID);
            ViewBag.StarID = new SelectList(db.tblStars, "StarID", "Name", image.StarID);
            return View(image);
        }

        // POST: tblImage/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ImageID,Url,AltText,ArticleID,PlanetID,StarID")] tblImage image)
        {
            if (ModelState.IsValid)
            {
                db.Entry(image).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ArticleID = new SelectList(db.tblArticles, "ArticleID", "Title", image.ArticleID);
            ViewBag.PlanetID = new SelectList(db.tblPlanets, "PlanetID", "Name", image.PlanetID);
            ViewBag.StarID = new SelectList(db.tblStars, "StarID", "Name", image.StarID);
            return View(image);
        }

        // GET: tblImage/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblImage image = db.tblImages
                               .Include(i => i.tblArticle)
                               .Include(i => i.tblPlanet)
                               .Include(i => i.tblStar)
                               .FirstOrDefault(i => i.ImageID == id);

            if (image == null)
                return HttpNotFound();

            return View(image);
        }

        // POST: tblImage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblImage image = db.tblImages.Find(id);
            if (image != null)
            {
                db.tblImages.Remove(image);
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
