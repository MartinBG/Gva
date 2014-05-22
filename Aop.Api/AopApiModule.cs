﻿using Aop.Api.Controllers;
using Aop.Api.Models;
using Aop.Api.Repositories.Aop;
using Autofac;
using Common.Data;
using Common.Http;

namespace Aop.Api
{
    public class AopApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<AopDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<AppRepository>().As<IAppRepository>().InstancePerLifetimeScope();

            //controllers
            moduleBuilder.RegisterType<AppController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AppEmpController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AppNomController>().InstancePerLifetimeScope();
        }
    }
}
