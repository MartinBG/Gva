using System;
using System.Linq;
using System.Collections.Generic;
using Gva.Api.Models;
namespace Gva.Api.ModelsDO
{
    public class EquipmentDO
    {
        public EquipmentDO(
            GvaViewEquipment equipmentData)
        {
            this.Id = equipmentData.LotId;
            this.Name = equipmentData.Name;
            this.EquipmentType = equipmentData.EquipmentType;
            this.EquipmentProducer = equipmentData.EquipmentProducer;
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