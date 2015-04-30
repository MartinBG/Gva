using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons.Reports
{
    public class PersonReportRatingDO
    {
        public int LotId { get; set; }

        public int? Lin { get; set; }

        public string RatingSubClasses { get; set; }

        public string Limitations { get; set; }

        public string RatingTypes { get; set; }

        public DateTime? FirstIssueDate { get; set; }

        public DateTime? DateValidFrom { get; set; }

        public DateTime? DateValidTo { get; set; }

        public string Sector { get; set; }

        public string RatingClass { get; set; }

        public string AuthorizationCode { get; set; }

        public string AircraftTypeCategory { get; set; }

        public string AircraftTypeGroup { get; set; }

        public string LocationIndicator { get; set; }

        public string RatingLevel { get; set; }
    }
}
