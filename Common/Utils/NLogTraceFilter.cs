using System.Web.Http.Controllers;
using System.Web.Mvc;
using NLog;

namespace Common.Utils
{
    public class NLogTraceFilter : System.Web.Http.Filters.ActionFilterAttribute, System.Web.Mvc.IActionFilter
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Logger.Info(string.Empty);
        }

        void System.Web.Mvc.IActionFilter.OnActionExecuted(ActionExecutedContext filterContext)
        {
        }

        void System.Web.Mvc.IActionFilter.OnActionExecuting(ActionExecutingContext filterContext)
        {
            Logger.Info(string.Empty);
        }
    }
}