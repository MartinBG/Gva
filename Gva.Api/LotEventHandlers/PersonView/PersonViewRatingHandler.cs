using System;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.PersonView
{
    public class PersonViewRatingHandler : CommitEventHandler<GvaViewPersonRating>
    {
        public PersonViewRatingHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "personRating",
                partMatcher: pv => pv.Content.Get("ratingType") != null,
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.PartId == pv.Part.PartId)
        {
        }

        public override void Fill(GvaViewPersonRating rating, PartVersion part)
        {
            rating.LotId = part.Part.Lot.LotId;
            rating.Part = part.Part;
            rating.RatingType = part.Content.Get<string>("ratingType.name");
        }

        public override void Clear(GvaViewPersonRating rating)
        {
            throw new NotSupportedException();
        }
    }
}
