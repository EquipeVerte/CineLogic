using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CineLogic.Models;

namespace CineLogic.Controllers
{
    public class ShowDataController : Controller
    {
        private CineDBEntities db = new CineDBEntities();

        // GET: /ShowData/
        public ActionResult Index()
        {
            return View();
        }

        ModelServices mobjModel = new ModelServices();

        public ActionResult CinemaTable()
        {
            var cinemas = db.Cinemas;
            return View(cinemas.ToList());
        }

        [HttpGet]
        public JsonResult UpdateRecord(int id, string nom, string adresse, bool enExploitation, int responsableID, string programmateur)
        {
            bool result = false;
            try
            {
                result = mobjModel.UpdateCinema(id, nom, adresse, enExploitation, responsableID, programmateur);
            }
            catch (Exception ex)
            {
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult SaveRecord(string nom, string adresse, bool enExploitation, int responsableID, string programmateur)
        {
            bool result = false;
            try
            {
                result = mobjModel.SaveCinema(nom, adresse, enExploitation, responsableID, programmateur);
            }
            catch (Exception ex)
            {
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult DeleteRecord(int id)
        {
            bool result = false;
            try
            {
                result = mobjModel.DeleteCinema(id);
            }
            catch (Exception ex)
            {
            }

            return Json(new { result }, JsonRequestBehavior.AllowGet);
        }

    }

}