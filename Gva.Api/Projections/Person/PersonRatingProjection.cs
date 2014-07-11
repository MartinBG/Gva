using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Person;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonRatingProjection : Projection<GvaViewPersonRating>
    {
        public PersonRatingProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
        }

        public override IEnumerable<GvaViewPersonRating> Execute(PartCollection parts)
        {
            var ratings = parts.GetAll("ratings")
                .Where(pv => pv.Content.Get("ratingType") != null);

            return ratings.Select(r => this.Create(r));
        }

        public GvaViewPersonRating Create(PartVersion personRating)
        {
            GvaViewPersonRating rating = new GvaViewPersonRating();

            rating.LotId = personRating.Part.Lot.LotId;
            rating.PartIndex = personRating.Part.Index;
            rating.RatingTypeId = personRating.Content.Get<int>("ratingType.nomValueId");

            return rating;
        }
    }
}
