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
using Gva.Api.Repositories.EquipmentRepository;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Gva.Api.Repositories.PersonRepository;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;
using System;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/nomenclatures")]
    public class GvaNomController : ApiController
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private IPersonRepository personRepository;
        private IAircraftRepository aircraftRepository;
        private IAirportRepository airportRepository;
        private IEquipmentRepository equipmentRepository;
        private IOrganizationRepository organizationRepository;
        private IAircraftRegistrationRepository aircraftRegistrationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private INomRepository nomRepository;

        public GvaNomController(
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IPersonRepository personRepository,
            IAircraftRepository aircraftRepository,
            IAirportRepository airportRepository,
            IEquipmentRepository equipmentRepository,
            IOrganizationRepository organizationRepository,
            IAircraftRegistrationRepository aircraftRegistrationRepository,
            ICaseTypeRepository caseTypeRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.personRepository = personRepository;
            this.aircraftRepository = aircraftRepository;
            this.airportRepository = airportRepository;
            this.equipmentRepository = equipmentRepository;
            this.organizationRepository = organizationRepository;
            this.aircraftRegistrationRepository = aircraftRegistrationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.nomRepository = nomRepository;
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
        public IHttpActionResult GetOrganizationsAudits(int lotId)
        {
            var audits = this.lotRepository.GetLotIndex(lotId).Index.GetParts("organizationInspections")
                .Select(i => new
                {
                    nomValueId = i.Part.Index,
                    name = i.Content.Get<string>("documentNumber")
                });

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
                this.aircraftRepository.GetAircrafts(mark: null, manSN: null, model: term, airCategory: null, aircraftProducer: null, exact: false, offset: offset, limit: limit)
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
                this.organizationRepository.GetOrganizations(name: term, caseTypeId: null, offset: offset, limit: limit, uin: null, CAO: null, dateCAOValidTo: null, dateValidTo: null)
                .Select(e => new
                {
                    NomValueId = e.LotId,
                    Name = e.Name,
                    NameAlt = e.NameAlt
                });

            return Ok(returnValue);
        }

        [Route("registrations")]
        public IHttpActionResult GetRegistrations(string term = null, int offset = 0, int? limit = null, int? parentValueId = null)
        {
            var returnValue =
                            this.aircraftRegistrationRepository.GetRegistrations(parentValueId)
                            .Select(e => new
                            {
                                nomValueId = e.Part.Index,
                                name = e.ActNumber.ToString()
                            });

            return Ok(returnValue);
        }

        [Route("personCaseTypes/{id:int}"),
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


        [Route("{set:regex(^(?:person|organization)$)}CaseTypes")]
        public IHttpActionResult GetCaseTypes(string set, string term = null, int? lotId = null)
        {
            IEnumerable<GvaCaseType> caseTypes;
            if (lotId.HasValue)
            {
                caseTypes = this.caseTypeRepository.GetCaseTypesForLot(lotId.Value);
            }
            else
            {
                caseTypes = this.caseTypeRepository.GetCaseTypesForSet(set);
            }

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
        public IHttpActionResult GetDocumentParts(string set = null, int? parentValueId = null)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("documentParts");

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
        public IHttpActionResult GetLimitations66(string general = null)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("limitations66");
            if (!string.IsNullOrEmpty(general))
            {
                nomValues = nomValues.Where(l => l.TextContent.Get<string>("general") == general);
            }

            return Ok(nomValues);
        }

        [Route("licenceTypes")]
        public IHttpActionResult GetLlcenceTypes(string staffTypeAlias = null, string fclCode = null)
        {
            IEnumerable<NomValue> nomValues = this.nomRepository.GetNomValues("licenceTypes");
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

        [Route("auditPartRequirements")]
        public IHttpActionResult GetAuditPartRequirements(string type = null, string auditPartCode = null)
        {
            JObject defaultAuditResult = this.nomRepository.GetNomValues("auditResults").Where(r => r.Code == "-1")
                .Select(n => new JObject(
                    new JProperty("nomValueId", n.NomValueId),
                    new JProperty("code", n.Code),
                    new JProperty("name", n.Name))).First();

            var requirements = this.nomRepository.GetNomValues("auditPartRequirements");

            if (type == "organizations" || type == "aircrafts")
            {
                requirements = requirements.Where(r => r.TextContent.Get("idPart") != null);

                if (type == "organizations")
                {
                    requirements = requirements.Where(r => r.TextContent.Get<string>("idPart") == auditPartCode);
                }
                if (type == "aircrafts")
                {
                    requirements = requirements.Where(r => r.TextContent.Get<string>("idPart") == "aircrafts");
                }

                JArray auditPartRequirements = new JArray();
                foreach (var requirement in requirements)
                {
                    string sortOrder = requirement.TextContent.Get<string>("sortOrder");
                    auditPartRequirements.Add(
                        new JObject(
                            new JProperty("subject", requirement.Name),
                            new JProperty("auditResult", defaultAuditResult),
                            new JProperty("disparities", new JArray()),
                            new JProperty("code", requirement.Code),
                            new JProperty("sortOrder", sortOrder)
                    ));
                }

                return Ok(auditPartRequirements.OrderBy(e => e.Get<int>("sortOrder")));

            }

            return Ok(requirements);
        }

        [Route("auditDetails")]
        public IHttpActionResult GetAuditDetails(string type = null, string auditPartCode = null)
        {
            JObject defaultAuditResult = this.nomRepository.GetNomValues("auditResults").Where(r => r.Code == "-1")
                .Select(n => new JObject(
                    new JProperty("nomValueId", n.NomValueId),
                    new JProperty("code", n.Code),
                    new JProperty("name", n.Name))).First();


            JArray auditDetails = new JArray();

            if (type == "organizationRecommendations")
            {

                this.nomRepository.GetNomValues("auditPartSections").ToArray();
                var requirements = this.nomRepository.GetNomValues("auditPartSectionDetails")
                    .Where(d => d.ParentValue.TextContent.Get<string>("idPart") == auditPartCode)
                    .GroupBy(d => d.ParentValue.Code)
                    .OrderBy(d => d.Key);

                foreach (var requirement in requirements)
                {
                    JArray elements = new JArray();
                    foreach (var element in requirement)
                    {
                        string sortOrder = element.TextContent.Get<string>("sortOrder");

                        elements.Add(
                            new JObject(
                                new JProperty("groupTitle", element.ParentValue.Code + ' ' + element.ParentValue.Name),
                                new JProperty("subject", element.Name),
                                new JProperty("auditResult", defaultAuditResult),
                                new JProperty("disparities", new JArray()),
                                new JProperty("auditPart", element.Code),
                                new JProperty("titlePart", element.Code.Split('.')[0]),
                                new JProperty("sortOrder", sortOrder)));
                    }
                    auditDetails.Add(
                       new JObject(
                           new JProperty("groupTitle", elements.First().Get("groupTitle")),
                           new JProperty("group",
                               elements
                               .OrderBy(e => e.Get<string>("titlePart"))
                               .ThenBy(e => e.Get<int>("sortOrder")))));
                }
            }

            return Ok(auditDetails);
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
        public IHttpActionResult GetRatingClasses(string staffTypeAlias, string term)
        {
            var nomValues = this.nomRepository.GetNomValues(
                alias: "ratingClasses",
                parentAlias: "ratingClassGroups",
                prop: "staffTypeAlias",
                propValue: staffTypeAlias,
                term: term);

            return Ok(nomValues);
        }

        [Route("authorizations")]
        public IHttpActionResult GetAuthorizations(string staffTypeAlias, string term, int offset = 0, int? limit = null)
        {
            var nomValues = this.nomRepository.GetNomValues(
                alias: "authorizations",
                parentAlias: "authorizationGroups",
                prop: "staffTypeAlias",
                propValue: staffTypeAlias,
                term: term);

            return Ok(nomValues);
        }
    }
}

