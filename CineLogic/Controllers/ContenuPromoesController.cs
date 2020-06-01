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
    public class ContenuPromoesController : Controller
    {
        private CineDBEntities db = new CineDBEntities();

        // GET: ContenuPromoes
        public ActionResult Index()
        {
            return View(db.ContenuPromoes.ToList());
        }

        // GET: ContenuPromoes/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContenuPromo contenuPromo = db.ContenuPromoes.Find(id);
            if (contenuPromo == null)
            {
                return HttpNotFound();
            }
            return View(contenuPromo);
        }

        // GET: ContenuPromoes/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContenuPromoes/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Titre,RuntimeMins")] ContenuPromo contenuPromo)
        {
            if (ModelState.IsValid)
            {
                db.ContenuPromoes.Add(contenuPromo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contenuPromo);
        }

        // GET: ContenuPromoes/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContenuPromo contenuPromo = db.ContenuPromoes.Find(id);
            if (contenuPromo == null)
            {
                return HttpNotFound();
            }
            return View(contenuPromo);
        }

        // POST: ContenuPromoes/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Titre,RuntimeMins")] ContenuPromo contenuPromo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contenuPromo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contenuPromo);
        }

        // GET: ContenuPromoes/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContenuPromo contenuPromo = db.ContenuPromoes.Find(id);
            if (contenuPromo == null)
            {
                return HttpNotFound();
            }
            return View(contenuPromo);
        }

        // POST: ContenuPromoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ContenuPromo contenuPromo = db.ContenuPromoes.Find(id);
            db.ContenuPromoes.Remove(contenuPromo);
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
