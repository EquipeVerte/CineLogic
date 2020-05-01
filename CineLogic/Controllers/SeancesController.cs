using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CineLogic.Models;
using CineLogic.Models.Programmation;
using Newtonsoft.Json;

namespace CineLogic.Controllers
{
    public class SeancesController : Controller
    {
        private ISeanceRepository repository;
        private IMapper mapper = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Seance, SeanceViewModel>();
        })
            .CreateMapper();

        private CineDBEntities db = new CineDBEntities();

        public SeancesController()
        {
            repository = new EFSeanceRepository();
        }

        public SeancesController(ISeanceRepository repository)
        {
            this.repository = repository;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        public ActionResult Create(Seance seance)
        {
            repository.CreateSeance(seance);

            repository.SaveChanges();

            return Json(new { success = true });
        } 

        [HttpPost]
        public ActionResult Validate(Seance seance)
        {
            return Json(new { conflicts = repository.FindSeanceConflicts(seance) });
        }

        [HttpGet]
        public ContentResult Cinemas()
        {
            return Content(JsonConvert.SerializeObject(repository.GetCinemas()), "application/json");
        }

        [HttpGet]
        public ContentResult Salles(int cinemaID)
        {
            return Content(JsonConvert.SerializeObject(repository.GetSalles(cinemaID)), "application/json");
        }

        [HttpGet]
        public ContentResult Contenus(string filter)
        {
            return Content(JsonConvert.SerializeObject((from c in db.Contenus where c.Titre.Contains(filter) select c.Titre)), "application/json");
        }

        [HttpGet]
        public ContentResult Seances(int salleID)
        {
            List<SeanceViewModel> seanceVMs = mapper.Map<IEnumerable<Seance>, IEnumerable<SeanceViewModel>>(repository.GetSeancesBySalle(salleID)).ToList();

            return Content(JsonConvert.SerializeObject(seanceVMs), "application/json");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Seance seance = repository.GetSeance(id.Value);

            if (seance != null)
            {
                return View(seance);
            }
            else
            {
                return HttpNotFound();
            }
        }

        [HttpPost]
        public ActionResult Edit(Seance seance)
        {
            if (ModelState.IsValid)
            {
                repository.UpdateSeance(seance);

                repository.SaveChanges();

                return RedirectToAction("Index");
            }
            else
            {
                return View(seance);
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                repository.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
