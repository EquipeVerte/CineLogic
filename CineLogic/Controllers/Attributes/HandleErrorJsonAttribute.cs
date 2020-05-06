using CineLogic.Business;
using CineLogic.Business.Programmation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;

namespace CineLogic.Controllers.Attributes
{
    public class HandleErrorJsonAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            filterContext.ExceptionHandled = true;
            filterContext.HttpContext.Response.Clear();
            filterContext.HttpContext.Response.StatusCode = 500;
            filterContext.HttpContext.Response.TrySkipIisCustomErrors = true;

            AjaxError data = null;

            if (filterContext.Exception.GetType() == typeof(DBException))
            {
                data = new AjaxError("Base de données", new string[] { filterContext.Exception.Message });
            }
            else if (filterContext.Exception.GetType() == typeof(ScheduleException)) {
                data = new AjaxError("Horaire", new string[] { filterContext.Exception.Message });
            }
            else
            {
                data = new AjaxError("Source Inconnu", new string[] { filterContext.Exception.Message });
            }

            filterContext.Result = new JsonResult
            {
                JsonRequestBehavior = JsonRequestBehavior.AllowGet,
                ContentType = "application/json",
                Data = new AjaxError[] { data }
            };
        }

    }
}