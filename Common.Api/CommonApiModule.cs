using Autofac;
using Autofac.Integration.WebApi;
using Common.Api.Controllers;
using Common.Api.Models;
using Common.Api.OAuth;
using Common.Api.Repositories.NomRepository;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Http;
using Microsoft.Owin.Security.OAuth;

namespace Common.Api
{
    public class CommonApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<CommonDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<CommonApiWebApiConfig>().As<IWebApiConfig>().SingleInstance();
            moduleBuilder.RegisterType<NomRepository>().As<INomRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UserRepository>().As<IUserRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationOAuthProvider>().As<IOAuthAuthorizationServerProvider>().SingleInstance();

            //controllers
            moduleBuilder.RegisterType<BlobController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NomController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SignController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UserController>().InstancePerLifetimeScope();
        }
    }
}
