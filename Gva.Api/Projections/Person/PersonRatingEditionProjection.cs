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
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Common.Api.Repositories.NomRepository;

namespace Gva.Api.Projections.Person
{
    public class PersonRatingEditionProjection : Projection<GvaViewPersonRatingEdition>
    {
        private INomRepository nomRepository;

        public PersonRatingEditionProjection(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository)
            : base(unitOfWork, "Person")
        {
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewPersonRatingEdition> Execute(PartCollection parts)
        {
            var editions = parts.GetAll<PersonRatingEditionDO>("ratingEditions");
            return editions.Select(a => this.Create(a));
        }

        private GvaViewPersonRatingEdition Create(PartVersion<PersonRatingEditionDO> licenceEdition)
        {
            GvaViewPersonRatingEdition edition = new GvaViewPersonRatingEdition();

            edition.LotId = licenceEdition.Part.Lot.LotId;
            edition.PartId = licenceEdition.Part.PartId;
            edition.PartIndex = licenceEdition.Part.Index;
            edition.RatingPartIndex = licenceEdition.Content.RatingPartIndex.Value;
            edition.RatingSubClasses = licenceEdition.Content.RatingSubClasses.Count > 0 ? string.Join(",", this.nomRepository.GetNomValues("ratingSubClasses", licenceEdition.Content.RatingSubClasses.ToArray()).Select(s => s.Code)) : string.Empty;
            edition.Limitations = licenceEdition.Content.Limitations.Count > 0 ? string.Join(GvaConstants.ConcatenatingExp, this.nomRepository.GetNomValues("limitations66", licenceEdition.Content.Limitations.ToArray()).Select(t => t.Code)) : null;

            if (licenceEdition.Content.DocumentDateValidTo.HasValue)
            {
                edition.DocDateValidTo = licenceEdition.Content.DocumentDateValidTo.Value;
            }

            edition.Notes = licenceEdition.Content.Notes;
            edition.NotesAlt = licenceEdition.Content.NotesAlt;
            edition.Index = licenceEdition.Content.Index;

            if (licenceEdition.Content.DocumentDateValidFrom.HasValue)
            {
                edition.DocDateValidFrom = licenceEdition.Content.DocumentDateValidFrom.Value;
            }

            return edition;
        }
    }
}
