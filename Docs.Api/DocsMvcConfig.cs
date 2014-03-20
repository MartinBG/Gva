using Common.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Docs.Api
{
    public class DocsMvcConfig : IMvcConfig
    {
        public void RegisterRoutes(System.Web.Routing.RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.IgnoreRoute("app/{*pathInfo}");
        }
    }
}
