using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.Models.Views.SModeCode;
using Gva.Api.ModelsDO.SModeCodes;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Projections.SModeCode
{
    public class SModeCodeProjection : Projection<GvaViewSModeCode>
    {
        private IUnitOfWork unitOfWork;

        public SModeCodeProjection(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository)
            : base(unitOfWork, "SModeCode")
        {
            this.unitOfWork = unitOfWork;
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
            code.Description = sModeCodeData.Content.Description;
            code.Identifier = sModeCodeData.Content.Identifier;
            code.RegMark = sModeCodeData.Content.RegMark;
            code.CodeHex = sModeCodeData.Content.CodeHex;
            code.AircraftId = sModeCodeData.Content.AircraftId;
            code.TheirDate = sModeCodeData.Content.TheirDate;
            code.TheirNumber = sModeCodeData.Content.TheirNumber;
            code.CaaDate = sModeCodeData.Content.CaaDate;
            code.CaaNumber = sModeCodeData.Content.CaaNumber;
            code.Applicant = sModeCodeData.Content.ApplicantIsOrg ?
                (sModeCodeData.Content.ApplicantOrganization != null ? sModeCodeData.Content.ApplicantOrganization.Name : null) :
                (sModeCodeData.Content.ApplicantPerson != null ? sModeCodeData.Content.ApplicantPerson.Name : null);

            if (sModeCodeData.Content.AircraftId.HasValue)
            {
                GvaViewAircraftRegistration registration = this.unitOfWork.DbContext.Set<GvaViewAircraftRegistration>()
                    .Where(a => a.LotId == sModeCodeData.Content.AircraftId.Value && a.RegMark == sModeCodeData.Content.RegMark)
                    .OrderByDescending(s => s.CertDate)
                    .FirstOrDefault();

                if (registration != null)
                {
                    code.RegistrationPartIndex = registration.PartIndex;
                }
            }

            return code;
        }
    }
}
