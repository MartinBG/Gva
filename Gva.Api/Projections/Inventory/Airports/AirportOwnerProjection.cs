using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Airports;
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
            var owners = parts.GetAll<AirportOwnerDO>("airportDocumentOwners");

            return owners.Select(o => this.Create(o));
        }

        public GvaViewInventoryItem Create(PartVersion<AirportOwnerDO> airportOwner)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = airportOwner.Part.Lot.LotId;
            invItem.PartId = airportOwner.Part.PartId;
            invItem.SetPartAlias = airportOwner.Part.SetPart.Alias;
            invItem.Name = airportOwner.Part.SetPart.Name;
            invItem.TypeId = airportOwner.Content.AirportRelation.NomValueId;
            invItem.Number = airportOwner.Content.DocumentNumber;
            invItem.Date = airportOwner.Content.DocumentDate.Value;
            string personName = airportOwner.Content.Person == null ? null : airportOwner.Content.Person.Name;
            string organizationName = airportOwner.Content.Organization == null ? null : airportOwner.Content.Organization.Name;
            invItem.Publisher = personName ?? organizationName;
            invItem.Valid = null;
            invItem.FromDate = airportOwner.Content.FromDate.Value;
            invItem.ToDate = airportOwner.Content.ToDate;
            invItem.Notes = airportOwner.Content.Notes;

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
