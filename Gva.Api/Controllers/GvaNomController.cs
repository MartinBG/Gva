using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.PersonRepository;
using Gva.Api.Repositories.AircraftRepository;
using Newtonsoft.Json.Linq;
using Gva.Api.Repositories.OrganizationRepository;
using System;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/nomenclatures")]
    public class GvaNomController : ApiController
    {
        private IPersonRepository personRepository;
        private IAircraftRepository aircraftRepository;
        private IOrganizationRepository organizationRepository;
        private ICaseTypeRepository caseTypeRepository;
        private INomRepository nomRepository;

        public GvaNomController(
            IPersonRepository personRepository,
            IAircraftRepository aircraftRepository,
            IOrganizationRepository organizationRepository,
            ICaseTypeRepository caseTypeRepository,
            INomRepository nomRepository)
        {
            this.personRepository = personRepository;
            this.aircraftRepository = aircraftRepository;
            this.organizationRepository = organizationRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.nomRepository = nomRepository;
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
                this.personRepository.GetPersons(caseTypeAlias: "inspector", names: term, exact: false, offset: offset, limit: limit)
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
                this.personRepository.GetPersons(caseTypeAlias: "examiner", names: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = e.Names
                });

            return Ok(returnValue);
        }

        [Route("aircrafts/{id:int}")]
        public IHttpActionResult GetAircraft(int id)
        {
            var aircraft = this.aircraftRepository.GetAircraft(id);
            return Ok(new
            {
                nomValueId = aircraft.LotId,
                name = aircraft.Model
            });
        }

        [Route("aircrafts")]
        public IHttpActionResult GetAircrafts(string term = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.aircraftRepository.GetAircrafts(model: term, exact: false, offset: offset, limit: limit)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = e.Model
                });

            return Ok(returnValue);
        }

        [Route("organizations/{id:int}")]
        public IHttpActionResult GetOrganization(int id)
        {
            var organization = this.organizationRepository.GetOrganization(id);
            return Ok(new
            {
                nomValueId = organization.LotId,
                name = organization.Name
            });
        }

        [Route("organizations")]
        public IHttpActionResult GetOrganizations(string name = null, int offset = 0, int? limit = null)
        {
            var returnValue =
                this.organizationRepository.GetOrganizations(name: name, offset: offset, limit: limit, uin: null, CAO: null, dateCAOValidTo: null, dateValidTo: null)
                .Select(e => new
                {
                    nomValueId = e.LotId,
                    name = e.Name
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
                nomValueId = caseType.GvaCaseTypeId,
                name = caseType.Name
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
                    nomValueId = ct.GvaCaseTypeId,
                    name = ct.Name
                }));
        }

        [Route("schools")]
        public IHttpActionResult GetSchools(string term = null, int? graduationId = null, int offset = 0, int? limit = null)
        {
            if (graduationId == null)
            {
                return Ok(new List<NomValue>());
            }

            return Ok(
                this.nomRepository.GetNomValues("schools", term)
                .Where(nv => JObject.Parse(nv.TextContent).Value<JArray>("graduationIds").Values<string>().Contains(graduationId.ToString()))
                .WithOffsetAndLimit(offset, limit));
        }

        [Route("addressTypes")]
        public IHttpActionResult GetAddressTypes(string term = null, string type = null, int offset = 0, int? limit = null)
        {
            return Ok(
                this.nomRepository.GetNomValues("addressTypes", term)
                .Where(nv => JObject.Parse(nv.TextContent).Value<string>("type") == type)
                .WithOffsetAndLimit(offset, limit));
        }

        [Route("documentRoles")]
        public IHttpActionResult GetDocumentRoles(string term = null, string categoryAlias = null, [FromUri] string[] staffAliases = null, int offset = 0, int? limit = null)
        {
            return Ok(
                this.nomRepository.GetNomValues("documentRoles", term)
                .Where(nv =>
                {
                    JObject content = JObject.Parse(nv.TextContent);
                    bool isMatch = true;

                    if (categoryAlias != null)
                    {
                        isMatch &= content.Value<string>("categoryAlias") == categoryAlias;
                    }

                    JToken staffAlias;
                    if (isMatch && staffAliases != null && staffAliases.Length > 0 && content.TryGetValue("staffAlias", out staffAlias))
                    {
                        isMatch &= staffAliases.Contains(staffAlias.ToString());
                    }

                    return isMatch;
                })
                .WithOffsetAndLimit(offset, limit));
        }

        [Route("documentTypes")]
        public IHttpActionResult GetDocumentTypes(string term = null, bool? isIdDocument = null, [FromUri] string[] staffAliases = null, int offset = 0, int? limit = null)
        {
            return Ok(
                this.nomRepository.GetNomValues("documentTypes", term)
                .Where(nv =>
                {
                    JObject content = JObject.Parse(nv.TextContent);
                    bool isMatch = true;

                    if (isIdDocument != null)
                    {
                        isMatch &= content.Value<bool>("isIdDocument") == isIdDocument;
                    }

                    JToken staffAlias;
                    if (isMatch && staffAliases != null && staffAliases.Length > 0 && content.TryGetValue("staffAlias", out staffAlias))
                    {
                        isMatch &= staffAliases.Contains(staffAlias.ToString());
                    }

                    return isMatch;
                })
                .WithOffsetAndLimit(offset, limit));
        }

        [Route("auditPartRequirements")]
        public IHttpActionResult GetAuditPartRequirements(string type = null, string auditPartCode = null)
        {
            JObject defaultAuditResult = this.nomRepository.GetNomValues("auditResults").Where(r => r.Code == "-1")
                .Select(n => new JObject(
                    new JProperty("nomValueId", n.NomValueId),
                    new JProperty("code", n.Code),
                    new JProperty("name", n.Name))).First();

            var requirements = this.nomRepository.GetNomValues("auditPartRequirmants");

            JArray auditPartRequirements = new JArray();

            if (type == "aircrafts")
            {
                requirements = requirements.Where(r => r.OldId == "21");

                foreach (dynamic requirement in requirements)
                {
                    if (requirement.Code == "145.А.75") 
                    {
                        continue;
                    }

                    auditPartRequirements.Add(
                        new JObject(
                            new JProperty("subject", requirement.Name),
                            new JProperty("auditResult", defaultAuditResult),
                            new JProperty("disparities", new JArray()),
                            new JProperty("code", Convert.ToInt32(requirement.Code))
                    ));
                }
            }


            if (type == "organizations")
            {
                if(auditPartCode == "TR")
                {
                    requirements = requirements.Where(r => r.OldId == "22");
                }
                else if (auditPartCode == "145")
                {
                    requirements = requirements.Where(r => r.OldId == "1");
                }
                else if (auditPartCode == "147")
                {
                    requirements = requirements.Where(r => r.OldId == "4");
                }
                else if (auditPartCode == "ACAM")
                {
                    requirements = requirements.Where(r => (int.Parse(r.OldId) <= 211 && int.Parse(r.OldId) >= 200) || r.OldId == "42");
                }
                else if (auditPartCode == "MF")
                {
                    requirements = requirements.Where(r => r.OldId == "2");
                }
                else if (auditPartCode == "MG")
                {
                    requirements = requirements.Where(r => r.OldId == "3");
                }

                foreach (dynamic requirement in requirements)
                {
                    auditPartRequirements.Add(
                        new JObject(
                            new JProperty("subject", requirement.Name),
                            new JProperty("auditResult", defaultAuditResult),
                            new JProperty("disparities", new JArray()),
                            new JProperty("code", requirement.Code)
                    ));
                }

            }
            return Ok(auditPartRequirements.OrderBy(e => e.SelectToken("code")));
        }

        [Route("auditDetails")]
        public IHttpActionResult GetAuditDetails(string type = null) 
        {
            JObject defaultAuditResult =   this.nomRepository.GetNomValues("auditResults").Where(r => r.Code == "-1")
                .Select( n => new JObject(
                    new JProperty("nomValueId", n.NomValueId ),
                    new JProperty("code", n.Code),
                    new JProperty("name", n.Name))).First();


            JArray auditDetails = new JArray();

            if(type == "organizationRecommendations")
            {
                this.nomRepository.GetNomValues("auditPartSections").ToArray();
                var requirements = this.nomRepository.GetNomValues("auditPartSectionDetails")
                    .Where(d => JObject.Parse(d.ParentValue.TextContent).SelectToken("idPart") != null)
                    .Where(d => JObject.Parse(d.ParentValue.TextContent).SelectToken("idPart").ToString() == "4")
                    .GroupBy(d => d.ParentValue.Code)
                    .OrderBy(d => d.Key);

                foreach(dynamic requirement in requirements)
                {
                    JArray elements = new JArray();
                    foreach (dynamic element in requirement)
                    {
                        string sortOrder = JObject.Parse(element.TextContent).SelectToken("sortOrder");

                        elements.Add(new JObject(
                            new JProperty("groupTitle", element.ParentValue.Code + ' ' + element.ParentValue.Name),
                            new JProperty("subject", element.Name),
                            new JProperty("auditResult", defaultAuditResult),
                            new JProperty("disparities", new JArray()),
                            new JProperty("auditPart", element.Code),
                            new JProperty("titlePart", element.Code.Split('.')[0]),
                            new JProperty("sortOrder", sortOrder)
                        ));
                    }
                     auditDetails.Add( new JObject(
                        new JProperty("groupTitle", elements.First().SelectToken("groupTitle")),
                        new JProperty("group", elements.OrderBy(e => e.SelectToken("titlePart")).ThenBy(e => Convert.ToInt32(e.SelectToken("sortOrder"))))));
                }
            }

            return Ok(auditDetails);
        }
    }
}

