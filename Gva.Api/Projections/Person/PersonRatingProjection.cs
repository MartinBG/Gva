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
            return ratings.Select(r => this.Create(r));
        }

        private GvaViewPersonRating Create(PartVersion<PersonRatingDO> personRating)
        {
            GvaViewPersonRating rating = new GvaViewPersonRating();

            rating.LotId = personRating.Part.Lot.LotId;
            rating.PartId = personRating.Part.PartId;
            rating.PartIndex = personRating.Part.Index;
            rating.LocationIndicatorId = personRating.Content.LocationIndicatorId;
            rating.RatingTypes = personRating.Content.RatingTypes.Count() > 0 ? string.Join(", ", this.nomRepository.GetNomValues("ratingTypes", personRating.Content.RatingTypes.ToArray()).Select(t => t.Code)) : null;
            rating.RatingLevelId = personRating.Content.PersonRatingLevelId;
            rating.RatingClassId = personRating.Content.RatingClassId;
            rating.AircraftTypeGroupId = personRating.Content.AircraftTypeGroupId;
            rating.AuthorizationId = personRating.Content.AuthorizationId;
            rating.Sector = personRating.Content.Sector;
            rating.CaaId = personRating.Content.CaaId;
            rating.AircraftTypeCategoryId = personRating.Content.AircraftTypeCategoryId;

            return rating;
        }
    }
}
