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
            // blobs
            this.MapRoute(config, HttpMethod.Get , "api/file", "Blob", "Get");
            this.MapRoute(config, HttpMethod.Post, "api/file", "Blob", "Post");

            // nomenclatures
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/addressTypes"     , "Nomenclature", "GetAddressTypes");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/documentRoles"    , "Nomenclature", "GetDocumentRoles");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/documentTypes"    , "Nomenclature", "GetDocumentTypes");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/otherDocumentRole", "Nomenclature", "GetOtherRoles");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/{alias}"          , "Nomenclature", "GetNoms");
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
