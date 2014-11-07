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
    public class GvaViewPersonRatingEditionDO
    {
        public GvaViewPersonRatingEditionDO(GvaViewPersonRating rating, GvaViewPersonRatingEdition edition)
        {
            this.LotId = rating.LotId;
            this.EditionIndex = edition.Index;
            this.RatingPartIndex = rating.PartIndex;
            this.EditionPartIndex = edition.PartIndex;
            this.RatingType = rating.RatingType;
            this.PersonRatingLevel = rating.RatingLevel;
            this.RatingClass = rating.RatingClass;
            this.AircraftTypeGroup = rating.AircraftTypeGroup;
            this.Authorization = rating.Authorization;
            this.RatingSubClasses = edition.RatingSubClasses;
            this.Limitations = edition.Limitations;
            this.DocDateValidFrom = edition.DocDateValidFrom;
            this.DocDateValidTo = edition.DocDateValidTo;
            this.LocationIndicator = rating.LocationIndicator;
        }

        public int LotId { get; set; }

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

        public DateTime DocDateValidFrom { get; set; }

        public DateTime DocDateValidTo { get; set; }
    }
}
