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
        public GvaViewPersonRatingDO(
            GvaViewPersonRating rating,
            GvaViewPersonRatingEdition firstEdition,
            GvaViewPersonRatingEdition currentEdition)
        {
            this.LotId = rating.LotId;
            this.EditionIndex = currentEdition.Index;
            this.RatingPartIndex = rating.PartIndex;
            this.EditionPartIndex = currentEdition.PartIndex;
            this.RatingTypes = rating.RatingTypes;
            this.PersonRatingLevel = rating.RatingLevel;
            this.RatingClass = rating.RatingClass;
            this.AircraftTypeGroup = rating.AircraftTypeGroup;
            this.Authorization = rating.Authorization;
            this.AircraftTypeCategory = rating.AircraftTypeCategory;
            this.RatingSubClasses = currentEdition.RatingSubClasses;
            this.Limitations = currentEdition.Limitations;
            this.LastDocDateValidFrom = currentEdition.DocDateValidFrom;
            this.LastDocDateValidTo = currentEdition.DocDateValidTo;
            this.FirstDocDateValidFrom = firstEdition.DocDateValidFrom;
            this.Notes = currentEdition.Notes;
            this.NotesAlt = currentEdition.NotesAlt;
            this.LocationIndicator = rating.LocationIndicator;
            this.Sector = rating.Sector;
        }

        public int LotId { get; set; }

        public int EditionIndex { get; set; }

        public int RatingPartIndex { get; set; }

        public int EditionPartIndex { get; set; }

        public NomValue LocationIndicator { get; set; }

        public string Sector { get; set; }

        public string RatingTypes { get; set; }

        public NomValue PersonRatingLevel { get; set; }

        public NomValue RatingClass { get; set; }

        public NomValue AircraftTypeCategory { get; set; }

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
