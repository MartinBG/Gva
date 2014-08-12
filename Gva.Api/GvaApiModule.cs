using Autofac;
using Common.Data;
using Gva.Api.Controllers;
using Gva.Api.Models;
using Gva.Api.Projections.Aircraft;
using Gva.Api.Projections.Airport;
using Gva.Api.Projections.Application;
using Gva.Api.Projections.Equipment;
using Gva.Api.Projections.Inventory.Aircrafts;
using Gva.Api.Projections.Inventory.Airports;
using Gva.Api.Projections.Inventory.Equipments;
using Gva.Api.Projections.Inventory.Organizations;
using Gva.Api.Projections.Inventory.Persons;
using Gva.Api.Projections.Organization;
using Gva.Api.Projections.Person;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.AirportRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.EquipmentRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonRepository;
using Gva.Api.Repositories.PublisherRepository;
using Gva.Api.Repositories.StageRepository;
using Gva.Api.Repositories.ApplicationStageRepository;
using Gva.Api.WordTemplates;
using Regs.Api.LotEvents;
using Gva.Api.Controllers.Persons;
using Gva.Api.Controllers.Aircrafts;

namespace Gva.Api
{
    public class GvaApiModule : Module
    {
        protected override void Load(ContainerBuilder moduleBuilder)
        {
            moduleBuilder.RegisterType<GvaDbConfiguration>().As<IDbConfiguration>().SingleInstance();

            //ApplicationView
            moduleBuilder.RegisterType<ApplicationAircraftProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationAirportProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationEquipmentProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationOrganizationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationPersonProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //AircraftView
            moduleBuilder.RegisterType<AircraftProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftRegistrationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftRegMarkProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //AirportView
            moduleBuilder.RegisterType<AirportProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //EquipmentView
            moduleBuilder.RegisterType<EquipmentProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //OrganizationView
            moduleBuilder.RegisterType<OrganizationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationExaminerProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationRecommendationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationInspectionProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationInspectionRecommendationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //PersonView
            moduleBuilder.RegisterType<PersonProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonInspectorProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //InventoryView
            moduleBuilder.RegisterType<AircraftApplicationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftDebtProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftOccurrenceProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftOtherProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftOwnerProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<AirportApplicationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportOtherProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportOwnerProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<EquipmentApplicationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentOtherProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentOwnerProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<OrganizationApplicationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationOtherProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<PersonApplicationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonCheckProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonDocumentIdProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonEducationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonEmploymentProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonMedicalProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonOtherProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonTrainingProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //Repositories
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
            moduleBuilder.RegisterType<StageRepository>().As<IStageRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationStageRepository>().As<IApplicationStageRepository>().InstancePerLifetimeScope();

            //Person controllers
            moduleBuilder.RegisterType<ChecksController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<LicencesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<MedicalsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RatingsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<TrainingsController>().InstancePerLifetimeScope();

            //Aircraft controllers
            moduleBuilder.RegisterType<RadiosController>().InstancePerLifetimeScope();

            //controllers
            moduleBuilder.RegisterType<ApplicationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<GvaLotsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<GvaNomController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<GvaSuggestionController>().InstancePerLifetimeScope();
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
