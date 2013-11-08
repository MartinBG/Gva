using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Gva.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("app/{*pathInfo}");

            routes.MapRoute(
               name: null,
               url: "file",
               defaults: new { controller = "File", action = "Index" }
            );

            routes.MapRoute(
                name: "Test",
                url: "test",
                defaults: new { controller = "Home", action = "Test" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{*pathInfo}",
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}