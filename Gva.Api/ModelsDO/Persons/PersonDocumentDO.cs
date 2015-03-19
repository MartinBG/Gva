using System;
using Common.Api.Models;
namespace Gva.Api.ModelsDO.Persons
{
    public class PersonDocumentDO
    {
        public int LotId { get; set; }

        public int PartIndex { get; set; }

        public string DocumentNumber { get; set; }

        public int? DocumentPersonNumber { get; set; }

        public int? DocumentTypeId { get; set; }

        public int? DocumentRoleId { get; set; }

        public string DocumentPublisher { get; set; }

        public DateTime? DocumentDateValidFrom { get; set; }
    }
}
