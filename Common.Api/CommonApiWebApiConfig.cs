using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using System.Web.Http.Routing.Constraints;
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
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/{alias}/{id}"        , "Nom", "GetNom", new Dictionary<string, object>() { {"id", new IntRouteConstraint() } });
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/{alias}/{valueAlias}", "Nom", "GetNom");
            this.MapRoute(config, HttpMethod.Get, "api/nomenclatures/{alias}"             , "Nom", "GetNoms");

            // signs
            this.MapRoute(config, HttpMethod.Post, "api/signXml", "Sign", "PostSignXml");
            this.MapRoute(config, HttpMethod.Post, "api/signOffice", "Sign", "PostSignOffice");

            //user
            this.MapRoute(config, HttpMethod.Get, "api/user/currentData", "User", "GetUserData");
            this.MapRoute(config, HttpMethod.Get, "api/user/duplicateUnit", "User", "CheckDuplicateUnit");
            this.MapRoute(config, HttpMethod.Get, "api/users", "User", "GetUsers");
            this.MapRoute(config, HttpMethod.Get, "api/users/{id}", "User", "GetUser");
            this.MapRoute(config, HttpMethod.Post, "api/users/{id}", "User", "UpdateUser");
            this.MapRoute(config, HttpMethod.Post, "api/users", "User", "CreateUser");
            

            //role
            this.MapRoute(config, HttpMethod.Get, "api/roles", "User", "GetRoles");
        }

        private void MapRoute(HttpConfiguration config, HttpMethod method, string route, string controller, string action, IDictionary<string, object> paramConstraints = null)
        {
            var constraints = (IDictionary<string, object>)new ExpandoObject();
            if (paramConstraints != null)
            {
                foreach (var kvp in paramConstraints)
                {
                    constraints.Add(kvp.Key, kvp.Value);
                }
            }
            constraints.Add("httpMethod", new HttpMethodConstraint(method));

            config.Routes.MapHttpRoute(
                name: Guid.NewGuid().ToString(),
                routeTemplate: route,
                defaults: new { controller = controller, action = action },
                constraints: constraints);
        }
    }
}
