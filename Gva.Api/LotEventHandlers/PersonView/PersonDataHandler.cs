using System;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.PersonView
{
    public class PersonDataHandler : CommitEventHandler<GvaViewPerson>
    {
        public PersonDataHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "data",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId)
        {
        }

        public override void Fill(GvaViewPerson person, PartVersion part)
        {
            person.Lot = part.Part.Lot;

            person.Lin =part.DynamicContent.lin;
            person.Uin = part.DynamicContent.uin;
            person.Names = string.Format(
                "{0} {1} {2}",
                part.DynamicContent.firstName,
                part.DynamicContent.middleName,
                part.DynamicContent.lastName);
            person.BirtDate = Convert.ToDateTime(part.DynamicContent.dateOfBirth);
        }

        public override void Clear(GvaViewPerson person)
        {
            throw new NotSupportedException();
        }
    }
}
