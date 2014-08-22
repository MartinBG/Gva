﻿using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonRatingDO
    {
        public int NextIndex { get; set; }

        [Required(ErrorMessage = "StaffType is required.")]
        public NomValue StaffType { get; set; }

        public NomValue PersonRatingLevel { get; set; }

        public NomValue RatingClass { get; set; }

        public NomValue RatingType { get; set; }

        public NomValue Authorization { get; set; }

        public NomValue LocationIndicator { get; set; }

        public string Sector { get; set; }

        [Required(ErrorMessage = "PersonRatingModel is required.")]
        public NomValue PersonRatingModel { get; set; }

        public NomValue AircraftTypeGroup { get; set; }

        public NomValue AircraftTypeCategory { get; set; }

        public NomValue Caa { get; set; }

        public PersonRatingEditionDO[] Editions { get; set; }
    }
}