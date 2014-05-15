using System;
using System.Collections.Generic;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Owin;
using Autofac.Integration.WebApi.Owin;
using Common;
using Common.Api;
using Common.Api.OAuth;
using Common.Http;
using Common.Utils;
using Docs.Api;
using Gva.Api;
using Gva.Web.Owin;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Regs.Api;
using Common.Api.Jobs;
using Gva.Rio;
using Common.Rio;

namespace Gva.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAutoMapper();
            var container = CreateAutofacContainer();
            app.UseAutofacMiddleware(container);
            ConfigureAuth(app);
            ConfigureWebApi(app, container);
            ConfigureStaticFiles(app);
            StartJobs(container);
        }

        public static void ConfigureAutoMapper()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new Gva.RioBridge.GvaRioBridgeMapperProfile());
            });
        }

        public static IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new CommonApiModule());
            builder.RegisterModule(new DocsApiModule());
            builder.RegisterModule(new GvaApiModule());
            builder.RegisterModule(new RegsApiModule());
            builder.RegisterModule(new CommonRioModule());
            builder.RegisterModule(new GvaRioBrdigeModule());
            builder.RegisterModule(new GvaRioModule());
            return builder.Build();
        }

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseOAuthBearerTokens(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/token"),
                Provider = new ApplicationOAuthProvider(),
                //override the token serialization/deserialization to be able to capture the properties
                AccessTokenProvider = new AuthenticationTokenProvider()
                {
                    OnCreate = (c) =>
                    {
                        c.SetToken(c.SerializeTicket());
                    },
                    OnReceive = (c) =>
                    {
                        c.DeserializeTicket(c.Token);
                        c.OwinContext.Environment["oauth.Properties"] = c.Ticket.Properties;
                    }
                },
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(8),
                AllowInsecureHttp = true
            });
        }

        public void ConfigureWebApi(IAppBuilder app, IContainer container)
        {
            HttpConfiguration config = new HttpConfiguration();

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

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

            config.Filters.Add(new NLogTraceFilter());
            config.Filters.Add(new NLogExceptionFilter());

            config.MapHttpAttributeRoutes();

            foreach (IWebApiConfig webApiConfig in container.Resolve<IEnumerable<IWebApiConfig>>())
            {
                webApiConfig.RegisterRoutes(config);
            }

            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }

        public void ConfigureStaticFiles(IAppBuilder app)
        {
            app.UseReroute("/", "/app/build/index.html");
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = new PathString(""),
                FileSystem = new PhysicalFileSystem("./"),
                ContentTypeProvider = new ContentTypeProvider(),
                ServeUnknownFileTypes = false
            });
        }

        public void StartJobs(IContainer container)
        {
            var jobs = container.Resolve<IJob[]>();

            foreach (var job in jobs)
            {
                (new JobHost(job)).Start();
            }
        }
    }
}