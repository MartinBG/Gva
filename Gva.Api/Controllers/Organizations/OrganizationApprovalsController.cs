using System;
using System.Linq;
using System.Web.Http;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationApprovals")]
    [Authorize]
    public class OrganizationApprovalsController : GvaCaseTypePartController<OrganizationApprovalDO>
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IFileRepository fileRepository;
        private IOrganizationRepository organizationRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

        public OrganizationApprovalsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IFileRepository fileRepository,
            IOrganizationRepository organizationRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
            : base("approvals", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.fileRepository = fileRepository;
            this.organizationRepository = organizationRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
        }

        [Route("new")]
        public IHttpActionResult GetNewApproval(int lotId)
        {
            OrganizationApprovalDO approval = new OrganizationApprovalDO();

            return Ok(new CaseTypePartDO<OrganizationApprovalDO>(approval));
        }

        [NonAction]
        public override IHttpActionResult PostNewPart(int lotId, CaseTypePartDO<OrganizationApprovalDO> partVersionDO)
        {
            throw new NotSupportedException();
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostNewPart(int lotId, OrganizationApprovalNewDO newApproval)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var approvalPartVersion = lot.CreatePart("approvals/*", newApproval.Approval.Part, this.userContext);
                this.fileRepository.AddFileReference(approvalPartVersion.Part, newApproval.Approval.Case);

                newApproval.Amendment.Part.ApprovalPartIndex = approvalPartVersion.Part.Index;
                var amendmentPartVersion = lot.CreatePart("approvalAmendments/*", newApproval.Amendment.Part, this.userContext);
                this.fileRepository.AddFileReference(amendmentPartVersion.Part, newApproval.Amendment.Case);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(approvalPartVersion.PartId);
                this.lotRepository.ExecSpSetLotPartTokens(amendmentPartVersion.PartId);

                transaction.Commit();

                return Ok(new OrganizationApprovalNewDO()
                {
                    Approval = new CaseTypePartDO<OrganizationApprovalDO>(approvalPartVersion),
                    Amendment = new CaseTypePartDO<OrganizationAmendmentDO>(amendmentPartVersion)
                });
            }
        }

        public override IHttpActionResult GetParts(int lotId, int? caseTypeId = null)
        {
            var approvals = this.organizationRepository.GetApprovals(lotId, caseTypeId);

            return Ok(approvals.Select(approval =>  {

                var lastAmendment = approval.Amendments.OrderByDescending(am => am.Index).First();
                var lotFile = this.fileRepository.GetFileReference(lastAmendment.PartId, caseTypeId);

                ApplicationNomDO application = null;
                if (lotFile != null)
                {
                    var applications = new CaseDO(lotFile).Applications;
                    if (applications.Count > 0)
                    {
                        application = applications.First();
                    }
                }

                return new GvaViewOrganizationApprovalDO(approval, application);
            }));
        }

        [Route("{approvalPartIndex}/lastAmendmentIndex")]
        public IHttpActionResult GetLastApprovalAmendmentIndex(int lotId, int approvalPartIndex)
        {
            var lastApprovalAmendmentIndex = this.organizationRepository.GetLastApprovalAmendmentIndex(lotId, approvalPartIndex);

            return Ok(new { LastIndex = lastApprovalAmendmentIndex });
        }
    }
}