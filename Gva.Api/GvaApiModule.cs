using Autofac;
using Autofac.Integration.WebApi;
using Common.Data;
using Gva.Api.Controllers;
using Gva.Api.LotEventHandlers.ApplicationView;
using Gva.Api.LotEventHandlers.InventoryView;
using Gva.Api.LotEventHandlers.OrganizationView;
using Gva.Api.LotEventHandlers.PersonView;
using Gva.Api.Models;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonRepository;
using Gva.Api.Repositories.AircraftRepository;
using Regs.Api.LotEvents;
using Gva.Api.LotEventHandlers.AircraftView;

namespace Gva.Api
{
    public class GvaApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<GvaDbConfiguration>().As<IDbConfiguration>().SingleInstance();
            moduleBuilder.RegisterType<PersonDataHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonEmploymentHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonLicenceHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonRatingHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<ApplicationHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<CheckHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<DocumentIdHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<EducationHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<EmploymentHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<MedicalHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<OtherHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<TrainingHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<ApplicationsHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<OrganizationDataHandler>().As<ILotEventHandler>().InstancePerApiRequest();

            moduleBuilder.RegisterType<AircraftApplicationHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<AircraftDebtHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<AircraftInspectionHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<AircraftOccurrenceHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<AircraftOtherHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<AircraftOwnerHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<AircraftDataHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            
            moduleBuilder.RegisterType<PersonRepository>().As<IPersonRepository>();
            moduleBuilder.RegisterType<AircraftRepository>().As<IAircraftRepository>();
            moduleBuilder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>();
            moduleBuilder.RegisterType<ApplicationRepository>().As<IApplicationRepository>();
            moduleBuilder.RegisterType<InventoryRepository>().As<IInventoryRepository>();
            moduleBuilder.RegisterType<FileRepository>().As<IFileRepository>();
            moduleBuilder.RegisterType<CaseTypeRepository>().As<ICaseTypeRepository>();

            //controllers
            moduleBuilder.RegisterType<ApplicationsController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<GvaLotsController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonsController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<AircraftsController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<GvaNomController>().InstancePerApiRequest();
            moduleBuilder.RegisterType<OrganizationsController>().InstancePerApiRequest();

        }
    }
}
