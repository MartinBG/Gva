using System;
using System.Collections.Generic;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationDocumentOthers")]
    [Authorize]
    public class OrganizationDocumentOthersController : GvaFilePartController<OrganizationDocumentOtherDO>
    {
        private IApplicationRepository applicationRepository;
        private ILotRepository lotRepository;

        public OrganizationDocumentOthersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("organizationDocumentOthers", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.applicationRepository = applicationRepository;
            this.lotRepository = lotRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewDocumentOther(int lotId, int? appId = null)
        {
            OrganizationDocumentOtherDO newDocumentOther = new OrganizationDocumentOtherDO()
            {
                DocumentDateValidFrom = DateTime.Now
            };

            var cases = new List<CaseDO>();
            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);
                cases.Add(new CaseDO()
                {
                    IsAdded = true,
                    Applications = new List<ApplicationNomDO>()
                    {
                        this.applicationRepository.GetInitApplication(appId)
                    }
                });
            }

            return Ok(new FilePartVersionDO<OrganizationDocumentOtherDO>(newDocumentOther, cases));
        }
    }
}