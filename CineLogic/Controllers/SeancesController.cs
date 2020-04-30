using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CineLogic.Models;
using CineLogic.Models.Programmation;
using Newtonsoft.Json;

namespace CineLogic.Controllers
{
    public class SeancesController : Controller
    {
        private ISeanceRepository repository;

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
            

            return Json(new { success = true });
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
