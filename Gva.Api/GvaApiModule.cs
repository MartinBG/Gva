using Autofac;
using Autofac.Integration.WebApi;
using Common.Data;
using Gva.Api.Controllers;
using Gva.Api.LotEventHandlers;
using Gva.Api.Mappers;
using Gva.Api.Mappers.Resolvers;
using Gva.Api.Models;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.PersonRepository;
using Regs.Api.LotEvents;

namespace Gva.Api
{
    public class GvaApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<GvaDbConfiguration>().As<IDbConfiguration>().SingleInstance();

            moduleBuilder.RegisterType<PersonLotEventHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<InventoryLotEventHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<ApplicationLotEventHandler>().As<ILotEventHandler>().InstancePerApiRequest();

            moduleBuilder.RegisterType<PersonRepository>().As<IPersonRepository>();
            moduleBuilder.RegisterType<ApplicationRepository>().As<IApplicationRepository>();
            moduleBuilder.RegisterType<InventoryRepository>().As<IInventoryRepository>();
            moduleBuilder.RegisterType<FileRepository>().As<IFileRepository>();
            moduleBuilder.RegisterType<CaseTypeRepository>().As<ICaseTypeRepository>();

            moduleBuilder.RegisterType<JObjectMapper>().As<IMapper>().SingleInstance();
            moduleBuilder.RegisterType<PartVersionMapper>().As<IMapper>().SingleInstance();
            moduleBuilder.RegisterType<FilePartVersionMapper>().As<IMapper>().SingleInstance();
            moduleBuilder.RegisterType<PersonMapper>().As<IMapper>().SingleInstance();
            moduleBuilder.RegisterType<RatingPartVersionMapper>().As<IMapper>().SingleInstance();
            moduleBuilder.RegisterType<InventoryItemMapper>().As<IMapper>().SingleInstance();
            moduleBuilder.RegisterType<ApplicationMapper>().As<IMapper>().SingleInstance();
            moduleBuilder.RegisterType<CaseTypeNomMapper>().As<IMapper>().SingleInstance();
            moduleBuilder.RegisterType<FileResolver>();

            //controllers
            moduleBuilder.RegisterType<ApplicationsController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<GvaLotsController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonsController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<GvaNomController>().InstancePerApiRequest();
        }
    }
}
