using System;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Web.Mvc;
using AstroAdventure.Models;

namespace AstroAdventure.Controllers
{
    public class tblArticleController : Controller
    {
        private AstroSpace db = new AstroSpace();

        // GET: tblArticle
        public ActionResult Index()
        {
            var articles = db.tblArticles.Include(a => a.tblCategory)
                                         .Include(a => a.tblUser)
                                         .OrderByDescending(a => a.CreatedAt)
                                         .ToList();
            return View(articles);
        }

        // GET: tblArticle/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblArticle tblArticle = db.tblArticles
                                      .Include(a => a.tblCategory)
                                      .Include(a => a.tblUser)
                                      .Include(a => a.tblComments)
                                      .Include(a => a.tblImages)
                                      .FirstOrDefault(a => a.ArticleID == id);

            if (tblArticle == null)
                return HttpNotFound();

            return View(tblArticle);
        }

        // GET: tblArticle/Create
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.tblCategories, "CategoryID", "CategoryName");
            ViewBag.AuthorID = new SelectList(db.tblUsers, "UserID", "UserName");
            return View(new tblArticle());
        }

        // POST: tblArticle/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Title,Content,CategoryID,AuthorID")] tblArticle tblArticle)
        {
            if (ModelState.IsValid)
            {
                tblArticle.CreatedAt = DateTime.Now;
                db.tblArticles.Add(tblArticle);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.tblCategories, "CategoryID", "CategoryName", tblArticle.CategoryID);
            ViewBag.AuthorID = new SelectList(db.tblUsers, "UserID", "UserName", tblArticle.AuthorID);
            return View(tblArticle);
        }

        // GET: tblArticle/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblArticle tblArticle = db.tblArticles.Find(id);
            if (tblArticle == null)
                return HttpNotFound();

            ViewBag.CategoryID = new SelectList(db.tblCategories, "CategoryID", "CategoryName", tblArticle.CategoryID);
            ViewBag.AuthorID = new SelectList(db.tblUsers, "UserID", "UserName", tblArticle.AuthorID);
            return View(tblArticle);
        }

        // POST: tblArticle/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ArticleID,Title,Content,CategoryID,AuthorID")] tblArticle tblArticle)
        {
            if (ModelState.IsValid)
            {
                var articleInDb = db.tblArticles.Find(tblArticle.ArticleID);
                if (articleInDb == null)
                    return HttpNotFound();

                articleInDb.Title = tblArticle.Title;
                articleInDb.Content = tblArticle.Content;
                articleInDb.CategoryID = tblArticle.CategoryID;
                articleInDb.AuthorID = tblArticle.AuthorID;
                articleInDb.UpdatedAt = DateTime.Now;

                db.Entry(articleInDb).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.tblCategories, "CategoryID", "CategoryName", tblArticle.CategoryID);
            ViewBag.AuthorID = new SelectList(db.tblUsers, "UserID", "UserName", tblArticle.AuthorID);
            return View(tblArticle);
        }

        // GET: tblArticle/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);

            tblArticle tblArticle = db.tblArticles
                                      .Include(a => a.tblCategory)
                                      .Include(a => a.tblUser)
                                      .FirstOrDefault(a => a.ArticleID == id);

            if (tblArticle == null)
                return HttpNotFound();

            return View(tblArticle);
        }

        // POST: tblArticle/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tblArticle tblArticle = db.tblArticles.Find(id);
            if (tblArticle != null)
            {
                db.tblArticles.Remove(tblArticle);
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
