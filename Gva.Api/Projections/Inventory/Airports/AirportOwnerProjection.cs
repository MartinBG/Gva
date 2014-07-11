using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Airports
{
    public class AirportOwnerProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public AirportOwnerProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Airport")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var owners = parts.GetAll("airportDocumentOwners");

            return owners.Select(o => this.Create(o));
        }

        public GvaViewInventoryItem Create(PartVersion airportOwner)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = airportOwner.Part.Lot.LotId;
            invItem.PartId = airportOwner.Part.PartId;
            invItem.SetPartAlias = airportOwner.Part.SetPart.Alias;
            invItem.Name = airportOwner.Part.SetPart.Name;
            invItem.TypeId = airportOwner.Content.Get<int>("airportRelation.nomValueId");
            invItem.Number = airportOwner.Content.Get<string>("documentNumber");
            invItem.Date = airportOwner.Content.Get<DateTime>("documentDate");
            invItem.Publisher = airportOwner.Content.Get<string>("person.name") ?? airportOwner.Content.Get<string>("organization.name");
            invItem.Valid = null;
            invItem.FromDate = airportOwner.Content.Get<DateTime>("fromDate");
            invItem.ToDate = airportOwner.Content.Get<DateTime?>("toDate");

            invItem.CreatedBy = this.userRepository.GetUser(airportOwner.Part.CreatorId).Fullname;
            invItem.CreationDate = airportOwner.Part.CreateDate;

            if (airportOwner.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(airportOwner.CreatorId).Fullname;
                invItem.EditedDate = airportOwner.CreateDate;
            }

            return invItem;
        }
    }
}
