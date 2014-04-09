using System;
using Common.Data;
using Gva.Api.Models;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.EquipmentView
{
    public class EquipmentDataHandler : CommitEventHandler<GvaViewEquipment>
    {
        public EquipmentDataHandler(IUnitOfWork unitOfWork)
            : base(
                unitOfWork: unitOfWork,
                setAlias: "Equipment",
                setPartAlias: "equipmentData",
                viewMatcher: pv =>
                    v => v.LotId == pv.Part.Lot.LotId)
        {
        }

        public override void Fill(GvaViewEquipment equipment, PartVersion part)
        {
            equipment.Lot = part.Part.Lot;

            equipment.Name = part.DynamicContent.name;
            equipment.EquipmentProducer = part.DynamicContent.equipmentProducer.name;
            equipment.Place = part.DynamicContent.place;
            equipment.EquipmentType = part.DynamicContent.equipmentType.name;
            equipment.ManDate = part.DynamicContent.manDate;
            equipment.ManPlace = part.DynamicContent.manPlace;
            equipment.OperationalDate = part.DynamicContent.operationalDate;
            equipment.Note = part.DynamicContent.note;
        }

        public override void Clear(GvaViewEquipment equipment)
        {
            throw new NotSupportedException();
        }
    }
}
