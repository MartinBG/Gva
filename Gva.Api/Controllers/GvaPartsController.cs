﻿using System;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Gva.Api.ModelsDO;
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

        [Route("getNewCase")]
        public IHttpActionResult GetNewCase(int lotId, int caseTypeId, int? appId = null)
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

            if (appId.HasValue)
            {
                caseDO.Applications.Add(this.applicationRepository.GetNomApplication(appId.Value));
            }

            return Ok(caseDO);
        }
    }
}

