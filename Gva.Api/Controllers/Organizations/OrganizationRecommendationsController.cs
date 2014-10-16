using System.Collections.Generic;
using System.Web.Http;
using System.Linq;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.Models.Views.Organization;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.Repositories.CaseTypeRepository;
using Common.Api.Models;
using Gva.Api.Repositories.FileRepository;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationRecommendations")]
    [Authorize]
    public class OrganizationRecommendationsController : GvaCaseTypesPartController<OrganizationRecommendationDO>
    {
        private ILotRepository lotRepository;
        private IOrganizationRepository organizationRepository;
        private IUnitOfWork unitOfWork;
        private ICaseTypeRepository caseTypeRepository;
        private IFileRepository fileRepository;

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
            this.lotRepository = lotRepository;
            this.unitOfWork = unitOfWork;
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