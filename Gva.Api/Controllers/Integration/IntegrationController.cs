using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Blob;
using Common.Data;
using Common.Json;
using Common.Utils;
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

        private Dictionary<string, List<string>> CaseTypeAliasByPortalAppCode =
            new Dictionary<string, List<string>>(){
                    {"flightCrew", new List<string>() {
                        "R-4186", "R-4244", "R-4864", "R-4900", "R-5144",
                        "R-4296", "R-5178", "R-5196", "R-5248", "R-5250",
                        "R-5242", "R-5134", "R-5244", "R-5246"
                    }},
                    {"ovd", new List<string>() {
                        "R-4242", "R-4284", "R-5160", "R-5164", "R-5166"
                    }},
                    {"to_vs", new List<string>() {
                        "R-4240"
                    }},
                    {"to_suvd", new List<string>() {
                         "R-4958", "R-5168", "R-5170", "R-5218"
                    }},
                    {"aircraft", new List<string>() {
                        "R-4356", "R-4378", "R-4396", "R-4470", "R-4490", "R-4514", "R-4544", "R-4566", "R-4578", "R-5104"
                    }},
                    {"airCarrier", new List<string>() {
                        "R-4686"
                    }},
                    {"educationOrg", new List<string>() {
                        "R-4824", "R-4834", "R-4860", "R-4862"
                    }},
                    {"airNavSvcProvider", new List<string>() {
                        "R-4738"
                    }},
                    {"equipment", new List<string>() { // case types in equipments are not separated
                        "R-4764", "R-4766", "R-4614" 
                    }},
                    {"airport", new List<string>() {
                        "R-4588", "R-4590"
                    }},
                    {"airportOperator", new List<string>() {
                        "R-4598", "R-4606"
                    }},
                    {"approvedOrg", new List<string>() {
                        "R-4926", "R-5094", "R-5096", "R-5116", "R-5132"
                    }}
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
                var caseTypeAlias = CaseTypeAliasByPortalAppCode
                    .Where(c => c.Value.Contains(docRelation.Doc.DocType.Alias))
                    .SingleOrDefault();

                IntegrationDocRelationDO intDocRelation = null;
                GvaCaseType caseType = null;
                if (caseTypeAlias.Key != null)
                {
                    caseType = this.caseTypeRepository.GetCaseType(caseTypeAlias.Key);
                    intDocRelation = new IntegrationDocRelationDO(docRelation, caseType);
                    result.Add(intDocRelation);
                }

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
                        case "R-4356":
                            {
                                var aircraftData = new AircraftDataDO();
                                var concreteApp = (R_4356.AircraftRegistrationCertificateApplication)rioApplication;
                                string producerName = concreteApp.AircraftManufactureData.ManufacturerContactData.ManufacturerName;
                                aircraftData.AircraftProducer = this.nomRepository.GetNomValues("aircraftProducers", producerName).FirstOrDefault();
                                
                                intDocRelation.AircraftData = aircraftData;
                                intDocRelation.CorrespondentData = this.integrationRepository.ConvertElServiceRecipientToCorrespondent(concreteApp.ElectronicServiceRecipient);
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
                                intDocRelation.CorrespondentData = this.correspondentRepository.GetCorrespondentFromOrganization(organizationData.Name, organizationData.Uin);
                                break;
                            }
                        default:
                            break;

                    }
                }
            }
            return result;
        }

        [HttpPost]
        [Route("createApplication")]
        public ApplicationMainDO CreateApplication(IntegrationNewAppDO newAppDO)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var doc = this.docRepository.Find(
                    newAppDO.DocId,
                    d => d.DocType,
                    d => d.DocFiles.Select(df => df.DocFileOriginType));

                var lot = this.lotRepository.GetLotIndex(newAppDO.LotId);

                PersonDataDO personData = null;
                List<string> lotCaseTypes = null;
                List<int> correspondentIds = null;
                AircraftDataDO aircraftData = null;
                OrganizationDataDO organizationData = null;

                if (lot.Set.Alias == "Person")
                {
                    personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
                    lotCaseTypes = personData.CaseTypes.Select(c => c.Alias).ToList();
                    correspondentIds = this.integrationRepository.GetCorrespondentIdsPerPersonLot(personData, this.userContext);
                }
                else if (lot.Set.Alias == "Aircraft")
                {
                    aircraftData = lot.Index.GetPart<AircraftDataDO>("aircraftData").Content;
                    lotCaseTypes = new List<string>() { "aircraft" };
                    correspondentIds = this.integrationRepository.CreateCorrespondent(newAppDO.CorrespondentData, this.userContext);
                }
                else if (lot.Set.Alias == "Organization")
                {
                    organizationData = lot.Index.GetPart<OrganizationDataDO>("organizationData").Content;
                    lotCaseTypes = organizationData.CaseTypes.Select(c => c.Alias).ToList();
                    correspondentIds = this.integrationRepository.CreateCorrespondent(newAppDO.CorrespondentData, this.userContext);
                }

                if(!lotCaseTypes.Any(c => c == newAppDO.CaseType.Alias))
                {
                    if (lot.Set.Alias == "Person" || lot.Set.Alias == "Organization")
                    {
                        this.integrationRepository.UpdateLotCaseTypes(lot.Set.Alias, newAppDO.CaseType, lot, this.userContext);
                    }
                }

                ApplicationNewDO applicationNewDO = new ApplicationNewDO()
                {
                    LotId = newAppDO.LotId,
                    SetPartPath =  lot.Set.Alias.ToLower()+"DocumentApplications",
                    Correspondents = correspondentIds,
                    ApplicationType = newAppDO.ApplicationType,
                    CaseTypeId = newAppDO.CaseType.GvaCaseTypeId
                };

                ApplicationMainDO newAppMainData = this.applicationRepository.CreateNewApplication(applicationNewDO, this.userContext, doc.RegUri);

                transaction.Commit();

                return newAppMainData;
            }
        }
    }
}
