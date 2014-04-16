using Autofac;
using Autofac.Integration.WebApi;
using Common.Data;
using Common.Http;
using Docs.Api.Controllers;
using Docs.Api.Models;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.Repositories.DocRepository;

namespace Docs.Api
{
    public class DocsApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<DocsWebApiConfig>().As<IWebApiConfig>().SingleInstance();
            moduleBuilder.RegisterType<DocsDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<CorrespondentRepository>().As<ICorrespondentRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DocRepository>().As<IDocRepository>().InstancePerLifetimeScope();

            //controllers
            moduleBuilder.RegisterType<CorrespondentController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DocController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DocNomController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<UnitController>().InstancePerLifetimeScope();
        }
    }
}
