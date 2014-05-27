using System;
using Common.Data;
using Common.Json;
using Mosv.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Mosv.Api.LotEventHandlers.AdmissionView
{
    public class AdmissionDataHandler : CommitEventHandler<MosvViewAdmission>
    {
        public AdmissionDataHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Admission",
                setPartAlias: "admissionData",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId)
        {
        }

        public override void Fill(MosvViewAdmission admission, PartVersion part)
        {
            admission.Lot = part.Part.Lot;

            admission.IncomingLot = part.Content.Get<string>("incomingLot");
            admission.IncomingDate = part.Content.Get<DateTime?>("incomingDate");
            admission.IncomingNumber = part.Content.Get<string>("incomingNumber");
            admission.Applicant = part.Content.Get<string>("applicant");
            admission.ApplicantType = part.Content.Get<string>("applicantType.name");
        }

        public override void Clear(MosvViewAdmission admission)
        {
            throw new NotSupportedException();
        }
    }
}
