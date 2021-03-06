﻿using Autofac;
using Common.Data;
using Gva.Api.Controllers;
using Gva.Api.Controllers.Aircrafts;
using Gva.Api.Controllers.Airports;
using Gva.Api.Controllers.Applications;
using Gva.Api.Controllers.Equipments;
using Gva.Api.Controllers.Integration;
using Gva.Api.Controllers.Organizations;
using Gva.Api.Controllers.Persons;
using Gva.Api.Controllers.Reports;
using Gva.Api.Controllers.SModeCodes;
using Gva.Api.Models;
using Gva.Api.Projections.Aircraft;
using Gva.Api.Projections.Airport;
using Gva.Api.Projections.Application;
using Gva.Api.Projections.Equipment;
using Gva.Api.Projections.Inventory.Aircrafts;
using Gva.Api.Projections.Inventory.Airports;
using Gva.Api.Projections.Inventory.Equipments;
using Gva.Api.Projections.Inventory.Organizations;
using Gva.Api.Projections.Organization;
using Gva.Api.Projections.Person;
using Gva.Api.Projections.SModeCode;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.AirportRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.ApplicationStageRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.EquipmentRepository;
using Gva.Api.Repositories.ExaminationSystemRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.IntegrationRepository;
using Gva.Api.Repositories.InventoryRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonDocumentRepository;
using Gva.Api.Repositories.PersonLangCertRepository;
using Gva.Api.Repositories.PersonRepository;
using Gva.Api.Repositories.PersonTrainingRepository;
using Gva.Api.Repositories.PrintRepository;
using Gva.Api.Repositories.PublisherRepository;
using Gva.Api.Repositories.Reports;
using Gva.Api.Repositories.SModeCodeRepository;
using Gva.Api.Repositories.StageRepository;
using Gva.Api.Repositories.WordTemplateRepository;
using Gva.Api.WordTemplates;
using Regs.Api.LotEvents;

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
            moduleBuilder.RegisterType<AircraftCertProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            
            //AirportView
            moduleBuilder.RegisterType<AirportProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //EquipmentView
            moduleBuilder.RegisterType<EquipmentProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //OrganizationView
            moduleBuilder.RegisterType<OrganizationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationRecommendationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationInspectionProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationInspectionRecommendationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //PersonView
            moduleBuilder.RegisterType<PersonProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonInspectorProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonExaminerProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonLicenceProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonLicenceEditionProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonRatingProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonRatingEditionProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PrintedLicenceRatingEditionProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonQualificationProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonDocumentProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonCheckProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonReportProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonReportCheckProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonApplicationExamProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

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
            moduleBuilder.RegisterType<OrganizationApprovalProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationAmendmentProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<SModeCodeProjection>().As<ILotEventHandler>().InstancePerLifetimeScope();

            //Repositories
            moduleBuilder.RegisterType<ExaminationSystemRepository>().As<IExaminationSystemRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PrintRepository>().As<IPrintRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonRepository>().As<IPersonRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonDocumentRepository>().As<IPersonDocumentRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonLangCertRepository>().As<IPersonLangCertRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonTrainingRepository>().As<IPersonTrainingRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonsReportRepository>().As<IPersonsReportRepository>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonsReportsExportExcelRepository>().As<IPersonsReportsExportExcelRepository>().InstancePerLifetimeScope();
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
            moduleBuilder.RegisterType<SModeCodeRepository>().As<ISModeCodeRepository>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<IntegrationRepository>().As<IIntegrationRepository>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<WordTemplateRepository>().As<IWordTemplateRepository>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<IntegrationController>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<WordTemplatesController>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<PapersController>().InstancePerLifetimeScope();

            //Person controllers
            moduleBuilder.RegisterType<PersonsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonAddressesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonApplicationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonChecksController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonDocumentIdsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonDocumentOthersController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonEducationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonEmploymentsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonExamsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonFlyingExperiencesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonLanguageCertificatesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonLicencesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonLicenceEditionsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonMedicalsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonRatingsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonRatingEditionsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonStatusesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonTrainingsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonReportsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonExamSystemController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonExportXmlController>().InstancePerLifetimeScope();

            //Equipment controllers
            moduleBuilder.RegisterType<EquipmentsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentApplicationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentCertOperationalsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentDocumentOthersController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentInspectionsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<EquipmentOwnersController>().InstancePerLifetimeScope();

            //Airport controllers
            moduleBuilder.RegisterType<AirportsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportApplicationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportCertOperationalsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportDocumentOthersController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportInspectionsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AirportOwnersController>().InstancePerLifetimeScope();

            //Organization controllers
            moduleBuilder.RegisterType<OrganizationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationAddressesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationApplicationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationDocumentOthersController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationInspectionsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationAwExaminersController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationStaffManagementController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationCertAirportOperatorsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationCertGroundServiceOperatorsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationCertAirOperatorsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationCertAirNavigationServiceDeliverersController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationCertAirCarriersController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationRecommendationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationGroundServiceOperatorsSnoOperationalController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationApprovalsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<OrganizationApprovalAmendmentsController>().InstancePerLifetimeScope();

            //Aircraft controllers
            moduleBuilder.RegisterType<AircraftsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftRadiosController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftNoisesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftDocumentOthersController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftDocumentOwnersController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftDocumentOccurrencesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftDocumentApplicationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftCertAirworthinessesController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftDocumentDebtsFMController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftCertRegistrationsFMController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AircraftInspectionsController>().InstancePerLifetimeScope();

            //SModeCode controller
            moduleBuilder.RegisterType<SModeCodesController>().InstancePerLifetimeScope();

            // Application controllers
            moduleBuilder.RegisterType<ApplicationsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AplicationsCaseController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationStagesController>().InstancePerLifetimeScope();

            //Reports controllers
            moduleBuilder.RegisterType<PersonsReportsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PersonsReportsExportExcelController>().InstancePerLifetimeScope();

            // controllers
            moduleBuilder.RegisterType<GvaNomController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<GvaSuggestionController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<PrintController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<GvaPublisherController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<GvaPartsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ExamsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AuditsController>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ExaminationSystemController>().InstancePerLifetimeScope();

            //generators for persons
            moduleBuilder.RegisterType<Pilot142year2013>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CoordinatorSimi>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<StudentFilghtLicence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AMLNational>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CabinCrewLicence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ForeignLicence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FlightLicence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Atcl1Licence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SAtcl1Licence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CAL03year2013>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RVD_Licence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Pilot>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<CoordinatorLicence>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AML>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<AML_III>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ApplicationNote>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ExaminerCert>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<InstructorCert>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Part66AuthForm>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<FCLAuthForm>().As<IDataGenerator>().InstancePerLifetimeScope();

            moduleBuilder.RegisterType<AMLNationalRating>().As<IAMLNationalRatingDataGenerator>().InstancePerLifetimeScope();

            //generators for aircrafts
            moduleBuilder.RegisterType<Form24and25>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Form15a>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<SpecialCert>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<TechCert>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<Directive8>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<ExportCert>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<NoiseCert>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RegistrationCert>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<RadioCert>().As<IDataGenerator>().InstancePerLifetimeScope();
            moduleBuilder.RegisterType<DeregCert>().As<IDataGenerator>().InstancePerLifetimeScope();
        }
    }
}
