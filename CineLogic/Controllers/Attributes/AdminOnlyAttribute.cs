using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CineLogic.Models.Libraries;

namespace CineLogic.Controllers.Attributes
{
    public class AdminOnlyAttribute : ActionFilterAttribute
    {
        private const string Url = "~/Home/NotAuthorised";

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if ((string)HttpContext.Current.Session[SessionTypes.type] != UserTypes.admin)
            {
                filterContext.Result = new RedirectResult(Url);
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}