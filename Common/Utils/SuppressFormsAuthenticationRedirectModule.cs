using System;
using System.Web;

namespace Common.Utils
{
    public class SuppressAjaxFormsAuthRedirectModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            context.BeginRequest += this.OnBeginRequest;
        }

        public void Dispose()
        {
        }

        private void OnBeginRequest(object source, EventArgs e)
        {
            var context = (HttpApplication)source;
            var response = context.Response;
            var request = context.Request;

            if (request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                response.SuppressFormsAuthenticationRedirect = true;
            }
        }
    }
}
