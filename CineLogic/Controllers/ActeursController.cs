using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CineLogic.Controllers.Attributes;
using CineLogic.Models;

namespace CineLogic.Controllers
{
    [SessionActiveOnly]
    public class ActeursController : Controller
    {
        private CineDBEntities db = new CineDBEntities();

        // GET: Acteurs
        public ActionResult Index()
        {
            return View(db.Acteurs.ToList());
        }

        // GET: Acteurs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acteur acteur = db.Acteurs.Find(id);
            if (acteur == null)
            {
                return HttpNotFound();
            }
            return View(acteur);
        }

        // GET: Acteurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Acteurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nom")] Acteur acteur)
        {
            if (ModelState.IsValid)
            {
                db.Acteurs.Add(acteur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(acteur);
        }

        // GET: Acteurs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acteur acteur = db.Acteurs.Find(id);
            if (acteur == null)
            {
                return HttpNotFound();
            }
            return View(acteur);
        }

        // POST: Acteurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nom")] Acteur acteur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(acteur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(acteur);
        }

        // GET: Acteurs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Acteur acteur = db.Acteurs.Find(id);
            if (acteur == null)
            {
                return HttpNotFound();
            }
            return View(acteur);
        }

        // POST: Acteurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Acteur acteur = db.Acteurs.Find(id);
            db.Acteurs.Remove(acteur);
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
