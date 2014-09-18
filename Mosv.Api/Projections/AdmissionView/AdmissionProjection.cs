using System.Collections.Generic;
using Common.Data;
using Mosv.Api.Models;
using Mosv.Api.ModelsDO.Admission;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Mosv.Api.Projections.AdmissionView
{
    public class AdmissionProjection : Projection<MosvViewAdmission>
    {
        public AdmissionProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Admission")
        { }

        public override IEnumerable<MosvViewAdmission> Execute(PartCollection parts)
        {
            var admissionData = parts.Get<AdmissionDO>("admissionData");

            if (admissionData == null)
            {
                return new MosvViewAdmission[] { };
            }

            return new[] { this.Create(admissionData) };
        }

        private MosvViewAdmission Create(PartVersion<AdmissionDO> admissionData)
        {
            MosvViewAdmission admission = new MosvViewAdmission();

            admission.LotId = admissionData.Part.Lot.LotId;
            admission.IncomingLot = admissionData.Content.IncomingLot;
            admission.IncomingNumber = admissionData.Content.IncomingNumber;
            admission.IncomingDate = admissionData.Content.IncomingDate;
            admission.ApplicantType = admissionData.Content.ApplicantType == null ? null : admissionData.Content.ApplicantType.Name;
            admission.Applicant = admissionData.Content.Applicant;

            return admission;
        }
    }
}
