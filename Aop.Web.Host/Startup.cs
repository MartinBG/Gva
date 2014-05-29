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
using Common.Owin;
using Common.Utils;
using Docs.Api;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Owin;
using Regs.Api;
using Aop.Api;
using Aop.Rio;
using Common.Rio;

namespace Aop.Web.Host
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAutoMapper();
            var container = CreateAutofacContainer();

            App.Configure(app, container);
        }

        public static void ConfigureAutoMapper()
        {
            AutoMapper.Mapper.Initialize(cfg =>
            {
                cfg.AddProfile(new Aop.RioBridge.AopRioBridgeMapperProfile());
            });
        }

        public static IContainer CreateAutofacContainer()
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new CommonModule());
            builder.RegisterModule(new CommonApiModule());
            builder.RegisterModule(new DocsApiModule());
            builder.RegisterModule(new AopApiModule());
            builder.RegisterModule(new RegsApiModule());
            builder.RegisterModule(new CommonRioModule());
            builder.RegisterModule(new AopRioBrdigeModule());
            builder.RegisterModule(new AopRioModule());
            return builder.Build();
        }
    }
}