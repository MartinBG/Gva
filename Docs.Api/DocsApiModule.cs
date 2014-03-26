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
            moduleBuilder.RegisterType<CorrespondentRepository>().As<ICorrespondentRepository>();
            moduleBuilder.RegisterType<DocRepository>().As<IDocRepository>();

            //controllers
            moduleBuilder.RegisterType<CorrespondentController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<DocController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<MockNomController>().InstancePerApiRequest();
        }
    }
}
