using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ASPNET_MVC_Bootstrap4_Template.Models;

namespace ASPNET_MVC_Bootstrap4_Template.Controllers
{
    public class ContenusController : Controller
    {
        private CineDBEntities db = new CineDBEntities();

        // GET: Contenus
        public ActionResult Index()
        {
            return View(db.Contenus.ToList());
        }

        // GET: Contenus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contenu contenu = db.Contenus.Find(id);
            if (contenu == null)
            {
                return HttpNotFound();
            }
            return View(contenu);
        }

        // GET: Contenus/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contenus/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Titre,Description,Annee,RuntimeMins,Rating,Votes,Revenue,MetaScore")] Contenu contenu)
        {
            if (ModelState.IsValid)
            {
                db.Contenus.Add(contenu);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contenu);
        }

        // GET: Contenus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contenu contenu = db.Contenus.Find(id);
            if (contenu == null)
            {
                return HttpNotFound();
            }
            return View(contenu);
        }

        // POST: Contenus/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Titre,Description,Annee,RuntimeMins,Rating,Votes,Revenue,MetaScore")] Contenu contenu)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contenu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contenu);
        }

        // GET: Contenus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Contenu contenu = db.Contenus.Find(id);
            if (contenu == null)
            {
                return HttpNotFound();
            }
            return View(contenu);
        }

        // POST: Contenus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Contenu contenu = db.Contenus.Find(id);
            db.Contenus.Remove(contenu);
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
