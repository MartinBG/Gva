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
                "ContainerTransferFileCompetence.xsl");

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

            #endregion

            #region Application Mosv

            GrantPublicAccessInformationMetadata,
            ProposalMetadata,
            SignalMetadata,

            #endregion

            #region Application Aop

            AopApplicationMetadata,

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
                var currentCulture = String.Empty;
                if (System.Threading.Thread.CurrentThread.CurrentUICulture != null)
                {
                    currentCulture = System.Threading.Thread.CurrentThread.CurrentUICulture.Name.ToLower().Substring(0, 2);
                }

                switch (currentCulture)
                {
                    case "bg":
                        return _documentTypeName;
                    case "en":
                        return _documentTypeNameEnglish;
                    default:
                        return _documentTypeName;
                }
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
