using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class CheckOfForeignerDO
    {
        public string Names { get; set; }

        public DateTime? DocumentDate { get; set; }

        public string DocumentType { get; set; }

        public string DocumentNumber { get; set; }

        public string RatingTypes { get; set; }

        public string RatingClass { get; set; }
    }
}
