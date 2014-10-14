using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Common.Api.Repositories.NomRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Models.Views.Organization;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.Repositories.ApplicationRepository;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Projections.Organization
{
    public class OrganizationAmendmentProjection : Projection<GvaViewOrganizationAmendment>
    {
        private IFileRepository fileRepository;
        private IApplicationRepository applicationRepository;
        public OrganizationAmendmentProjection(
            IUnitOfWork unitOfWork,
            IFileRepository fileRepository,
            IApplicationRepository applicationRepository)
            : base(unitOfWork, "Organization")
        {
            this.applicationRepository = applicationRepository;
            this.fileRepository = fileRepository;
        }

        public override IEnumerable<GvaViewOrganizationAmendment> Execute(PartCollection parts)
        {
            var amendments = parts.GetAll<OrganizationAmendmentDO>("approvalAmendments");

            return amendments.Select(a => this.Create(a));
        }

        private GvaViewOrganizationAmendment Create(PartVersion<OrganizationAmendmentDO> organizationAmendment)
        {
            GvaViewOrganizationAmendment amendment = new GvaViewOrganizationAmendment();
            amendment.LotId = organizationAmendment.Part.Lot.LotId;
            amendment.PartIndex = organizationAmendment.Part.Index;
            amendment.DocumentNumber = organizationAmendment.Content.DocumentNumber;
            amendment.ChangeNum = organizationAmendment.Content.ChangeNum;
            amendment.DocumentDateIssue = organizationAmendment.Content.DocumentDateIssue;
            amendment.ApprovalPartIndex = organizationAmendment.Content.ApprovalPartIndex;

            GvaLotFile file = this.fileRepository.GetFileReference(organizationAmendment.PartId, null);
            if (file != null && file.GvaAppLotFiles.Count > 0) 
            {
                GvaApplication application = this.applicationRepository.GetNomApplication(file.GvaAppLotFiles.First().GvaApplicationId);
                amendment.ApplicationName = new ApplicationNomDO(application).ApplicationName;
            }

            return amendment;
        }
    }
}
