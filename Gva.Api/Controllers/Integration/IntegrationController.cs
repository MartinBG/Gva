using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Blob;
using Common.Data;
using Common.Json;
using Common.Utils;
using Docs.Api.DataObjects;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.Repositories.DocRepository;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.Applications;
using Gva.Api.ModelsDO.Integration;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.IntegrationRepository;
using Gva.Api.Repositories.PersonRepository;
using Regs.Api.Repositories.LotRepositories;
using Rio.Data.Utils.RioDocumentParser;

namespace Gva.Api.Controllers.Integration
{
    [RoutePrefix("api/integration")]
    public class IntegrationController : ApiController
    {
        private IDocRepository docRepository;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IPersonRepository personRepository;
        private IApplicationRepository applicationRepository;
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;
        private IIntegrationRepository integrationRepository;
        private ICorrespondentRepository correspondentRepository;
        private IRioDocumentParser rioDocumentParser;
        private UserContext userContext;

        private Dictionary<string, string> CaseTypeAliasByPortalAppCode =
            new Dictionary<string,string>
            {
                { "R-4186", "flightCrew" },
                { "R-4244", "flightCrew" },
                { "R-4864", "flightCrew" },
                { "R-5144", "flightCrew" },
                { "R-4296", "flightCrew" },
                { "R-5178", "flightCrew" },
                { "R-5196", "flightCrew" },
                { "R-5248", "flightCrew" },
                { "R-5250", "flightCrew" },
                { "R-5242", "flightCrew" },
                { "R-5134", "flightCrew" },
                { "R-5244", "flightCrew" },
                { "R-5246", "flightCrew" },

                { "R-4242", "ovd" },
                { "R-4284", "ovd" },
                { "R-5160", "ovd" },
                { "R-5164", "ovd" },
                { "R-5166", "ovd" },

                { "R-4240", "to_vs"},

                { "R-4958", "to_suvd" },
                { "R-5168", "to_suvd" },
                { "R-5170", "to_suvd" },
                { "R-5218", "to_suvd" },

                { "R-4356", "aircraft" },
                { "R-4378", "aircraft" },
                { "R-4396", "aircraft" },
                { "R-4470", "aircraft" },
                { "R-4490", "aircraft" },
                { "R-4514", "aircraft" },
                { "R-4544", "aircraft" },
                { "R-4566", "aircraft" },
                { "R-4578", "aircraft" },
                { "R-5104", "aircraft" },

                { "R-4686", "airCarrier" },

                { "R-4824", "educationOrg" },
                { "R-4834", "educationOrg" },
                { "R-4860", "educationOrg" },
                { "R-4862", "educationOrg" },

                { "R-4738", "airNavSvcProvider" },

                // case types in equipments are not separated
                { "R-4764", "equipment" },
                { "R-4766", "equipment" },
                { "R-4614", "equipment" },

                { "R-4588", "airport" },
                { "R-4590", "airport" },

                { "R-4598", "airportOperator" },
                { "R-4606", "airportOperator" },

                { "R-4926", "approvedOrg" },
                { "R-5094", "approvedOrg" },
                { "R-5096", "approvedOrg" },
                { "R-5116", "approvedOrg" },
                { "R-5132", "approvedOrg" },

                // 4810
                // 4900
                // 5000
                // 5090
            };

        public IntegrationController(
            IDocRepository docRepository,
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IPersonRepository personRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            IIntegrationRepository integrationRepository,
            INomRepository nomRepository,
            ICorrespondentRepository correspondentRepository,
            IRioDocumentParser rioDocumentParser,
            UserContext userContext)
        {
            this.docRepository = docRepository;
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.personRepository = personRepository;
            this.applicationRepository = applicationRepository;
            this.integrationRepository = integrationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.nomRepository = nomRepository;
            this.correspondentRepository = correspondentRepository;
            this.rioDocumentParser = rioDocumentParser;
            this.userContext = userContext;
        }

        [Route("caseApplications")]
        public IList<IntegrationDocRelationDO> GetCaseApplications(int docId)
        {
            var docRelations =
                this.docRepository
                .GetCaseRelationsByDocIdWithIncludes(docId, false, true)
                .Where(dr => dr.Doc.IsRegistered); 

            List<IntegrationDocRelationDO> result = new List<IntegrationDocRelationDO>();

            foreach (var docRelation in docRelations)
            {
                if (!CaseTypeAliasByPortalAppCode.ContainsKey(docRelation.Doc.DocType.Alias))
                {
                    continue;
                }

                GvaCaseType caseType = this.caseTypeRepository.GetCaseType(CaseTypeAliasByPortalAppCode[docRelation.Doc.DocType.Alias]);
                IntegrationDocRelationDO intDocRelation = new IntegrationDocRelationDO(docRelation, caseType);
                result.Add(intDocRelation);

                var appDocFiles =
                    docRelation.Doc.DocFiles
                    .Where(df => df.DocFileOriginType != null && df.DocFileOriginType.Alias == "EApplication")
                    .ToList();

                if (appDocFiles.Count == 1)
                {
                    byte[] content;
                    using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                    {
                        connection.Open();

                        using (MemoryStream m1 = new MemoryStream())
                        using (var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", appDocFiles[0].DocFileContentId))
                        {
                            blobStream.CopyTo(m1);
                            content = m1.ToArray();
                        }
                    }

                    string xmlContent = Utf8Utils.GetString(content);
                    object rioApplication = this.rioDocumentParser.XmlDeserializeApplication(xmlContent);

                    switch (docRelation.Doc.DocType.Alias)
                    {
                        case "R-4186":
                            {
                                var concreteApp = (R_4186.InitialCertificationCommercialPilotCapacityInstrumentFlightApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-4244":
                            {
                                var concreteApp = (R_4244.LicenseFlightCrewCabinCrewFlightEngineersNavigatorsFlightConvoyApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-4284":
                            {
                                var concreteApp = (R_4284.LicenseControllersAssistantFlightsATMCoordinatorsApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-4864":
                            {
                                var concreteApp = (R_4864.LicenseCabinCrewApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-5144":
                            {
                                var concreteApp = (R_5144.EstablishingAssessCompetenceApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-4296":
                            {
                                var concreteApp = (R_4296.RecognitionLicenseForeignNationals)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-5178":
                            {
                                var concreteApp = (R_5178.RegistrationRatingTypeClassAircraftIFRPilotLicensePilotPartFCLApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-5196":
                            {
                                var concreteApp = (R_5196.ConfirmationRecoveryRatingLicensePilotApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-5248":
                            {
                                var concreteApp = (R_5248.RegistrationAircraftTypePermissionFlightCrewApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-5250":
                            {
                                var concreteApp = (R_5250.ConfirmationRatingCrewApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-5242":
                            {
                                var concreteApp = (R_5242.RegistrationTrainingAircraftTypePermissionStewardHostessApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-5134":
                            {
                                var concreteApp = (R_5134.ChangeCompetentAuthorityLicensePilotAccordanceLicenseApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-5244":
                            {
                                var concreteApp = (R_5244.ConfirmationRecoveryTrainingAircraftTypePermissionStewardHostessApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-5246":
                            {
                                var concreteApp = (R_5246.ConfirmationConversionPursuantLicensePilotIssuedApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-4242":
                            {
                                var concreteApp = (R_4242.InitialIssueLicenseFlightDispatcherApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-5160":
                            {
                                var concreteApp = (R_5160.RegistrationRatingAuthorizationLicenseApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-5164":
                            {
                                var concreteApp = (R_5164.ConfirmationRecoveryRatingLicenseApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-5166":
                            {
                                var concreteApp = (R_5166.ReplacingLicenseFlightsCoordinatorsApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-4240":
                            {
                                var concreteApp = (R_4240.InitialAuthorizationMaintenanceAircraftAMLApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithFlightCrewDataToPersonData(concreteApp.FlightCrewPersonalData, caseType);
                                break;
                            }
                        case "R-4958":
                            {
                                var concreteApp = (R_4958.EngineeringTechnicalStaffPerformingMaintenanceFundsAirTrafficManagementApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithPersonAndForeignCitizenBasicDataToPersonData(
                                    concreteApp.CAAPersonalIdentificationNumber,
                                    concreteApp.ContactInformation.EmailAddress,
                                    concreteApp.PersonBasicData,
                                    concreteApp.ForeignCitizenBasicData,
                                    caseType);
                                break;
                            }
                        case "R-5218":
                            {
                                var concreteApp = (R_5218.ReplacementRemovalRestrictionsLicenseManagementApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithPersonAndForeignCitizenBasicDataToPersonData(
                                    concreteApp.CAAPersonalIdentificationNumber,
                                    concreteApp.ContactInformation.EmailAddress,
                                    concreteApp.PersonBasicData,
                                    concreteApp.ForeignCitizenBasicData,
                                    caseType);
                                break;
                            }
                        case "R-5170":
                            {
                                var concreteApp = (R_5170.ConfirmationRecoveryRatingLicenseEngineeringTechnicalStaffMaintenanceFundsManagementApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithPersonAndForeignCitizenBasicDataToPersonData(
                                    concreteApp.CAAPersonalIdentificationNumber,
                                    concreteApp.ContactInformation.EmailAddress,
                                    concreteApp.PersonBasicData,
                                    concreteApp.ForeignCitizenBasicData,
                                    caseType);
                                break;
                            }
                            
                        case "R-5168":
                            {
                                var concreteApp = (R_5168.EntryRatingAuthorizationLicenseEngineeringTechnicalStaffEngagedMaintenanceFundsManagementApplication)rioApplication;
                                intDocRelation.PersonData = this.integrationRepository.ConvertAppWithPersonAndForeignCitizenBasicDataToPersonData(
                                    concreteApp.CAAPersonalIdentificationNumber,
                                    concreteApp.ContactInformation.EmailAddress,
                                    concreteApp.PersonBasicData,
                                    concreteApp.ForeignCitizenBasicData,
                                    caseType);
                                break;
                            }
                        case "R-4356":
                            {
                                var aircraftData = new AircraftDataDO();
                                var concreteApp = (R_4356.AircraftRegistrationCertificateApplication)rioApplication;
                                string producerName = concreteApp.AircraftManufactureData.ManufacturerContactData.ManufacturerName;
                                aircraftData.AircraftProducer = this.nomRepository.GetNomValues("aircraftProducers", producerName).FirstOrDefault();
                                
                                intDocRelation.AircraftData = aircraftData;
                                break;
                            }
                        case "R-5132":
                            {
                                var organizationData = new OrganizationDataDO();
                                var concreteApp = (R_5132.ApprovalPartMSubpartGApplication)rioApplication;
                                organizationData.Name = concreteApp.EntityTradeName ?? concreteApp.EntityBasicData.Name;
                                string foreignName = null;
                                if (concreteApp.ForeignEntityBasicData != null)
                                { 
                                    foreignName = concreteApp.ForeignEntityBasicData.ForeignEntityName;
                                }
                                organizationData.NameAlt = foreignName ?? organizationData.Name;
                                organizationData.Uin = concreteApp.EntityBasicData.Identifier;
                                organizationData.Valid = this.nomRepository.GetNomValue("boolean", "yes");

                                intDocRelation.OrganizationData = organizationData;
                                break;
                            }
                        // 4378
                        // 4396
                        // 4470
                        // 4490
                        // 4514
                        // 4544
                        // 4566
                        // 4578
                        // 4588
                        // 4590
                        // 4598
                        // 4606
                        // 4614
                        // 4686
                        // 4738
                        // 4764
                        // 4766
                        // 4810
                        // 4824
                        // 4834
                        // 4860
                        // 4862
                        // 4900
                        // 4926
                        // 5000
                        // 5090
                        // 5094
                        // 5096
                        // 5104
                        // 5116
                        default:
                            break;
                    }
                }
            }

            return result;
        }

        [HttpGet]
        [Route("convertLotToCorrespondent")]
        public CorrespondentDO ConvertLotDataToCorrespondent(int lotId)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            CorrespondentDO corr = null;
            if (lot.Set.Alias == "Person")
            {
               PersonDataDO personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
               corr = this.integrationRepository.ConvertPersonDataToCorrespondent(personData);
            }
            else if (lot.Set.Alias == "Organization")
            {
                OrganizationDataDO organizationData = lot.Index.GetPart<OrganizationDataDO>("organizationData").Content;
                corr = this.integrationRepository.ConvertOrganizationDataToCorrespondent(organizationData);
            }
            else
            {
                corr = this.correspondentRepository.GetNewCorrespondent();
            }

            return corr;
        }

        [HttpPost]
        [Route("createApplication")]
        public ApplicationMainDO CreateApplication(IntegrationNewAppDO newAppDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                ApplicationNewDO applicationNewDO;

                if (newAppDO.ApplicationType != null)
                {
                    var lot = this.lotRepository.GetLotIndex(newAppDO.LotId);

                    List<string> lotCaseTypes = null;
                    if (lot.Set.Alias == "Person")
                    {
                        var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
                        lotCaseTypes = personData.CaseTypes.Select(c => c.Alias).ToList();
                    }
                    else if (lot.Set.Alias == "Aircraft")
                    {
                        var aircraftData = lot.Index.GetPart<AircraftDataDO>("aircraftData").Content;
                        lotCaseTypes = new List<string>() { "aircraft" };
                    }
                    else if (lot.Set.Alias == "Organization")
                    {
                        var organizationData = lot.Index.GetPart<OrganizationDataDO>("organizationData").Content;
                        lotCaseTypes = organizationData.CaseTypes.Select(c => c.Alias).ToList();
                    }

                    var appType = this.nomRepository.GetNomValue(newAppDO.ApplicationType.NomValueId);
                    var caseTypeAlias = appType.TextContent.GetItems<string>("caseTypes").First();
                    var caseType = this.caseTypeRepository.GetCaseType(caseTypeAlias);

                    if (!lotCaseTypes.Contains(caseTypeAlias))
                    {
                        if (lot.Set.Alias == "Person" || lot.Set.Alias == "Organization")
                        {
                            this.integrationRepository.UpdateLotCaseTypes(lot.Set.Alias, caseType, lot, this.userContext);
                        }
                    }

                    applicationNewDO = new ApplicationNewDO()
                    {
                        LotId = newAppDO.LotId,
                        SetPartPath = lot.Set.Alias.ToLower() + "DocumentApplications",
                        Correspondents = new List<int>(),
                        ApplicationType = newAppDO.ApplicationType,
                        CaseTypeId = caseType.GvaCaseTypeId
                    };
                }
                else
                {
                    applicationNewDO = new ApplicationNewDO()
                    {
                        LotId = newAppDO.LotId,
                        Correspondents = new List<int>()
                    };
                }

                ApplicationMainDO newAppMainData = this.applicationRepository.CreateNewApplication(applicationNewDO, this.userContext, newAppDO.DocId);

                transaction.Commit();

                return newAppMainData;
            }
        }
    }
}
