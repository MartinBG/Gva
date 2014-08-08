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

namespace Gva.Api.Projections.Inventory.Airports
{
    public class AirportOtherProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public AirportOtherProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Airport")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var others = parts.GetAll("airportDocumentOthers");

            return others.Select(o => this.Create(o));
        }

        public GvaViewInventoryItem Create(PartVersion airportOther)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = airportOther.Part.Lot.LotId;
            invItem.PartId = airportOther.Part.PartId;
            invItem.SetPartAlias = airportOther.Part.SetPart.Alias;

            invItem.Name = airportOther.Content.Get<string>("documentRole.name");
            invItem.TypeId = airportOther.Content.Get<int>("documentType.nomValueId");
            invItem.Number = airportOther.Content.Get<string>("documentNumber");
            invItem.Date = airportOther.Content.Get<DateTime?>("documentDateValidFrom");
            invItem.Publisher = airportOther.Content.Get<string>("documentPublisher");
            NomValue valid = airportOther.Content.Get<NomValue>("valid");
            invItem.Valid = valid == null ? (bool?)null : valid.Code == "Y";
            invItem.FromDate = airportOther.Content.Get<DateTime?>("documentDateValidFrom");
            invItem.ToDate = airportOther.Content.Get<DateTime?>("documentDateValidTo");
            invItem.Notes = airportOther.Content.Get<string>("notes");

            invItem.CreatedBy = this.userRepository.GetUser(airportOther.Part.CreatorId).Fullname;
            invItem.CreationDate = airportOther.Part.CreateDate;

            if (airportOther.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(airportOther.CreatorId).Fullname;
                invItem.EditedDate = airportOther.CreateDate;
            }

            return invItem;
        }
    }
}
