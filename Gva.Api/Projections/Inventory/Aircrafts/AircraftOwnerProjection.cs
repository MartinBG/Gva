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
            var owners = parts.GetAll<AircraftDocumentOwnerDO>("aircraftDocumentOwners");

            return owners.Select(o => this.Create(o));
        }

        private GvaViewInventoryItem Create(PartVersion<AircraftDocumentOwnerDO> aircraftOwner)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = aircraftOwner.Part.Lot.LotId;
            invItem.PartId = aircraftOwner.Part.PartId;
            invItem.SetPartAlias = aircraftOwner.Part.SetPart.Alias;
            invItem.Name = aircraftOwner.Part.SetPart.Name;
            invItem.TypeId = aircraftOwner.Content.AircraftRelation.NomValueId;
            invItem.Number = aircraftOwner.Content.DocumentNumber;
            invItem.Date = aircraftOwner.Content.DocumentDate.Value;

            string personName = aircraftOwner.Content.Person == null ? null : aircraftOwner.Content.Person.Name;
            string organizationName = aircraftOwner.Content.Organization == null ? null : aircraftOwner.Content.Organization.Name;
            invItem.Publisher = personName ?? organizationName;
            invItem.Valid = null;
            invItem.FromDate = aircraftOwner.Content.FromDate;
            invItem.ToDate = aircraftOwner.Content.ToDate;
            invItem.Notes = aircraftOwner.Content.Notes;

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
