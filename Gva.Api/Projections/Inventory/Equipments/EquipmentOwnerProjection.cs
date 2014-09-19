using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Equipments;
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
            var owners = parts.GetAll<EquipmentOwnerDO>("equipmentDocumentOwners");

            return owners.Select(o => this.Create(o));
        }

        public GvaViewInventoryItem Create(PartVersion<EquipmentOwnerDO> equipmentOwner)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = equipmentOwner.Part.Lot.LotId;
            invItem.PartId = equipmentOwner.Part.PartId;
            invItem.SetPartAlias = equipmentOwner.Part.SetPart.Alias;
            invItem.Name = equipmentOwner.Part.SetPart.Name;
            invItem.TypeId = equipmentOwner.Content.EquipmentRelation.NomValueId;
            invItem.Number = equipmentOwner.Content.DocumentNumber;
            invItem.Date = equipmentOwner.Content.DocumentDate.Value;
            string personName = equipmentOwner.Content.Person == null ? null : equipmentOwner.Content.Person.Name;
            string organizationName = equipmentOwner.Content.Organization == null ? null : equipmentOwner.Content.Organization.Name;
            invItem.Publisher = personName ?? organizationName;
            invItem.Valid = null;
            invItem.FromDate = equipmentOwner.Content.FromDate.Value;
            invItem.ToDate = equipmentOwner.Content.ToDate;
            invItem.Notes = equipmentOwner.Content.Notes;

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
