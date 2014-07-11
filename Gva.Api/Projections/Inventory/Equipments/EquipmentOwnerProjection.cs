using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Equipments
{
    public class EquipmentOwnerProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public EquipmentOwnerProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Equipment")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var owners = parts.GetAll("equipmentDocumentOwners");

            return owners.Select(o => this.Create(o));
        }

        public GvaViewInventoryItem Create(PartVersion equipmentOwner)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = equipmentOwner.Part.Lot.LotId;
            invItem.PartId = equipmentOwner.Part.PartId;
            invItem.SetPartAlias = equipmentOwner.Part.SetPart.Alias;
            invItem.Name = equipmentOwner.Part.SetPart.Name;
            invItem.TypeId = equipmentOwner.Content.Get<int>("equipmentRelation.nomValueId");
            invItem.Number = equipmentOwner.Content.Get<string>("documentNumber");
            invItem.Date = equipmentOwner.Content.Get<DateTime>("documentDate");
            invItem.Publisher = equipmentOwner.Content.Get<string>("person.name") ?? equipmentOwner.Content.Get<string>("organization.name");
            invItem.Valid = null;
            invItem.FromDate = equipmentOwner.Content.Get<DateTime>("fromDate");
            invItem.ToDate = equipmentOwner.Content.Get<DateTime?>("toDate");

            invItem.CreatedBy = this.userRepository.GetUser(equipmentOwner.Part.CreatorId).Fullname;
            invItem.CreationDate = equipmentOwner.Part.CreateDate;

            if (equipmentOwner.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(equipmentOwner.CreatorId).Fullname;
                invItem.EditedDate = equipmentOwner.CreateDate;
            }

            return invItem;
        }
    }
}
