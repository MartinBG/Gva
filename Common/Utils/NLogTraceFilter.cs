using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using NLog;
using System.Web.Http.Controllers;
using System.Web.Mvc;

namespace Common.Utils
{
    public class NLogTraceFilter : System.Web.Http.Filters.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            logger.Info(string.Empty);
        }

        void System.Web.Mvc.IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        void System.Web.Mvc.IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            logger.Info(string.Empty);
        }
    }
}