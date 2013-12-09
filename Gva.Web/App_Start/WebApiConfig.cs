using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Routing;
using Common.Http;
using Common.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;
using NLog.Config;

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
        }

        private static void MapRoute(HttpConfiguration config, HttpMethod method, string route, string controller, string action)
        {
            config.Routes.MapHttpRoute(
                name: Guid.NewGuid().ToString(),
                routeTemplate: route,
                defaults: new { controller = controller, action = action },
                constraints: new { httpMethod = new HttpMethodConstraint(method) });
        }
    }
}
