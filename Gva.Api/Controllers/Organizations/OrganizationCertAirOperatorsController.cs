﻿using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationCertAirOperators")]
    [Authorize]
    public class OrganizationCertAirOperatorsController : GvaCaseTypesPartController<OrganizationCertAirOperatorDO>
    {
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;

        public OrganizationCertAirOperatorsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IOrganizationRepository organizationRepository,
            INomRepository nomRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            ICaseTypeRepository caseTypeRepository,
            UserContext userContext)
            : base("organizationCertAirOperators", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertAirOperator(int lotId)
        {
            List<CaseDO> cases = this.caseTypeRepository.GetCaseTypesForSet("organization")
                .Select(c => new CaseDO()
                {
                    CaseType = new NomValue()
                    {
                        NomValueId = c.GvaCaseTypeId,
                        Name = c.Name,
                        Alias = c.Alias
                    },
                    IsAdded = true
                })
                .ToList();

            OrganizationCertAirOperatorDO certificate = new OrganizationCertAirOperatorDO()
            {
                Valid = this.nomRepository.GetNomValue("boolean", "yes")
            };

            return Ok(new CaseTypesPartDO<OrganizationCertAirOperatorDO>(certificate, cases));
        }
    }
}