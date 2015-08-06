using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonStatusViewDO
    {
        public int PartIndex { get; set; }

        public List<CaseDO> Cases { get; set; }

        public NomValue PersonStatusType { get; set; }

        public string DocumentNumber { get; set; }

        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public string Notes { get; set; }
    }
}
