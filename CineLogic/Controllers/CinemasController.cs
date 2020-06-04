using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CineLogic.Models;
using AutoMapper;
using Newtonsoft.Json;
using CineLogic.Models.Libraries;
using CineLogic.Models.Programmation;

namespace CineLogic.Controllers
{
    public class CinemasController : Controller
    {
        private CineDBEntities db = new CineDBEntities();

        private IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Cinema, CinemaViewModel>();
            cfg.CreateMap<CinemaViewModel, Cinema>();
        }).CreateMapper();

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
            CinemaViewModel c = mapper.Map<Cinema, CinemaViewModel>(cinema);
            ViewBag.ResponsableID = new SelectList(db.Responsables, "ResponsableID", "Nom", cinema.ResponsableID);
            ViewBag.Programmateur = new SelectList(db.Users.Where(u => u.Type == UserTypes.prog), "Login", "NomComplet", cinema.Programmateur);
            return View(c);
        }

        // POST: Cinemas/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CinemaID,Nom,Adresse,EnExploitation,ResponsableID,Programmateur")] CinemaViewModel cinema)
        {
            Cinema c = mapper.Map<CinemaViewModel, Cinema>(cinema);
            if (ModelState.IsValid)
            {
                db.Entry(c).State = EntityState.Modified;
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
            if (cinema.Salles.Count > 0)
            {
                bool contientSeances = false;
                List<Salle> salles = db.Salles.Where(t => t.CinemaID == id).ToList();
                foreach (Salle salle in salles)
                {
                    if (salle.Seances.Count > 0)
                        contientSeances = true;
                }

                if (contientSeances)
                {
                    var seances = db.Seances.Where(t => t.SalleID == id).ToList();
                    db.Seances.RemoveRange(seances);
                    db.Salles.RemoveRange(salles);
                    db.Cinemas.Remove(cinema);
                }
                else
                {
                    db.Salles.RemoveRange(salles);
                    db.Cinemas.Remove(cinema);
                }

            }
            else
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
    }
}

