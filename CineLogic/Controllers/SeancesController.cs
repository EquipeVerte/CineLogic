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
using CineLogic.Models;
using CineLogic.Models.Programmation;
using Newtonsoft.Json;

namespace CineLogic.Controllers
{
    public class SeancesController : Controller
    {
        private ISeanceService seanceService;

        private CineDBEntities db = new CineDBEntities();

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
        public ActionResult Create(Seance seance)
        {
            if (SeanceHasConflicts(seance) || seance.HeureFin <= seance.HeureDebut)
            {
                return Json(new { success = false });
            }

            seanceService.CreateSeance(seance);

            return Json(new { success = true });
        }

        [HttpGet]
        public ContentResult Seances(int salleID)
        {
            

            return Content(JsonConvert.SerializeObject(seanceService.GetSeancesBySalle(salleID)), "application/json");
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null) 
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Seance seance = seanceService.GetSeance(id.Value);

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
                seanceService.UpdateSeance(seance);

                return RedirectToAction("Index");
            }
            else
            {
                return View(seance);
            }
        }

        [HttpPost]
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
            return Content(JsonConvert.SerializeObject(seanceService.GetCinemas()), "application/json");
        }

        [HttpGet]
        public ContentResult Salles(int cinemaID)
        {
            return Content(JsonConvert.SerializeObject(seanceService.GetSalles(cinemaID)), "application/json");
        }

        [HttpGet]
        public ContentResult Contenus(string filter)
        {
            return Content(JsonConvert.SerializeObject((from c in db.Contenus where c.Titre.Contains(filter) select c.Titre)), "application/json");
        }
    }
}
