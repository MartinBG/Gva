using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonFlyingExperienceDO
    {
        public PersonFlyingExperienceDO()
        {
            RatingTypes = new List<int>();
        }

        [Required(ErrorMessage = "DocumentDate is required.")]
        public DateTime? DocumentDate { get; set; }

        public string Notes { get; set; }

        public int? OrganizationId { get; set; }

        public int? AircraftId { get; set; }

        public List<int> RatingTypes { get; set; }

        public int? RatingClassId { get; set; }

        public int? LicenceTypeId { get; set; }

        public int? AuthorizationId { get; set; }

        public int? LocationIndicatorId { get; set; }

        public string Sector { get; set; }

        [Required(ErrorMessage = "ExperienceRole is required.")]
        public int? ExperienceRoleId { get; set; }

        [Required(ErrorMessage = "ExperienceMeasure is required.")]
        public int?  ExperienceMeasureId { get; set; }

        public TimeDO DayIFR { get; set; }

        public TimeDO NightIFR { get; set; }

        public TimeDO DayVFR { get; set; }

        public TimeDO NightVFR { get; set; }

        public int? DayLandings { get; set; }

        public int? NightLandings { get; set; }

        [Required(ErrorMessage = "TotalDoc is required.")]
        public long? TotalDoc { get; set; }

        public int? TotalLastMonths { get; set; }

        public long? Total { get; set; }
    }
}
