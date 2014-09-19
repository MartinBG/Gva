using System.Collections.Generic;
using Common.Data;
using Gva.Api.Models.Views.Equipment;
using Gva.Api.ModelsDO.Equipments;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Equipment
{
    public class EquipmentProjection : Projection<GvaViewEquipment>
    {
        public EquipmentProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Equipment")
        {
        }

        public override IEnumerable<GvaViewEquipment> Execute(PartCollection parts)
        {
            var equipmentData = parts.Get<EquipmentDataDO>("equipmentData");

            if (equipmentData == null)
            {
                return new GvaViewEquipment[] { };
            }

            return new[] { this.Create(equipmentData) };
        }

        private GvaViewEquipment Create(PartVersion<EquipmentDataDO> equipmentData)
        {
            GvaViewEquipment equipment = new GvaViewEquipment();

            equipment.LotId = equipmentData.Part.Lot.LotId;
            equipment.Name = equipmentData.Content.Name;
            equipment.EquipmentProducerId = equipmentData.Content.EquipmentProducer.NomValueId;
            equipment.Place = equipmentData.Content.Place;
            equipment.EquipmentTypeId = equipmentData.Content.EquipmentType.NomValueId;
            equipment.ManDate = equipmentData.Content.ManDate.Value;
            equipment.ManPlace = equipmentData.Content.ManPlace;
            equipment.OperationalDate = equipmentData.Content.OperationalDate;
            equipment.Note = equipmentData.Content.Note;

            return equipment;
        }
    }
}
