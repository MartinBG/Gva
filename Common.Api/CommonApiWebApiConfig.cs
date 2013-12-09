using System;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Common.Http;

namespace Common.Api
{
    public class CommonApiWebApiConfig : IWebApiConfig
    {
        public void RegisterRoutes(HttpConfiguration config)
        {
            this.MapRoute(config, HttpMethod.Get, "api/file", "Blob", "Get");
            this.MapRoute(config, HttpMethod.Post, "api/file", "Blob", "Post");
        }

        private void MapRoute(HttpConfiguration config, HttpMethod method, string route, string controller, string action)
        {
            config.Routes.MapHttpRoute(
                name: Guid.NewGuid().ToString(),
                routeTemplate: route,
                defaults: new { controller = controller, action = action },
                constraints: new { httpMethod = new HttpMethodConstraint(method) });
        }
    }
}
