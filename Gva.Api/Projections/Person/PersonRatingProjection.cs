using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Person;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Projections.Person
{
    public class PersonRatingProjection : Projection<GvaViewPersonRating>
    {
        private INomRepository nomRepository;

        public PersonRatingProjection(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository)
            : base(unitOfWork, "Person")
        {
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewPersonRating> Execute(PartCollection parts)
        {
            var ratings = parts.GetAll<PersonRatingDO>("ratings");
            var editions = parts.GetAll<PersonRatingEditionDO>("ratingEditions");

            return ratings.Select(r =>
            {
                var ratingEditions = editions.Where(e => e.Content.RatingPartIndex == r.Part.Index).ToArray();
                return this.Create(r, ratingEditions);
            });
        }

        private GvaViewPersonRating Create(PartVersion<PersonRatingDO> personRating, PartVersion<PersonRatingEditionDO>[] editions)
        {
            GvaViewPersonRating rating = new GvaViewPersonRating();

            var firstEdition = editions.Where(ed => ed.Content.Index == editions.Min(e => e.Content.Index)).SingleOrDefault();
            var lastEdition = editions.Where(ed => ed.Content.Index == editions.Max(e => e.Content.Index)).SingleOrDefault();

            rating.LotId = personRating.Part.Lot.LotId;
            rating.PartId = personRating.Part.PartId;
            rating.EditionIndex = lastEdition.Content.Index;
            rating.RatingPartIndex = personRating.Part.Index;
            rating.EditionPartIndex = lastEdition.Part.Index;
            rating.LocationIndicatorId = personRating.Content.LocationIndicator != null ? personRating.Content.LocationIndicator.NomValueId : (int?)null;
            rating.RatingTypeId = personRating.Content.RatingType != null ? personRating.Content.RatingType.NomValueId : (int?)null;
            rating.RatingLevelId = personRating.Content.PersonRatingLevel != null ?  personRating.Content.PersonRatingLevel.NomValueId : (int?)null;
            rating.RatingClassId = personRating.Content.RatingClass != null ? personRating.Content.RatingClass.NomValueId : (int?)null;
            rating.AircraftTypeGroupId = personRating.Content.AircraftTypeGroup != null ? personRating.Content.AircraftTypeGroup.NomValueId : (int?)null;
            rating.AuthorizationId = personRating.Content.Authorization != null ? personRating.Content.Authorization.NomValueId : (int?)null;
            string[] ratinSubClasses = lastEdition.Content.RatingSubClasses != null ? lastEdition.Content.RatingSubClasses.Select(t => t.Code).ToArray() : null;
            rating.RatingSubClasses = ratinSubClasses != null ? string.Join(", ", ratinSubClasses) : null;
            string[] limitations = lastEdition.Content.Limitations != null ? lastEdition.Content.Limitations.Select(t => t.Code).ToArray() : null;
            rating.Limitations = limitations != null ? string.Join(", ", limitations) : null;
            if (lastEdition.Content.DocumentDateValidFrom.HasValue)
            {
                rating.LastDocDateValidFrom = lastEdition.Content.DocumentDateValidFrom.Value;
            }
            rating.LastDocDateValidTo = lastEdition.Content.DocumentDateValidTo;
            if (firstEdition.Content.DocumentDateValidFrom.HasValue)
            {
                rating.FirstDocDateValidFrom = firstEdition.Content.DocumentDateValidFrom.Value;
            }
            rating.Notes = lastEdition.Content.Notes;
            rating.NotesAlt = lastEdition.Content.NotesAlt;

            return rating;
        }
    }
}
