using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Equipments;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Equipments
{
    public class EquipmentOtherProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;
        private INomRepository nomRepository;

        public EquipmentOtherProjection(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            INomRepository nomRepository)
            : base(unitOfWork, "Equipment")
        {
            this.userRepository = userRepository;
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var others = parts.GetAll<EquipmentDocumentOtherDO>("equipmentDocumentOthers");

            return others.Select(o => this.Create(o));
        }

        public GvaViewInventoryItem Create(PartVersion<EquipmentDocumentOtherDO> equipmentOther)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = equipmentOther.Part.Lot.LotId;
            invItem.PartId = equipmentOther.Part.PartId;
            invItem.SetPartAlias = equipmentOther.Part.SetPart.Alias;
            invItem.Name = equipmentOther.Content.DocumentRoleId.HasValue ? this.nomRepository.GetNomValue("documentRoles", equipmentOther.Content.DocumentRoleId.Value).Name : null;
            invItem.TypeId = equipmentOther.Content.DocumentTypeId;
            invItem.Number = equipmentOther.Content.DocumentNumber;
            invItem.Date = equipmentOther.Content.DocumentDateValidFrom;
            invItem.Publisher = equipmentOther.Content.DocumentPublisher;
            invItem.Valid = !equipmentOther.Content.ValidId.HasValue ? (bool?)null : this.nomRepository.GetNomValue("boolean", equipmentOther.Content.ValidId.Value).Code == "Y";
            invItem.FromDate = equipmentOther.Content.DocumentDateValidFrom;
            invItem.ToDate = equipmentOther.Content.DocumentDateValidTo;
            invItem.Notes = equipmentOther.Content.Notes;

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
