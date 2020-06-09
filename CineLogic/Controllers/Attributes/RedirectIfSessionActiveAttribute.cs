using System.Web.Mvc;
using CineLogic.Models.Libraries;

namespace CineLogic.Controllers.Attributes
{
    public class RedirectIfSessionActiveAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (filterContext.HttpContext.Session[SessionTypes.login] != null)
            {
                filterContext.Result = new RedirectResult("~/Home/Admin");
                return;
            }
            base.OnActionExecuting(filterContext);
        }
    }
}