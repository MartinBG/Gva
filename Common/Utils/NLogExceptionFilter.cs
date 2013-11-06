using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Filters;
using System.Net.Http;
using NLog;
using System.Web.Mvc;

namespace Common.Utils
{
    public class NLogExceptionFilter : ExceptionFilterAttribute, System.Web.Mvc.IExceptionFilter
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, string.Empty, string.Empty);
            errorEvent.Exception = actionExecutedContext.Exception;
            logger.Log(errorEvent);
        }

        void System.Web.Mvc.IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            LogEventInfo errorEvent = new LogEventInfo(LogLevel.Error, string.Empty, string.Empty);
            errorEvent.Exception = filterContext.Exception;
            logger.Log(errorEvent);
        }
    }
}