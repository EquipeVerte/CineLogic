using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CineLogic.Models;

namespace CineLogic.Controllers.Attributes
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            HttpContext ctxt = HttpContext.Current;
            if ((string)HttpContext.Current.Session["type"] != "admin")
            {
                filterContext.Result = new RedirectResult("~/Home/NotAuthorised");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}