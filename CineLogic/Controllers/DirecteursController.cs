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
    public class DirecteursController : Controller
    {
        private CineDBEntities db = new CineDBEntities();

        // GET: Directeurs
        public ActionResult Index()
        {
            return View(db.Directeurs.ToList());
        }

        // GET: Directeurs/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directeur directeur = db.Directeurs.Find(id);
            if (directeur == null)
            {
                return HttpNotFound();
            }
            return View(directeur);
        }

        // GET: Directeurs/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Directeurs/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Nom")] Directeur directeur)
        {
            if (ModelState.IsValid)
            {
                db.Directeurs.Add(directeur);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(directeur);
        }

        // GET: Directeurs/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directeur directeur = db.Directeurs.Find(id);
            if (directeur == null)
            {
                return HttpNotFound();
            }
            return View(directeur);
        }

        // POST: Directeurs/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Nom")] Directeur directeur)
        {
            if (ModelState.IsValid)
            {
                db.Entry(directeur).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(directeur);
        }

        // GET: Directeurs/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Directeur directeur = db.Directeurs.Find(id);
            if (directeur == null)
            {
                return HttpNotFound();
            }
            return View(directeur);
        }

        // POST: Directeurs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Directeur directeur = db.Directeurs.Find(id);
            db.Directeurs.Remove(directeur);
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
