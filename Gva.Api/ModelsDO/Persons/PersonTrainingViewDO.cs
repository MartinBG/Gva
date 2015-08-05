using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonTrainingViewDO
    {
        public PersonTrainingViewDO()
        {
            RatingTypes = new List<NomValue>();
        }

        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public CaseDO Case { get; set; }

        public string DocumentNumber { get; set; }

        public int? DocumentPersonNumber { get; set; }

        public string DocumentPublisher { get; set; }

        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public List<NomValue> RatingTypes { get; set; }

        public NomValue AircraftTypeCategory { get; set; }

        public NomValue AircraftTypeGroup { get; set; }

        public NomValue LicenceType { get; set; }

        public NomValue LocationIndicator { get; set; }

        public string Sector { get; set; }

        public NomValue RatingClass { get; set; }

        public NomValue Authorization { get; set; }

        public NomValue DocumentType { get; set; }

        public NomValue DocumentRole { get; set; }

        public NomValue Valid { get; set; }

        public string Notes { get; set; }
    }
}
