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

namespace Gva.Api.Projections.Inventory.Aircrafts
{
    public class AircraftOtherProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public AircraftOtherProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Aircraft")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var others = parts.GetAll("aircraftDocumentOthers");

            return others.Select(o => this.Create(o));
        }

        private GvaViewInventoryItem Create(PartVersion aircraftOther)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = aircraftOther.Part.Lot.LotId;
            invItem.PartId = aircraftOther.Part.PartId;
            invItem.SetPartAlias = aircraftOther.Part.SetPart.Alias;
            invItem.Name = aircraftOther.Content.Get<string>("otherDocumentRole.name");
            invItem.TypeId = aircraftOther.Content.Get<int>("otherDocumentType.nomValueId");
            invItem.Number = aircraftOther.Content.Get<string>("documentNumber");
            invItem.Date = aircraftOther.Content.Get<DateTime?>("documentDateValidFrom");
            invItem.Publisher = aircraftOther.Content.Get<string>("documentPublisher");
            NomValue valid = aircraftOther.Content.Get<NomValue>("valid");
            invItem.Valid = valid == null ? (bool?)null : valid.Code == "Y";
            invItem.FromDate = aircraftOther.Content.Get<DateTime?>("documentDateValidFrom");
            invItem.ToDate = aircraftOther.Content.Get<DateTime?>("documentDateValidTo");
            invItem.Notes = aircraftOther.Content.Get<string>("notes");

            invItem.CreatedBy = this.userRepository.GetUser(aircraftOther.Part.CreatorId).Fullname;
            invItem.CreationDate = aircraftOther.Part.CreateDate;

            if (aircraftOther.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(aircraftOther.CreatorId).Fullname;
                invItem.EditedDate = aircraftOther.CreateDate;
            }

            return invItem;
        }
    }
}
