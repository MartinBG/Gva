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

namespace Gva.Api.Projections.Person
{
    public class PersonRatingEditionProjection : Projection<GvaViewPersonRatingEdition>
    {
        public PersonRatingEditionProjection(
            IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
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
            string[] ratinSubClasses = licenceEdition.Content.RatingSubClasses != null ? licenceEdition.Content.RatingSubClasses.Select(t => t.Code).ToArray() : null;
            edition.RatingSubClasses = ratinSubClasses != null ? string.Join(", ", ratinSubClasses) : null;
            string[] limitations = licenceEdition.Content.Limitations != null ? licenceEdition.Content.Limitations.Select(t => t.Code).ToArray() : null;
            edition.Limitations = limitations != null ? string.Join(", ", limitations) : null;
            edition.DocDateValidTo = licenceEdition.Content.DocumentDateValidTo;
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
