using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonRatingDO
    {
        public int NextIndex { get; set; }

        public NomValue PersonRatingLevel { get; set; }

        public NomValue RatingClass { get; set; }

        public NomValue RatingType { get; set; }

        public NomValue Authorization { get; set; }

        public NomValue LocationIndicator { get; set; }

        public string Sector { get; set; }

        public NomValue AircraftTypeGroup { get; set; }

        public NomValue AircraftTypeCategory { get; set; }

        public NomValue Caa { get; set; }
    }
}
