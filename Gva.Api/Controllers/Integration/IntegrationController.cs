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
using Common.Utils;
using Common.Json;
using Docs.Api.Models;
using Docs.Api.Repositories.DocRepository;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Integration;
using Gva.Api.Repositories.ApplicationRepository;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Rio.Data.RioObjectExtractor;
using Rio.Data.Utils.RioDocumentParser;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.PersonRepository;
using Common.Api.Models;
using Gva.Api.ModelsDO.Applications;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.DataObjects;
using Regs.Api.LotEvents;
using Gva.Api.Repositories.IntegrationRepository;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.Organizations;

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
        private IRioObjectExtractor rioObjectExtractor;
        private UserContext userContext;
        private ILotEventDispatcher lotEventDispatcher;

        private Dictionary<string, string> AppTypeCodeByAlias =
            new Dictionary<string, string>(){
                    {"R-4284", "АП-5D"},
                    {"R-4356", "ВС-05"},
                    {"R-5132", "АО-04"}
            };

        private const string LicenseControllerAndCoordinatorAppType = "R-4284";
        private const string CertRegAircraftAppType = "R-4356";
        private const string EasaForm14OrganizationAppType = "R-5132";

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
            IRioObjectExtractor rioObjectExtractor,
            UserContext userContext,
            ILotEventDispatcher lotEventDispatcher)
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
            this.rioObjectExtractor = rioObjectExtractor;
            this.userContext = userContext;
            this.lotEventDispatcher = lotEventDispatcher;
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
                var intDocRelation = new IntegrationDocRelationDO(docRelation);
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
                    if (AppTypeCodeByAlias.ContainsKey(docRelation.Doc.DocType.Alias))
                    {
                        string applicationTypeCode = AppTypeCodeByAlias[docRelation.Doc.DocType.Alias];
                        var application = this.nomRepository.GetNomValues("applicationTypes")
                            .Where(v => v.Code == applicationTypeCode)
                            .SingleOrDefault();

                        intDocRelation.ApplicationType = application;
                        var caseTypeNames = application.TextContent.GetItems<string>("caseTypes");
                        List<GvaCaseType> caseTypes = caseTypeNames.Select(c => this.caseTypeRepository.GetCaseType(c)).ToList();
                        intDocRelation.CaseTypes = caseTypes;
                        intDocRelation.Set = caseTypes.First().LotSet.Alias;

                        switch (docRelation.Doc.DocType.Alias)
                        {
                            case LicenseControllerAndCoordinatorAppType:
                                {
                                    var personData = new PersonDataDO();
                                    var concreteApp = (R_4284.LicenseControllersAssistantFlightsATMCoordinatorsApplication)rioApplication;

                                    if (!string.IsNullOrEmpty(concreteApp.FlightCrewPersonalData.CAAPersonalIdentificationNumber))
                                    {
                                        int lin = 0;
                                        bool canParse = int.TryParse(concreteApp.FlightCrewPersonalData.CAAPersonalIdentificationNumber, out lin);
                                        var person = canParse ? this.personRepository.GetPersons(lin: lin).FirstOrDefault() : null;
                                        if (person != null)
                                        {
                                            intDocRelation.PersonData = this.lotRepository.GetLotIndex(person.LotId).Index.GetPart<PersonDataDO>("personData").Content;
                                            break;
                                        }
                                        else if (canParse)
                                        {
                                            personData.Lin = lin;
                                            personData.LinType = this.nomRepository.GetNomValues("linTypes").Where(l => l.Code == "none").Single();
                                        }
                                    }
                                    string countryName = concreteApp.FlightCrewPersonalData.Citizenship.CountryName;
                                    string countryCode = concreteApp.FlightCrewPersonalData.Citizenship.CountryGRAOCode;
                                    if (!string.IsNullOrEmpty(countryName))
                                    {
                                        personData.Country = this.nomRepository.GetNomValues("countries").Where(c => c.Name == countryName).FirstOrDefault();
                                    }

                                    if (countryCode == "BG")
                                    {
                                        personData.Country = this.nomRepository.GetNomValue("countries", "BG");
                                        personData.FirstName = concreteApp.FlightCrewPersonalData.BulgarianCitizen.PersonNames.First;
                                        personData.MiddleName = concreteApp.FlightCrewPersonalData.BulgarianCitizen.PersonNames.Middle;
                                        personData.LastName = concreteApp.FlightCrewPersonalData.BulgarianCitizen.PersonNames.Last;
                                        personData.DateOfBirth = concreteApp.FlightCrewPersonalData.BulgarianCitizen.BirthDate;
                                    }
                                    else
                                    {
                                        personData.FirstName = concreteApp.FlightCrewPersonalData.ForeignCitizen.ForeignCitizenNames.FirstCyrillic;
                                        personData.LastName = concreteApp.FlightCrewPersonalData.ForeignCitizen.ForeignCitizenNames.LastCyrillic;
                                        personData.DateOfBirth = concreteApp.FlightCrewPersonalData.ForeignCitizen.BirthDate;
                                    }

                                    personData.FirstNameAlt = concreteApp.FlightCrewPersonalData.PersonNamesLatin.PersonFirstNameLatin;
                                    personData.MiddleNameAlt = concreteApp.FlightCrewPersonalData.PersonNamesLatin.PersonMiddleNameLatin;
                                    personData.LastNameAlt = concreteApp.FlightCrewPersonalData.PersonNamesLatin.PersonLastNameLatin;

                                    personData.Email = concreteApp.FlightCrewPersonalData.ContactData.EmailAddress;
                                    
                                    personData.CaseTypes = caseTypes
                                        .Select(ct => new NomValue()
                                        {
                                            NomValueId = ct.GvaCaseTypeId,
                                            Name = ct.Name,
                                            Alias = ct.Alias
                                        })
                                        .ToList();

                                    intDocRelation.PersonData = personData;
                                    break;
                                }
                            case CertRegAircraftAppType:
                                {
                                    var aircraftData = new AircraftDataDO();
                                    var concreteApp = (R_4356.AircraftRegistrationCertificateApplication)rioApplication;
                                    string producerName = concreteApp.AircraftManufactureData.ManufacturerContactData.ManufacturerName;
                                    aircraftData.AircraftProducer = this.nomRepository.GetNomValues("aircraftProducers", producerName).FirstOrDefault();
                                
                                    intDocRelation.AircraftData = aircraftData;
                                    intDocRelation.CorrespondentData = this.correspondentRepository.ConvertElServiceRecipientToCorrespondent(concreteApp.ElectronicServiceRecipient);
                                    break;
                                }
                            case EasaForm14OrganizationAppType:
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

                                    intDocRelation.OrganizationData = organizationData;
                                    intDocRelation.CorrespondentData = this.correspondentRepository.GetCorrespondentFromOrganization(organizationData.Name, organizationData.Uin);
                                    break;
                                }
                            default:
                                break;

                        }

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

                int? caseTypeId = null;
                var caseType = newAppDO.CaseTypes.Where(a => lotCaseTypes.Any(c => c == a.Alias)).FirstOrDefault();
                if(caseType != null)
                {
                    caseTypeId = caseType.GvaCaseTypeId;
                }
                else
                {
                    var firstAppCaseTypes = newAppDO.CaseTypes.First();
                    if (lot.Set.Alias == "Person" || lot.Set.Alias == "Organization")
                    {
                        this.integrationRepository.UpdateLotCaseTypes(lot.Set.Alias, firstAppCaseTypes, lot, this.userContext);
                    }
                    caseTypeId = firstAppCaseTypes.GvaCaseTypeId;
                 }

                ApplicationNewDO applicationNewDO = new ApplicationNewDO()
                {
                    LotId = newAppDO.LotId,
                    SetPartPath =  lot.Set.Alias.ToLower()+"DocumentApplications",
                    Correspondents = correspondentIds,
                    ApplicationType = newAppDO.ApplicationType,
                    CaseTypeId = caseTypeId.Value
                };

                ApplicationMainDO newAppMainData = this.applicationRepository.CreateNewApplication(applicationNewDO, this.userContext, doc.RegUri);

                transaction.Commit();

                return newAppMainData;
            }
        }
    }
}
