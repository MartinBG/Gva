using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects
{
    public class RioDocumentMetadata
    {
        #region Static

        #region Applications Gva

        public static readonly RioDocumentMetadata InitialCertificationCommercialPilotCapacityInstrumentFlightApplicationMetadata =
            new RioDocumentMetadata(
                "R4186",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004186" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004186" },
                "Заявление за издаване на свидетелство за правоспособност на авиационен персонал – пилоти",
                "Application for a certificate of competency of flight crew - pilots",
                typeof(R_4186.InitialCertificationCommercialPilotCapacityInstrumentFlightApplication),
                "/app:InitialCertificationCommercialPilotCapacityInstrumentFlightApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4186"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "InitialCertificationCommercialPilotCapacityInstrumentFlightApplication.xsl");

        public static readonly RioDocumentMetadata InitialAuthorizationMaintenanceAircraftAMLApplicationMetadata =
            new RioDocumentMetadata(
                "R4240",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004240" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004240" },
                "Заявление за първоначално издаване на лиценз за техническо обслужване на въздухоплавателни средства (AML) по Част 66",
                "Application for initial license maintenance aircraft (AML) Part 66",
                typeof(R_4240.InitialAuthorizationMaintenanceAircraftAMLApplication),
                "/app:InitialAuthorizationMaintenanceAircraftAMLApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4240"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "InitialAuthorizationMaintenanceAircraftAMLApplication.xsl");

        public static readonly RioDocumentMetadata InitialIssueLicenseFlightDispatcherApplicationMetadata =
            new RioDocumentMetadata(
                "R4242",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004242" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004242" },
                "Заявление за издаване на свидетелство за правоспособност на авиационен персонал – полетни диспечери",
                "Application for issuance of a license to flight crew - flight dispatchers",
                typeof(R_4242.InitialIssueLicenseFlightDispatcherApplication),
                "/app:InitialIssueLicenseFlightDispatcherApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4242"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "InitialIssueLicenseFlightDispatcherApplication.xsl");

        public static readonly RioDocumentMetadata LicenseFlightCrewCabinCrewFlightEngineersNavigatorsFlightConvoyApplicationMetadata =
            new RioDocumentMetadata(
                "R4244",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004244" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004244" },
                "Заявление за издаване на свидетелство за правоспособност на авиационен персонал – членове на летателния състав от екипажите на ВС, различни от пилоти",
                "Application for a license to flight crew - members of the flight crew of the aircraft crews other than pilots",
                typeof(R_4244.LicenseFlightCrewCabinCrewFlightEngineersNavigatorsFlightConvoyApplication),
                "/app:LicenseFlightCrewCabinCrewFlightEngineersNavigatorsFlightConvoyApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4244"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "LicenseFlightCrewCabinCrewFlightEngineersNavigatorsFlightConvoyApplication.xsl");

        public static readonly RioDocumentMetadata LicenseControllersAssistantFlightsATMCoordinatorsApplicationMetadata =
            new RioDocumentMetadata(
                "R4284",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004284" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004284" },
                "Заявление за издаване на свидетелство за правоспособност на ръководители на полети, на ученик-ръководители на полети, на асистент-координатори на полети и на координатори по УВД",
                "Application for a license to controllers, pupil-controllers, assistant coordinators of flights and coordinators ATM",
                typeof(R_4284.LicenseControllersAssistantFlightsATMCoordinatorsApplication),
                "/app:LicenseControllersAssistantFlightsATMCoordinatorsApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4284"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "LicenseControllersAssistantFlightsATMCoordinatorsApplication.xsl");

        public static readonly RioDocumentMetadata RecognitionLicenseForeignNationalsMetadata =
            new RioDocumentMetadata(
                "R4296",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004296" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004296" },
                "Заявление за признаване на свидетелство за правоспособност на чужди граждани",
                "Application for recognition of license to foreign nationals",
                typeof(R_4296.RecognitionLicenseForeignNationals),
                "/app:RecognitionLicenseForeignNationals/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4296"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "RecognitionLicenseForeignNationals.xsl");

        public static readonly RioDocumentMetadata AircraftRegistrationCertificateApplicationMetadata =
            new RioDocumentMetadata(
                "R4356",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004356" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004356" },
                "Заявление за издаване на удостоверение за регистрация  на ВС",
                "Application for issuance of certificate of registration of the aircraft",
                typeof(R_4356.AircraftRegistrationCertificateApplication),
                "/app:AircraftRegistrationCertificateApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4356"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AircraftRegistrationCertificateApplication.xsl");

        public static readonly RioDocumentMetadata AppointmentSmodeCodeApplicationMetadata =
            new RioDocumentMetadata(
                "R4378",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004378" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004378" },
                "Заявление за назначаване на S-Mode код (24 битов адрес на ВС)",
                "Application for appointment of S-Mode code (24 bit aircraft address)",
                typeof(R_4378.AppointmentSmodeCodeApplication),
                "/app:AppointmentSmodeCodeApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4378"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AppointmentSmodeCodeApplication.xsl");

        public static readonly RioDocumentMetadata AircraftDeletionApplicationMetadata =
            new RioDocumentMetadata(
                "R4396",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004396" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004396" },
                "Заявление за издаване на Удостоверение за отписване от Регистъра на ВС",
                "Application for a Certificate of deregistration of the aircraft",
                typeof(R_4396.AircraftDeletionApplication),
                "/app:AircraftDeletionApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4396"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AircraftDeletionApplication.xsl");

        public static readonly RioDocumentMetadata AircraftAirworthinessCertificateApplicationMetadata =
            new RioDocumentMetadata(
                "R4470",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004470" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004470" },
                "Заявление за издаване на удостоверение за летателна годност на ВС",
                "Application for a certificate of airworthiness for the aircraft",
                typeof(R_4470.AircraftAirworthinessCertificateApplication),
                "/app:AircraftAirworthinessCertificateApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4470"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AircraftAirworthinessCertificateApplication.xsl");

        public static readonly RioDocumentMetadata AuthorizationUseRadioAircraftApplicationMetadata =
            new RioDocumentMetadata(
                "R4490",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004490" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004490" },
                "Заявление за издаване на Разрешително за ползване на радиостанция на въздухоплавателно средство",
                "Application for authorization to use radio aircraft",
                typeof(R_4490.AuthorizationUseRadioAircraftApplication),
                "/app:AuthorizationUseRadioAircraftApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4490"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AuthorizationUseRadioAircraftApplication.xsl");

        public static readonly RioDocumentMetadata NoiseCertificateApplicationMetadata =
            new RioDocumentMetadata(
                "R4514",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004514" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004514" },
                "Заявление за издаване на удостоверение за съответствие с нормите за авиационен шум",
                "Application for a certificate of conformity with standards for aircraft noise",
                typeof(R_4514.NoiseCertificateApplication),
                "/app:NoiseCertificateApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4514"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "NoiseCertificateApplication.xsl");

        public static readonly RioDocumentMetadata ReviewAircraftAirworthinessApplicationMetadata =
            new RioDocumentMetadata(
                "R4544",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004544" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004544" },
                "Заявление за издаване на удостоверение за преглед на летателна годност (EASA Form 15a)",
                "Application for a certificate of airworthiness review (EASA Form 15a)",
                typeof(R_4544.ReviewAircraftAirworthinessApplication),
                "/app:ReviewAircraftAirworthinessApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4544"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ReviewAircraftAirworthinessApplication.xsl");

        public static readonly RioDocumentMetadata AirworthinessExportCertificateApplicationMetadata =
            new RioDocumentMetadata(
                "R4566",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004566" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004566" },
                "Заявление за издаване на експортно удостоверение за летателна годност",
                "Application for an export certificate of airworthiness",
                typeof(R_4566.AirworthinessExportCertificateApplication),
                "/app:AirworthinessExportCertificateApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4566"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AirworthinessExportCertificateApplication.xsl");

        public static readonly RioDocumentMetadata RegistrationAmateurBuiltAircraftApplicationMetadata =
            new RioDocumentMetadata(
                "R4576",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004576" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004576" },
                "Заявление за издаване на удостоверение за регистрация любителски построено въздухоплавателно средство",
                "Application for a certificate of registration amateur built aircraft",
                typeof(R_4576.RegistrationAmateurBuiltAircraftApplication),
                "/app:RegistrationAmateurBuiltAircraftApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4576"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "RegistrationAmateurBuiltAircraftApplication.xsl");

        public static readonly RioDocumentMetadata AirworthinessAmateurBuiltAircraftApplicationMetadata =
            new RioDocumentMetadata(
                "R4578",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004578" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004578" },
                "Заявление за издаване на Специално удостоверение за ЛГ за любителски построено ВС",
                "Application for a special certificate for LG for amateur built aircraft",
                typeof(R_4578.AirworthinessAmateurBuiltAircraftApplication),
                "/app:AirworthinessAmateurBuiltAircraftApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4578"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AirworthinessAmateurBuiltAircraftApplication.xsl");

        public static readonly RioDocumentMetadata CertificateFitnessAirportApplicationMetadata =
            new RioDocumentMetadata(
                "R4588",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004588" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004588" },
                "Заявление за издаване на удостоверение за експлоатационна годност на граждански летища",
                "Application for a certificate of fitness of civil airports",
                typeof(R_4588.CertificateFitnessAirportApplication),
                "/app:CertificateFitnessAirportApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4588"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "CertificateFitnessAirportApplication.xsl");

        public static readonly RioDocumentMetadata CertificateFitnessAirfieldApplicationMetadata =
            new RioDocumentMetadata(
                "R4590",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004590" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004590" },
                "Заявление за издаване на удостоверение за експлоатационна годност на летателни площадки",
                "Application for a certificate of fitness of airfield",
                typeof(R_4590.CertificateFitnessAirfieldApplication),
                "/app:CertificateFitnessAirfieldApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4590"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "CertificateFitnessAirfieldApplication.xsl");

        public static readonly RioDocumentMetadata AuthorizationAirportOperatorApplicationMetadata =
            new RioDocumentMetadata(
                "R4598",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004598" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004598" },
                "Заявление за издаване на лиценз на летищен оператор",
                "Application for authorization of airport operator",
                typeof(R_4598.AuthorizationAirportOperatorApplication),
                "/app:AuthorizationAirportOperatorApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4598"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AuthorizationAirportOperatorApplication.xsl");

        public static readonly RioDocumentMetadata LicenseOperatorApplicationMetadata =
            new RioDocumentMetadata(
                "R4606",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004606" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004606" },
                "Заявление за издаване на лиценз на оператор по наземно обслужване или самообслужване на летище",
                "Application for a license to operate the ground service or self-service airport",
                typeof(R_4606.LicenseOperatorApplication),
                "/app:LicenseOperatorApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4606"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "LicenseOperatorApplication.xsl");

        public static readonly RioDocumentMetadata GroundHandlingEquipmentApplicationMetadata =
            new RioDocumentMetadata(
                "R4614",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004614" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004614" },
                "Заявление за издаване на удостоверение за експлоатационна годност на системи и съоръжения за наземно обслужване",
                "Application for a certificate of fitness of systems and equipment for ground handling",
                typeof(R_4614.GroundHandlingEquipmentApplication),
                "/app:GroundHandlingEquipmentApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4614"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "GroundHandlingEquipmentApplication.xsl");

        public static readonly RioDocumentMetadata AirCarrierOperationLicenseApplicationMetadata =
            new RioDocumentMetadata(
                "R4686",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004686" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004686" },
                "Заявление за издаване на оперативен лиценз на въздушен превозвач",
                "Application for an operating license to the air carrier",
                typeof(R_4686.AirCarrierOperationLicenseApplication),
                "/app:AirCarrierOperationLicenseApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4686"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AirCarrierOperationLicenseApplication.xsl");

        public static readonly RioDocumentMetadata AirNavigationServiceProviderApplicationMetadata =
            new RioDocumentMetadata(
                "R4738",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004738" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004738" },
                "Заявление за издаване на свидетелство на доставчик на аеронавигационно обслужване",
                "Application for certification of air navigation service provider",
                typeof(R_4738.AirNavigationServiceProviderApplication),
                "/app:AirNavigationServiceProviderApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4738"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AirNavigationServiceProviderApplication.xsl");

        public static readonly RioDocumentMetadata FitnessAutomatedATMApplicationMetadata =
            new RioDocumentMetadata(
                "R4764",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004764" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004764" },
                "Заявление за издаване на свидетелство за експлоатационна годност на автоматизираните системи за УВД",
                "Application for a certificate of fitness of automated ATM",
                typeof(R_4764.FitnessAutomatedATMApplication),
                "/app:FitnessAutomatedATMApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4764"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "FitnessAutomatedATMApplication.xsl");

        public static readonly RioDocumentMetadata NavigationalAidsApplicationMetadata =
            new RioDocumentMetadata(
                "R4766",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004766" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004766" },
                "Заявление за издаване на свидетелства за експлоатационна годност на навигационните съоръжения за въздушна навигация и кацане",
                "Application for issuance of certificates of fitness of navigational aids to air navigation and landing",
                typeof(R_4766.NavigationalAidsApplication),
                "/app:NavigationalAidsApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4766"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "NavigationalAidsApplication.xsl");

        public static readonly RioDocumentMetadata IssuanceMaintenanceCertificateCompetencePerformingTasksApplicationMetadata =
            new RioDocumentMetadata(
                "R4810",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004810" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004810" },
                "Заявление за издаване и поддържане на сертификат за компетентност на лица, изпълняващи задачи по проверка и контрол за сигурност в гражданското въздухоплаване",
                "Application for the issuance and maintenance of a certificate of competence of persons performing tasks in the inspection and control of civil aviation security",
                typeof(R_4810.IssuanceMaintenanceCertificateCompetencePerformingTasksApplication),
                "/app:IssuanceMaintenanceCertificateCompetencePerformingTasksApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4810"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "IssuanceMaintenanceCertificateCompetencePerformingTasksApplication.xsl");

        public static readonly RioDocumentMetadata TeacherAviationTrainingCentersApplicationMetadata =
            new RioDocumentMetadata(
                "R4824",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004824" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004824" },
                "Заявление за издаване на свидетелство за преподавател към авиационни учебни центрове",
                "Application for certification as a teacher to aviation training centers",
                typeof(R_4824.TeacherAviationTrainingCentersApplication),
                "/app:TeacherAviationTrainingCentersApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4824"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "TeacherAviationTrainingCentersApplication.xsl");

        public static readonly RioDocumentMetadata VocationalAviationTrainingCenterApplicationMetadata =
            new RioDocumentMetadata(
                "R4834",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004834" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004834" },
                "Заявление за издаване на свидетелство за професионално обучение на авиационен учебен център",
                "Application for certification of vocational training for aviation training center",
                typeof(R_4834.VocationalAviationTrainingCenterApplication),
                "/app:VocationalAviationTrainingCenterApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4834"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "VocationalAviationTrainingCenterApplication.xsl");

        public static readonly RioDocumentMetadata UseAviationSimulatorApplicationMetadata =
            new RioDocumentMetadata(
                "R4860",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004860" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004860" },
                "Заявление за издаване на свидетелство за одобрение на организация за обучение",
                "Application for a certificate of approval of a training organization",
                typeof(R_4860.UseAviationSimulatorApplication),
                "/app:UseAviationSimulatorApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4860"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "UseAviationSimulatorApplication.xsl");

        public static readonly RioDocumentMetadata AviationTrainingCenterAnotherCountryApplicationMetadata =
            new RioDocumentMetadata(
                "R4862",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004862" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004862" },
                "Заявление за издаване на удостоверение за одобрение на организация за обучение, намираща се в друга държава",
                "Application for a certificate of approval of training organization located in another country",
                typeof(R_4862.AviationTrainingCenterAnotherCountryApplication),
                "/app:AviationTrainingCenterAnotherCountryApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4862"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AviationTrainingCenterAnotherCountryApplication.xsl");

        public static readonly RioDocumentMetadata LicenseCabinCrewApplicationMetadata =
            new RioDocumentMetadata(
                "R4864",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004864" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004864" },
                "Заявление за издаване на свидетелство за правоспособност на членовете на кабинния екипаж",
                "Application for a license to cabin crew",
                typeof(R_4864.LicenseCabinCrewApplication),
                "/app:LicenseCabinCrewApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4864"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "LicenseCabinCrewApplication.xsl");

        public static readonly RioDocumentMetadata AviationSimulatorApplicationMetadata =
            new RioDocumentMetadata(
                "R4900",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004900" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004900" },
                "Заявление за издаване на удостоверение за одобрение за ползване на авиационен тренажор",
                "Application for a certificate of approval for the use of aviation simulator",
                typeof(R_4900.AviationSimulatorApplication),
                "/app:AviationSimulatorApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4900"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AviationSimulatorApplication.xsl");

        public static readonly RioDocumentMetadata PermitFlyApplicationMetadata =
            new RioDocumentMetadata(
                "R4926",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004926" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004926" },
                "Заявление за издаване на разрешение за полет по част 21 (EASA Form20)",
                "Application for a permit to fly under Part 21 (EASA Form20)",
                typeof(R_4926.PermitFlyApplication),
                "/app:PermitFlyApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4926"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "PermitFlyApplication.xsl");

        public static readonly RioDocumentMetadata EngineeringTechnicalStaffPerformingMaintenanceFundsAirTrafficManagementApplicationMetadata =
            new RioDocumentMetadata(
                "R4958",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "004958" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "004958" },
                "Заявление за издаване на свидетелство за правоспособност на инженерно-технически състав, извършващ техническо обслужване на средствата за управление на въздушното движение",
                "Application for a license to the engineering and technical staff performing maintenance funds for air traffic management",
                typeof(R_4958.EngineeringTechnicalStaffPerformingMaintenanceFundsAirTrafficManagementApplication),
                "/app:EngineeringTechnicalStaffPerformingMaintenanceFundsAirTrafficManagementApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-4958"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "EngineeringTechnicalStaffPerformingMaintenanceFundsAirTrafficManagementApplication.xsl");

        public static readonly RioDocumentMetadata AOCConductingAerialWorkApplicationMetadata =
            new RioDocumentMetadata(
                "R5000",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005000" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005000" },
                "Заявление за издаване на свидетелство за авиационен оператор, извършващ специализирани авиационни работи",
                "Application for an AOC conducting aerial work",
                typeof(R_5000.AOCConductingAerialWorkApplication),
                "/app:AOCConductingAerialWorkApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5000"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AOCConductingAerialWorkApplication.xsl");

        public static readonly RioDocumentMetadata EngagedCommercialTransportationPassengersCargoApplicationMetadata =
            new RioDocumentMetadata(
                "R5090",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005090" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005090" },
                "Заявление за издаване на свидетелство за авиационен оператор, извършващ търговски превози на пътници и товари",
                "Application for an AOC engaged in commercial transportation of passengers and cargo",
                typeof(R_5090.EngagedCommercialTransportationPassengersCargoApplication),
                "/app:EngagedCommercialTransportationPassengersCargoApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5090"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "EngagedCommercialTransportationPassengersCargoApplication.xsl");

        public static readonly RioDocumentMetadata ApprovalDescriptionManagementOrganizationMaintenanceApplicationMetadata =
            new RioDocumentMetadata(
                "R5094",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005094" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005094" },
                "Заявление за одобряване на описание на организация за управление поддържането на постоянна летателна годност",
                "Application for approval of a description of the management organization to maintain a constant airworthiness",
                typeof(R_5094.ApprovalDescriptionManagementOrganizationMaintenanceApplication),
                "/app:ApprovalDescriptionManagementOrganizationMaintenanceApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5094"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ApprovalDescriptionManagementOrganizationMaintenanceApplication.xsl");

        public static readonly RioDocumentMetadata ApprovalTechnicalLogApplicationMetadata =
            new RioDocumentMetadata(
                "R5096",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005096" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005096" },
                "Заявление за одобряване на технически борден дневник",
                "Application for approval of the technical log",
                typeof(R_5096.ApprovalTechnicalLogApplication),
                "/app:ApprovalTechnicalLogApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5096"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ApprovalTechnicalLogApplication.xsl");

        public static readonly RioDocumentMetadata ApprovalProgramPlanMaintenanceAircraftApplicationMetadata =
            new RioDocumentMetadata(
                "R5104",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005104" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005104" },
                "Заявление за одобряване програма (план) за техническо обслужване на ВС",
                "Application for approval program (plan) for maintenance of aircraft",
                typeof(R_5104.ApprovalProgramPlanMaintenanceAircraftApplication),
                "/app:ApprovalProgramPlanMaintenanceAircraftApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5104"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ApprovalProgramPlanMaintenanceAircraftApplication.xsl");

        public static readonly RioDocumentMetadata ApprovalManagementPersonnelApplicationMetadata =
            new RioDocumentMetadata(
                "R5116",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005116" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005116" },
                "Заявление за одобряване на ръководен персонал (EASA Form 4)",
                "Application for approval of management personnel (EASA Form 4)",
                typeof(R_5116.ApprovalManagementPersonnelApplication),
                "/app:ApprovalManagementPersonnelApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5116"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ApprovalManagementPersonnelApplication.xsl");

        public static readonly RioDocumentMetadata ApprovalPartMSubpartGApplicationMetadata =
            new RioDocumentMetadata(
                "R5132",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005132" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005132" },
                "Заявление за издаване и промяна на удостоверение за одобрение на организация за управление на постоянна летателна годност  (EASA Form 14)",
                "Application for issuance and change of certificate for approval of organization for management of permanent airworthiness (EASA Form 14)",
                typeof(R_5132.ApprovalPartMSubpartGApplication),
                "/app:ApprovalPartMSubpartGApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5132"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ApprovalPartMSubpartGApplication.xsl");

        public static readonly RioDocumentMetadata ChangeCompetentAuthorityLicensePilotAccordanceLicenseApplicationMetadata =
            new RioDocumentMetadata(
                "R5134",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005134" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005134" },
                "Заявление за промяна на компетентен орган на свидетелство за правоспособност на пилот в съответствие с PART-FCL издадено от друга държава членка",
                "Application to change the competent authority of a license of pilot in accordance with PART-FCL license issued by another Member State",
                typeof(R_5134.ChangeCompetentAuthorityLicensePilotAccordanceLicenseApplication),
                "/app:ChangeCompetentAuthorityLicensePilotAccordanceLicenseApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5134"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ChangeCompetentAuthorityLicensePilotAccordanceLicenseApplication.xsl");

        public static readonly RioDocumentMetadata EstablishingAssessCompetenceApplicationMetadata =
            new RioDocumentMetadata(
                "R5144",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005144" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005144" },
                "Заявление за определяне на старши проверяващ или инспектор за извършване на оценка на компетентност на проверяващ и за издаване, потвърждване, подновяване и/или разширяване на правата за проверяващ",
                "An application for establishing a senior examiner or inspector to assess the competence of an examiner and for issuing, confirmations, renewal and / or extension of a verifier",
                typeof(R_5144.EstablishingAssessCompetenceApplication),
                "/app:EstablishingAssessCompetenceApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5144"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "EstablishingAssessCompetenceApplication.xsl");

        public static readonly RioDocumentMetadata RegistrationRatingAuthorizationLicenseApplicationMetadata =
            new RioDocumentMetadata(
                "R5160",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005160" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005160" },
                "Заявление за вписване на квалификационен клас и/или разрешение към него  в свидетелство за правоспособност на ръководители на полети, ученик-ръководител полети, на асистент координатори на полети и на координатори по УВД",
                "Application for registration of the rating and / or authorization to him in a license of air traffic controllers, student-manager flights assistant coordinators flights and coordinators ATM",
                typeof(R_5160.RegistrationRatingAuthorizationLicenseApplication),
                "/app:RegistrationRatingAuthorizationLicenseApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5160"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "RegistrationRatingAuthorizationLicenseApplication.xsl");

        public static readonly RioDocumentMetadata ConfirmationRecoveryRatingLicenseApplicationMetadata =
            new RioDocumentMetadata(
                "R5164",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005164" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005164" },
                "Заявление за потвърждаване и/или възстановяване на квалификационен клас в  свидетелство за правоспособност на ръководители на полети, ученик-ръководител полети, на асистент координатори",
                "Application for confirmation and / or recovery of rating in a license of air traffic controllers, student-manager flights assistant coordinators",
                typeof(R_5164.ConfirmationRecoveryRatingLicenseApplication),
                "/app:ConfirmationRecoveryRatingLicenseApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5164"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ConfirmationRecoveryRatingLicenseApplication.xsl");

        public static readonly RioDocumentMetadata ReplacingLicenseFlightsCoordinatorsApplicationMetadata =
            new RioDocumentMetadata(
                "R5166",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005166" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005166" },
                "Заявление за замяна на  свидетелство за правоспособност на ръководители на полети, ученик-ръководител полети, на асистент координатори на полети и на координатори по УВД",
                "Application for replacement of a license of air traffic controllers, student-manager flights assistant coordinators flights and coordinators ATM",
                typeof(R_5166.ReplacingLicenseFlightsCoordinatorsApplication),
                "/app:ReplacingLicenseFlightsCoordinatorsApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5166"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ReplacingLicenseFlightsCoordinatorsApplication.xsl");

        public static readonly RioDocumentMetadata EntryRatingAuthorizationLicenseEngineeringTechnicalStaffEngagedMaintenanceFundsManagementApplicationMetadata =
            new RioDocumentMetadata(
                "R5168",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005168" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005168" },
                "Заявление за вписване квалификационен клас и/или разрешение към него  в свидетелство за правоспособност на инженеро-технически състав, извършващ техническо обслужване на средствата за управление на въздушното движение",
                "Application for entry rating and / or authorization to him in license of engineering and technical staff engaged in maintenance funds for air traffic management",
                typeof(R_5168.EntryRatingAuthorizationLicenseEngineeringTechnicalStaffEngagedMaintenanceFundsManagementApplication),
                "/app:EntryRatingAuthorizationLicenseEngineeringTechnicalStaffEngagedMaintenanceFundsManagementApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5168"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "EntryRatingAuthorizationLicenseEngineeringTechnicalStaffEngagedMaintenanceFundsManagementApplication.xsl");

        public static readonly RioDocumentMetadata ConfirmationRecoveryRatingLicenseEngineeringTechnicalStaffMaintenanceFundsManagementApplicationMetadata =
            new RioDocumentMetadata(
                "R5170",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005170" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005170" },
                "Заявление за потвърждаване и/или възстановяване на квалификационен клас в  свидетелство за правоспособност на инженеро-технически състав, извършващ техническо обслужване на средствата за управление на въздушното движение",
                "Application for confirmation and / or recovery of rating in a license of engineering and technical staff engaged in maintenance funds for air traffic management",
                typeof(R_5170.ConfirmationRecoveryRatingLicenseEngineeringTechnicalStaffMaintenanceFundsManagementApplication),
                "/app:ConfirmationRecoveryRatingLicenseEngineeringTechnicalStaffMaintenanceFundsManagementApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5170"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ConfirmationRecoveryRatingLicenseEngineeringTechnicalStaffMaintenanceFundsManagementApplication.xsl");

        public static readonly RioDocumentMetadata RegistrationRatingTypeClassAircraftIFRPilotLicensePilotPartFCLApplicationMetadata =
            new RioDocumentMetadata(
                "R5178",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005178" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005178" },
                "Заявление за вписване на квалификационен клас тип и клас ВС и/или полети по прибори в свидетелство за летателна правоспособност на пилот съгласно PART-FCL",
                "Application for registration of rating type and class of aircraft and / or IFR in pilot license to pilot under PART-FCL",
                typeof(R_5178.RegistrationRatingTypeClassAircraftIFRPilotLicensePilotPartFCLApplication),
                "/app:RegistrationRatingTypeClassAircraftIFRPilotLicensePilotPartFCLApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5178"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "RegistrationRatingTypeClassAircraftIFRPilotLicensePilotPartFCLApplication.xsl");

        public static readonly RioDocumentMetadata ConfirmationRecoveryRatingLicensePilotApplicationMetadata =
            new RioDocumentMetadata(
                "R5196",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005196" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005196" },
                "Заявление за потвърждаване/възстановяване на квалификационен клас в свидетелство за правоспособност на пилот",
                "Application for confirmation / recovery rating to license pilot",
                typeof(R_5196.ConfirmationRecoveryRatingLicensePilotApplication),
                "/app:ConfirmationRecoveryRatingLicensePilotApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5196"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ConfirmationRecoveryRatingLicensePilotApplication.xsl");

        public static readonly RioDocumentMetadata ReplacementRemovalRestrictionsLicenseManagementApplicationMetadata =
            new RioDocumentMetadata(
                "R5218",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005218" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005218" },
                "Заявление за замяна/подмяна/премахване на ограничения на  свидетелство за правоспособност на инженеро-технически състав, извършващ техническо обслужване на средствата за управление на въздушното движение",
                "Application for replacement / replacement / removal of restrictions of a license of engineering and technical staff engaged in maintenance funds for air traffic management",
                typeof(R_5218.ReplacementRemovalRestrictionsLicenseManagementApplication),
                "/app:ReplacementRemovalRestrictionsLicenseManagementApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5218"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ReplacementRemovalRestrictionsLicenseManagementApplication.xsl");

        public static readonly RioDocumentMetadata RegistrationTrainingAircraftTypePermissionStewardHostessApplicationMetadata =
            new RioDocumentMetadata(
                "R5242",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005242" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005242" },
                "Заявление за вписване/потвърждаване/възстановяване на квалификация тип ВС и/или разрешение на стюард/еса",
                "Application for registration / confirmation / recovery training aircraft type and / or permission of the Steward / Hostess",
                typeof(R_5242.RegistrationTrainingAircraftTypePermissionStewardHostessApplication),
                "/app:RegistrationTrainingAircraftTypePermissionStewardHostessApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5242"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "RegistrationTrainingAircraftTypePermissionStewardHostessApplication.xsl");

        public static readonly RioDocumentMetadata ConfirmationRecoveryTrainingAircraftTypePermissionStewardHostessApplicationMetadata =
            new RioDocumentMetadata(
                "R5244",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005244" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005244" },
                "Заявление за вписване/потвърждаване/възстановяване на квалификация тип ВС и/или разрешение на стюард/еса",
                "Application for registration / confirmation / recovery training aircraft type and / or permission of the Steward / Hostess",
                typeof(R_5244.ConfirmationRecoveryTrainingAircraftTypePermissionStewardHostessApplication),
                "/app:ConfirmationRecoveryTrainingAircraftTypePermissionStewardHostessApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5244"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ConfirmationRecoveryTrainingAircraftTypePermissionStewardHostessApplication.xsl");

        public static readonly RioDocumentMetadata ConfirmationConversionPursuantLicensePilotIssuedApplicationMetadata =
            new RioDocumentMetadata(
                "R5246",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005246" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005246" },
                "Заявление за потвърждаване или преобразуване съгласно PART-FCL на свидетелство за правоспособност на пилот, издадено съгласно приложение 1 на ИКАО",
                "Application for confirmation or conversion pursuant PART-FCL license a pilot issued under ICAO Annex 1",
                typeof(R_5246.ConfirmationConversionPursuantLicensePilotIssuedApplication),
                "/app:ConfirmationConversionPursuantLicensePilotIssuedApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5246"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ConfirmationConversionPursuantLicensePilotIssuedApplication.xsl");

        public static readonly RioDocumentMetadata RegistrationAircraftTypePermissionFlightCrewApplicationMetadata =
            new RioDocumentMetadata(
                "R5248",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005248" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005248" },
                "Заявление за вписване на квалификационен клас за тип ВС и/или разрешение на членове на летателния състав от екипажите на ВС, различни от пилоти ",
                "Application for registration of aircraft type aircraft and / or permission of the flight crew members of the crew of the aircraft other than the pilots",
                typeof(R_5248.RegistrationAircraftTypePermissionFlightCrewApplication),
                "/app:RegistrationAircraftTypePermissionFlightCrewApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5248"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "RegistrationAircraftTypePermissionFlightCrewApplication.xsl");

        public static readonly RioDocumentMetadata ConfirmationRatingCrewApplicationMetadata =
            new RioDocumentMetadata(
                "R5250",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "005250" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "005250" },
                "Заявление за потвърждаване на квалификационен клас на членове на екипажа",
                "Application for confirmation of the rating of the crew",
                typeof(R_5250.ConfirmationRatingCrewApplication),
                "/app:ConfirmationRatingCrewApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-5250"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ConfirmationRatingCrewApplication.xsl");

        #endregion

        #region Applications Mosv

        public static readonly RioDocumentMetadata GrantPublicAccessInformationMetadata =
            new RioDocumentMetadata(
                "R6016",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "006016" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "006016" },
                "Заявление за предоставяне на достъп до обществена информация",
                "Application for access to public information",
                typeof(R_6016.GrantPublicAccessInformation),
                "/app:GrantPublicAccessInformation/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-6016"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "GrantPublicAccessInformation.xsl");

        public static readonly RioDocumentMetadata ProposalMetadata =
            new RioDocumentMetadata(
                "R6054",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "006054" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "006054" },
                "Подаване на предложение",
                "Submitting a proposal",
                typeof(R_6054.Proposal),
                "/app:Proposal/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-6054"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "Proposal.xsl",
                false);

        public static readonly RioDocumentMetadata SignalMetadata =
            new RioDocumentMetadata(
                "R6056",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "006056" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "006056" },
                "Подаване на сигнал",
                "Submitting a signal",
                typeof(R_6056.Signal),
                "/app:Signal/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-6056"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "Signal.xsl",
                false);

        #endregion

        #region Applications Aop

        public static readonly RioDocumentMetadata AopApplicationMetadata =
            new RioDocumentMetadata(
                "AopApplication",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "aop" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "aop" },
                "Подаване на заявление за предварителен контрол по чл. 19, ал. 2, т. 22 от ЗОП",
                "Application for Public Procurement",
                typeof(Aop.AopApplication),
                "/aop:AopApplication/aop:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"aop", "http://ereg.egov.bg/segment/Aop"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "AopApplication.xsl",
                false);

        //public static readonly RioDocumentMetadata FedDocumentMetadata =
        //    new RioDocumentMetadata(
        //        "FedDocument",
        //        new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "fed" },
        //        new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "fed" },
        //        "Обявление",
        //        "Notice",
        //        typeof(FEDv5.document),
        //        "/aa:FedDocument",
        //        new Dictionary<string, string>
        //        {
        //            {"aa", "http://ereg.egov.bg/segment/FedDocument"},
        //        },
        //        "FedDocument.xsl");

        #endregion

        #region Applications Ngo

        public static readonly RioDocumentMetadata InitialRegistrationChangesNonProfitEntityApplicationMetadata =
            new RioDocumentMetadata(
                "R7070",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "007070" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "007070" },
                "Заявление за първоначално вписване и вписване на промени за юридическо лице с нестопанска цел",
                "Application for initial registration and registration changes for legal non-profit entity",
                typeof(R_7070.InitialRegistrationChangesNonProfitEntityApplication),
                "/app:InitialRegistrationChangesNonProfitEntityApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-7070"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "InitialRegistrationChangesNonProfitEntityApplication.xsl");

        public static readonly RioDocumentMetadata InitialRegistrationChangesNonProfitForeignEntityBranchApplicationMetadata =
            new RioDocumentMetadata(
                "R7074",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "007074" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "007074" },
                "Заявление за първоначално вписване и вписване на промени за клон на чуждестранно юридическо лице с нестопанска цел",
                "Application for initial registration and registration of changes to a branch of a foreign legal non-profit entity",
                typeof(R_7074.InitialRegistrationChangesNonProfitForeignEntityBranchApplication),
                "/app:InitialRegistrationChangesNonProfitForeignEntityBranchApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-7074"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "InitialRegistrationChangesNonProfitForeignEntityBranchApplication.xsl");

        public static readonly RioDocumentMetadata RegistrationCircumstancesBranchLegalEntityRegisteredBulgariaApplicationMetadata =
            new RioDocumentMetadata(
                "R7076",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "007076" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "007076" },
                "Заявление за вписване на обстоятелства относно клон на юридическо лице, регистрирано в Република България",
                "Application for registration of circumstances on a branch of a legal entity registered in the Republic of Bulgaria",
                typeof(R_7076.RegistrationCircumstancesBranchLegalEntityRegisteredBulgariaApplication),
                "/app:RegistrationCircumstancesBranchLegalEntityRegisteredBulgariaApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-7076"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "RegistrationCircumstancesBranchLegalEntityRegisteredBulgariaApplication.xsl");

        public static readonly RioDocumentMetadata DeclarationInformationActivitiesPreviousYearApplicationMetadata =
            new RioDocumentMetadata(
                "R7088",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "007088" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "007088" },
                "Заявление за обявяване на информация за дейността през предходната година в ЦРЮЛНЦ",
                "Application for declaration of information about the activities in the previous year",
                typeof(R_7088.DeclarationInformationActivitiesPreviousYearApplication),
                "/app:DeclarationInformationActivitiesPreviousYearApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-7088"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "DeclarationInformationActivitiesPreviousYearApplication.xsl");

        public static readonly RioDocumentMetadata CancellationRegistrationNonProfitEntityBranchApplicationMetadata =
            new RioDocumentMetadata(
                "R7094",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "007094" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "007094" },
                "Заявление за заличаване на вписването в Централния регистър на юридическите лица с нестопанска цел на юридическо лице и клон на чуждестранно юридическо лице",
                "Application for cancellation of the registration in the Central Register of non-profit legal entity and a branch of a foreign legal entity",
                typeof(R_7094.CancellationRegistrationNonProfitEntityBranchApplication),
                "/app:CancellationRegistrationNonProfitEntityBranchApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-7094"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "CancellationRegistrationNonProfitEntityBranchApplication.xsl");

        #endregion

        #region Applications Bim

        public static readonly RioDocumentMetadata MeasuringEquipmentApprovalApplicationMetadata =
            new RioDocumentMetadata(
                "R1044",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "001044" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "001044" },
                "Заявление за одобряване на типа на средство за измерване",
                "Application for approval of a measuring instrument",
                typeof(R_1044.MeasuringEquipmentApprovalApplication),
                "/meaa:MeasuringEquipmentApprovalApplication/meaa:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"meaa", "http://ereg.egov.bg/segment/R-1044"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "MeasuringEquipmentApprovalApplication.xsl");

        public static readonly RioDocumentMetadata MEVerificationApplicationMetadata =
            new RioDocumentMetadata(
                "R1090",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "001090" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "001090" },
                "Заявление за проверка на средства за измерване, които подлежат на метрологичен контрол",
                "Application for verification of measuring instruments subject to metrological control",
                typeof(R_1090.MEVerificationApplication),
                "/app:MEVerificationApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-1090"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "MEVerificationApplication.xsl");

        public static readonly RioDocumentMetadata InstrumentalMetrologyExpertiseApplicationMetadata =
            new RioDocumentMetadata(
                "R1132",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "001132" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "001132" },
                "Заявление за метрологична експертиза на средство за измерване",
                "Application for metrological expertise of measuring equipment",
                typeof(R_1132.InstrumentalMetrologyExpertiseApplication),
                "/app:InstrumentalMetrologyExpertiseApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-1132"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "InstrumentalMetrologyExpertiseApplication.xsl");

        public static readonly RioDocumentMetadata TypeExaminationFiscalDeviceApplicationMetadata =
            new RioDocumentMetadata(
                "R1144",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "001144" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "001144" },
                "Заявление за изпитване на тип фискално устройство съгласно изискванията на Наредба № 18  от 13.12.2006 г. на Министерство на финансите",
                "Application for type-examination fiscal device according to the requirements of Ordinance № 18 of 13.12.2006 of the Ministry of Finance",
                typeof(R_1144.TypeExaminationFiscalDeviceApplication),
                "/app:TypeExaminationFiscalDeviceApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-1144"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "TypeExaminationFiscalDeviceApplication.xsl");

        public static readonly RioDocumentMetadata CalibrationInstrumentalComparingMaterialsApplicationMetadata =
            new RioDocumentMetadata(
                "R1168",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "001168" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "001168" },
                "Заявление за калибриране на средства за измерване (СИ)/сравнителни материали (СМ)",
                "Application for calibration of measuring instruments (JI) / Reference Materials (RM)",
                typeof(R_1168.CalibrationInstrumentalComparingMaterialsApplication),
                "/app:CalibrationInstrumentalComparingMaterialsApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-1168"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "CalibrationInstrumentalComparingMaterialsApplication.xsl");

        public static readonly RioDocumentMetadata PlayingFacilityTypeApprovalApplicationMetadata =
            new RioDocumentMetadata(
                "R1182",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "001182" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "001182" },
                "Заявление за одобряване на типа на игрално съоръжение",
                "Application for approval of a gambling device",
                typeof(R_1182.PlayingFacilityTypeApprovalApplication),
                "/app:PlayingFacilityTypeApprovalApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-1182"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "PlayingFacilityTypeApprovalApplication.xsl");

        public static readonly RioDocumentMetadata InformationRegisterApprovedTypesMEApplicationMetadata =
            new RioDocumentMetadata(
                "R1184",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "001184" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "001184" },
                "Заявление за предоставяне на справки от регистъра за одобрените за използване типове средства за измерване",
                "Application for information from the register of approved uses types of measuring equipment",
                typeof(R_1184.InformationRegisterApprovedTypesMEApplication),
                "/app:InformationRegisterApprovedTypesMEApplication/rtia:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-1184"},
                    {"rtia","http://ereg.egov.bg/segment/R-1076"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "InformationRegisterApprovedTypesMEApplication.xsl");

        public static readonly RioDocumentMetadata ElectromagneticCompatibilityTestingApplicationMetadata =
            new RioDocumentMetadata(
                "R1192",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "001192" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "001192" },
                "Заявление за изпитване на електромагнитна съвместимост",
                "Application for testing electromagnetic compatibility",
                typeof(R_1192.ElectromagneticCompatibilityTestingApplication),
                "/app:ElectromagneticCompatibilityTestingApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-1192"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ElectromagneticCompatibilityTestingApplication.xsl");

        public static readonly RioDocumentMetadata ConformityAssessmentNonAutomaticWeighingApplicationMetadata =
            new RioDocumentMetadata(
                "R1208",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "001208" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "001208" },
                "Заявление за оценяване на съответствието на везни с неавтоматично действие",
                "Application for conformity assessment of non-automatic weighing",
                typeof(R_1208.ConformityAssessmentNonAutomaticWeighingApplication),
                "/app:ConformityAssessmentNonAutomaticWeighingApplication/app:ElectronicAdministrativeServiceFooter/easf:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"app", "http://ereg.egov.bg/segment/R-1208"},
                    {"easf", "http://ereg.egov.bg/segment/0009-000153"},
                },
                "ConformityAssessmentNonAutomaticWeighingApplication.xsl");
        
        #endregion

        #region Acknowledgements

        public static readonly RioDocumentMetadata ReceiptNotAcknowledgedMessageMetadata =
            new RioDocumentMetadata(
                "R0090",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "000017" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "000001" },
                "Съобщение, че получаването не се потвърждава",
                "Message that the receipt is not confirmed",
                typeof(R_0009_000017.ReceiptNotAcknowledgedMessage),
                "/rna:ReceiptNotAcknowledgedMessage/rna:Signature",
                new Dictionary<string, string>
                {
                    {"rna", "http://ereg.egov.bg/segment/0009-000017"},
                },
                "ReceiptNotAcknowledgedMessage.xsl");

        public static readonly RioDocumentMetadata ReceiptAcknowledgedMessageMetadata =
            new RioDocumentMetadata(
                "R0101",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "000019" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "000002" },
                "Потвърждаване на получаването",
                "Acknowledgment of receipt",
                typeof(R_0009_000019.ReceiptAcknowledgedMessage),
                "/ra:ReceiptAcknowledgedMessage/ra:Signature",
                new Dictionary<string, string>
                {
                    {"ra", "http://ereg.egov.bg/segment/0009-000019"},
                },
                "ReceiptAcknowledgedMessage.xsl");

        #endregion

        #region Refusals

        public static readonly RioDocumentMetadata RemovingIrregularitiesInstructionsMetadata =
            new RioDocumentMetadata(
                "R3010",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "003010" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "003010" },
                "Указания за отстраняване на нередовности",
                "Instructions for regularization",
                typeof(R_3010.RemovingIrregularitiesInstructions),
                "/rii:RemovingIrregularitiesInstructions/rii:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"rii", "http://ereg.egov.bg/segment/R-3010"},
                },
                "RemovingIrregularitiesInstructions");


        #endregion

        #region Certificates

        public static readonly RioDocumentMetadata DecisionGrantAccessPublicInformationMetadata =
            new RioDocumentMetadata(
                "R6090",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "006090" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "006090" },
                "Решение за предоставяне на достъп до обществена информация",
                "Decision to grant access to public information",
                typeof(R_6090.DecisionGrantAccessPublicInformation),
                "/cert:DecisionGrantAccessPublicInformation/cert:XMLDigitalSignature",
                new Dictionary<string, string>
                {
                    {"cert", "http://ereg.egov.bg/segment/R-6090"},
                },
                "DecisionGrantAccessPublicInformation");

        #endregion

        #region Containers

        public static readonly RioDocumentMetadata ContainerTransferFileCompetenceMetadata =
            new RioDocumentMetadata(
                "R6064",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "006064" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "006064" },
                "Контейнер за пренос на преписка по компетентност",
                "Container transfer file on competence",
                typeof(R_6064.ContainerTransferFileCompetence),
                "/ctfc:ContainerTransferFileCompetence/",
                new Dictionary<string, string>
                {
                    {"ctfc", "http://ereg.egov.bg/segment/R-6064"},
                },
                "ContainerTransferFileCompetence.xsl",
                false);

        public static readonly RioDocumentMetadata TransferContainerMetadata =
            new RioDocumentMetadata(
                "R0113",
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0009", BatchNumber = "000021" },
                new R_0009_000003.DocumentTypeURI { RegisterIndex = "0010", BatchNumber = "000003" },
                "Контейнер за пренос",
                "Transfer Container",
                typeof(R_0009_000021.TransferContainer),
                "/tc:TransferContainer/tc:Signature",
                new Dictionary<string, string>
                {
                    {"tc", "http://ereg.egov.bg/segment/0009-000021"},
                },
                "TransferContainer.xsl");

        #endregion

        public static readonly IEnumerable<RioDocumentMetadata> Values = new List<RioDocumentMetadata>
        {
            #region Common

            TransferContainerMetadata,
            ReceiptNotAcknowledgedMessageMetadata,
            ReceiptAcknowledgedMessageMetadata,
            RemovingIrregularitiesInstructionsMetadata,

            #endregion

            #region Application Gva

            InitialCertificationCommercialPilotCapacityInstrumentFlightApplicationMetadata,
            InitialAuthorizationMaintenanceAircraftAMLApplicationMetadata,
            InitialIssueLicenseFlightDispatcherApplicationMetadata,
            LicenseFlightCrewCabinCrewFlightEngineersNavigatorsFlightConvoyApplicationMetadata,
            LicenseControllersAssistantFlightsATMCoordinatorsApplicationMetadata,
            RecognitionLicenseForeignNationalsMetadata,
            AircraftRegistrationCertificateApplicationMetadata,
            AppointmentSmodeCodeApplicationMetadata,
            AircraftDeletionApplicationMetadata,
            AircraftAirworthinessCertificateApplicationMetadata,
            AuthorizationUseRadioAircraftApplicationMetadata,
            NoiseCertificateApplicationMetadata,
            ReviewAircraftAirworthinessApplicationMetadata,
            AirworthinessExportCertificateApplicationMetadata,
            RegistrationAmateurBuiltAircraftApplicationMetadata,
            AirworthinessAmateurBuiltAircraftApplicationMetadata,
            CertificateFitnessAirportApplicationMetadata,
            CertificateFitnessAirfieldApplicationMetadata,
            AuthorizationAirportOperatorApplicationMetadata,
            LicenseOperatorApplicationMetadata,
            GroundHandlingEquipmentApplicationMetadata,
            AirCarrierOperationLicenseApplicationMetadata,
            AirNavigationServiceProviderApplicationMetadata,
            FitnessAutomatedATMApplicationMetadata,
            NavigationalAidsApplicationMetadata,
            IssuanceMaintenanceCertificateCompetencePerformingTasksApplicationMetadata,
            TeacherAviationTrainingCentersApplicationMetadata,
            VocationalAviationTrainingCenterApplicationMetadata,
            UseAviationSimulatorApplicationMetadata,
            AviationTrainingCenterAnotherCountryApplicationMetadata,
            LicenseCabinCrewApplicationMetadata,
            AviationSimulatorApplicationMetadata,
            PermitFlyApplicationMetadata,
            EngineeringTechnicalStaffPerformingMaintenanceFundsAirTrafficManagementApplicationMetadata,
            AOCConductingAerialWorkApplicationMetadata,
            EngagedCommercialTransportationPassengersCargoApplicationMetadata,
            ApprovalDescriptionManagementOrganizationMaintenanceApplicationMetadata,
            ApprovalTechnicalLogApplicationMetadata,
            ApprovalProgramPlanMaintenanceAircraftApplicationMetadata,
            ApprovalManagementPersonnelApplicationMetadata,
            ApprovalPartMSubpartGApplicationMetadata,
            ChangeCompetentAuthorityLicensePilotAccordanceLicenseApplicationMetadata,
            EstablishingAssessCompetenceApplicationMetadata,
            RegistrationRatingAuthorizationLicenseApplicationMetadata,
            ConfirmationRecoveryRatingLicenseApplicationMetadata,
            ReplacingLicenseFlightsCoordinatorsApplicationMetadata,
            EntryRatingAuthorizationLicenseEngineeringTechnicalStaffEngagedMaintenanceFundsManagementApplicationMetadata,
            ConfirmationRecoveryRatingLicenseEngineeringTechnicalStaffMaintenanceFundsManagementApplicationMetadata,
            RegistrationRatingTypeClassAircraftIFRPilotLicensePilotPartFCLApplicationMetadata,
            ConfirmationRecoveryRatingLicensePilotApplicationMetadata,
            ReplacementRemovalRestrictionsLicenseManagementApplicationMetadata,
            RegistrationTrainingAircraftTypePermissionStewardHostessApplicationMetadata,
            ConfirmationRecoveryTrainingAircraftTypePermissionStewardHostessApplicationMetadata,
            ConfirmationConversionPursuantLicensePilotIssuedApplicationMetadata,
            RegistrationAircraftTypePermissionFlightCrewApplicationMetadata,
            ConfirmationRatingCrewApplicationMetadata,

            #endregion

            #region Application Mosv

            GrantPublicAccessInformationMetadata,
            ProposalMetadata,
            SignalMetadata,

            #endregion

            #region Application Aop

            AopApplicationMetadata,

            #endregion
            
            #region Application Ngo

            InitialRegistrationChangesNonProfitEntityApplicationMetadata,
            InitialRegistrationChangesNonProfitForeignEntityBranchApplicationMetadata,
            RegistrationCircumstancesBranchLegalEntityRegisteredBulgariaApplicationMetadata,
            DeclarationInformationActivitiesPreviousYearApplicationMetadata,
            CancellationRegistrationNonProfitEntityBranchApplicationMetadata,

            #endregion
            
            #region Application Bim

            MeasuringEquipmentApprovalApplicationMetadata,
            MEVerificationApplicationMetadata,
            InstrumentalMetrologyExpertiseApplicationMetadata,
            TypeExaminationFiscalDeviceApplicationMetadata,
            CalibrationInstrumentalComparingMaterialsApplicationMetadata,
            PlayingFacilityTypeApprovalApplicationMetadata,
            InformationRegisterApprovedTypesMEApplicationMetadata,
            ElectromagneticCompatibilityTestingApplicationMetadata,
            ConformityAssessmentNonAutomaticWeighingApplicationMetadata,

            #endregion

            #region Certificate Mosv

            DecisionGrantAccessPublicInformationMetadata,

            #endregion

            #region Containers

            ContainerTransferFileCompetenceMetadata,
            TransferContainerMetadata,

            #endregion
        };

        public static RioDocumentMetadata GetMetadataByRioCode(string rioCode)
        {
            return Values.Where(m => m.RioCode == rioCode).Single();
        }

        public static RioDocumentMetadata GetMetadataBySegmentTypeUri(string registerIndex, string batchNumber)
        {
            return Values.Where(m => m.SegmentTypeURI.RegisterIndex == registerIndex && m.SegmentTypeURI.BatchNumber == batchNumber).Single();
        }

        public static RioDocumentMetadata GetMetadataByDocumentTypeURI(string documentTypeURI)
        {
            return Values.Where(m => m.DocumentTypeURIValue == documentTypeURI).Single();
        }

        #endregion

        #region Public

        public string RioCode
        {
            get
            {
                return _rioCode;
            }
        }

        public R_0009_000003.DocumentTypeURI DocumentTypeURI
        {
            get
            {
                return new R_0009_000003.DocumentTypeURI
                {
                    BatchNumber = _documentTypeURI.BatchNumber,
                    RegisterIndex = _documentTypeURI.RegisterIndex,
                };
            }
        }

        public string DocumentTypeURIValue
        {
            get
            {
                return String.Format("{0}-{1}", _documentTypeURI.RegisterIndex, _documentTypeURI.BatchNumber);
            }
        }

        public string DocumentTypeName
        {
            get
            {
                return _documentTypeName;
            }
        }

        public Type RioObjectType
        {
            get
            {
                return _rioObjectType;
            }
        }

        public string SignatureXPath
        {
            get
            {
                return _signatureXPath;
            }
        }

        public IDictionary<string, string> SignatureXPathNamespaces
        {
            get
            {
                return _signatureXPathNamespaces;
            }
        }

        public R_0009_000003.DocumentTypeURI SegmentTypeURI
        {
            get
            {
                return _segmentTypeURI;
            }
        }

        public string XslTemplateName
        {
            get
            {
                return _xslTemplateName;
            }
        }

        public bool IsZeuService
        {
            get
            {
                return _isZeuService;
            }
        }

        #endregion

        #region Private

        private string _rioCode;
        private R_0009_000003.DocumentTypeURI _segmentTypeURI;
        private R_0009_000003.DocumentTypeURI _documentTypeURI;
        private string _documentTypeName;
        private string _documentTypeNameEnglish;
        private string _signatureXPath;
        private IDictionary<string, string> _signatureXPathNamespaces;
        private Type _rioObjectType;
        private string _xslTemplateName;
        private bool _isZeuService;

        private RioDocumentMetadata(
            string rioCode,
            R_0009_000003.DocumentTypeURI segmentTypeURI,
            R_0009_000003.DocumentTypeURI documentTypeURI,
            string documentTypeName,
            string documentTypeNameEnglish,
            Type rioObjectType,
            string signatureXPath,
            IDictionary<string, string> signatureXPathNamespaces,
            string xslTemplateName,
            bool isZeuService = true)
        {
            _rioCode = rioCode;
            _segmentTypeURI = segmentTypeURI;
            _documentTypeURI = documentTypeURI;
            _documentTypeName = documentTypeName;
            _documentTypeNameEnglish = documentTypeNameEnglish;
            _rioObjectType = rioObjectType;
            _signatureXPath = signatureXPath;
            _signatureXPathNamespaces = new Dictionary<string, string>(signatureXPathNamespaces);
            _xslTemplateName = xslTemplateName;
            _isZeuService = isZeuService;
        }

        #endregion
    }
}
