using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.PersonRepository;
using Newtonsoft.Json.Linq;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/nomenclatures")]
    public class GvaNomController : ApiController
    {
        private IPersonRepository personRepository;
        private ICaseTypeRepository caseTypeRepository;
        private INomRepository nomRepository;

        public GvaNomController(
            IPersonRepository personRepository,
            ICaseTypeRepository caseTypeRepository,
            INomRepository nomRepository)
        {
            this.personRepository = personRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.nomRepository = nomRepository;
        }

        [Route("persons/{id:int}")]
        public IHttpActionResult GetPerson(int id)
        {
            var person = this.personRepository.GetPerson(id);
            return Ok(new
            {
                nomValueId = person.GvaPersonLotId,
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
                    nomValueId = e.GvaPersonLotId,
                    name = e.Names
                });

            return Ok(returnValue);
        }

        [Route("personCaseTypes/{id:int}")]
        public IHttpActionResult GetCaseType(int id)
        {
            var caseType = this.caseTypeRepository.GetCaseType(id);
            return Ok(new
            {
                nomValueId = caseType.GvaCaseTypeId,
                name = caseType.Name
            });
        }

        [Route("personCaseTypes")]
        public IHttpActionResult GetCaseTypes(string term = null, int? lotId = null)
        {
            IEnumerable<GvaCaseType> caseTypes;
            if (lotId.HasValue)
            {
                caseTypes = this.caseTypeRepository.GetCaseTypesForLot(lotId.Value);
            }
            else
            {
                caseTypes = this.caseTypeRepository.GetCaseTypesForSet("Person");
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
                this.nomRepository.GetNoms("schools", term)
                .Where(nv => JObject.Parse(nv.TextContent).Value<JArray>("graduationIds").Values<string>().Contains(graduationId.ToString()))
                .WithOffsetAndLimit(offset, limit));
        }

        [Route("addressTypes")]
        public IHttpActionResult GetAddressTypes(string term = null, string type = null, int offset = 0, int? limit = null)
        {
            return Ok(
                this.nomRepository.GetNoms("addressTypes", term)
                .Where(nv => JObject.Parse(nv.TextContent).Value<string>("type") == type)
                .WithOffsetAndLimit(offset, limit));
        }

        [Route("documentRoles")]
        public IHttpActionResult GetDocumentRoles(string term = null, string categoryAlias = null, [FromUri] string[] staffCodes = null, int offset = 0, int? limit = null)
        {
            return Ok(
                this.nomRepository.GetNoms("documentRoles", term)
                .Where(nv =>
                {
                    JObject content = JObject.Parse(nv.TextContent);
                    bool isMatch = true;

                    if (categoryAlias != null)
                    {
                        isMatch &= content.Value<string>("categoryAlias") == categoryAlias;
                    }

                    JToken staffAlias;
                    if (isMatch && staffCodes != null && content.TryGetValue("staffAlias", out staffAlias))
                    {
                        isMatch &= staffCodes.Contains(staffAlias.ToString());
                    }

                    return isMatch;
                })
                .WithOffsetAndLimit(offset, limit));
        }

        [Route("documentTypes")]
        public IHttpActionResult GetDocumentTypes(string term = null, bool? isIdDocument = null, [FromUri] string[] staffCodes = null, int offset = 0, int? limit = null)
        {
            return Ok(
                this.nomRepository.GetNoms("documentTypes", term)
                .Where(nv =>
                {
                    JObject content = JObject.Parse(nv.TextContent);
                    bool isMatch = true;

                    if (isIdDocument != null)
                    {
                        isMatch &= content.Value<bool>("isIdDocument") == isIdDocument;
                    }

                    JToken staffAlias;
                    if (isMatch && staffCodes != null && content.TryGetValue("staffAlias", out staffAlias))
                    {
                        isMatch &= staffCodes.Contains(staffAlias.ToString());
                    }

                    return isMatch;
                })
                .WithOffsetAndLimit(offset, limit));
        }
    }
}

