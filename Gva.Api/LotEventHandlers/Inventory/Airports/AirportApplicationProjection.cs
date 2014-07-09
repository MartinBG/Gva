using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.Inventory.Airports
{
    public class AirportApplicationProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public AirportApplicationProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Airport")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var applications = parts.GetAll("airportDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewInventoryItem Create(PartVersion airportApplication)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = airportApplication.Part.Lot.LotId;
            invItem.PartId = airportApplication.Part.PartId;
            invItem.SetPartAlias = airportApplication.Part.SetPart.Alias;
            invItem.Name = airportApplication.Part.SetPart.Name;
            invItem.TypeId = airportApplication.Content.Get<int>("applicationType.nomValueId");
            invItem.Number = airportApplication.Content.Get<string>("documentNumber");
            invItem.Date = airportApplication.Content.Get<DateTime>("documentDate");
            invItem.Publisher = null;
            invItem.Valid = null;
            invItem.FromDate = null;
            invItem.ToDate = null;

            invItem.CreatedBy = this.userRepository.GetUser(airportApplication.Part.CreatorId).Fullname;
            invItem.CreationDate = airportApplication.Part.CreateDate;

            if (airportApplication.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(airportApplication.CreatorId).Fullname;
                invItem.EditedDate = airportApplication.CreateDate;
            }

            return invItem;
        }
    }
}
