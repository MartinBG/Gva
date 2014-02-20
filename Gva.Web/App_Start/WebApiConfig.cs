using Common.Http;
using Common.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Gva.Web
{
    public static class WebApiConfig
    {
        public static void Register(IKernel kernel, HttpConfiguration config)
        {
            config.DependencyResolver = new NinjectDependencyResolver(kernel);

            config.Formatters.JsonFormatter.SerializerSettings =
                new JsonSerializerSettings()
                {
#if DEBUG
                    Formatting = Newtonsoft.Json.Formatting.Indented,
#endif
                    ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    ContractResolver = new CamelCasePropertyNamesContractResolver()
                };

            RegisterGlobalFilters(config);

            RegisterRoutes(config);

            foreach (IWebApiConfig webApiConfig in kernel.GetAll<IWebApiConfig>())
            {
                webApiConfig.RegisterRoutes(config);
            }
        }

        private static void RegisterGlobalFilters(HttpConfiguration config)
        {
            config.Filters.Add(new NLogTraceFilter());
            config.Filters.Add(new NLogExceptionFilter());
        }

        public static void RegisterRoutes(HttpConfiguration config)
        {
            // nomenclatures
            MapRoute(config, HttpMethod.Get, "api/nomenclatures/addressTypes", "Nomenclature", "GetAddressTypes");
            MapRoute(config, HttpMethod.Get, "api/nomenclatures/{alias}", "Nomenclature", "GetNoms");

            //persons
            MapRoute(config, HttpMethod.Post, "api/persons"       , "Person", "PostPerson");
            MapRoute(config, HttpMethod.Get, "api/persons/{lotId}", "Person", "GetPerson");

            //lots
            MapRoute(config, HttpMethod.Get, "api/persons/{lotId}/personData", "Lot", "GetPart");
            MapRoute(config, HttpMethod.Get, "api/persons/{lotId}/{*path}", "Lot", "GetPart", new Dictionary<string, string>() { { "path", @"^(.+/)*\d+$" } });
            MapRoute(config, HttpMethod.Get, "api/persons/{lotId}/{*path}", "Lot", "GetParts");
            MapRoute(config, HttpMethod.Post, "api/persons/{lotId}/personData", "Lot", "PostPart");
            MapRoute(config, HttpMethod.Post, "api/persons/{lotId}/{*path}", "Lot", "PostPart", new Dictionary<string, string>() { { "path", @"^(.+/)*\d+$" } });
            MapRoute(config, HttpMethod.Post, "api/persons/{lotId}/{*path}", "Lot", "PostNewPart");
        }

        private static void MapRoute(HttpConfiguration config, HttpMethod method, string route, string controller, string action, Dictionary<string, string> regExpressions = null)
        {
            dynamic constraints = new ExpandoObject();
            constraints.httpMethod = new HttpMethodConstraint(method);
            if (regExpressions != null)
            {
                foreach (var prop in regExpressions)
                {
                    (constraints as IDictionary<string, object>)[prop.Key] = prop.Value;
                }
            }

            config.Routes.MapHttpRoute(
                name: Guid.NewGuid().ToString(),
                routeTemplate: route,
                defaults: new { controller = controller, action = action, path = "personData" },
                constraints: (object)constraints);
        }
    }
}
