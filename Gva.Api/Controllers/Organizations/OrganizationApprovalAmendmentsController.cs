using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Filters;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers.Organizations
{
    [RoutePrefix("api/organizations/{lotId}/organizationApprovals/{approvalPartIndex}/approvalAmendments")]
    [Authorize]
    public class OrganizationApprovalAmendmentsController : GvaCaseTypePartController<OrganizationAmendmentDO>
    {
        private string path;
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IApplicationRepository applicationRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private INomRepository nomRepository;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;
        private UserContext userContext;

        public OrganizationApprovalAmendmentsController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IApplicationRepository applicationRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            INomRepository nomRepository,
            UserContext userContext)
            : base("approvalAmendments", unitOfWork, lotRepository, fileRepository, lotEventDispatcher, userContext)
        {
            this.path = "approvalAmendments";
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.applicationRepository = applicationRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.userContext = userContext;
        }

        [Route("~/api/organizations/{lotId}/approvalAmendments/new")]
        public IHttpActionResult GetNewApprovalAmendment(int lotId, int caseTypeId, int? appId = null, int? approvalPartIndex = null)
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
                BookPageNumber = this.fileRepository.GetNextBPN(lotId, caseType.GvaCaseTypeId).ToString()
            };

            if (appId.HasValue)
            {
                this.lotRepository.GetLotIndex(lotId);

                caseDO.Applications.Add(this.applicationRepository.GetInitApplication(appId));
            }

            OrganizationAmendmentDO newApprovalAmendment = new OrganizationAmendmentDO()
            {
                DocumentDateIssue = DateTime.Now,
                ApprovalPartIndex = approvalPartIndex.HasValue ? approvalPartIndex.Value : 0,
            };

            return Ok(new CaseTypePartDO<OrganizationAmendmentDO>(newApprovalAmendment, caseDO));
        }

        [Route("")]
        public IHttpActionResult GetParts(int lotId, int approvalPartIndex, int? caseTypeId = null)
        {
            var amendmentsPartVersions = this.lotRepository.GetLotIndex(lotId).Index.GetParts<OrganizationAmendmentDO>("approvalAmendments")
                .Where(epv => epv.Content.ApprovalPartIndex == approvalPartIndex)
                .OrderBy(epv => epv.Content.Index);

            List<CaseTypePartDO<OrganizationAmendmentDO>> partVersionDOs = new List<CaseTypePartDO<OrganizationAmendmentDO>>();
            foreach (var amendmentsPartVersion in amendmentsPartVersions)
            {
                var lotFile = this.fileRepository.GetFileReference(amendmentsPartVersion.PartId, caseTypeId);
                if (!caseTypeId.HasValue || lotFile != null)
                {
                    partVersionDOs.Add(new CaseTypePartDO<OrganizationAmendmentDO>(amendmentsPartVersion, lotFile));
                }
            }

            return Ok(partVersionDOs);
        }

        [Route("")]
        [Validate]
        public IHttpActionResult PostNewPart(int lotId, int approvalPartIndex, CaseTypePartDO<OrganizationAmendmentDO> amendment)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var amendmentsPartVersions = lot.Index.GetParts<OrganizationAmendmentDO>("approvalAmendments").Where(epv => epv.Content.ApprovalPartIndex == approvalPartIndex);
                var nextIndex = amendmentsPartVersions.Select(e => e.Content.Index).Max() + 1;
                amendment.Part.Index = nextIndex;

                var partVersion = lot.CreatePart(this.path + "/*", amendment.Part, this.userContext);

                this.fileRepository.AddFileReference(partVersion.Part, amendment.Case);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();

                return Ok(new CaseTypePartDO<OrganizationAmendmentDO>(partVersion));
            }
        }

        [Route("{partIndex}")]
        [Validate]
        public IHttpActionResult PostPart(int lotId, int approvalPartIndex, int partIndex, CaseTypePartDO<OrganizationAmendmentDO> amendment, int? caseTypeId = null)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);

                var partVersion = lot.UpdatePart(string.Format("{0}/{1}", this.path, partIndex), amendment.Part, this.userContext);

                this.fileRepository.AddFileReference(partVersion.Part, amendment.Case);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(partVersion.PartId);

                transaction.Commit();
                var lotFile = this.fileRepository.GetFileReference(partVersion.PartId, caseTypeId);

                return Ok(new CaseTypePartDO<OrganizationAmendmentDO>(partVersion, lotFile));
            }
        }

        [Route("{partIndex}")]
        public IHttpActionResult DeletePart(int lotId, int approvalPartIndex, int partIndex)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                var lot = this.lotRepository.GetLotIndex(lotId);
                PartVersion approvalPartVersion = null;
                var amendmentPartVersion = lot.DeletePart<OrganizationAmendmentDO>(string.Format("{0}/{1}", this.path, partIndex), this.userContext);
                var amendmentsPartVersions = lot.Index.GetParts<OrganizationAmendmentDO>("approvalAmendments")
                    .Where(epv => epv.Content.ApprovalPartIndex == approvalPartIndex);

                if (amendmentsPartVersions.Count() == 0)
                {
                    approvalPartVersion = lot.DeletePart<OrganizationApprovalDO>(string.Format("{0}/{1}", "approvals", approvalPartIndex), this.userContext);
                }

                this.applicationRepository.DeleteApplicationRefs(amendmentPartVersion.Part);

                lot.Commit(this.userContext, this.lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(amendmentPartVersion.PartId);
                if (approvalPartVersion != null)
                {
                    this.lotRepository.ExecSpSetLotPartTokens(approvalPartVersion.PartId);
                }

                transaction.Commit();

                return Ok();
            }
        }

    }
}