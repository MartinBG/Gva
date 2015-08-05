using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Equipments
{
    public class EquipmentDocumentOtherViewDO
    {
        public int PartIndex { get; set; }

        public int PartId { get; set; }

        public CaseDO Case { get; set; }

        public string DocumentNumber { get; set; }

        public string DocumentPublisher { get; set; }

        public DateTime? DocumentDateValidFrom { get; set; }

        public DateTime? DocumentDateValidTo { get; set; }

        public NomValue DocumentType { get; set; }

        public NomValue DocumentRole { get; set; }

        public NomValue Valid { get; set; }

        public string Notes { get; set; }
    }
}
