using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views.Organization;
using Gva.Api.ModelsDO.Organizations;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Organization
{
    public class OrganizationExaminerProjection : Projection<GvaViewOrganizationExaminer>
    {
        public OrganizationExaminerProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Organization")
        {
        }

        public override IEnumerable<GvaViewOrganizationExaminer> Execute(PartCollection parts)
        {
            var staffExaminers = parts.GetAll<OrganizationStaffExaminerDO>("organizationStaffExaminer");

            return staffExaminers.Select(se => this.Create(se));
        }

        private GvaViewOrganizationExaminer Create(PartVersion<OrganizationStaffExaminerDO> part)
        {
            GvaViewOrganizationExaminer examiner = new GvaViewOrganizationExaminer();

            examiner.LotId = part.Part.Lot.LotId;
            examiner.PartIndex = part.Part.Index;
            examiner.PersonId = part.Content.Person.NomValueId;
            examiner.ExaminerCode = part.Content.ExaminerCode;
            examiner.StampNum = part.Content.StampNum;
            examiner.PermitedAW = part.Content.PermitedAW.Code == "Y";
            examiner.PermitedCheck = part.Content.PermitedCheck.Code == "Y";
            examiner.Valid = part.Content.Valid.Code == "Y";

            return examiner;
        }
    }
}
