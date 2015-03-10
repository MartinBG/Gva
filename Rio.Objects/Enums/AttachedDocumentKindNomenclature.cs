using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Script.Serialization;

namespace Rio.Objects.Enums
{
    public class AttachedDocumentKindNomenclature : BaseNomenclature
    {
        public AttachedDocumentKindNomenclature()
        {
            this.Values = new List<BaseNomenclature>()
            {
            };
        }

        [ScriptIgnore]
        public string Name
        {
            get
            {
                if (string.IsNullOrWhiteSpace(ResourceKey))
                {
                    return string.Empty;
                }
                else
                {
                    return App_LocalResources.AttachedDocumentKindNomenclature.ResourceManager.GetString(ResourceKey);
                }
            }
        }

        [ScriptIgnore]
        public string Code { get; set; }
        [ScriptIgnore]
        public string ResourceKey { get; set; }
        [ScriptIgnore]
        public string FileExtensions { get; set; }

        #region Gva

        public static readonly AttachedDocumentKindNomenclature IDCopy = new AttachedDocumentKindNomenclature { ResourceKey = "IDCopy", Code = "10" };
        public static readonly AttachedDocumentKindNomenclature MedicalCertificate = new AttachedDocumentKindNomenclature { ResourceKey = "MedicalCertificate", Code = "11" };
        public static readonly AttachedDocumentKindNomenclature DyplomCopy = new AttachedDocumentKindNomenclature { ResourceKey = "DyplomCopy", Code = "12" };
        public static readonly AttachedDocumentKindNomenclature PracticalTraining = new AttachedDocumentKindNomenclature { ResourceKey = "PracticalTraining", Code = "13" };
        public static readonly AttachedDocumentKindNomenclature TheoreticalExamCPL = new AttachedDocumentKindNomenclature { ResourceKey = "TheoreticalExamCPL", Code = "14" };
        public static readonly AttachedDocumentKindNomenclature Logbook = new AttachedDocumentKindNomenclature { ResourceKey = "Logbook", Code = "15" };
        public static readonly AttachedDocumentKindNomenclature FeePaid = new AttachedDocumentKindNomenclature { ResourceKey = "FeePaid", Code = "16" };
        public static readonly AttachedDocumentKindNomenclature InformationFlight = new AttachedDocumentKindNomenclature { ResourceKey = "InformationFlight", Code = "17" };
        public static readonly AttachedDocumentKindNomenclature CopyLicenseHeld = new AttachedDocumentKindNomenclature { ResourceKey = "CopyLicenseHeld", Code = "18" };
        public static readonly AttachedDocumentKindNomenclature ColorPhotograph = new AttachedDocumentKindNomenclature { ResourceKey = "ColorPhotograph", Code = "19" };

        public static List<AttachedDocumentKindNomenclature> R4186Values = new List<AttachedDocumentKindNomenclature>
        {
            IDCopy,
            MedicalCertificate,
            DyplomCopy,
            PracticalTraining, 
            TheoreticalExamCPL,
            Logbook,
            FeePaid,
            InformationFlight,
            CopyLicenseHeld,
            ColorPhotograph
        };

        public static readonly AttachedDocumentKindNomenclature PracticalExperience = new AttachedDocumentKindNomenclature { ResourceKey = "PracticalExperience", Code = "22" };
        public static readonly AttachedDocumentKindNomenclature TheoreticalExamApprovedOrganization = new AttachedDocumentKindNomenclature { ResourceKey = "TheoreticalExamApprovedOrganization", Code = "23" };

        public static List<AttachedDocumentKindNomenclature> R4240Values = new List<AttachedDocumentKindNomenclature>
        {
            IDCopy,
            DyplomCopy,
            PracticalExperience,
            TheoreticalExamApprovedOrganization,
            FeePaid
        };

        public static readonly AttachedDocumentKindNomenclature TheoreticalExam = new AttachedDocumentKindNomenclature { ResourceKey = "TheoreticalExam", Code = "34" };

        public static List<AttachedDocumentKindNomenclature> R4242Values = new List<AttachedDocumentKindNomenclature>
        {
            IDCopy,
            MedicalCertificate,
            DyplomCopy,
            PracticalTraining,
            TheoreticalExam,
            FeePaid
        };

        public static List<AttachedDocumentKindNomenclature> R4244Values = new List<AttachedDocumentKindNomenclature>
        {
            IDCopy,
            MedicalCertificate,
            DyplomCopy,
            PracticalTraining,
            TheoreticalExam,
            Logbook,
            FeePaid
        };

        public static readonly AttachedDocumentKindNomenclature Conclusion = new AttachedDocumentKindNomenclature { ResourceKey = "Conclusion", Code = "56" };
        public static readonly AttachedDocumentKindNomenclature Protocols = new AttachedDocumentKindNomenclature { ResourceKey = "Protocols", Code = "57" };
        public static readonly AttachedDocumentKindNomenclature PassMark = new AttachedDocumentKindNomenclature { ResourceKey = "PassMark", Code = "58" };
        public static readonly AttachedDocumentKindNomenclature Admission = new AttachedDocumentKindNomenclature { ResourceKey = "Admission", Code = "59" };

        public static List<AttachedDocumentKindNomenclature> R4284Values = new List<AttachedDocumentKindNomenclature>
        {
            IDCopy,
            MedicalCertificate,
            DyplomCopy,
            PracticalTraining,
            TheoreticalExam,
            FeePaid,
            Conclusion,
            Protocols,
            PassMark,
            Admission 
        };

        public static readonly AttachedDocumentKindNomenclature CopyLicense = new AttachedDocumentKindNomenclature { ResourceKey = "CopyLicense", Code = "60" };
        public static readonly AttachedDocumentKindNomenclature CopyMedicalCertificateJAR = new AttachedDocumentKindNomenclature { ResourceKey = "CopyMedicalCertificateJAR", Code = "61" };
        public static readonly AttachedDocumentKindNomenclature CopyLogbookLast12Months = new AttachedDocumentKindNomenclature { ResourceKey = "CopyLogbookLast12Months", Code = "62" };
        public static readonly AttachedDocumentKindNomenclature IdentityDocumentCopy = new AttachedDocumentKindNomenclature { ResourceKey = "IdentityDocumentCopy", Code = "63" };

        public static List<AttachedDocumentKindNomenclature> R4296Values = new List<AttachedDocumentKindNomenclature>
        {
            CopyLicense,
            CopyMedicalCertificateJAR,
            CopyLogbookLast12Months,
            IdentityDocumentCopy,
            FeePaid
        };

        public static readonly AttachedDocumentKindNomenclature Contract = new AttachedDocumentKindNomenclature { ResourceKey = "Contract", Code = "70" };
        public static readonly AttachedDocumentKindNomenclature RemovedRegister = new AttachedDocumentKindNomenclature { ResourceKey = "RemovedRegister", Code = "71" };
        public static readonly AttachedDocumentKindNomenclature ExportCertificate = new AttachedDocumentKindNomenclature { ResourceKey = "ExportCertificate", Code = "72" };
        public static readonly AttachedDocumentKindNomenclature ApprovedOrder = new AttachedDocumentKindNomenclature { ResourceKey = "ApprovedOrder", Code = "74" };

        public static List<AttachedDocumentKindNomenclature> ContractList = new List<AttachedDocumentKindNomenclature>
        {
            Contract
        };

        public static List<AttachedDocumentKindNomenclature> R4356AircraftHiringValues = new List<AttachedDocumentKindNomenclature>
        {
            Contract,
            ApprovedOrder
        };

        public static List<AttachedDocumentKindNomenclature> R4356Values = new List<AttachedDocumentKindNomenclature>
        {
            RemovedRegister,
            ExportCertificate,
            FeePaid
        };

        public static readonly AttachedDocumentKindNomenclature WrittenAgreement = new AttachedDocumentKindNomenclature { ResourceKey = "WrittenAgreement", Code = "90" };
        public static readonly AttachedDocumentKindNomenclature PermitRadios = new AttachedDocumentKindNomenclature { ResourceKey = "PermitRadios", Code = "91" };
        public static readonly AttachedDocumentKindNomenclature ContractTransfer = new AttachedDocumentKindNomenclature { ResourceKey = "ContractTransfer", Code = "92" };
        public static readonly AttachedDocumentKindNomenclature CertificateRegistration = new AttachedDocumentKindNomenclature { ResourceKey = "CertificateRegistration", Code = "93" };
        public static readonly AttachedDocumentKindNomenclature CertificateCompliance = new AttachedDocumentKindNomenclature { ResourceKey = "CertificateCompliance", Code = "94" };
        public static readonly AttachedDocumentKindNomenclature CertificateAirworthiness = new AttachedDocumentKindNomenclature { ResourceKey = "CertificateAirworthiness", Code = "95" };

        public static List<AttachedDocumentKindNomenclature> WrittenAgreementList = new List<AttachedDocumentKindNomenclature> 
        {
            WrittenAgreement    
        };

        public static List<AttachedDocumentKindNomenclature> R4396Values = new List<AttachedDocumentKindNomenclature>
        {
            WrittenAgreement,
            CertificateRegistration,
            CertificateAirworthiness,
            PermitRadios,
            CertificateCompliance,
            ContractTransfer
        };


        public static readonly AttachedDocumentKindNomenclature CertificateExport = new AttachedDocumentKindNomenclature { ResourceKey = "CertificateExport", Code = "100" };
        public static readonly AttachedDocumentKindNomenclature CertificateFlyingWorthiness = new AttachedDocumentKindNomenclature { ResourceKey = "CertificateFlyingWorthiness", Code = "101" };
        public static readonly AttachedDocumentKindNomenclature CertificateFlyingWorthinessAuthorizedBody = new AttachedDocumentKindNomenclature { ResourceKey = "CertificateFlyingWorthinessAuthorizedBody", Code = "102" };

        public static readonly AttachedDocumentKindNomenclature CertificateQuality = new AttachedDocumentKindNomenclature { ResourceKey = "CertificateQuality", Code = "103" };
        public static readonly AttachedDocumentKindNomenclature CertificateQualityApprovedManufacturer = new AttachedDocumentKindNomenclature { ResourceKey = "CertificateQualityApprovedManufacturer", Code = "104" };

        public static List<AttachedDocumentKindNomenclature> R4470WithWhenAppliedValues = new List<AttachedDocumentKindNomenclature>
        {
            CertificateQualityApprovedManufacturer,
            CertificateFlyingWorthiness,
            CertificateFlyingWorthinessAuthorizedBody
        };

        public static List<AttachedDocumentKindNomenclature> R4470GVAWithWhenAppliedValues = new List<AttachedDocumentKindNomenclature>
        {
            CertificateFlyingWorthinessAuthorizedBody,
            CertificateExport
        };

        public static readonly AttachedDocumentKindNomenclature ManufactureGuides = new AttachedDocumentKindNomenclature { ResourceKey = "ManufactureGuides", Code = "105" };
        public static readonly AttachedDocumentKindNomenclature RegulationMaintenance = new AttachedDocumentKindNomenclature { ResourceKey = "RegulationMaintenance", Code = "106" };
        public static readonly AttachedDocumentKindNomenclature ListServiceBulletins = new AttachedDocumentKindNomenclature { ResourceKey = "ListServiceBulletins", Code = "107" };
        public static readonly AttachedDocumentKindNomenclature ListMaxRejections = new AttachedDocumentKindNomenclature { ResourceKey = "ListMaxRejections", Code = "108" };
        public static readonly AttachedDocumentKindNomenclature ProgramMaintenance = new AttachedDocumentKindNomenclature { ResourceKey = "ProgramMaintenance", Code = "109" };
        public static readonly AttachedDocumentKindNomenclature ProgramGroundTests = new AttachedDocumentKindNomenclature { ResourceKey = "ProgramGroundTests", Code = "1010" };
        public static readonly AttachedDocumentKindNomenclature ProgramFlyingTests = new AttachedDocumentKindNomenclature { ResourceKey = "ProgramFlyingTests", Code = "1011" };

        public static readonly AttachedDocumentKindNomenclature FlightManualCopy = new AttachedDocumentKindNomenclature { ResourceKey = "FlightManualCopy", Code = "1018" };
        public static readonly AttachedDocumentKindNomenclature TimeLimitComponents = new AttachedDocumentKindNomenclature { ResourceKey = "TimeLimitComponents", Code = "1019" };

        public static List<AttachedDocumentKindNomenclature> R4470SupportingDocumentationValues = new List<AttachedDocumentKindNomenclature>
        {
            FlightManualCopy,
            ManufactureGuides,
            RegulationMaintenance,
            ListServiceBulletins,
            ListMaxRejections,
            TimeLimitComponents,
            ProgramMaintenance,
            ProgramGroundTests,
            ProgramFlyingTests
        };

        public static readonly AttachedDocumentKindNomenclature GliderEngines = new AttachedDocumentKindNomenclature { ResourceKey = "GliderEngines", Code = "1012" };
        public static readonly AttachedDocumentKindNomenclature ComparissionTable = new AttachedDocumentKindNomenclature { ResourceKey = "ComparissionTable", Code = "1013" };
        public static readonly AttachedDocumentKindNomenclature ResourceAggregatesList = new AttachedDocumentKindNomenclature { ResourceKey = "ResourceAggregatesList", Code = "1014" };
        public static readonly AttachedDocumentKindNomenclature CenterMassProtocol = new AttachedDocumentKindNomenclature { ResourceKey = "CenterMassProtocol", Code = "1015" };
        public static readonly AttachedDocumentKindNomenclature ModificationsList = new AttachedDocumentKindNomenclature { ResourceKey = "ModificationsList", Code = "1016" };
        public static readonly AttachedDocumentKindNomenclature IncidentsReport = new AttachedDocumentKindNomenclature { ResourceKey = "IncidentsReport", Code = "1017" };

        public static List<AttachedDocumentKindNomenclature> R4470InspectionReportValues = new List<AttachedDocumentKindNomenclature>
        {
            GliderEngines,
            ResourceAggregatesList,
            ComparissionTable,
            CenterMassProtocol,
            ModificationsList,
            IncidentsReport
        };


        public static readonly AttachedDocumentKindNomenclature ListCylindersHighPressure = new AttachedDocumentKindNomenclature { ResourceKey = "ListCylindersHighPressure", Code = "133" };
        public static readonly AttachedDocumentKindNomenclature EmergencyEquipmentState = new AttachedDocumentKindNomenclature { ResourceKey = "EmergencyEquipmentState", Code = "134" };
        public static readonly AttachedDocumentKindNomenclature LastPTODatePlace = new AttachedDocumentKindNomenclature { ResourceKey = "LastPTODatePlace", Code = "139" };
        public static readonly AttachedDocumentKindNomenclature AircraftFormsEngines = new AttachedDocumentKindNomenclature { ResourceKey = "AircraftFormsEngines", Code = "1310" };
        public static readonly AttachedDocumentKindNomenclature ComponentsPassports = new AttachedDocumentKindNomenclature { ResourceKey = "ComponentsPassports", Code = "1311" };

        public static List<AttachedDocumentKindNomenclature> R4544Values = new List<AttachedDocumentKindNomenclature>
        {
            GliderEngines,
            ResourceAggregatesList,
            ListCylindersHighPressure,
            EmergencyEquipmentState,
            ComparissionTable,
            CenterMassProtocol,
            ModificationsList,
            IncidentsReport,
            LastPTODatePlace,
            AircraftFormsEngines,
            ComponentsPassports
        };

        public static readonly AttachedDocumentKindNomenclature ConfirmationAdditionalRequirements = new AttachedDocumentKindNomenclature { ResourceKey = "ConfirmationAdditionalRequirements", Code = "141" };
        public static readonly AttachedDocumentKindNomenclature ConfirmationApproveExceptions = new AttachedDocumentKindNomenclature { ResourceKey = "ConfirmationApproveExceptions", Code = "142" };

        public static readonly AttachedDocumentKindNomenclature CertificateQualityNewBuilt = new AttachedDocumentKindNomenclature { ResourceKey = "CertificateQualityNewBuilt", Code = "143" };
        public static readonly AttachedDocumentKindNomenclature ValidCertificateAirworthiness = new AttachedDocumentKindNomenclature { ResourceKey = "ValidCertificateAirworthiness", Code = "144" };
        public static readonly AttachedDocumentKindNomenclature ResourceStatusAggregates = new AttachedDocumentKindNomenclature { ResourceKey = "ResourceStatusAggregates", Code = "145" };
        public static readonly AttachedDocumentKindNomenclature BulletinsStatus = new AttachedDocumentKindNomenclature { ResourceKey = "BulletinsStatus", Code = "146" };

        public static List<AttachedDocumentKindNomenclature> R4566Values = new List<AttachedDocumentKindNomenclature>
        {
            CertificateQualityNewBuilt,
            ValidCertificateAirworthiness,
            ResourceStatusAggregates,
            BulletinsStatus,
            ModificationsList,
            IncidentsReport,
            CenterMassProtocol,
            AircraftFormsEngines,
            ComponentsPassports
        };

        public static readonly AttachedDocumentKindNomenclature NotarizedStatement = new AttachedDocumentKindNomenclature { ResourceKey = "NotarizedStatement", Code = "152" };

        public static List<AttachedDocumentKindNomenclature> FeePaidList = new List<AttachedDocumentKindNomenclature>
        {
            FeePaid
        };

        public static List<AttachedDocumentKindNomenclature> NotarizedStatementList = new List<AttachedDocumentKindNomenclature>
        {
            NotarizedStatement
        };

        public static readonly AttachedDocumentKindNomenclature OwnershipTerrainUsage = new AttachedDocumentKindNomenclature { ResourceKey = "OwnershipTerrainUsage", Code = "171" };
        public static readonly AttachedDocumentKindNomenclature ExploitationManual = new AttachedDocumentKindNomenclature { ResourceKey = "ExploitationManual", Code = "172" };
        public static readonly AttachedDocumentKindNomenclature AirportCharacteristics = new AttachedDocumentKindNomenclature { ResourceKey = "AirportCharacteristics", Code = "173" };
        public static readonly AttachedDocumentKindNomenclature PermitEnvironment = new AttachedDocumentKindNomenclature { ResourceKey = "PermitEnvironment", Code = "174" };

        public static List<AttachedDocumentKindNomenclature> R4588Values = new List<AttachedDocumentKindNomenclature>
        {
            OwnershipTerrainUsage,
            ExploitationManual,
            AirportCharacteristics,
            PermitEnvironment,
            FeePaid
        };

        public static readonly AttachedDocumentKindNomenclature Deed = new AttachedDocumentKindNomenclature { ResourceKey = "Deed", Code = "176" };
        public static readonly AttachedDocumentKindNomenclature Sketch = new AttachedDocumentKindNomenclature { ResourceKey = "Sketch", Code = "177" };
        public static readonly AttachedDocumentKindNomenclature Geodetic = new AttachedDocumentKindNomenclature { ResourceKey = "Geodetic", Code = "178" };
        public static readonly AttachedDocumentKindNomenclature RentContract = new AttachedDocumentKindNomenclature { ResourceKey = "RentContract", Code = "179" };
        public static readonly AttachedDocumentKindNomenclature OtherOne = new AttachedDocumentKindNomenclature { ResourceKey = "OtherOne", Code = "1710" };

        public static List<AttachedDocumentKindNomenclature> R4588OwnershipValues = new List<AttachedDocumentKindNomenclature>
        {
            Deed,
            Sketch,
            Geodetic,
            RentContract,
            OtherOne
        };

        public static readonly AttachedDocumentKindNomenclature Security = new AttachedDocumentKindNomenclature { ResourceKey = "Security", Code = "1713" };
        public static readonly AttachedDocumentKindNomenclature OtherTwo = new AttachedDocumentKindNomenclature { ResourceKey = "OtherTwo", Code = "1714" };

        public static List<AttachedDocumentKindNomenclature> R4588TechnicalValues = new List<AttachedDocumentKindNomenclature>
        {
            ExploitationManual,
            AirportCharacteristics,
            Security,
            OtherTwo
        };

        public static readonly AttachedDocumentKindNomenclature Balance = new AttachedDocumentKindNomenclature { ResourceKey = "Balance", Code = "191" };
        public static readonly AttachedDocumentKindNomenclature ReportIncome = new AttachedDocumentKindNomenclature { ResourceKey = "ReportIncome", Code = "192" };
        public static readonly AttachedDocumentKindNomenclature ReportMoneyFlow = new AttachedDocumentKindNomenclature { ResourceKey = "ReportMoneyFlow", Code = "193" };
        public static readonly AttachedDocumentKindNomenclature DataListQualification = new AttachedDocumentKindNomenclature { ResourceKey = "DataListQualification", Code = "194" };
        public static readonly AttachedDocumentKindNomenclature ProvesCandidateRights = new AttachedDocumentKindNomenclature { ResourceKey = "ProvesCandidateRights", Code = "195" };
        public static readonly AttachedDocumentKindNomenclature AirportManagement = new AttachedDocumentKindNomenclature { ResourceKey = "AirportManagement", Code = "196" };

        public static List<AttachedDocumentKindNomenclature> R4598Values = new List<AttachedDocumentKindNomenclature>
        {
            Balance,
            ReportIncome,
            ReportMoneyFlow,
            DataListQualification,
            ProvesCandidateRights,
            AirportManagement,
            FeePaid
        };

        public static readonly AttachedDocumentKindNomenclature DocumentsCopies = new AttachedDocumentKindNomenclature { ResourceKey = "DocumentsCopies", Code = "204" };
        public static readonly AttachedDocumentKindNomenclature AirportOperationGuidance = new AttachedDocumentKindNomenclature { ResourceKey = "AirportOperationGuidance", Code = "206" };

        public static List<AttachedDocumentKindNomenclature> R4606Values = new List<AttachedDocumentKindNomenclature>
        {
            Balance,
            ReportIncome,
            ReportMoneyFlow,
            DocumentsCopies,
            ProvesCandidateRights,
            AirportOperationGuidance,
            FeePaid
        };

        public static readonly AttachedDocumentKindNomenclature DocumentObjectRights = new AttachedDocumentKindNomenclature { ResourceKey = "DocumentObjectRights", Code = "211" };
        public static readonly AttachedDocumentKindNomenclature GuidanceExploitationTechnicalMaintenance = new AttachedDocumentKindNomenclature { ResourceKey = "GuidanceExploitationTechnicalMaintenance", Code = "212" };
        public static readonly AttachedDocumentKindNomenclature TechnicalDataRequirements = new AttachedDocumentKindNomenclature { ResourceKey = "TechnicalDataRequirements", Code = "213" };
        public static readonly AttachedDocumentKindNomenclature ProtocolsComplianceSSNO = new AttachedDocumentKindNomenclature { ResourceKey = "ProtocolsComplianceSSNO", Code = "214" };
        public static readonly AttachedDocumentKindNomenclature ListAuthorizedPeopleTechnicalMaintenance = new AttachedDocumentKindNomenclature { ResourceKey = "ListAuthorizedPeopleTechnicalMaintenance", Code = "215" };
        public static readonly AttachedDocumentKindNomenclature FeePaidNonState = new AttachedDocumentKindNomenclature { ResourceKey = "FeePaidNonState", Code = "216" };

        public static List<AttachedDocumentKindNomenclature> R4614Values = new List<AttachedDocumentKindNomenclature>
        {
            DocumentObjectRights,
            GuidanceExploitationTechnicalMaintenance,
            TechnicalDataRequirements,
            ProtocolsComplianceSSNO,
            ListAuthorizedPeopleTechnicalMaintenance,
            FeePaidNonState,
        };

        public static readonly AttachedDocumentKindNomenclature CurrentState = new AttachedDocumentKindNomenclature { ResourceKey = "CurrentState", Code = "221" };
        public static readonly AttachedDocumentKindNomenclature CurrentStructure = new AttachedDocumentKindNomenclature { ResourceKey = "CurrentStructure", Code = "222" };
        public static readonly AttachedDocumentKindNomenclature CurrentStatute = new AttachedDocumentKindNomenclature { ResourceKey = "CurrentStatute", Code = "223" };
        public static readonly AttachedDocumentKindNomenclature Policies = new AttachedDocumentKindNomenclature { ResourceKey = "Policies", Code = "224" };

        public static readonly AttachedDocumentKindNomenclature ConvictionCertificate = new AttachedDocumentKindNomenclature { ResourceKey = "ConvictionCertificate", Code = "225" };
        public static readonly AttachedDocumentKindNomenclature CertificateNotBankruptcy = new AttachedDocumentKindNomenclature { ResourceKey = "CertificateNotBankruptcy", Code = "226" };

        public static readonly AttachedDocumentKindNomenclature BusinessPlanThreeYears = new AttachedDocumentKindNomenclature { ResourceKey = "BusinessPlanThreeYears", Code = "227" };

        public static readonly AttachedDocumentKindNomenclature InterimFinancialStatements = new AttachedDocumentKindNomenclature { ResourceKey = "InterimFinancialStatements", Code = "228" };
        public static readonly AttachedDocumentKindNomenclature ForecastBalance = new AttachedDocumentKindNomenclature { ResourceKey = "ForecastBalance", Code = "229" };
        public static readonly AttachedDocumentKindNomenclature BasisEstimatesCostsRevenues = new AttachedDocumentKindNomenclature { ResourceKey = "BasisEstimatesCostsRevenues", Code = "2210" };
        public static readonly AttachedDocumentKindNomenclature ForecastReportCostsRevenues = new AttachedDocumentKindNomenclature { ResourceKey = "ForecastReportCostsRevenues", Code = "2211" };
        public static readonly AttachedDocumentKindNomenclature ForecastThreeYears = new AttachedDocumentKindNomenclature { ResourceKey = "ForecastThreeYears", Code = "2212" };
        public static readonly AttachedDocumentKindNomenclature ForecastCostsEconomicalElements = new AttachedDocumentKindNomenclature { ResourceKey = "ForecastCostsEconomicalElements", Code = "2213" };
        public static readonly AttachedDocumentKindNomenclature FinanceSourcesInformation = new AttachedDocumentKindNomenclature { ResourceKey = "FinanceSourcesInformation", Code = "2214" };
        public static readonly AttachedDocumentKindNomenclature ForecastMoneyFlow = new AttachedDocumentKindNomenclature { ResourceKey = "ForecastMoneyFlow", Code = "2215" };
        public static readonly AttachedDocumentKindNomenclature LeasingInstallmentsInformation = new AttachedDocumentKindNomenclature { ResourceKey = "LeasingInstallmentsInformation", Code = "2216" };

        public static List<AttachedDocumentKindNomenclature> R4686TwentyOrMorePlacesValues = new List<AttachedDocumentKindNomenclature>
        {
            CurrentState,
            CurrentStructure,
            CurrentStatute,
            Policies,
            ConvictionCertificate,
            CertificateNotBankruptcy,
            BusinessPlanThreeYears,
            InterimFinancialStatements,
            ForecastBalance,
            BasisEstimatesCostsRevenues,
            ForecastReportCostsRevenues,
            ForecastThreeYears,
            ForecastCostsEconomicalElements,
            FinanceSourcesInformation,
            ForecastMoneyFlow,
            LeasingInstallmentsInformation,
            FeePaidNonState
        };

        public static readonly AttachedDocumentKindNomenclature PersonalCapitalPresence = new AttachedDocumentKindNomenclature { ResourceKey = "PersonalCapitalPresence", Code = "2224" };

        public static List<AttachedDocumentKindNomenclature> R4686NineteenOrLessPlacesValues = new List<AttachedDocumentKindNomenclature>
        {
            CurrentState,
            CurrentStructure,
            CurrentStatute,
            Policies,
            ConvictionCertificate,
            CertificateNotBankruptcy,
            PersonalCapitalPresence,
            FeePaid
        };

        public static readonly AttachedDocumentKindNomenclature OrganizationalStructure = new AttachedDocumentKindNomenclature { ResourceKey = "OrganizationalStructure", Code = "233" };
        public static readonly AttachedDocumentKindNomenclature BusinessPlan = new AttachedDocumentKindNomenclature { ResourceKey = "BusinessPlan", Code = "234" };
        public static readonly AttachedDocumentKindNomenclature AnnualPlan = new AttachedDocumentKindNomenclature { ResourceKey = "AnnualPlan", Code = "235" };
        public static readonly AttachedDocumentKindNomenclature ManagementSystem = new AttachedDocumentKindNomenclature { ResourceKey = "ManagementSystem", Code = "236" };
        public static readonly AttachedDocumentKindNomenclature Certificates = new AttachedDocumentKindNomenclature { ResourceKey = "Certificates", Code = "237" };
        public static readonly AttachedDocumentKindNomenclature Operative = new AttachedDocumentKindNomenclature { ResourceKey = "Operative", Code = "238" };
        public static readonly AttachedDocumentKindNomenclature DescriptionManagementSystem = new AttachedDocumentKindNomenclature { ResourceKey = "DescriptionManagementSystem", Code = "239" };
        public static readonly AttachedDocumentKindNomenclature HR = new AttachedDocumentKindNomenclature { ResourceKey = "HR", Code = "2310" };
        public static readonly AttachedDocumentKindNomenclature Economical = new AttachedDocumentKindNomenclature { ResourceKey = "Economical", Code = "2311" };
        public static readonly AttachedDocumentKindNomenclature Provision = new AttachedDocumentKindNomenclature { ResourceKey = "Provision", Code = "2312" };
        public static readonly AttachedDocumentKindNomenclature Contracts = new AttachedDocumentKindNomenclature { ResourceKey = "Contracts", Code = "2313" };
        public static readonly AttachedDocumentKindNomenclature Providing = new AttachedDocumentKindNomenclature { ResourceKey = "Providing", Code = "2314" };
        public static readonly AttachedDocumentKindNomenclature Plans = new AttachedDocumentKindNomenclature { ResourceKey = "Plans", Code = "2315" };
        public static readonly AttachedDocumentKindNomenclature AnnualReport = new AttachedDocumentKindNomenclature { ResourceKey = "AnnualReport", Code = "2316" };
        public static readonly AttachedDocumentKindNomenclature Accordance = new AttachedDocumentKindNomenclature { ResourceKey = "Accordance", Code = "2317" };

        public static List<AttachedDocumentKindNomenclature> R4738EAUValues = new List<AttachedDocumentKindNomenclature>
        {
            OtherOne
        };

        public static List<AttachedDocumentKindNomenclature> R4738UnnumberedValues = new List<AttachedDocumentKindNomenclature>
        {
            OrganizationalStructure,
            BusinessPlan,
            AnnualPlan,
            ManagementSystem,
            Certificates,
            Operative,
            DescriptionManagementSystem,
            HR,
            Economical,
            Provision,
            Contracts,
            Providing,
            Plans,
            AnnualReport,
            Accordance,
            OtherOne
        };

        public static List<AttachedDocumentKindNomenclature> R4764EAUValues = new List<AttachedDocumentKindNomenclature>
        {
            GuidanceExploitationTechnicalMaintenance,
            TechnicalDataRequirements,
            ProtocolsComplianceSSNO,
            ListAuthorizedPeopleTechnicalMaintenance,
            OtherOne
        };

        public static List<AttachedDocumentKindNomenclature> R4764UnnumberedValues = new List<AttachedDocumentKindNomenclature>
        {
            RentContract,
            OtherOne
        };

        public static readonly AttachedDocumentKindNomenclature Facility = new AttachedDocumentKindNomenclature { ResourceKey = "Facility", Code = "254" };
        public static readonly AttachedDocumentKindNomenclature Guide = new AttachedDocumentKindNomenclature { ResourceKey = "Guide", Code = "255" };
        public static readonly AttachedDocumentKindNomenclature Technical = new AttachedDocumentKindNomenclature { ResourceKey = "Technical", Code = "256" };
        public static readonly AttachedDocumentKindNomenclature PeopleMaintenanceList = new AttachedDocumentKindNomenclature { ResourceKey = "PeopleMaintenanceList", Code = "257" };
        public static readonly AttachedDocumentKindNomenclature Certificate = new AttachedDocumentKindNomenclature { ResourceKey = "Certificate", Code = "258" };

        public static List<AttachedDocumentKindNomenclature> R4766EAUValues = new List<AttachedDocumentKindNomenclature>
        {
            Facility,
            Guide,
            Technical,
            PeopleMaintenanceList,
            Certificate,
            OtherOne
        };

        public static readonly AttachedDocumentKindNomenclature CopyOrder = new AttachedDocumentKindNomenclature { ResourceKey = "CopyOrder", Code = "262" };
        public static readonly AttachedDocumentKindNomenclature Declaration = new AttachedDocumentKindNomenclature { ResourceKey = "Declaration", Code = "263" };

        public static readonly AttachedDocumentKindNomenclature Protocol = new AttachedDocumentKindNomenclature { ResourceKey = "Protocol", Code = "265" };
        public static readonly AttachedDocumentKindNomenclature Specialized = new AttachedDocumentKindNomenclature { ResourceKey = "Specialized", Code = "266" };
        public static readonly AttachedDocumentKindNomenclature LastPeriodical = new AttachedDocumentKindNomenclature { ResourceKey = "LastPeriodical", Code = "267" };

        public static List<AttachedDocumentKindNomenclature> R4810AdditionalValues = new List<AttachedDocumentKindNomenclature>
        {
            CopyOrder,
            Declaration,
            OtherOne
        };

        public static List<AttachedDocumentKindNomenclature> R4810EAUValues = new List<AttachedDocumentKindNomenclature>
        {
            Protocol,
            Specialized,
            LastPeriodical,
            OtherOne
        };

        public static readonly AttachedDocumentKindNomenclature Diploma = new AttachedDocumentKindNomenclature { ResourceKey = "Diploma", Code = "272" };

        public static readonly AttachedDocumentKindNomenclature JobResume = new AttachedDocumentKindNomenclature { ResourceKey = "JobResume", Code = "273" };
        public static readonly AttachedDocumentKindNomenclature Licenses = new AttachedDocumentKindNomenclature { ResourceKey = "Licenses", Code = "274" };
        public static readonly AttachedDocumentKindNomenclature Development = new AttachedDocumentKindNomenclature { ResourceKey = "Development", Code = "275" };
        public static readonly AttachedDocumentKindNomenclature Evidencing = new AttachedDocumentKindNomenclature { ResourceKey = "Evidencing", Code = "276" };

        public static List<AttachedDocumentKindNomenclature> R4824Values = new List<AttachedDocumentKindNomenclature>
        {
            FeePaid,
            Diploma
        };

        public static List<AttachedDocumentKindNomenclature> R4824ValuesTwo = new List<AttachedDocumentKindNomenclature>
        {
            JobResume,
            Licenses,
            Development,
            Evidencing
        };

        public static readonly AttachedDocumentKindNomenclature Tax = new AttachedDocumentKindNomenclature { ResourceKey = "Tax", Code = "282" };
        public static readonly AttachedDocumentKindNomenclature ManualAviationalCenter = new AttachedDocumentKindNomenclature { ResourceKey = "ManualAviationalCenter", Code = "283" };
        public static readonly AttachedDocumentKindNomenclature ListMethodological = new AttachedDocumentKindNomenclature { ResourceKey = "ListMethodological", Code = "284" };
        public static readonly AttachedDocumentKindNomenclature Evidence = new AttachedDocumentKindNomenclature { ResourceKey = "Evidence", Code = "285" };
        public static readonly AttachedDocumentKindNomenclature DocumentAUC = new AttachedDocumentKindNomenclature { ResourceKey = "DocumentAUC", Code = "286" };

        public static List<AttachedDocumentKindNomenclature> R4834Values = new List<AttachedDocumentKindNomenclature>
        {
            Tax,
            ManualAviationalCenter,
            ListMethodological,
            Evidence,
            DocumentAUC
        };

        public static readonly AttachedDocumentKindNomenclature DeclarationManager = new AttachedDocumentKindNomenclature { ResourceKey = "DeclarationManager", Code = "292" };
        public static readonly AttachedDocumentKindNomenclature EvidenceManagers = new AttachedDocumentKindNomenclature { ResourceKey = "EvidenceManagers", Code = "293" };
        public static readonly AttachedDocumentKindNomenclature MME = new AttachedDocumentKindNomenclature { ResourceKey = "MME", Code = "294" };
        public static readonly AttachedDocumentKindNomenclature OM = new AttachedDocumentKindNomenclature { ResourceKey = "OM", Code = "295" };
        public static readonly AttachedDocumentKindNomenclature TRM = new AttachedDocumentKindNomenclature { ResourceKey = "TRM", Code = "296" };
        public static readonly AttachedDocumentKindNomenclature TrainingCoursesProgram = new AttachedDocumentKindNomenclature { ResourceKey = "TrainingCoursesProgram", Code = "297" };
        public static readonly AttachedDocumentKindNomenclature InstructorsList = new AttachedDocumentKindNomenclature { ResourceKey = "InstructorsList", Code = "298" };
        public static readonly AttachedDocumentKindNomenclature AirportsList = new AttachedDocumentKindNomenclature { ResourceKey = "AirportsList", Code = "299" };
        public static readonly AttachedDocumentKindNomenclature Subcontracts = new AttachedDocumentKindNomenclature { ResourceKey = "Subcontracts", Code = "2910" };
        public static readonly AttachedDocumentKindNomenclature AircraftTraining = new AttachedDocumentKindNomenclature { ResourceKey = "AircraftTraining", Code = "2911" };
        public static readonly AttachedDocumentKindNomenclature Simulators = new AttachedDocumentKindNomenclature { ResourceKey = "Simulators", Code = "2912" };

        public static List<AttachedDocumentKindNomenclature> R4860Values = new List<AttachedDocumentKindNomenclature>
        {
            DeclarationManager,
            EvidenceManagers,
            MME,
            OM,
            TRM,
            TrainingCoursesProgram,
            InstructorsList,
            AirportsList,
            Subcontracts,
            AircraftTraining,
            Simulators,
            OtherOne
        };

        public static readonly AttachedDocumentKindNomenclature MedicalFitnessCopy = new AttachedDocumentKindNomenclature { ResourceKey = "MedicalFitnessCopy", Code = "311" };
        public static readonly AttachedDocumentKindNomenclature SAA = new AttachedDocumentKindNomenclature { ResourceKey = "SAA", Code = "312" };
        public static readonly AttachedDocumentKindNomenclature TheoreticalClassType = new AttachedDocumentKindNomenclature { ResourceKey = "TheoreticalClassType", Code = "313" };
        public static readonly AttachedDocumentKindNomenclature PracticalTrainingSingle = new AttachedDocumentKindNomenclature { ResourceKey = "PracticalTrainingSingle", Code = "314" };
        public static readonly AttachedDocumentKindNomenclature LogbookSingle = new AttachedDocumentKindNomenclature { ResourceKey = "LogbookSingle", Code = "315" };
        public static readonly AttachedDocumentKindNomenclature OriginalSkillTest = new AttachedDocumentKindNomenclature { ResourceKey = "OriginalSkillTest", Code = "316" };
        public static readonly AttachedDocumentKindNomenclature InitialImage = new AttachedDocumentKindNomenclature { ResourceKey = "InitialImage", Code = "317" };

        public static List<AttachedDocumentKindNomenclature> R4864Values = new List<AttachedDocumentKindNomenclature>
        {
            Diploma,
            IDCopy,
            MedicalFitnessCopy,
            SAA,
            TheoreticalClassType,
            PracticalTrainingSingle,
            OriginalSkillTest,
            LogbookSingle,
            InitialImage,
            FeePaid
        };

        public static List<AttachedDocumentKindNomenclature> IDCopyList = new List<AttachedDocumentKindNomenclature>
        {
            IDCopy
        };

        public static readonly AttachedDocumentKindNomenclature OperatorFSTDCertificate = new AttachedDocumentKindNomenclature { ResourceKey = "OperatorFSTDCertificate", Code = "321" };
        public static readonly AttachedDocumentKindNomenclature QualificationCertificate = new AttachedDocumentKindNomenclature { ResourceKey = "QualificationCertificate", Code = "322" };
        public static readonly AttachedDocumentKindNomenclature ComplianceDocument = new AttachedDocumentKindNomenclature { ResourceKey = "ComplianceDocument", Code = "323" };

        public static List<AttachedDocumentKindNomenclature> R4900Values = new List<AttachedDocumentKindNomenclature>
        {
            OperatorFSTDCertificate,
            QualificationCertificate,
            ComplianceDocument,
            OtherOne
        };

        public static List<AttachedDocumentKindNomenclature> R4926Values = new List<AttachedDocumentKindNomenclature>
        {
            FeePaid,
            OtherOne
        };

        public static readonly AttachedDocumentKindNomenclature DyplomCopies = new AttachedDocumentKindNomenclature { ResourceKey = "DyplomCopies", Code = "342" };
        public static readonly AttachedDocumentKindNomenclature MainCourseCompleted = new AttachedDocumentKindNomenclature { ResourceKey = "MainCourseCompleted", Code = "343" };
        public static readonly AttachedDocumentKindNomenclature ProtocolExamSuccessGVA = new AttachedDocumentKindNomenclature { ResourceKey = "ProtocolExamSuccessGVA", Code = "344" };
        public static readonly AttachedDocumentKindNomenclature TechnicalCertifiedTraining = new AttachedDocumentKindNomenclature { ResourceKey = "TechnicalCertifiedTraining", Code = "345" };
        public static readonly AttachedDocumentKindNomenclature SP = new AttachedDocumentKindNomenclature { ResourceKey = "SP", Code = "346" };

        public static readonly AttachedDocumentKindNomenclature InternshipDocument = new AttachedDocumentKindNomenclature { ResourceKey = "InternshipDocument", Code = "348" };
        public static readonly AttachedDocumentKindNomenclature ProtocolExamSuccess = new AttachedDocumentKindNomenclature { ResourceKey = "ProtocolExamSuccess", Code = "349" };

        public static List<AttachedDocumentKindNomenclature> R4958Values = new List<AttachedDocumentKindNomenclature>
        {
            FeePaid,
            DyplomCopies,
            MainCourseCompleted,
            ProtocolExamSuccessGVA,
            TechnicalCertifiedTraining,
            SP,
            IDCopy
        };

        public static List<AttachedDocumentKindNomenclature> R4958ValuesTwo = new List<AttachedDocumentKindNomenclature>
        {
            InternshipDocument,
            ProtocolExamSuccess,
            OtherOne
        };

        public static readonly AttachedDocumentKindNomenclature CourtRegistration = new AttachedDocumentKindNomenclature { ResourceKey = "CourtRegistration", Code = "352" };
        public static readonly AttachedDocumentKindNomenclature Standing = new AttachedDocumentKindNomenclature { ResourceKey = "Standing", Code = "353" };

        public static readonly AttachedDocumentKindNomenclature Privacy = new AttachedDocumentKindNomenclature { ResourceKey = "Privacy", Code = "355" };
        public static readonly AttachedDocumentKindNomenclature ContractsProcurators = new AttachedDocumentKindNomenclature { ResourceKey = "ContractsProcurators", Code = "356" };
        public static readonly AttachedDocumentKindNomenclature InformationAircraftsEquipment = new AttachedDocumentKindNomenclature { ResourceKey = "InformationAircraftsEquipment", Code = "357" };
        public static readonly AttachedDocumentKindNomenclature ListDirectors = new AttachedDocumentKindNomenclature { ResourceKey = "ListDirectors", Code = "358" };
        public static readonly AttachedDocumentKindNomenclature ListNames4 = new AttachedDocumentKindNomenclature { ResourceKey = "ListNames4", Code = "359" };
        public static readonly AttachedDocumentKindNomenclature ListNames5 = new AttachedDocumentKindNomenclature { ResourceKey = "ListNames5", Code = "3510" };
        public static readonly AttachedDocumentKindNomenclature OperationsFlights = new AttachedDocumentKindNomenclature { ResourceKey = "OperationsFlights", Code = "3511" };
        public static readonly AttachedDocumentKindNomenclature ManualControl = new AttachedDocumentKindNomenclature { ResourceKey = "ManualControl", Code = "3512" };
        public static readonly AttachedDocumentKindNomenclature OperatorManual = new AttachedDocumentKindNomenclature { ResourceKey = "OperatorManual", Code = "3513" };
        public static readonly AttachedDocumentKindNomenclature ListInvolved = new AttachedDocumentKindNomenclature { ResourceKey = "ListInvolved", Code = "3514" };
        public static readonly AttachedDocumentKindNomenclature DocumentsApproval = new AttachedDocumentKindNomenclature { ResourceKey = "DocumentsApproval", Code = "3515" };
        public static readonly AttachedDocumentKindNomenclature InformationAssess = new AttachedDocumentKindNomenclature { ResourceKey = "InformationAssess", Code = "3516" };
        public static readonly AttachedDocumentKindNomenclature DeclarationTaxInformation = new AttachedDocumentKindNomenclature { ResourceKey = "DeclarationTaxInformation", Code = "3517" };

        public static List<AttachedDocumentKindNomenclature> R5000UnnumberedValues = new List<AttachedDocumentKindNomenclature>
        {
            CourtRegistration,
            Standing,
            OtherOne
        };

        public static List<AttachedDocumentKindNomenclature> R5000EAUValues = new List<AttachedDocumentKindNomenclature>
        {
            Privacy,
            ContractsProcurators,
            InformationAircraftsEquipment,
            ListDirectors,
            ListNames4,
            ListNames5,
            OperationsFlights,
            ManualControl,
            OperatorManual,
            ListInvolved,
            DocumentsApproval,
            InformationAssess,
            DeclarationTaxInformation,
            OtherOne
        };

        public static readonly AttachedDocumentKindNomenclature Agreement = new AttachedDocumentKindNomenclature { ResourceKey = "Agreement", Code = "362" };

        public static readonly AttachedDocumentKindNomenclature MOE = new AttachedDocumentKindNomenclature { ResourceKey = "MOE", Code = "364" };
        public static readonly AttachedDocumentKindNomenclature Maintenence = new AttachedDocumentKindNomenclature { ResourceKey = "Maintenence", Code = "365" };
        public static readonly AttachedDocumentKindNomenclature MaintenanceProgram = new AttachedDocumentKindNomenclature { ResourceKey = "MaintenanceProgram", Code = "366" };
        public static readonly AttachedDocumentKindNomenclature Reliability = new AttachedDocumentKindNomenclature { ResourceKey = "Reliability", Code = "367" };
        public static readonly AttachedDocumentKindNomenclature TechnicalDiary = new AttachedDocumentKindNomenclature { ResourceKey = "TechnicalDiary", Code = "368" };

        public static List<AttachedDocumentKindNomenclature> R5090UnnumberedValues = new List<AttachedDocumentKindNomenclature>
        {
            Agreement,
            OtherOne
        };

        public static List<AttachedDocumentKindNomenclature> R5090EAUTechnicalValues = new List<AttachedDocumentKindNomenclature>
        {
            MOE,
            Maintenence,
            MaintenanceProgram,
            Reliability,
            TechnicalDiary,
            OtherOne
        };

        public static readonly AttachedDocumentKindNomenclature MaintainAirworthiness = new AttachedDocumentKindNomenclature { ResourceKey = "MaintainAirworthiness", Code = "371" };
        public static readonly AttachedDocumentKindNomenclature ConformityRequirements = new AttachedDocumentKindNomenclature { ResourceKey = "ConformityRequirements", Code = "372" };
        public static readonly AttachedDocumentKindNomenclature ContractUPPLG = new AttachedDocumentKindNomenclature { ResourceKey = "ContractUPPLG", Code = "373" };

        public static List<AttachedDocumentKindNomenclature> R5094Values = new List<AttachedDocumentKindNomenclature>
        {
            MaintainAirworthiness,
            ConformityRequirements,
            ContractUPPLG,
            OtherTwo
        };

        public static readonly AttachedDocumentKindNomenclature TechnicalLog = new AttachedDocumentKindNomenclature { ResourceKey = "TechnicalLog", Code = "381" };
        public static readonly AttachedDocumentKindNomenclature DeclarationReleaseOperation = new AttachedDocumentKindNomenclature { ResourceKey = "DeclarationReleaseOperation", Code = "382" };

        public static List<AttachedDocumentKindNomenclature> R5096Values = new List<AttachedDocumentKindNomenclature>
        {
            TechnicalLog,
            DeclarationReleaseOperation,
            OtherTwo
        };

        public static readonly AttachedDocumentKindNomenclature ProgramPlanMaintenence = new AttachedDocumentKindNomenclature { ResourceKey = "ProgramPlanMaintenence", Code = "391" };
        public static readonly AttachedDocumentKindNomenclature DeclarationServicedAircraft = new AttachedDocumentKindNomenclature { ResourceKey = "DeclarationServicedAircraft", Code = "392" };
        public static readonly AttachedDocumentKindNomenclature ReliabilityProgramIfApplicable = new AttachedDocumentKindNomenclature { ResourceKey = "ReliabilityProgramIfApplicable", Code = "393" };

        public static List<AttachedDocumentKindNomenclature> R5104Values = new List<AttachedDocumentKindNomenclature>
        {
            ProgramPlanMaintenence,
            DeclarationServicedAircraft,
            ReliabilityProgramIfApplicable,
            OtherTwo
        };

        public static List<AttachedDocumentKindNomenclature> R5116Values = new List<AttachedDocumentKindNomenclature>
        {
            OtherTwo
        };

        public static readonly AttachedDocumentKindNomenclature IdDocumentCopy = new AttachedDocumentKindNomenclature { ResourceKey = "IdDocumentCopy", Code = "421" };
        public static readonly AttachedDocumentKindNomenclature FlyingLicenseCopyEachClass = new AttachedDocumentKindNomenclature { ResourceKey = "FlyingLicenseCopyEachClass", Code = "422" };
        public static readonly AttachedDocumentKindNomenclature MedicalCertificatePartMedCopy = new AttachedDocumentKindNomenclature { ResourceKey = "MedicalCertificatePartMedCopy", Code = "423" };

        public static List<AttachedDocumentKindNomenclature> R5134Values = new List<AttachedDocumentKindNomenclature>
        {
            IdDocumentCopy,
            FlyingLicenseCopyEachClass,
            MedicalCertificatePartMedCopy,
            FeePaidNonState,
            CopyLicenseHeld
        };

        public static readonly AttachedDocumentKindNomenclature DocumentStandardizationPassRate = new AttachedDocumentKindNomenclature { ResourceKey = "DocumentStandardizationPassRate", Code = "431" };
        public static readonly AttachedDocumentKindNomenclature OwnedInstructorCertificateCopy = new AttachedDocumentKindNomenclature { ResourceKey = "OwnedInstructorCertificateCopy", Code = "432" };
        public static readonly AttachedDocumentKindNomenclature LicenseExaminerCopy = new AttachedDocumentKindNomenclature { ResourceKey = "LicenseExaminerCopy", Code = "433" };

        public static List<AttachedDocumentKindNomenclature> R5144Values = new List<AttachedDocumentKindNomenclature>
        {
            DocumentStandardizationPassRate,
            OwnedInstructorCertificateCopy,
            CopyLicenseHeld,
            LicenseExaminerCopy,
            MedicalFitnessCopy
        };

        #endregion

        #region Aop

        public static readonly AttachedDocumentKindNomenclature AopFed = new AttachedDocumentKindNomenclature { ResourceKey = "AopFed", Code = "0", FileExtensions = "*.fed" };
        public static readonly AttachedDocumentKindNomenclature AopMethodica = new AttachedDocumentKindNomenclature { ResourceKey = "AopMethodica", Code = "1", FileExtensions = "*.doc | *.docx" };
        public static readonly AttachedDocumentKindNomenclature AopLetter = new AttachedDocumentKindNomenclature { ResourceKey = "AopLetter", Code = "2" };
        public static readonly AttachedDocumentKindNomenclature OtherFed = new AttachedDocumentKindNomenclature { ResourceKey = "OtherFed", Code = "3", FileExtensions = "*.fed" };

        #endregion

        #region Ngo

        public static readonly AttachedDocumentKindNomenclature DeclarationExistence = new AttachedDocumentKindNomenclature { ResourceKey = "DeclarationExistence", Code = "10" };
        public static readonly AttachedDocumentKindNomenclature DeclarationOverdue = new AttachedDocumentKindNomenclature { ResourceKey = "DeclarationOverdue", Code = "11" };
        public static readonly AttachedDocumentKindNomenclature RulesProcedures = new AttachedDocumentKindNomenclature { ResourceKey = "RulesProcedures", Code = "12" };
        public static readonly AttachedDocumentKindNomenclature Proxy = new AttachedDocumentKindNomenclature { ResourceKey = "Proxy", Code = "13" };

        public static List<string> R7070Values(bool hasHistory = false)
        {
            if (hasHistory)
                return new List<string>
                {
                    RulesProcedures.Name,
                    Proxy.Name
                };
            else
                return new List<string>
                {
                    DeclarationExistence.Name,
                    DeclarationOverdue.Name,
                    RulesProcedures.Name,
                    Proxy.Name
                };
        }

        public static List<string> R7076Values()
        {
            return new List<string>
            {
                Proxy.Name
            };
        }

        #endregion

    }
}
