using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonRatingEditionDO
    {
        public PersonRatingEditionDO()
        {
            this.RatingSubClasses = new List<NomValue>();
            this.Limitations = new List<NomValue>();
        }

        public int? Index { get; set; }

        public int RatingPartIndex { get; set; }

        public List<NomValue> RatingSubClasses { get; set; }

        public List<NomValue> Limitations { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required.")]
        public DateTime? DocumentDateValidFrom { get; set; }

        [Required(ErrorMessage = "DocumentDateValidTo is required.")]
        public DateTime? DocumentDateValidTo { get; set; }

        public NomValue Inspector { get; set; }

        public string Notes { get; set; }

        public string NotesAlt { get; set; }
    }
}
