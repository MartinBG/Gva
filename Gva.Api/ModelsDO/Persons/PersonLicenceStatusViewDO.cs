using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceStatusViewDO
    {
        public NomValue Valid { get; set; }

        public NomValue ChangeReason { get; set; }

        public DateTime? ChangeDate { get; set; }

        public NomValue Inspector { get; set; }

        public string Notes { get; set; }
    }
}
