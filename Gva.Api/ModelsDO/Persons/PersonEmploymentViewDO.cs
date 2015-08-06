using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonEmploymentViewDO
    {
        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public CaseDO Case { get; set; }

        public DateTime? Hiredate { get; set; }

        public NomValue Valid { get; set; }

        public NomValue Organization { get; set; }

        public NomValue EmploymentCategory { get; set; }

        public NomValue Country { get; set; }

        public string Notes { get; set; }
    }
}
