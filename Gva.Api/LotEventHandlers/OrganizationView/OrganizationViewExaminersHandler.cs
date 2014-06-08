using System;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.PersonView
{
    public class OrganizationViewExaminersHandler : CommitEventHandler<GvaViewOrganizationExaminer>
    {
        public OrganizationViewExaminersHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Organization",
                setPartAlias: "organizationStaffExaminer",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.PartId == pv.Part.PartId)
        {
        }

        public override void Fill(GvaViewOrganizationExaminer examiner, PartVersion part)
        {
            examiner.Lot = part.Part.Lot;
            examiner.Part = part.Part;
            examiner.PersonLotId = part.Content.Get<int>("person.nomValueId");
            examiner.ExaminerCode = part.Content.Get<string>("examinerCode");
            examiner.StampNum = part.Content.Get<string>("stampNum");
            examiner.PermitedAW = part.Content.Get<string>("permitedAW.code") == "Y";
            examiner.PermitedCheck = part.Content.Get<string>("permitedCheck.code") == "Y";
            examiner.IsValid = part.Content.Get<string>("valid.code") == "Y";
        }

        public override void Clear(GvaViewOrganizationExaminer examiner)
        {
            throw new NotSupportedException();
        }
    }
}
