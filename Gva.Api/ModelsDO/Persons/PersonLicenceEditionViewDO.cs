using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using Common.Filters;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceEditionViewDO
    {
        public int? LicencePartIndex { get; set; }

        public int? PartIndex { get; set; }

        public int Index { get; set; }

        public string Inspector { get; set; }

        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public NomValue LicenceAction { get; set; }

        public List<NomValue> Limitations { get; set; }

        public string Notes { get; set; }

        public string NotesAlt { get; set; }

        public string StampNumber { get; set; }

        public List<CaseDO> Cases { get; set; }
    }
}
