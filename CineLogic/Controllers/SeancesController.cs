using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using CineLogic.Business.Programmation;
using CineLogic.Controllers.Attributes;
using CineLogic.Models;
using CineLogic.Models.Programmation;
using Newtonsoft.Json;

namespace CineLogic.Controllers
{
    public class SeancesController : Controller
    {
        private ISeanceService seanceService;

        public SeancesController()
        {
            seanceService = new SeanceService();
        }

        public SeancesController(ISeanceService seanceService)
        {
            this.seanceService = seanceService;
        }

        public ActionResult Index()
        {
            return View();
        }
        
        [HttpPost]
        [HandleErrorJson]
        public ActionResult Create(SeanceViewModel seance)
        {
            seanceService.CreateSeance(seance);

            return Json(new { success = true });
        }

        [HttpGet]
        public ContentResult Seances(int salleID)
        {
            return Content(JsonConvert.SerializeObject(seanceService.GetSeancesBySalle(salleID)), "application/json");
        }

        [HttpGet]
        [HandleError]
        public ActionResult Edit(int? id)
        {
            if (id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeanceViewModel seance = seanceService.GetSeance(id.Value);

            return View(seance);
        }

        [HttpPost]
        [HandleError]
        public ActionResult Edit(SeanceViewModel seance)
        {
            if (ModelState.IsValid)
            {
                seanceService.UpdateSeance(seance);

                return RedirectToAction("Index");
            }
            else
            {
                return View(seance);
            }
        }

        [HttpPost]
        [HandleErrorJson]
        public ActionResult Delete(int seanceID)
        {
            seanceService.DeleteSeance(seanceID);

            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                seanceService.Dispose();
            }
            base.Dispose(disposing);
        }

        //  Méthodes à remplacer dans les autres controlleurs.
        [HttpGet]
        public ContentResult Cinemas()
        {
            CineDBEntities db = new CineDBEntities();

            IMapper mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cinema, CinemaSelectionItem>();
            }).CreateMapper();

            return Content(JsonConvert.SerializeObject(mapper.Map<IEnumerable<Cinema>, IEnumerable<CinemaSelectionItem>>(db.Cinemas)), "application/json");
        }

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

        [HttpGet]
        public ContentResult Contenus(string filter)
        {
            CineDBEntities db = new CineDBEntities();

            return Content(JsonConvert.SerializeObject((from c in db.Contenus where c.Titre.Contains(filter) select c.Titre)), "application/json");
        }
    }
}
