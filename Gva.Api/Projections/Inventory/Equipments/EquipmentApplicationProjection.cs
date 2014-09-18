using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Common;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Equipments
{
    public class EquipmentApplicationProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public EquipmentApplicationProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Equipment")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var applications = parts.GetAll<DocumentApplicationDO>("equipmentDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        public GvaViewInventoryItem Create(PartVersion<DocumentApplicationDO> equipmentApplication)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = equipmentApplication.Part.Lot.LotId;
            invItem.PartId = equipmentApplication.Part.PartId;
            invItem.SetPartAlias = equipmentApplication.Part.SetPart.Alias;
            invItem.Name = equipmentApplication.Part.SetPart.Name;
            invItem.TypeId = equipmentApplication.Content.ApplicationType.NomValueId;
            invItem.Number = equipmentApplication.Content.DocumentNumber;
            invItem.Date = equipmentApplication.Content.DocumentDate.Value;
            invItem.Publisher = null;
            invItem.Valid = null;
            invItem.FromDate = null;
            invItem.ToDate = null;
            invItem.Notes = equipmentApplication.Content.Notes;

            invItem.CreatedBy = this.userRepository.GetUser(equipmentApplication.Part.CreatorId).Fullname;
            invItem.CreationDate = equipmentApplication.Part.CreateDate;

            if (equipmentApplication.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(equipmentApplication.CreatorId).Fullname;
                invItem.EditedDate = equipmentApplication.CreateDate;
            }

            return invItem;
        }
    }
}
