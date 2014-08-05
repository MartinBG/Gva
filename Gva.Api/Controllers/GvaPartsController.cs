using System;
using System.Linq;
using System.Web.Http;
using Common.Json;
using Gva.Api.ModelsDO;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.Repositories.FileRepository;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/gvaParts/{lotId}")]
    public class GvaPartsController : ApiController
    {
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;

        public GvaPartsController(ILotRepository lotRepository, IFileRepository fileRepository)
        {
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
        }

        [Route("{*path:regex(^aircraftCertRegistrationsFM$)}")]
        public IHttpActionResult GetRegistrations(int lotId, string path, string term = null, int offset = 0, int? limit = null)
        {
            return this.GetParts(
                lotId: lotId,
                path: path,
                description: (o) => o.Get<string>("certNumber"),
                term: term,
                offset: offset,
                limit: limit);
        }

        private IHttpActionResult GetParts(
            int lotId,
            string path,
            Func<JObject, string> description,
            string term,
            int offset,
            int? limit)
        {
            term = term ?? string.Empty;

            var index = this.lotRepository.GetLotIndex(lotId).Index;
            var result = index.GetParts(path).Select(pv => new PartSelectDO
                {
                    PartIndex = pv.Part.Index,
                    Description = description(pv.Content)
                })
                .Where(p => p.Description.Contains(term))
                .OrderBy(r => r.Description)
                .Skip(offset);

            if (limit.HasValue)
            {
                result = result.Take(limit.Value);
            }

            return Ok(result);
        }

        [HttpGet]
        [Route("isUniqueBPN")]
        public IHttpActionResult IsUniqueBPN(int lotId, int caseTypeId, string bookPageNumber, int? fileId = null)
        {
            return Ok(new
            {
                isUnique = this.fileRepository.IsUniqueBPN(lotId, caseTypeId, bookPageNumber, fileId)
            });
        }
    }
}

