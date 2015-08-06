using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonEducationViewDO
    {
        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public List<CaseDO> Cases { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? CompletionDate { get; set; }

        public NomValue Graduation { get; set; }

        public NomValue School { get; set; }

        public string Speciality { get; set; }

        public string Notes { get; set; }
    }
}
