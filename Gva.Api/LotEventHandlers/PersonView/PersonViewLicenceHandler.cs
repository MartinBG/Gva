using System;
using Common.Data;
using Common.Json;
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
                partMatcher: pv => pv.Content.Get("licenceType") != null,
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.PartId == pv.Part.PartId)
        {
        }

        public override void Fill(GvaViewPersonLicence licence, PartVersion part)
        {
            licence.LotId = part.Part.Lot.LotId;
            licence.Part = part.Part;
            licence.LicenceType = part.Content.Get<string>("licenceType.name");
        }

        public override void Clear(GvaViewPersonLicence licence)
        {
            throw new NotSupportedException();
        }
    }
}
