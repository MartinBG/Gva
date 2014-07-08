using System;
using System.Collections.Generic;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Equipment;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.Equipment
{
    public class EquipmentProjection : Projection<GvaViewEquipment>
    {
        public EquipmentProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Equipment")
        {
        }

        public override IEnumerable<GvaViewEquipment> Execute(PartCollection parts)
        {
            var equipmentData = parts.Get("equipmentData");

            if (equipmentData == null)
            {
                return new GvaViewEquipment[] { };
            }

            return new[] { this.Create(equipmentData) };
        }

        private GvaViewEquipment Create(PartVersion equipmentData)
        {
            GvaViewEquipment equipment = new GvaViewEquipment();

            equipment.LotId = equipmentData.Part.Lot.LotId;
            equipment.Name = equipmentData.Content.Get<string>("name");
            equipment.EquipmentProducerId = equipmentData.Content.Get<int>("equipmentProducer.nomValueId");
            equipment.Place = equipmentData.Content.Get<string>("place");
            equipment.EquipmentTypeId = equipmentData.Content.Get<int>("equipmentType.nomValueId");
            equipment.ManDate = equipmentData.Content.Get<DateTime>("manDate");
            equipment.ManPlace = equipmentData.Content.Get<string>("manPlace");
            equipment.OperationalDate = equipmentData.Content.Get<DateTime?>("operationalDate");
            equipment.Note = equipmentData.Content.Get<string>("note");

            return equipment;
        }
    }
}
