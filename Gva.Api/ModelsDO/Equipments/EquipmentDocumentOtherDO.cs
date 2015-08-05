using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Equipments
{
    public class EquipmentDocumentOtherDO
    {
        public string DocumentNumber { get; set; }

        public string DocumentPublisher { get; set; }

        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public int? DocumentTypeId { get; set; }

        public int? DocumentRoleId { get; set; }

        public int? ValidId { get; set; }

        public string Notes { get; set; }
    }
}
