﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using AutoMapper;
using CineLogic.Business.Programmation;
using CineLogic.Controllers.Attributes;
using CineLogic.Models;
using CineLogic.Models.Programmation;
using Newtonsoft.Json;

namespace CineLogic.Controllers
{
    [SessionActiveOnly]
    public class SeancesController : Controller
    {
        private const string SESSION_SS = "SeanceService";
        private const string SESSION_UV = "UnsavedData";

        private ISeanceService seanceService;

        public SeancesController()
        {
            if (System.Web.HttpContext.Current.Session[SESSION_SS] == null)
            {
                seanceService = new SeanceService();
                System.Web.HttpContext.Current.Session[SESSION_SS] = seanceService;
            }
            else
            {
                seanceService = (SeanceService)System.Web.HttpContext.Current.Session[SESSION_SS];
            }
        }

        public SeancesController(ISeanceService seanceService)
        {
            this.seanceService = seanceService;
        }

        public ActionResult Index(int? salleID, DateTime? seanceDate)
        {
            if (System.Web.HttpContext.Current.Session[SESSION_UV] != null)
            {
                if((bool)System.Web.HttpContext.Current.Session[SESSION_UV])
                {
                    ViewBag.UnsavedChanges = true;
                }
            }

            if(salleID != null)
            {
                ViewBag.SalleID = salleID;

                using (CineDBEntities db = new CineDBEntities())
                {
                    ViewBag.CineID = db.Salles.Find(salleID).CinemaID;
                }
                if(seanceDate != null)
                {
                    ViewBag.InitialDate = seanceDate;
                }
                else
                {
                    ViewBag.InitialDate = null;
                }
            }
            else
            {
                ViewBag.SalleID = "";
                ViewBag.CineID = "";
            }

            return View();
        }
        
        [HttpPost]
        [HandleErrorJson]
        [ValidateAjax]
        public ActionResult Create(SeanceViewModel seance)
        {
            seanceService.CreateSeance(seance);

            System.Web.HttpContext.Current.Session[SESSION_UV] = true;

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

            if (System.Web.HttpContext.Current.Session[SESSION_UV] != null)
            {
                if ((bool)System.Web.HttpContext.Current.Session[SESSION_UV])
                {
                    ViewBag.UnsavedChanges = true;
                }
            }

            if(TempData["DuplicateError"] != null)
            {
                ViewBag.DuplicateError = TempData["DuplicateError"];
            }

            return View(seance);
        }

        [HttpPost]
        [HandleError]
        public ActionResult Edit(SeanceViewModel seance)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    seanceService.UpdateSeance(seance);

                    seanceService.UpdateSeanceContents(seance);

                    System.Web.HttpContext.Current.Session[SESSION_UV] = true;

                    return RedirectToAction("Edit");
                }
                catch (ScheduleException ex)
                {
                    ViewBag.ScheduleError = ex.Message;

                    return View(seance);
                }
            }
            else
            {
                return View(seance);
            }
        }

        
        [HttpPost]
        [HandleError]
        public ActionResult Delete(int seanceID)
        {
            seanceService.DeleteSeance(seanceID);

            System.Web.HttpContext.Current.Session[SESSION_UV] = true;

            return RedirectToAction("Index");
        }

        [HttpPost]
        [HandleErrorJson]
        public ActionResult DeleteAjax(int seanceID)
        {
            seanceService.DeleteSeance(seanceID);

            System.Web.HttpContext.Current.Session[SESSION_UV] = true;

            return Json(new { success = true });
        }

        [HttpPost]
        [HandleErrorJson]
        public ActionResult UpdateTimes(SeanceViewModel seanceVM)
        {
            seanceService.UpdateSeanceTimes(seanceVM);

            System.Web.HttpContext.Current.Session[SESSION_UV] = true;

            return Json(new { success = true });
        }

        [HttpPost]
        [HandleErrorJson]
        public ActionResult AdjustTimes(int seanceID)
        {
            seanceService.AdjustTimeToContent(seanceID);

            System.Web.HttpContext.Current.Session[SESSION_UV] = true;

            return Json(new { success = true });
        }

        [HttpPost]
        public ActionResult AddContent(int seanceID, string contenuTitre)
        {
            try
            {
                seanceService.AddContentToSeance(seanceID, contenuTitre);

                System.Web.HttpContext.Current.Session[SESSION_UV] = true;
            }
            catch(DuplicateContentException ex)
            {
                TempData["DuplicateError"] = ex.Message;
            }

            return RedirectToAction("Edit", new { id = seanceID });
        }

        [HttpPost]
        public ActionResult DeleteContent(int seanceID, string contenuTitre)
        {
            seanceService.DeleteContentFromSeance(seanceID, contenuTitre);

            System.Web.HttpContext.Current.Session[SESSION_UV] = true;

            return RedirectToAction("Edit", new { id = seanceID });
        }

        public ActionResult Save()
        {
            seanceService.SaveChanges();

            System.Web.HttpContext.Current.Session[SESSION_UV] = false;

            //return View("Index");

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Annuler()
        {
            seanceService.Dispose();

            System.Web.HttpContext.Current.Session[SESSION_SS] = null;

            System.Web.HttpContext.Current.Session[SESSION_UV] = false;

            //return View("Index");
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //seanceService.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
