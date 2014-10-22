using System;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/gvaParts/{lotId}")]
    public class GvaPartsController : ApiController
    {
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;

        public GvaPartsController(
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository)
        {
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("{*path:regex(^aircraftCertRegistrationsFM$)}")]
        public IHttpActionResult GetRegistrations(int lotId, string path, string term = null, int offset = 0, int? limit = null)
        {
            return this.GetParts<AircraftCertRegistrationFMDO>(
                lotId: lotId,
                path: path,
                description: (o) => o.CertNumber.HasValue ? o.CertNumber.ToString() : null,
                term: term,
                offset: offset,
                limit: limit);
        }

        private IHttpActionResult GetParts<T>(
            int lotId,
            string path,
            Func<T, string> description,
            string term,
            int offset,
            int? limit)
            where T : class
        {
            term = term ?? string.Empty;

            var index = this.lotRepository.GetLotIndex(lotId).Index;
            var result = index.GetParts<T>(path).Select(pv => new PartSelectDO
                {
                    PartIndex = pv.Part.Index,
                    Description = description(pv.Content)
                })
                .Where(p => !string.IsNullOrEmpty(p.Description) && p.Description.Contains(term))
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

        [Route("getNewCase")]
        public IHttpActionResult GetNewCase(int lotId, int caseTypeId, int? appId = null, bool appOnly = false)
        {
            var caseType = this.caseTypeRepository.GetCaseType(caseTypeId);
            CaseDO caseDO = new CaseDO()
            {
                CaseType = new NomValue
                {
                    NomValueId = caseType.GvaCaseTypeId,
                    Name = caseType.Name,
                    Alias = caseType.Alias
                },
                IsAdded = true
            };

            if (!appOnly)
            {
                caseDO.BookPageNumber = this.fileRepository.GetNextBPN(lotId, caseTypeId).ToString();
            }

            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);

                caseDO.Applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            return Ok(caseDO);
        }
    }
}

