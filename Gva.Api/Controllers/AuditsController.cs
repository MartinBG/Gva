using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Common.Json;
using Newtonsoft.Json.Linq;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/audits")]
    [Authorize]
    public class AuditsController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private INomRepository nomRepository;

        public AuditsController(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository)
        {
            this.unitOfWork = unitOfWork;
            this.nomRepository = nomRepository;
        }

        [Route("requirementDetails")]
        public IHttpActionResult GetDetails(string type = null, string auditPartCode = null)
        {
            JObject defaultAuditResult = this.nomRepository.GetNomValues("auditResults").Where(r => r.Code == "-1")
                .Select(n => new JObject(
                    new JProperty("nomValueId", n.NomValueId),
                    new JProperty("code", n.Code),
                    new JProperty("name", n.Name)))
                .First();

            IEnumerable<NomValue> auditPartRequirements;

            if (type == "organizations")
            {
                auditPartRequirements = this.nomRepository.GetNomValues("auditPartRequirements", "auditPart", propValue: auditPartCode);
            }
            else if (type == "aircrafts")
            {
                auditPartRequirements = this.nomRepository.GetNomValues("auditPartRequirements", "auditPart", propValue: "aircrafts");
            }
            else
            {
                throw new ArgumentException();
            }

            var result = auditPartRequirements
                .Select(r =>
                    new JObject(
                        new JProperty("subject", r.Name),
                        new JProperty("auditResult", defaultAuditResult),
                        new JProperty("code", r.Code)));

            return Ok(new JArray(result));
        }

        [Route("sectionDetails")]
        public IHttpActionResult GetAuditDetails(string auditPartCode = null)
        {
            JObject defaultRecommendationResult = this.nomRepository.GetNomValues("recommendationResults").Where(r => r.Code == "U")
                .Select(n => new JObject(
                    new JProperty("nomValueId", n.NomValueId),
                    new JProperty("code", n.Code),
                    new JProperty("name", n.Name)))
                .First();

            var sections = this.unitOfWork.DbContext.GetNomValuesByTextContentProperty("auditPartSections", "auditPart", auditPartCode);
            var details = this.unitOfWork.DbContext.Set<NomValue>().Where(nv => nv.Nom.Alias == "auditPartSectionDetails");

            var result = (from section in sections
                          join detail in details on section.NomValueId equals detail.ParentValueId
                          group detail by new { section.NomValueId, section.Name, section.Code, section.Order } into g
                          orderby g.Key.Order
                          select new
                          {
                              SectionName = g.Key.Name,
                              SectionCode = g.Key.Code,
                              Groups = from gi in g
                                       orderby gi.Order
                                       select new
                                       {
                                           gi.Name,
                                           gi.Code,
                                           gi.Order
                                       }
                          })
                    .ToList()
                    .Select(s =>
                        new JObject(
                            new JProperty("sectionName", s.SectionName),
                            new JProperty("sectionCode", s.SectionCode),
                            new JProperty("details",
                                (from gi in s.Groups
                                 select new JObject(
                                     new JProperty("subject", gi.Name),
                                     new JProperty("recommendationResult", defaultRecommendationResult),
                                     new JProperty("code", gi.Code))))))
                    .ToArray();

            return Ok(new JArray(result));
        }
    }
}
