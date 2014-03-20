using System.Web.Http.Filters;
using System.Web.Mvc;
using NLog;

namespace Common.Utils
{
    public class NLogExceptionFilter : ExceptionFilterAttribute, System.Web.Mvc.IExceptionFilter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Logger.ErrorException(string.Empty, actionExecutedContext.Exception);
        }

        void System.Web.Mvc.IExceptionFilter.OnException(ExceptionContext filterContext)
        {
            Logger.ErrorException(string.Empty, filterContext.Exception);
        }
    }
}