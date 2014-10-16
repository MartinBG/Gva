using System.Collections.Generic;
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
    [RoutePrefix("api/organizations/{lotId}/organizationCertAirportOperators")]
    [Authorize]
    public class OrganizationCertAirportOperatorsController : GvaCaseTypesPartController<OrganizationCertAirportOperatorDO>
    {
        private INomRepository nomRepository;
        private ICaseTypeRepository caseTypeRepository;

        public OrganizationCertAirportOperatorsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IOrganizationRepository organizationRepository,
            INomRepository nomRepository,
            IFileRepository fileRepository,
            ILotEventDispatcher lotEventDispatcher,
            ICaseTypeRepository caseTypeRepository,
            UserContext userContext)
            : base("organizationCertAirportOperators", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.nomRepository = nomRepository;
        }

        [Route("new")]
        public IHttpActionResult GetNewCertAirportOperator(int lotId)
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

            OrganizationCertAirportOperatorDO certificate = new OrganizationCertAirportOperatorDO()
            {
                Valid = this.nomRepository.GetNomValue("boolean", "yes")
            };

            return Ok(new CaseTypesPartDO<OrganizationCertAirportOperatorDO>(certificate, cases));
        }
    }
}