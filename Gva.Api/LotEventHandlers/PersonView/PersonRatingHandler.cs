using System;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.PersonView
{
    public class PersonRatingHandler : CommitEventHandler<GvaViewPersonRating>
    {
        public PersonRatingHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Person",
                setPartAlias: "rating",
                partMatcher: pv => pv.DynamicContent.ratingType != null,
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId && v.PartId == pv.Part.PartId)
        {
        }

        public override void Fill(GvaViewPersonRating rating, PartVersion part)
        {
            rating.Lot = part.Part.Lot;
            rating.Part = part.Part;
            rating.RatingType = part.DynamicContent.ratingType.name;
        }

        public override void Clear(GvaViewPersonRating rating)
        {
            throw new NotSupportedException();
        }
    }
}
