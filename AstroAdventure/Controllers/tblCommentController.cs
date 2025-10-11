using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using AstroAdventure.Models;

namespace AstroAdventure.Controllers
{
    public class tblCommentController : Controller
    {
        private AstroSpace db = new AstroSpace();

        // GET: tblComment
        public ActionResult Index()
        {
            var comments = db.tblComments
                             .Include(c => c.tblUser)
                             .Include(c => c.tblArticle)
                             .Include(c => c.tblPlanet)
                             .OrderByDescending(c => c.CreatedAt)
                             .ToList();
            return View(comments);
        }

        // GET: tblComment/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblComment comment = db.tblComments
                                   .Include(c => c.tblUser)
                                   .Include(c => c.tblArticle)
                                   .Include(c => c.tblPlanet)
                                   .FirstOrDefault(c => c.CommentID == id);

            if (comment == null)
                return HttpNotFound();

            return View(comment);
        }

        // GET: tblComment/Create
        public ActionResult Create()
        {
            ViewBag.UserID = new SelectList(db.tblUsers, "UserID", "UserName");
            ViewBag.ArticleID = new SelectList(db.tblArticles, "ArticleID", "Title");
            ViewBag.PlanetID = new SelectList(db.tblPlanets, "PlanetID", "Name");
            return View(new tblComment());
        }

        // POST: tblComment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,ArticleID,PlanetID,Content")] tblComment comment)
        {
            if (ModelState.IsValid)
            {
                comment.CreatedAt = DateTime.Now;
                db.tblComments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.tblUsers, "UserID", "UserName", comment.UserID);
            ViewBag.ArticleID = new SelectList(db.tblArticles, "ArticleID", "Title", comment.ArticleID);
            ViewBag.PlanetID = new SelectList(db.tblPlanets, "PlanetID", "Name", comment.PlanetID);
            return View(comment);
        }

        // GET: tblComment/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblComment comment = db.tblComments.Find(id);
            if (comment == null)
                return HttpNotFound();

            ViewBag.UserID = new SelectList(db.tblUsers, "UserID", "UserName", comment.UserID);
            ViewBag.ArticleID = new SelectList(db.tblArticles, "ArticleID", "Title", comment.ArticleID);
            ViewBag.PlanetID = new SelectList(db.tblPlanets, "PlanetID", "Name", comment.PlanetID);
            return View(comment);
        }

        // POST: tblComment/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommentID,UserID,ArticleID,PlanetID,Content")] tblComment comment)
        {
            if (ModelState.IsValid)
            {
                var commentInDb = db.tblComments.Find(comment.CommentID);
                if (commentInDb == null)
                    return HttpNotFound();

                commentInDb.Content = comment.Content;
                commentInDb.UserID = comment.UserID;
                commentInDb.ArticleID = comment.ArticleID;
                commentInDb.PlanetID = comment.PlanetID;

                db.Entry(commentInDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.UserID = new SelectList(db.tblUsers, "UserID", "UserName", comment.UserID);
            ViewBag.ArticleID = new SelectList(db.tblArticles, "ArticleID", "Title", comment.ArticleID);
            ViewBag.PlanetID = new SelectList(db.tblPlanets, "PlanetID", "Name", comment.PlanetID);
            return View(comment);
        }

        // GET: tblComment/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblComment comment = db.tblComments
                                   .Include(c => c.tblUser)
                                   .Include(c => c.tblArticle)
                                   .Include(c => c.tblPlanet)
                                   .FirstOrDefault(c => c.CommentID == id);

            if (comment == null)
                return HttpNotFound();

            return View(comment);
        }

        // POST: tblComment/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblComment comment = db.tblComments.Find(id);
            if (comment != null)
            {
                db.tblComments.Remove(comment);
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
