using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CineLogic.Controllers.Attributes;
using CineLogic.Models;
using CineLogic.Models.Programmation;
using Newtonsoft.Json;

namespace CineLogic.Controllers
{
    [SessionActiveOnly]
    public class SallesController : Controller
    {
        private CineDBEntities db = new CineDBEntities();

        // GET: Salles
        public ActionResult Index()
        {
            var salles = db.Salles.Include(s => s.Cinema);
            return View(salles.ToList());
        }

        // GET: Salles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salle salle = db.Salles.Find(id);
            if (salle == null)
            {
                return HttpNotFound();
            }
            return View(salle);
        }

        // GET: Salles/Create
        public ActionResult Create()
        {
            ViewBag.CinemaID = new SelectList(db.Cinemas, "CinemaID", "Nom");
            return View();
        }

        // POST: Salles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "SalleID,Nom,TypeEcran,SystemSon,EnExploitation,CinemaID")] Salle salle)
        {
            if (ModelState.IsValid)
            {
                db.Salles.Add(salle);
                db.SaveChanges();
                return RedirectToAction("Index", "Cinemas");
            }

            ViewBag.CinemaID = new SelectList(db.Cinemas, "CinemaID", "Nom", salle.CinemaID);
            return View(salle);
        }

        // GET: Salles/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salle salle = db.Salles.Find(id);
            if (salle == null)
            {
                return HttpNotFound();
            }
            ViewBag.CinemaID = new SelectList(db.Cinemas, "CinemaID", "Nom", salle.CinemaID);
            return View(salle);
        }

        // POST: Salles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalleID,Nom,TypeEcran,SystemSon,EnExploitation,CinemaID")] Salle salle)
        {
            if (ModelState.IsValid)
            {
                db.Entry(salle).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CinemaID = new SelectList(db.Cinemas, "CinemaID", "Nom", salle.CinemaID);
            return View(salle);
        }

        // GET: Salles/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Salle salle = db.Salles.Find(id);
            if (salle == null)
            {
                return HttpNotFound();
            }
            return View(salle);
        }

        // POST: Salles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Salle salle = db.Salles.Find(id);
            db.Salles.Remove(salle);
            db.SaveChanges();
            return RedirectToAction("Index", "Salles");
        }

        //  Ajax get salles.
        [HttpGet]
        public ContentResult Salles(int cinemaID)
        {
            CineDBEntities db = new CineDBEntities();

            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Salle, SalleSelectionItem>();
            }).CreateMapper();

            return Content(JsonConvert.SerializeObject(mapper.Map<IEnumerable<Salle>, IEnumerable<SalleSelectionItem>>(db.Salles.Where(s => s.CinemaID == cinemaID))), "application/json");
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
