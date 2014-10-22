using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonFlyingExperienceDO
    {
        [Required(ErrorMessage = "DocumentDate is required.")]
        public DateTime? DocumentDate { get; set; }

        public PeriodDO Period { get; set; }

        public string Notes { get; set; }

        public NomValue Organization { get; set; }

        public NomValue Aircraft { get; set; }

        public NomValue RatingType { get; set; }

        public NomValue RatingClass { get; set; }

        public NomValue LicenceType { get; set; }

        public NomValue Authorization { get; set; }

        public NomValue LocationIndicator { get; set; }

        public string Sector { get; set; }

        [Required(ErrorMessage = "ExperienceRole is required.")]
        public NomValue ExperienceRole { get; set; }

        [Required(ErrorMessage = "ExperienceMeasure is required.")]
        public NomValue ExperienceMeasure { get; set; }

        public TimeDO DayIFR { get; set; }

        public TimeDO NightIFR { get; set; }

        public TimeDO DayVFR { get; set; }

        public TimeDO NightVFR { get; set; }

        public int? DayLandings { get; set; }

        public int? NightLandings { get; set; }

        [Required(ErrorMessage = "TotalDoc is required.")]
        public int? TotalDoc { get; set; }

        public int? TotalLastMonths { get; set; }

        public int? Total { get; set; }
    }
}
