using System.Web.Http.Filters;
using NLog;

namespace Common.Utils
{
    public class NLogExceptionFilter : ExceptionFilterAttribute
    {
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        public override void OnException(HttpActionExecutedContext actionExecutedContext)
        {
            Logger.ErrorException(string.Empty, actionExecutedContext.Exception);
        }
    }
}