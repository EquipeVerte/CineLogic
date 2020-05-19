using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CineLogic.Controllers.Attributes
{
    public class DontAllowAccessAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectResult("~/Home/Login");

            base.OnActionExecuting(filterContext);
        }
    }
}