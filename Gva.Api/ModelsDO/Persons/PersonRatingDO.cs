using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonRatingDO
    {
        public PersonRatingDO()
        {
            RatingTypes = new List<int>();
        }

        public int? NextIndex { get; set; }

        public int? PersonRatingLevelId { get; set; }

        public int? RatingClassId { get; set; }

        public List<int> RatingTypes { get; set; }

        public int? AuthorizationId { get; set; }

        public int? LocationIndicatorId { get; set; }

        public string Sector { get; set; }

        public int? AircraftTypeGroupId { get; set; }

        public int? AircraftTypeCategoryId { get; set; }

        public int? CaaId { get; set; }
    }
}
