using System;
using Gva.Api.Models.Views.Equipment;

namespace Gva.Api.ModelsDO.Equipments
{
    public class EquipmentViewDO
    {
        public EquipmentViewDO(GvaViewEquipment equipmentData)
        {
            this.Id = equipmentData.LotId;
            this.Name = equipmentData.Name;
            this.EquipmentType = equipmentData.EquipmentType.Name;
            this.EquipmentProducer = equipmentData.EquipmentProducer.Name;
            this.ManPlace = equipmentData.ManPlace;
            this.ManDate = equipmentData.ManDate;
            this.Place = equipmentData.Place;
            this.OperationalDate = equipmentData.OperationalDate;
            this.Note = equipmentData.Note;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string EquipmentType { get; set; }

        public string EquipmentProducer { get; set; }

        public string ManPlace { get; set; }

        public DateTime ManDate { get; set; }

        public string Place { get; set; }

        public DateTime? OperationalDate { get; set; }

        public string Note { get; set; }
    }
}