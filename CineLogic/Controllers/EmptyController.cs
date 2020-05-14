using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CineLogic.Controllers
{
    public class EmptyController : Controller
    {
        // GET: Empty
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddEmpty()
        {
            return PartialView("AddEmpty");
        }

        public ActionResult ShowEmpty()
        {
            return PartialView("ShowEmpty");
        }

        public ActionResult EditEmpty()
        {
            return PartialView("EditEmpty");
        }

        public ActionResult DeleteEmpty()
        {
            return PartialView("DeleteEmpty");
        }
    }
}