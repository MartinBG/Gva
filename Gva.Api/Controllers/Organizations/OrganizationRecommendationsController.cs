using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationRecommendations")]
    [Authorize]
    public class OrganizationRecommendationsController : GvaCaseTypesPartController<OrganizationRecommendationDO>
    {
        private IOrganizationRepository organizationRepository;
        private ICaseTypeRepository caseTypeRepository;

        public OrganizationRecommendationsController(
            IUnitOfWork unitOfWork,
            IOrganizationRepository organizationRepository,
            ILotRepository lotRepository,
            ILotEventDispatcher lotEventDispatcher,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            UserContext userContext)
            : base("organizationRecommendations", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.organizationRepository = organizationRepository;
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewRecommendation(int lotId)
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

            OrganizationRecommendationDO newRecommendation = new OrganizationRecommendationDO();

            return Ok(new CaseTypesPartDO<OrganizationRecommendationDO>(newRecommendation, cases));
        }

        [Route("views")]
        public IHttpActionResult GetRecommendations(int lotId)
        {
            return Ok(this.organizationRepository.GetRecommendations(lotId));
        }
    }
}