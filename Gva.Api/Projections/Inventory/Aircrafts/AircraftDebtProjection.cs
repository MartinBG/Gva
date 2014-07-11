using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views;
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
            var debts = parts.GetAll("aircraftDocumentDebtsFM");

            return debts.Select(d => this.Create(d));
        }

        private GvaViewInventoryItem Create(PartVersion aircraftDebt)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = aircraftDebt.Part.Lot.LotId;
            invItem.PartId = aircraftDebt.Part.PartId;
            invItem.SetPartAlias = aircraftDebt.Part.SetPart.Alias;
            invItem.Name = aircraftDebt.Part.SetPart.Name;
            invItem.TypeId = aircraftDebt.Content.Get<int>("aircraftDebtType.nomValueId");
            invItem.Number = aircraftDebt.Content.Get<string>("documentNumber");
            invItem.Date = aircraftDebt.Content.Get<DateTime?>("documentDate");
            invItem.Publisher = aircraftDebt.Content.Get<string>("aircraftCreditor.name");
            invItem.Valid = null;
            invItem.FromDate = null;
            invItem.ToDate = null;

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
