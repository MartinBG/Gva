using Autofac;
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
using Gva.Api.Repositories.EquipmentRepository;
using Gva.Api.LotEventHandlers.EquipmentView;
using Gva.Api.LotEventHandlers.AirportView;
using Gva.Api.Repositories.AirportRepository;
using Gva.Api.WordTemplates;
using Gva.Api.Repositories.PublisherRepository;

namespace Gva.Api
{
    public class GvaApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<GvaDbConfiguration>().As<IDbConfiguration>().SingleInstance();

            //ApplicationView
            moduleBuilder.RegisterType<ApplicationsViewAircraftHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationsViewPersonHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationsViewOrganizationHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationsViewAirportHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationsViewEquipmentHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //AircraftView
            moduleBuilder.RegisterType<AircraftProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftRegistrationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftRegMarkProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //InventaryView
            moduleBuilder.RegisterType<AircraftApplicationHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftDebtHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftOccurrenceHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftOtherHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftOwnerHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<OrganizationApplicationHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationOtherHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonApplicationHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonCheckHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonDocumentIdHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonEducationHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonEmploymentHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonMedicalHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonOtherHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonTrainingHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //OrganizationView
            moduleBuilder.RegisterType<OrganizationViewDataHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationViewExaminersHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            
            //PersonView
            moduleBuilder.RegisterType<PersonViewDataHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonViewEmploymentHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonViewInspectorHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonViewLicenceHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonViewRatingHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<AirportDataHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportOtherHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportOwnerHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportApplicationHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<EquipmentDataHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentOtherHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentOwnerHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentApplicationHandler>().As<ILotEventHandler>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<PersonRepository>().As<IPersonRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftRepository>().As<IAircraftRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationRepository>().As<IOrganizationRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentRepository>().As<IEquipmentRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportRepository>().As<IAirportRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftRegistrationRepository>().As<IAircraftRegistrationRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftRegMarkRepository>().As<IAircraftRegMarkRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationRepository>().As<IApplicationRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<InventoryRepository>().As<IInventoryRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FileRepository>().As<IFileRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CaseTypeRepository>().As<ICaseTypeRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PublisherRepository>().As<IPublisherRepository>().InstancePerLifetimeScope();

            //controllers
            moduleBuilder.RegisterType<ApplicationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<GvaLotsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<GvaNomController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PrintController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<GvaPublisherController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<GvaPartsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ExamsController>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<Pilot142year2013>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CoordinatorSimi>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<StudentFilghtLicence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AMLNational>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CabinCrewLicence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ForeignLicence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FlightLicence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ControllerLicence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CAL03year2013>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RVD_Licence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Pilot>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Pilot142>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CoordinatorLicence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AML>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AML_III>().As<IDataGenerator>().InstancePerLifetimeScope();
        }
    }
}
