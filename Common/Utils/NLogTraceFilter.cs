using System.Web.Http.Controllers;
using NLog;

namespace Common.Utils
{
    public class NLogTraceFilter : System.Web.Http.Filters.ActionFilterAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            Logger.Info(string.Empty);
        }
    }
}