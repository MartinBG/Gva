using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class GvaViewPersonRatingDO
    {
        public GvaViewPersonRatingDO(GvaViewPersonRating rating, List<GvaViewPersonRatingEdition> editions)
        {
            GvaViewPersonRatingEdition lastEdition = editions.Last();
            GvaViewPersonRatingEdition firstEdition = editions.First();

            this.LotId = rating.LotId;
            this.PartIndex = rating.Part.Index;
            this.EditionIndex = lastEdition.Index;
            this.RatingPartIndex = rating.PartIndex;
            this.EditionPartIndex = lastEdition.PartIndex;
            this.RatingType = rating.RatingType;
            this.PersonRatingLevel = rating.RatingLevel;
            this.RatingClass = rating.RatingClass;
            this.AircraftTypeGroup = rating.AircraftTypeGroup;
            this.Authorization = rating.Authorization;
            this.RatingSubClasses = lastEdition.RatingSubClasses;
            this.Limitations = lastEdition.Limitations;
            this.LastDocDateValidFrom = lastEdition.DocDateValidFrom;
            this.LastDocDateValidTo = lastEdition.DocDateValidTo;
            this.FirstDocDateValidFrom = firstEdition.DocDateValidFrom;
            this.Notes = lastEdition.Notes;
            this.NotesAlt = lastEdition.NotesAlt;
            this.LocationIndicator = rating.LocationIndicator;
        }

        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public int EditionIndex { get; set; }

        public int RatingPartIndex { get; set; }

        public int EditionPartIndex { get; set; }

        public NomValue LocationIndicator { get; set; }

        public NomValue RatingType { get; set; }

        public NomValue PersonRatingLevel { get; set; }

        public NomValue RatingClass { get; set; }

        public NomValue AircraftTypeGroup { get; set; }

        public NomValue Authorization { get; set; }

        public string RatingSubClasses { get; set; }

        public string Limitations { get; set; }

        public DateTime LastDocDateValidFrom { get; set; }

        public DateTime? LastDocDateValidTo { get; set; }

        public DateTime FirstDocDateValidFrom { get; set; }

        public string Notes { get; set; }

        public string NotesAlt { get; set; }
    }
}
