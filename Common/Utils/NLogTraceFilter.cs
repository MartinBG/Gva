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
        public static readonly string MvcRequestIdKey = "__Log4NetTraceFilterRequestIdKey__";
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            string userHostAddress = string.Empty;
            if (actionContext.Request.Properties.ContainsKey("MS_HttpContext"))
            {
               userHostAddress = ((System.Web.HttpContextWrapper)actionContext.Request.Properties["MS_HttpContext"]).Request.UserHostAddress;
            }

            LogEventInfo infoEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);
            logger.Log(infoEvent);
        }

        void System.Web.Mvc.IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        void System.Web.Mvc.IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            LogEventInfo infoEvent = new LogEventInfo(LogLevel.Info, string.Empty, string.Empty);
            logger.Log(infoEvent);
        }
    }
}