using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CineLogic.Models;

namespace CineLogic.Controllers
{
    public class CinemasController : Controller
    {
        private CineDBEntities db = new CineDBEntities();

        // GET: Cinemas
        public ActionResult Index()
        {
            var cinemas = db.Cinemas.Include(c => c.Responsable).Include(c => c.User);
            return View(cinemas.ToList());
        }

        // GET: Cinemas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinema cinema = db.Cinemas.Find(id);
            if (cinema == null)
            {
                return HttpNotFound();
            }
            return View(cinema);
        }

        // GET: Cinemas/Create
        public ActionResult Create()
        {
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ResponsableID", "Nom");
            ViewBag.Programmateur = new SelectList(db.Users, "Login", "NomComplet");
            return View();
        }

        // POST: Cinemas/Create
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CinemaID,Nom,Adresse,EnExploitation,ResponsableID,Programmateur")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                db.Cinemas.Add(cinema);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ResponsableID = new SelectList(db.Responsables, "ResponsableID", "Nom", cinema.ResponsableID);
            ViewBag.Programmateur = new SelectList(db.Users, "Login", "NomComplet", cinema.Programmateur);
            return View(cinema);
        }

        // GET: Cinemas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinema cinema = db.Cinemas.Find(id);
            if (cinema == null)
            {
                return HttpNotFound();
            }
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ResponsableID", "Nom", cinema.ResponsableID);
            ViewBag.Programmateur = new SelectList(db.Users, "Login", "NomComplet", cinema.Programmateur);
            return View(cinema);
        }

        // POST: Cinemas/Edit/5
        // Чтобы защититься от атак чрезмерной передачи данных, включите определенные свойства, для которых следует установить привязку. Дополнительные 
        // сведения см. в статье https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CinemaID,Nom,Adresse,EnExploitation,ResponsableID,Programmateur")] Cinema cinema)
        {
            if (ModelState.IsValid)
            {
                db.Entry(cinema).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ResponsableID", "Nom", cinema.ResponsableID);
            ViewBag.Programmateur = new SelectList(db.Users, "Login", "NomComplet", cinema.Programmateur);
            return View(cinema);
        }

        // GET: Cinemas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cinema cinema = db.Cinemas.Find(id);
            if (cinema == null)
            {
                return HttpNotFound();
            }
            return View(cinema);
        }

        // POST: Cinemas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cinema cinema = db.Cinemas.Find(id);
            db.Cinemas.Remove(cinema);
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
