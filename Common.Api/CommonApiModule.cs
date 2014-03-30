using Autofac;
using Autofac.Integration.WebApi;
using Common.Api.Controllers;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Http;

namespace Common.Api
{
    public class CommonApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<CommonDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<CommonApiWebApiConfig>().As<IWebApiConfig>().SingleInstance();
            moduleBuilder.RegisterType<NomRepository>().As<INomRepository>();
            moduleBuilder.RegisterType<UserRepository>().As<IUserRepository>();

            //controllers
            moduleBuilder.RegisterType<BlobController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<NomController>().InstancePerApiRequest();
        }
    }
}
