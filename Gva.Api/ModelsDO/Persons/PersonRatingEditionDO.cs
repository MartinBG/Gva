﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonRatingEditionDO
    {
        public PersonRatingEditionDO()
        {
            this.RatingSubClasses = new List<int>();
            this.Limitations = new List<int>();
        }

        public int Index { get; set; }

        public int? RatingPartIndex { get; set; }

        public List<int> RatingSubClasses { get; set; }

        public List<int> Limitations { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required.")]
        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public int? InspectorId { get; set; }

        public int? ExaminerId { get; set; }

        public string Notes { get; set; }

        public string NotesAlt { get; set; }
    }
}
