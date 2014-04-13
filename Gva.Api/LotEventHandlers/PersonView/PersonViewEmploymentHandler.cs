using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.PersonView
{
    public class PersonViewEmploymentHandler : CommitEventHandler<GvaViewPerson>
    {
        public PersonViewEmploymentHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "personEmployment",
                partMatcher: pv => pv.Content.Get<string>("valid.code") == "Y",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId,
                isPrincipal: false)
        {
        }

        public override void Fill(GvaViewPerson person, PartVersion part)
        {
            person.Employment = part.Content.Get<string>("employmentCategory.name");
            person.Organization = part.Content.Get<string>("organization.name");
        }

        public override void Clear(GvaViewPerson person)
        {
            person.Employment = null;
            person.Organization = null;
        }
    }
}
