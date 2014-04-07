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

            //ApplicationView
            moduleBuilder.RegisterType<ApplicationsViewAircraftHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<ApplicationsViewPersonHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<ApplicationsViewOrganizationHandler>().As<ILotEventHandler>().InstancePerApiRequest();

            //AircraftView
            moduleBuilder.RegisterType<AircraftViewDataHandler>().As<ILotEventHandler>().InstancePerApiRequest();

            //InventaryView
            moduleBuilder.RegisterType<AircraftApplicationHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<AircraftDebtHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<AircraftInspectionHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<AircraftOccurrenceHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<AircraftOtherHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<AircraftOwnerHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<OrganizationApplicationHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<OrganizationOtherHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonApplicationHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonCheckHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonDocumentIdHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonEducationHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonEmploymentHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonMedicalHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonOtherHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonTrainingHandler>().As<ILotEventHandler>().InstancePerApiRequest();

            //OrganizationView
            moduleBuilder.RegisterType<OrganizationViewDataHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            
            //PersonView
            moduleBuilder.RegisterType<PersonViewDataHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonViewEmploymentHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonViewLicenceHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            moduleBuilder.RegisterType<PersonViewRatingHandler>().As<ILotEventHandler>().InstancePerApiRequest();
            
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
