using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
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

        private IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Salle, SalleViewModel>();
            cfg.CreateMap<SalleViewModel, Salle>();
        }).CreateMapper();

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
                return RedirectToAction("Index", "Cinemas", new { area = "" });
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
            SalleViewModel s = mapper.Map<Salle, SalleViewModel>(salle);
            ViewBag.CinemaID = new SelectList(db.Cinemas, "CinemaID", "Nom", salle.CinemaID);
            return View(s);
        }

        // POST: Salles/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "SalleID,Nom,TypeEcran,SystemSon,EnExploitation,CinemaID")] SalleViewModel salle)
        {
            Salle s = mapper.Map<SalleViewModel, Salle>(salle);
            if (ModelState.IsValid)
            {
                db.Entry(s).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Cinemas", new { area = "" });
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
            if (salle.Seances.Count > 0)
            {
                List<Seance> seances = db.Seances.Where(t => t.SalleID == id).ToList();
                List<int> seancesId = new List<int>();
                foreach (Seance seance in seances)
                {
                    if (seance.SeanceContenus.Count > 0)
                        seancesId.Add(seance.SeanceID);
                }

                if (seancesId.Count > 0)
                {
                    List<SeanceContenu> seancesContenu = new List<SeanceContenu>();
                    List<SeancePromo> seancesPromo = new List<SeancePromo>();
                    foreach (int j in seancesId)
                    {
                        seancesContenu = db.SeanceContenus.Where(t => t.SeanceID == j).ToList();
                        seancesPromo = db.SeancePromoes.Where(t => t.SeanceID == j).ToList();
                        db.SeanceContenus.RemoveRange(seancesContenu);
                        db.SeancePromoes.RemoveRange(seancesPromo);
                    }
                }
                db.Seances.RemoveRange(seances);
                db.Salles.Remove(salle);
            }
            else
                db.Salles.Remove(salle);
            db.SaveChanges();
            return RedirectToAction("Index", "Cinemas", new { area = "" });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
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
    }
}
