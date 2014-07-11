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
    public class AircraftOwnerProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public AircraftOwnerProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Aircraft")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var owners = parts.GetAll("aircraftDocumentOwners");

            return owners.Select(o => this.Create(o));
        }

        private GvaViewInventoryItem Create(PartVersion aircraftOwner)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = aircraftOwner.Part.Lot.LotId;
            invItem.PartId = aircraftOwner.Part.PartId;
            invItem.SetPartAlias = aircraftOwner.Part.SetPart.Alias;
            invItem.Name = aircraftOwner.Part.SetPart.Name;
            invItem.TypeId = aircraftOwner.Content.Get<int>("aircraftRelation.nomValueId");
            invItem.Number = aircraftOwner.Content.Get<string>("documentNumber");
            invItem.Date = aircraftOwner.Content.Get<DateTime>("documentDate");
            invItem.Publisher = aircraftOwner.Content.Get<string>("person.name") ?? aircraftOwner.Content.Get<string>("organization.name");
            invItem.Valid = null;
            invItem.FromDate = aircraftOwner.Content.Get<DateTime?>("fromDate");
            invItem.ToDate = aircraftOwner.Content.Get<DateTime?>("toDate");

            invItem.CreatedBy = this.userRepository.GetUser(aircraftOwner.Part.CreatorId).Fullname;
            invItem.CreationDate = aircraftOwner.Part.CreateDate;

            if (aircraftOwner.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(aircraftOwner.CreatorId).Fullname;
                invItem.EditedDate = aircraftOwner.CreateDate;
            }

            return invItem;
        }
    }
}
