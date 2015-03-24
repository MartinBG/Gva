using Common.DomainValidation;
using Newtonsoft.Json;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Web.Http.Filters;

namespace Common.Utils
{
    class DomainErrorExceptionFilter : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is DomainErrorException)
            {
                var exception = (DomainErrorException)context.Exception;
                var responseMessage = new ResponseMessage { Messages = exception.ErrorMessages };
                responseMessage.Status = "Error";

                context.Response = new HttpResponseMessage((HttpStatusCode)422);
                context.Response.Content = new StringContent(JsonConvert.SerializeObject(responseMessage));
            }
        }
    }
}
