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
using CineLogic.Models.Libraries;

namespace CineLogic.Controllers
{
    [SessionActiveOnly]
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
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
        
        //  Ajax get cinemas.
        [HttpGet]
        public ContentResult Cinemas()
        {
            CineDBEntities db = new CineDBEntities();

            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cinema, CinemaSelectionItem>();
            }).CreateMapper();

            if (Session[SessionTypes.type].Equals(UserTypes.admin))
            {
                return Content(JsonConvert.SerializeObject(mapper.Map<IEnumerable<Cinema>, IEnumerable<CinemaSelectionItem>>(db.Cinemas.Where(c => c.Salles.Count > 0))), "application/json");
            }
            else
            {
                string type = (String)Session[SessionTypes.login];
                return Content(JsonConvert.SerializeObject(mapper.Map<IEnumerable<Cinema>, IEnumerable<CinemaSelectionItem>>(db.Cinemas.Where(c => c.Programmateur.Equals(type)))), "application/json");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        // GET: Cinemas/Assigner/5
        public ActionResult Assigner(int? id)
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

        // POST: Cinemas/Assigner/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Assigner([Bind(Include = "CinemaID,Nom,Adresse,EnExploitation,ResponsableID,Programmateur")] Cinema cinema)
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
    }
}
