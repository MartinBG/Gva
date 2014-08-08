using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Equipments
{
    public class EquipmentOtherProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public EquipmentOtherProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Equipment")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var others = parts.GetAll("equipmentDocumentOthers");

            return others.Select(o => this.Create(o));
        }

        public GvaViewInventoryItem Create(PartVersion equipmentOther)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = equipmentOther.Part.Lot.LotId;
            invItem.PartId = equipmentOther.Part.PartId;
            invItem.SetPartAlias = equipmentOther.Part.SetPart.Alias;
            invItem.Name = equipmentOther.Content.Get<string>("documentRole.name");
            invItem.TypeId = equipmentOther.Content.Get<int>("documentType.nomValueId");
            invItem.Number = equipmentOther.Content.Get<string>("documentNumber");
            invItem.Date = equipmentOther.Content.Get<DateTime?>("documentDateValidFrom");
            invItem.Publisher = equipmentOther.Content.Get<string>("documentPublisher");
            NomValue valid = equipmentOther.Content.Get<NomValue>("valid");
            invItem.Valid = valid == null ? (bool?)null : valid.Code == "Y";
            invItem.FromDate = equipmentOther.Content.Get<DateTime?>("documentDateValidFrom");
            invItem.ToDate = equipmentOther.Content.Get<DateTime?>("documentDateValidTo");
            invItem.Notes = equipmentOther.Content.Get<string>("notes");

            invItem.CreatedBy = this.userRepository.GetUser(equipmentOther.Part.CreatorId).Fullname;
            invItem.CreationDate = equipmentOther.Part.CreateDate;

            if (equipmentOther.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(equipmentOther.CreatorId).Fullname;
                invItem.EditedDate = equipmentOther.CreateDate;
            }

            return invItem;
        }
    }
}
