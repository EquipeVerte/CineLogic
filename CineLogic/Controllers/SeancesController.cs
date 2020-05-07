﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CineLogic.Models;
using CineLogic.Models.Programmation;

namespace CineLogic.Controllers
{
    public class SeancesController : Controller
    {
        private ISeanceRepository repository;

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
