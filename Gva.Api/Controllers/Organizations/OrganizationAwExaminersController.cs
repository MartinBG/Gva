using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationAwExaminers")]
    [Authorize]
    public class OrganizationAwExaminersController : GvaCaseTypesPartController<OrganizationAwExaminerDO>
    {
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;

        public OrganizationAwExaminersController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            INomRepository nomRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            ICaseTypeRepository caseTypeRepository,
            UserContext userContext)
            : base("organizationAwExaminers", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
            this.caseTypeRepository = caseTypeRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewAwExaminer(int lotId)
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

            OrganizationAwExaminerDO newAwExaminer = new OrganizationAwExaminerDO()
            {
                Valid = this.nomRepository.GetNomValue("boolean", "yes")
            };

            return Ok(new CaseTypesPartDO<OrganizationAwExaminerDO>(newAwExaminer, cases));
        }
    }
}