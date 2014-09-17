﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Http;
using Autofac;
using Autofac.Integration.Owin;
using Autofac.Integration.WebApi;
using Autofac.Integration.WebApi.Owin;
using Common.Http;
using Common.Jobs;
using Common.Utils;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;

namespace Common.Owin
{
    public class App
    {
        public static JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings()
                {
#if DEBUG
                    Formatting = Formatting.Indented,
#else
                    Formatting = Formatting.None,
#endif
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    NullValueHandling = NullValueHandling.Include,
                    DefaultValueHandling = DefaultValueHandling.Include,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    ContractResolver = new CamelCasePropertyNamesContractResolver(),
                    Converters = new List<JsonConverter>()
                    {
                        new StringEnumConverter { CamelCaseText = true }
                    }
                };

        public static void Configure(IAppBuilder app, IContainer container)
        {
            app.UseAutofacMiddleware(container);
            app.UseSession();
            ConfigureAuth(app, container);
            ConfigureWebApi(app, container);
            ConfigureStaticFiles(app);
            StartJobs(container);
        }

        public static void ConfigureAuth(IAppBuilder app, IContainer container)
        {
            app.UseOAuthAuthorizationServer(new OAuthAuthorizationServerOptions
            {
                TokenEndpointPath = new PathString("/api/token"),
                Provider = container.Resolve<IOAuthAuthorizationServerProvider>(),
                AccessTokenExpireTimeSpan = TimeSpan.FromHours(8),
                AllowInsecureHttp = true
            });

            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions
            {
                Provider = container.Resolve<IOAuthBearerAuthenticationProvider>(),
                //override the token deserialization to be able to capture the properties
                AccessTokenProvider = new AuthenticationTokenProvider()
                {
                    OnReceive = (c) =>
                    {
                        c.DeserializeTicket(c.Token);

                        //check if invalid bearer token received
                        if (c.Ticket != null)
                        {
                            c.OwinContext.Environment["oauth.Properties"] = c.Ticket.Properties;
                        }
                    }
                }
            });
        }

        public static void ConfigureWebApi(IAppBuilder app, IContainer container)
        {
            HttpConfiguration config = new HttpConfiguration();

            ContainerBuilder builder = new ContainerBuilder();
            builder.RegisterHttpRequestMessage(config);
            builder.Update(container);

            config.SuppressDefaultHostAuthentication();
            config.Filters.Add(new HostAuthenticationFilter(OAuthDefaults.AuthenticationType));

            config.Formatters.JsonFormatter.SerializerSettings = JsonSerializerSettings;

            JsonConvert.DefaultSettings = () => JsonSerializerSettings;

            config.Filters.Add(new NLogTraceFilter());
            config.Filters.Add(new NLogExceptionFilter());

            config.MapHttpAttributeRoutes(new CustomDirectRouteProvider());

            foreach (IWebApiConfig webApiConfig in container.Resolve<IEnumerable<IWebApiConfig>>())
            {
                webApiConfig.RegisterRoutes(config);
            }

            app.UseAutofacWebApi(config);
            app.UseWebApi(config);
        }

        public static void ConfigureStaticFiles(IAppBuilder app)
        {
            app.UseReroute("/", "/index.html");
            app.UseStaticFiles(new StaticFileOptions
            {
                RequestPath = new PathString(""),
                FileSystem = new PhysicalFileSystem("./App"),
                ContentTypeProvider = new ContentTypeProvider(),
                ServeUnknownFileTypes = false
            });
        }

        public static void StartJobs(IContainer container)
        {
            var jobs = container.Resolve<IJob[]>();

            foreach (var job in jobs)
            {
                (new JobHost(job)).Start();
            }
        }
    }
}
