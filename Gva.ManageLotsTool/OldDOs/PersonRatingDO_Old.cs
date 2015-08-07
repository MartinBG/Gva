using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonRatingDO_Old
    {
        public PersonRatingDO_Old()
        {
            RatingTypes = new List<NomValue>();
        }

        public int? NextIndex { get; set; }

        public NomValue PersonRatingLevel { get; set; }

        public NomValue RatingClass { get; set; }

        public List<NomValue> RatingTypes { get; set; }

        public NomValue Authorization { get; set; }

        public NomValue LocationIndicator { get; set; }

        public string Sector { get; set; }

        public NomValue AircraftTypeGroup { get; set; }

        public NomValue AircraftTypeCategory { get; set; }

        public NomValue Caa { get; set; }
    }
}
