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
        private ICorrespondentRepository correspondentRepository;
        private IRioDocumentParser rioDocumentParser;
        private IRioObjectExtractor rioObjectExtractor;
        private UserContext userContext;
        private ILotEventDispatcher lotEventDispatcher;

        private Dictionary<string, string> AppTypeCodeByAlias =
            new Dictionary<string, string>(){
                    {"R-4284", "АП-5D" }
            };

        private const string LicenseControllerAndCoordinatorAppType = "R-4284";

        public IntegrationController(
            IDocRepository docRepository,
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IPersonRepository personRepository,
            IApplicationRepository applicationRepository,
            ICaseTypeRepository caseTypeRepository,
            ICorrespondentRepository correspondentRepository,
            INomRepository nomRepository,
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
            this.caseTypeRepository = caseTypeRepository;
            this.correspondentRepository = correspondentRepository;
            this.nomRepository = nomRepository;
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
                                        else
                                        {
                                            personData.Lin = canParse ? lin : (int?)null;
                                        }
                                    }

                                    personData.FirstNameAlt = concreteApp.FlightCrewPersonalData.PersonNamesLatin.PersonFirstNameLatin;
                                    personData.MiddleNameAlt = concreteApp.FlightCrewPersonalData.PersonNamesLatin.PersonMiddleNameLatin;
                                    personData.LastNameAlt = concreteApp.FlightCrewPersonalData.PersonNamesLatin.PersonLastNameLatin;

                                    string countryName = concreteApp.FlightCrewPersonalData.Citizenship.CountryName;
                                    if (!string.IsNullOrEmpty(countryName))
                                    {
                                        personData.Country = this.nomRepository.GetNomValues("countries").Where(c => c.Name == countryName).FirstOrDefault();
                                    }

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
                PersonDataDO personData = lot.Index.GetPart<PersonDataDO>("personData").Content;

                
                string personNames = string.Format("{0} {1} {2}", personData.FirstName, personData.MiddleName, personData.LastName);
                List<Correspondent> correspondents = this.correspondentRepository.GetCorrespondents(personNames, personData.Email, 10, 0);
                List<int> correspondentIds = correspondents.Select(c => c.CorrespondentId).ToList() ?? new List<int>();
                if (correspondentIds.Count() == 0)
                {
                    CorrespondentDO correspondent = this.correspondentRepository.GetNewCorrespondent();
                    if (lot.Set.Alias == "Person")
                    {
                        if (personData.Country.Code == "BG")
                        {
                            correspondent.BgCitizenFirstName = personData.FirstName;
                            correspondent.BgCitizenLastName = personData.LastName;
                            correspondent.BgCitizenUIN = personData.Uin;

                        }
                        else
                        {
                            correspondent.ForeignerFirstName = personData.FirstName;
                            correspondent.ForeignerLastName = personData.LastName;
                        }

                        correspondent.Email = personData.Email;
                    }
                    CorrespondentDO corr = this.correspondentRepository.CreateCorrespondent(correspondent);
                    correspondentIds.Add(corr.CorrespondentId.Value);
                }

                int? caseTypeId = null;
                List<string> personCaseTypes = personData.CaseTypes.Select(c => c.Alias).ToList();
                var caseType = newAppDO.CaseTypes.Where(a => personCaseTypes.Any(c => c == a.Alias)).FirstOrDefault();
                if(caseType != null)
                {
                    caseTypeId = caseType.GvaCaseTypeId;
                }
                else
                {
                    var firstAppCaseTypes = newAppDO.CaseTypes.First();
                    NomValue caseTypeNom = new NomValue()
                    {
                        NomValueId = firstAppCaseTypes.GvaCaseTypeId,
                        Name = firstAppCaseTypes.Name,
                        Alias = firstAppCaseTypes.Alias
                    };

                    //update person data part
                    personData.CaseTypes.Add(caseTypeNom);
                    this.caseTypeRepository.AddCaseTypes(lot, personData.CaseTypes.Select(ct => ct.NomValueId));
                    var personDataPart = lot.UpdatePart("personData", personData, this.userContext);
                    lot.Commit(this.userContext, this.lotEventDispatcher);
                    this.unitOfWork.Save();
                    this.lotRepository.ExecSpSetLotPartTokens(personDataPart.PartId);

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

                ApplicationMainDO newAppMainData = this.applicationRepository.CreateNewApplication(applicationNewDO, doc.RegUri);

                transaction.Commit();

                return newAppMainData;
            }
        }

    }
}
