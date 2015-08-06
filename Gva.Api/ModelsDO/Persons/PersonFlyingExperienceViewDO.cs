using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Gva.Api.ModelsDO.Common;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonFlyingExperienceViewDO
    {
        public PersonFlyingExperienceViewDO()
        {
            RatingTypes = new List<NomValue>();
        }

        public int PartIndex { get; set; }

        public CaseDO Case { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string Notes { get; set; }

        public NomValue Organization { get; set; }

        public NomValue Aircraft { get; set; }

        public List<NomValue> RatingTypes { get; set; }

        public NomValue RatingClass { get; set; }

        public NomValue LicenceType { get; set; }

        public NomValue Authorization { get; set; }

        public NomValue LocationIndicator { get; set; }

        public string Sector { get; set; }

        public NomValue ExperienceRole { get; set; }

        public NomValue ExperienceMeasure { get; set; }

        public TimeDO DayIFR { get; set; }

        public TimeDO NightIFR { get; set; }

        public TimeDO DayVFR { get; set; }

        public TimeDO NightVFR { get; set; }

        public int? DayLandings { get; set; }

        public int? NightLandings { get; set; }

        public long? TotalDoc { get; set; }

        public int? TotalLastMonths { get; set; }

        public long? Total { get; set; }
    }
}
