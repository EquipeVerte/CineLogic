using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CineLogic.Controllers.Attributes
{
    public class SessionActiveOnlyAttribute : ActionFilterAttribute
    {
        //  ActionFilter qui permet acces seulement si le session est active.

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["login"] == null)
            {
                filterContext.Result = new RedirectResult("~/Home/Login");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}