using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Equipments
{
    public class EquipmentDataDO
    {
        public EquipmentDataDO()
        {
            this.Ext = new EquipmentDataExtDO();
        }

        [Required(ErrorMessage = "EquipmentType is required.")]
        public NomValue EquipmentType { get; set; }

        [Required(ErrorMessage = "Name is required.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "EquipmentProducer is required.")]
        public NomValue EquipmentProducer { get; set; }

        [Required(ErrorMessage = "ManPlace is required.")]
        public string ManPlace { get; set; }

        [Required(ErrorMessage = "ManDate is required.")]
        public DateTime? ManDate { get; set; }

        public string Place { get; set; }

        public DateTime? OperationalDate { get; set; }

        public string Note { get; set; }

        public EquipmentDataExtDO Ext { get; set; }
    }
}
