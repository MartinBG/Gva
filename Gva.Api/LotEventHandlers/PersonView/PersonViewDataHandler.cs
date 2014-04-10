using System;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.PersonView
{
    public class PersonViewDataHandler : CommitEventHandler<GvaViewPerson>
    {
        public PersonViewDataHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "personData",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId)
        {
        }

        public override void Fill(GvaViewPerson person, PartVersion part)
        {
            person.Lot = part.Part.Lot;

            person.Lin =part.Content.Get<string>("lin");
            person.Uin = part.Content.Get<string>("uin");
            person.Names = string.Format(
                "{0} {1} {2}",
                part.Content.Get<string>("firstName"),
                part.Content.Get<string>("middleName"),
                part.Content.Get<string>("lastName"));
            person.BirtDate = part.Content.Get<DateTime>("dateOfBirth");
        }

        public override void Clear(GvaViewPerson person)
        {
            throw new NotSupportedException();
        }
    }
}
