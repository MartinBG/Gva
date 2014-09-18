using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Aircrafts;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Aircrafts
{
    public class AircraftDebtProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public AircraftDebtProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Aircraft")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var debts = parts.GetAll<AircraftDocumentDebtFMDO>("aircraftDocumentDebtsFM");

            return debts.Select(d => this.Create(d));
        }

        private GvaViewInventoryItem Create(PartVersion<AircraftDocumentDebtFMDO> aircraftDebt)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = aircraftDebt.Part.Lot.LotId;
            invItem.PartId = aircraftDebt.Part.PartId;
            invItem.SetPartAlias = aircraftDebt.Part.SetPart.Alias;
            invItem.Name = aircraftDebt.Part.SetPart.Name;
            invItem.TypeId = aircraftDebt.Content.AircraftDebtType.NomValueId;
            invItem.Number = aircraftDebt.Content.DocumentNumber;
            invItem.Date = aircraftDebt.Content.DocumentDate;
            invItem.Publisher = aircraftDebt.Content.AircraftCreditor == null ? null : aircraftDebt.Content.AircraftCreditor.Name;
            invItem.Valid = null;
            invItem.FromDate = null;
            invItem.ToDate = null;
            invItem.Notes = null;

            invItem.CreatedBy = this.userRepository.GetUser(aircraftDebt.Part.CreatorId).Fullname;
            invItem.CreationDate = aircraftDebt.Part.CreateDate;

            if (aircraftDebt.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(aircraftDebt.CreatorId).Fullname;
                invItem.EditedDate = aircraftDebt.CreateDate;
            }

            return invItem;
        }
    }
}
