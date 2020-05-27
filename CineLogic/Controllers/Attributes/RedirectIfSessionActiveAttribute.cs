using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CineLogic.Controllers.Attributes
{
    public class RedirectIfSessionActiveAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session["login"] != null)
            {
                filterContext.Result = new RedirectResult("~/Home/Admin");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}