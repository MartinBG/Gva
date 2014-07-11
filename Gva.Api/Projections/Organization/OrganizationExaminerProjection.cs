using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Organization;
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
            var staffExaminers = parts.GetAll("organizationStaffExaminer");

            return staffExaminers.Select(se => this.Create(se));
        }

        private GvaViewOrganizationExaminer Create(PartVersion part)
        {
            GvaViewOrganizationExaminer examiner = new GvaViewOrganizationExaminer();

            examiner.LotId = part.Part.Lot.LotId;
            examiner.PartIndex = part.Part.Index;
            examiner.PersonId = part.Content.Get<int>("person.nomValueId");
            examiner.ExaminerCode = part.Content.Get<string>("examinerCode");
            examiner.StampNum = part.Content.Get<string>("stampNum");
            examiner.PermitedAW = part.Content.Get<string>("permitedAW.code") == "Y";
            examiner.PermitedCheck = part.Content.Get<string>("permitedCheck.code") == "Y";
            examiner.Valid = part.Content.Get<string>("valid.code") == "Y";

            return examiner;
        }
    }
}
