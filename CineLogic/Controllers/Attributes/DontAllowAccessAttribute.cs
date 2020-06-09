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