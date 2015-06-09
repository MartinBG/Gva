using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views.SModeCode;
using Gva.Api.ModelsDO.SModeCodes;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.SModeCode
{
    public class SModeCodeProjection : Projection<GvaViewSModeCode>
    {
        public SModeCodeProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "SModeCode")
        {
        }

        public override IEnumerable<GvaViewSModeCode> Execute(PartCollection parts)
        {
            var sModeCodeData = parts.Get<SModeCodeDO>("sModeCodeData");

            if (sModeCodeData == null)
            {
                return new GvaViewSModeCode[] { };
            }

            return new[] { this.Create(sModeCodeData) };
        }

        private GvaViewSModeCode Create(PartVersion<SModeCodeDO> sModeCodeData)
        {
            GvaViewSModeCode code = new GvaViewSModeCode();
            code.LotId = sModeCodeData.Part.Lot.LotId;
            code.TypeId = sModeCodeData.Content.Type.NomValueId;
            code.Note = sModeCodeData.Content.Note;
            code.CodeHex = sModeCodeData.Content.CodeHex;
            code.AircraftId = sModeCodeData.Content.AircraftId;
            code.TheirDate = sModeCodeData.Content.TheirDate;
            code.TheirNumber = sModeCodeData.Content.TheirNumber;
            code.CaaDate = sModeCodeData.Content.CaaDate;
            code.CaaNumber = sModeCodeData.Content.CaaNumber;
            code.Applicant = sModeCodeData.Content.ApplicantIsOrg ?
                (sModeCodeData.Content.ApplicantOrganization != null ? sModeCodeData.Content.ApplicantOrganization.Name : null) :
                (sModeCodeData.Content.ApplicantPerson != null ? sModeCodeData.Content.ApplicantPerson.Name : null);

            return code;
        }
    }
}
