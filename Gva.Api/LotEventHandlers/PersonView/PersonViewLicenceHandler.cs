using System;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.PersonView
{
    public class PersonViewLicenceHandler : CommitEventHandler<GvaViewPersonLicence>
    {
        public PersonViewLicenceHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "personLicence",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.PartId == pv.Part.PartId)
        {
        }

        public override void Fill(GvaViewPersonLicence licence, PartVersion part)
        {
            licence.Lot = part.Part.Lot;
            licence.Part = part.Part;
            licence.LicenceType = part.DynamicContent.licenceType.name;
        }

        public override void Clear(GvaViewPersonLicence licence)
        {
            throw new NotSupportedException();
        }
    }
}
