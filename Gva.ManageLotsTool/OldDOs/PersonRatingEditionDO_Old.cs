using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.ManageLotsTool.OldDOs
{
    public class PersonRatingEditionDO_Old
    {
        public PersonRatingEditionDO_Old()
        {
            this.RatingSubClasses = new List<NomValue>();
            this.Limitations = new List<NomValue>();
        }

        public int Index { get; set; }

        public int? RatingPartIndex { get; set; }

        public List<NomValue> RatingSubClasses { get; set; }

        public List<NomValue> Limitations { get; set; }

        [Required(ErrorMessage = "DocumentDateValidFrom is required.")]
        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public NomValue Inspector { get; set; }

        public NomValue Examiner { get; set; }

        public string Notes { get; set; }

        public string NotesAlt { get; set; }
    }
}
