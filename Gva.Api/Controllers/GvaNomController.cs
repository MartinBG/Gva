using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.AircraftRepository;
using Gva.Api.Repositories.AirportRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.EquipmentRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.Repositories.StageRepository;
using System;
using Common.Data;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/nomenclatures")]
    [Authorize]
    public class GvaNomController : ApiController
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private IPersonRepository personRepository;
        private IAircraftRepository aircraftRepository;
        private IAirportRepository airportRepository;
        private IEquipmentRepository equipmentRepository;
        private IOrganizationRepository organizationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private INomRepository nomRepository;
        private IStageRepository stageRepository;

        public GvaNomController(
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IPersonRepository personRepository,
            IAircraftRepository aircraftRepository,
            IAirportRepository airportRepository,
            IEquipmentRepository equipmentRepository,
            IOrganizationRepository organizationRepository,
            ICaseTypeRepository caseTypeRepository,
            INomRepository nomRepository,
            IStageRepository stageRepository)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.personRepository = personRepository;
            this.aircraftRepository = aircraftRepository;
            this.airportRepository = airportRepository;
            this.equipmentRepository = equipmentRepository;
            this.organizationRepository = organizationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.nomRepository = nomRepository;
            this.stageRepository = stageRepository;
        }

        [Route("{lotId}/applications")]
        public IHttpActionResult GetApplications(int lotId, string term = null)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);

            var applications = this.applicationRepository.GetNomApplications(lotId).Select(a => new ApplicationNomDO(a));

            if (!string.IsNullOrWhiteSpace(term))
            {
                term = term.ToLower();
                applications = applications.Where(n => n.ApplicationName.ToLower().Contains(term)).ToArray();
            }

            return Ok(applications);
        }

        [Route("organizationsAudits")]
        public IHttpActionResult GetOrganizationsAudits(int lotId, string term = null)
        {
            var audits = this.lotRepository.GetLotIndex(lotId).Index.GetParts("organizationInspections")
                .Select(i => new
                {
                    nomValueId = i.Part.Index,
                    name = string.Format(
                        "№ {0} / {1} - {2}",
                        i.Content.Get<string>("documentNumber"),
                        i.Content.Get<string>("auditPart.name"),
                        i.Content.Get<string>("auditReason.name"))
                });

            if (!string.IsNullOrEmpty(term))
            {
                return Ok(audits.Where(a => a.name.Contains(term)));
            }

            return Ok(audits);
        }

        [Route("persons/{id:int}")]
        [Route("inspectors/{id:int}")]
        [Route("examiners/{id:int}")]
        public IHttpActionResult GetPerson(int id)
        {
            var person = this.personRepository.GetPerson(id);
            return Ok(new
            {
                nomValueId = person.LotId,
                name = person.Names
            });
        }

        [Route("persons")]
        public IHttpActionResult GetPersons(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.personRepository.GetPersons(names: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = e.Names
                });

            return Ok(returnValue);
        }

        [Route("commonQuestions")]
        public IHttpActionResult GetCommonQuestions(string term = null, int offset = 0, int? limit = null)
        {
            var aSExamQuestionType = this.nomRepository.GetNomValue("asExamQuestionTypes", "commonQuestions");

            var returnValue =
                this.personRepository.GetQuestions(aSExamQuestionType.NomValueId, name: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.ASExamVariantId,
                    name = e.Name
                });

            return Ok(returnValue);
        }

        [Route("specializedQuestions")]
        public IHttpActionResult GetSpecializedQuestions(string term = null, int offset = 0, int? limit = null)
        {
            var aSExamQuestionType = this.nomRepository.GetNomValue("asExamQuestionTypes", "specializedQuestions");

            var returnValue =
                this.personRepository.GetQuestions(aSExamQuestionType.NomValueId, name: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.ASExamVariantId,
                    name = e.Name
                });

            return Ok(returnValue);
        }

        [Route("inspectors")]
        public IHttpActionResult GetInspectors(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.personRepository.GetPersons(isInspector: true, names: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = e.Names
                });

            return Ok(returnValue);
        }

        [Route("examiners")]
        public IHttpActionResult GetExaminers(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.personRepository.GetPersons(isExaminer: true, names: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = e.Names
                });

            return Ok(returnValue);
        }

        [Route("lotSets/{id:int}")]
        public IHttpActionResult GetLotSet(int id)
        {
            var lotSet = this.applicationRepository.GetLotSet(id);
            return Ok(new
            {
                nomValueId = lotSet.SetId,
                name = lotSet.Name,
                alias = lotSet.Alias
            });
        }

        [Route("lotSets")]
        public IHttpActionResult GetLotSets(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.applicationRepository.GetLotSets(name: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.SetId,
                    name = e.Name,
                    alias = e.Alias
                });

            return Ok(returnValue);
        }

        [Route("aircrafts/{id:int}")]
        public IHttpActionResult GetAircraft(int id)
        {
            var ac = this.aircraftRepository.GetAircraft(id);
            return Ok(new
            {
                nomValueId = ac.LotId,
                name = ac.Model,
                nameAlt = ac.ModelAlt,
                textContent =
                    new
                    {
                        airCategory = ac.AirCategory,
                        aircraftProducer = ac.AircraftProducer
                    }
            });
        }

        [Route("aircrafts")]
        public IHttpActionResult GetAircrafts(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.aircraftRepository.GetAircrafts(mark: null, manSN: null, model: term, icao: null, airCategory: null, aircraftProducer: null, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = string.Format("{0} ({1})", e.Mark, e.Model),
                    nameAlt = string.Format("{0} ({1})", e.Mark, e.ModelAlt)
                });

            return Ok(returnValue);
        }

        [Route("aircraftModels/{id:int}")]
        public IHttpActionResult GetAircraftModel(int id)
        {
            var ac = this.aircraftRepository.GetAircraftModel(id);
            return Ok(new
            {
                nomValueId = ac.LotId,
                name = string.Format("{0} ({1})", ac.Mark, ac.Model),
                nameAlt = string.Format("{0} ({1})", ac.Mark, ac.ModelAlt)
            });
        }

        [Route("aircraftModels")]
        public IHttpActionResult GetAircraftModels(
            string term = null,
            int offset = 0,
            int? limit = null,
            string airCategory = "",
            string aircraftProducer = "")
        {
            var returnValue =
                this.aircraftRepository.GetAircraftModels(
                    airCategory: airCategory,
                    aircraftProducer: aircraftProducer,
                    offset: offset,
                    limit: limit)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = e.Model,
                    nameAlt = e.ModelAlt,
                    textContent =
                        new
                        {
                            airCategory = e.AirCategory,
                            aircraftProducer = e.AircraftProducer
                        }
                });

            return Ok(returnValue);
        }

        [Route("airports/{id:int}")]
        public IHttpActionResult GetAirport(int id)
        {
            var returnValue = this.airportRepository.GetAirport(id);
            return Ok(new
            {
                nomValueId = returnValue.LotId,
                name = returnValue.Name
            });
        }

        [Route("airports")]
        public IHttpActionResult GetAirports(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.airportRepository.GetAirports(name: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = e.Name
                });

            return Ok(returnValue);
        }

        [Route("equipments/{id:int}")]
        public IHttpActionResult GetEquipment(int id)
        {
            var returnValue = this.equipmentRepository.GetEquipment(id);
            return Ok(new
            {
                nomValueId = returnValue.LotId,
                name = returnValue.Name
            });
        }

        [Route("equipments")]
        public IHttpActionResult GetEquipments(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.equipmentRepository.GetEquipments(name: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = e.Name
                });

            return Ok(returnValue);
        }

        [Route("organizations/{id:int}")]
        public IHttpActionResult GetOrganization(int id)
        {
            var returnValue = this.organizationRepository.GetOrganization(id);
            return Ok(new
            {
                NomValueId = returnValue.LotId,
                Name = returnValue.Name,
                NameAlt = returnValue.NameAlt
            });
        }

        [Route("organizations")]
        public IHttpActionResult GetOrganizations(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.organizationRepository.GetOrganizations(name: term, caseTypeId: null, offset: offset, limit: limit, uin: null, cao: null, dateCaoValidTo: null, dateValidTo: null)
                .Select(e => new
                {
                    NomValueId = e.LotId,
                    Name = e.Name,
                    NameAlt = e.NameAlt
                });

            return Ok(returnValue);
        }

        [Route("{set:regex(^(?:person|organization)$)}CaseTypes")]
        public IHttpActionResult GetCaseTypes(string set, string term = null)
        {
            IEnumerable<GvaCaseType> caseTypes = this.caseTypeRepository.GetCaseTypesForSet(set);

            if (!string.IsNullOrWhiteSpace(term))
            {
                caseTypes = caseTypes.Where(ct => ct.Name.ToLower().Contains(term.ToLower()));
            }

            return Ok(
                caseTypes.Select(ct => new
                {
                    NomValueId = ct.GvaCaseTypeId,
                    Name = ct.Name,
                    Alias = ct.Alias
                }));
        }

        [Route("caseTypes")]
        public IHttpActionResult GetCaseTypesForLot(int lotId, string term = null)
        {
            term = term ?? string.Empty;

            var caseTypes = this.caseTypeRepository.GetCaseTypesForLot(lotId)
                .Where(ct => ct.Name.ToLower().Contains(term.ToLower()))
                .Select(ct => new
                {
                    NomValueId = ct.GvaCaseTypeId,
                    Name = ct.Name,
                    Alias = ct.Alias
                });

            return Ok(caseTypes);
        }

        [Route("caseTypes/{id:int}"),
         Route("personCaseTypes/{id:int}"),
         Route("organizationCaseTypes/{id:int}")]
        public IHttpActionResult GetCaseType(int id)
        {
            var caseType = this.caseTypeRepository.GetCaseType(id);
            return Ok(new
            {
                NomValueId = caseType.GvaCaseTypeId,
                Name = caseType.Name,
                Alias = caseType.Alias
            });
        }

        [Route("schools")]
        public IHttpActionResult GetSchools(string term = null, int? graduationId = null, int offset = 0, int? limit = null)
        {
            if (graduationId == null)
            {
                return Ok(this.nomRepository.GetNomValues("schools", term, offset: offset, limit: limit));
            }

            return Ok(
                this.nomRepository.GetNomValues("schools", term)
                .Where(nv => nv.TextContent.GetItems<int>("graduationIds").Contains(graduationId.Value))
                .WithOffsetAndLimit(offset, limit));
        }

        [Route("addressTypes")]
        public IHttpActionResult GetAddressTypes(string term = null, string type = null, int offset = 0, int? limit = null)
        {
            if (type == null)
            {
                return Ok(this.nomRepository.GetNomValues("addressTypes", term, offset: offset, limit: limit));
            }

            return Ok(
                this.nomRepository.GetNomValues("addressTypes", term)
                .Where(nv => nv.TextContent.Get<string>("type") == type)
                .WithOffsetAndLimit(offset, limit));
        }

        [Route("documentRoles")]
        public IHttpActionResult GetDocumentRoles(
            string term = null,
            string categoryAlias = null,
            [FromUri] string[] staffAliases = null,
            [FromUri] string[] categoryCodes = null,
            int offset = 0,
            int? limit = null)
        {
            IEnumerable<NomValue> nomValues;
            if (categoryAlias == null && (staffAliases == null || staffAliases.Length == 0))
            {
                nomValues = this.nomRepository.GetNomValues("documentRoles", term: term, offset: offset, limit: limit);
            }
            else
            {
                nomValues =
                    this.nomRepository.GetNomValues("documentRoles", term)
                    .Where(nv =>
                    {
                        bool isMatch = true;

                        if (categoryAlias != null)
                        {
                            isMatch &= nv.TextContent.Get<string>("categoryAlias") == categoryAlias;
                        }

                        if (isMatch && categoryCodes != null && categoryCodes.Length > 0)
                        {
                            isMatch &= categoryCodes.Contains(nv.TextContent.Get<string>("categoryCode"));
                        }

                        if (isMatch && staffAliases != null && staffAliases.Length > 0)
                        {
                            string staffTypeAliasStr = nv.TextContent.Get<string>("staffTypeAlias");
                            isMatch &= string.IsNullOrWhiteSpace(staffTypeAliasStr) || staffAliases.Contains(staffTypeAliasStr);
                        }

                        return isMatch;
                    })
                    .WithOffsetAndLimit(offset, limit);
            }

            return Ok(nomValues);
        }

        [Route("documentParts")]
        public IHttpActionResult GetDocumentParts(string term = null, string set = null, int? parentValueId = null)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("documentParts", term);

            if (!string.IsNullOrEmpty(set))
            {
                nomValues = nomValues.Where(p => p.Code.Contains(set));
            }

            if (parentValueId != null)
            {
                nomValues = nomValues.Where(n => n.ParentValueId == parentValueId);
            }

            return Ok(nomValues);
        }

        [Route("limitations66")]
        public IHttpActionResult GetLimitations66(string term = null, string general = null)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("limitations66", term);
            if (!string.IsNullOrEmpty(general))
            {
                nomValues = nomValues.Where(l => l.TextContent.Get<string>("general") == general);
            }

            return Ok(nomValues);
        }

        [Route("licenceTypes")]
        public IHttpActionResult GetLicenceTypes(string term = null, string staffTypeAlias = null, string fclCode = null)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("licenceTypes", term);
            if (fclCode == "Y")
            {
                nomValues = nomValues.Where(l => l.TextContent.Get<string>("licenceCode").Contains("FCL"));
            }

            if (!string.IsNullOrEmpty(staffTypeAlias))
            {
                nomValues = nomValues.Where(l => l.TextContent.Get<string>("staffTypeAlias") == staffTypeAlias);
            }

            return Ok(nomValues);
        }

        [Route("documentTypes")]
        public IHttpActionResult GetDocumentTypes(string term = null, bool? isIdDocument = null, [FromUri] string[] staffAliases = null, int offset = 0, int? limit = null)
        {
            IEnumerable<NomValue> nomValues;
            if (isIdDocument == null && (staffAliases == null || staffAliases.Length > 0))
            {
                nomValues = this.nomRepository.GetNomValues("documentTypes", term: term, offset: offset, limit: limit);
            }
            else
            {
                nomValues =
                    this.nomRepository.GetNomValues("documentTypes", term)
                    .Where(nv =>
                    {
                        bool isMatch = true;

                        if (isIdDocument != null)
                        {
                            isMatch &= nv.TextContent.Get<bool>("isIdDocument") == isIdDocument;
                        }

                        if (isMatch && staffAliases != null && staffAliases.Length > 0)
                        {
                            string staffTypeAliasStr = nv.TextContent.Get<string>("staffTypeAlias");
                            isMatch &= string.IsNullOrWhiteSpace(staffTypeAliasStr) || staffAliases.Contains(staffTypeAliasStr);
                        }

                        return isMatch;
                    })
                    .WithOffsetAndLimit(offset, limit);
            }

            return Ok(nomValues);
        }

        [Route("aircraftProducers")]
        public IHttpActionResult GetAircraftProducers(string term = null, int offset = 0, int? limit = null, bool? makeEngine = false, bool? makeRadio = false, bool? makePropeller = false, bool? makeAircraft = false)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("aircraftProducers", term).Where(ap =>
            {
                bool isMatch = true;

                if (makeEngine.Value)
                {
                    isMatch &= ap.TextContent.Get<bool>("makeEngine");
                }
                else if (makeRadio.Value)
                {
                    isMatch &= ap.TextContent.Get<bool>("makeRadio");
                }
                else if (makePropeller.Value)
                {
                    isMatch &= ap.TextContent.Get<bool>("makePropeller");
                }
                else if (makeAircraft.Value)
                {
                    isMatch &= ap.TextContent.Get<bool>("makeAircraft");
                }

                return isMatch;
            });

            return Ok(nomValues);
        }

        [Route("linTypes")]
        public IHttpActionResult GetLinTypes()
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("linTypes");

            return Ok(nomValues);
        }

        [Route("inspectorTypes")]
        public IHttpActionResult GetInspectorTypes(bool? showInspectors = true)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("inspectorTypes");
            if (!showInspectors.Value)
            {
                nomValues = nomValues.Where(e => e.Alias != "gvaInspector");
            }

            return Ok(nomValues);
        }

        [Route("ratingClasses")]
        public IHttpActionResult GetRatingClasses(string staffTypeAlias, string term = null, int offset = 0, int? limit = null)
        {
            var nomValues = this.nomRepository.GetNomValues(
                alias: "ratingClasses",
                parentAlias: "ratingClassGroups",
                parentProp: "staffTypeAlias",
                parentPropValue: staffTypeAlias,
                term: term,
                offset: offset,
                limit: limit);

            return Ok(nomValues);
        }

        [Route("authorizations")]
        public IHttpActionResult GetAuthorizations(string staffTypeAlias, string term = null, int offset = 0, int? limit = null)
        {
            var nomValues = this.nomRepository.GetNomValues(
                alias: "authorizations",
                parentAlias: "authorizationGroups",
                parentProp: "staffTypeAlias",
                parentPropValue: staffTypeAlias,
                term: term,
                offset: offset,
                limit: limit);

            return Ok(nomValues);
        }

        [Route("applicationTypes")]
        public IHttpActionResult GetApplicationTypes(string caseTypeAlias = null, string code = null, string name = null, int? lotId = null, string term = null, int offset = 0, int? limit = null)
        {
            var nomValues = this.nomRepository.GetNomValues(
                alias: "applicationTypes",
                term: term);

            if (!string.IsNullOrWhiteSpace(caseTypeAlias))
            {
                nomValues = nomValues.Where(nv => nv.TextContent.GetItems<string>("caseTypes").Contains(caseTypeAlias));
            }

            if (!string.IsNullOrWhiteSpace(code))
            {
                nomValues = nomValues.Where(nv => nv.Code.Contains(code));
            }

            if (!string.IsNullOrWhiteSpace(name))
            {
                nomValues = nomValues.Where(nv => nv.Name.Contains(name));
            }

            if (lotId.HasValue)
            {
                var caseTypeAliases = this.caseTypeRepository.GetCaseTypesForLot(lotId.Value)
                    .Select(ct => ct.Alias);

                nomValues = nomValues.Where(nv => nv.TextContent.GetItems<string>("caseTypes").Any(ct => caseTypeAliases.Contains(ct)));
            }

            nomValues = nomValues.Skip(offset);

            if (limit.HasValue)
            {
                nomValues = nomValues.Take(limit.Value);
            }

            return Ok(nomValues);
        }

        [Route("appStages")]
        public IHttpActionResult GetAppStages(string term = null)
        {
            var returnValue = this.stageRepository.GetStages(term)
                .Select(e => new
                {
                    nomValueId = e.GvaStageId,
                    name = e.Name
                });

            return Ok(returnValue);
        }

        [Route("appStages/{id:int}")]
        public IHttpActionResult GetAppStage(int id)
        {
            var stage = this.stageRepository.GetStage(id);

            return Ok(new
            {
                nomValueId = stage.GvaStageId,
                name = stage.Name
            });
        }

        [Route("employments")]
        public IHttpActionResult GetEmployments(int lotId)
        {
            var returnValue = this.lotRepository.GetLotIndex(lotId).Index.GetParts("personDocumentEmployments")
                .Select(e => new
                {
                    nomValueId = e.Part.Index,
                    name = string.Format(
                        "{0}, {1} {2}",
                        e.Content.Get<string>("organization.name"),
                        e.Content.Get<DateTime>("hiredate").ToString("dd.MM.yyyy"),
                        e.Content.Get<string>("valid.code") == "N" ? "(НЕВАЛИДНА)" : null)
                });

            return Ok(returnValue);
        }
    }
}

