using System.Web.Http;
using AutoMapper;
using Common.Http;
using Common.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Ninject;

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

            config.MapHttpAttributeRoutes();

            Mapper.Configuration.ConstructServicesUsing(x => kernel.Get(x));
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
    }
}
