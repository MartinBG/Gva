using System;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.PersonView
{
    public class PersonDataHandler : CommitEventHandler<GvaViewPersonData>
    {
        public PersonDataHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "data",
                viewMatcher: pv =>
                    v => v.GvaPersonLotId == pv.Part.Lot.LotId)
        {
        }

        public override void Fill(GvaViewPersonData person, PartVersion part)
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

        public override void Clear(GvaViewPersonData person)
        {
            throw new NotSupportedException();
        }
    }
}
