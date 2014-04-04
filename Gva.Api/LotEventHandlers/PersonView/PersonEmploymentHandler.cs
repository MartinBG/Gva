using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.PersonView
{
    public class PersonEmploymentHandler : CommitEventHandler<GvaViewPerson>
    {
        public PersonEmploymentHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "employment",
                partMatcher: pv => pv.DynamicContent.valid.code == "Y",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId,
                isPrincipalHandler: false)
        {
        }

        public override void Fill(GvaViewPerson person, PartVersion part)
        {
            person.Employment = part.DynamicContent.employmentCategory.name;

            if (part.DynamicContent.organization != null)
            {
                person.Organization = part.DynamicContent.organization.name;
            }
        }

        public override void Clear(GvaViewPerson person)
        {
            person.Employment = null;
            person.Organization = null;
        }
    }
}
