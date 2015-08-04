using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonDocumentIdViewDO
    {
        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public NomValue DocumentType { get; set; }

        public string DocumentNumber { get; set; }

        public NomValue Valid { get; set; }

        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public string DocumentPublisher { get; set; }

        public string Notes { get; set; }

        public List<CaseDO> Cases { get; set; }
    }
}
