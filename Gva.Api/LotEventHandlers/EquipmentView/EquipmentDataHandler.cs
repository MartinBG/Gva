using System;
using Common.Data;
using Common.Json;
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
            equipment.LotId = part.Part.Lot.LotId;

            equipment.Name = part.Content.Get<string>("name");
            equipment.EquipmentProducer = part.Content.Get<string>("equipmentProducer.name");
            equipment.Place = part.Content.Get<string>("place");
            equipment.EquipmentType = part.Content.Get<string>("equipmentType.name");
            equipment.ManDate = part.Content.Get<DateTime>("manDate");
            equipment.ManPlace = part.Content.Get<string>("manPlace");
            equipment.OperationalDate = part.Content.Get<DateTime?>("operationalDate");
            equipment.Note = part.Content.Get<string>("note");
        }

        public override void Clear(GvaViewEquipment equipment)
        {
            throw new NotSupportedException();
        }
    }
}
