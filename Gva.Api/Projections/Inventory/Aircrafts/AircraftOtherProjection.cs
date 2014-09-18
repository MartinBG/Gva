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
            var others = parts.GetAll<AircraftDocumentOtherDO>("aircraftDocumentOthers");

            return others.Select(o => this.Create(o));
        }

        private GvaViewInventoryItem Create(PartVersion<AircraftDocumentOtherDO> aircraftOther)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = aircraftOther.Part.Lot.LotId;
            invItem.PartId = aircraftOther.Part.PartId;
            invItem.SetPartAlias = aircraftOther.Part.SetPart.Alias;
            invItem.Name = aircraftOther.Content.OtherDocumentRole.Name;
            invItem.TypeId = aircraftOther.Content.OtherDocumentType.NomValueId;
            invItem.Number = aircraftOther.Content.DocumentNumber;
            invItem.Date = aircraftOther.Content.DocumentDateValidFrom;
            invItem.Publisher = aircraftOther.Content.DocumentPublisher;
            invItem.Valid = aircraftOther.Content.Valid == null ? (bool?)null : aircraftOther.Content.Valid.Code == "Y";
            invItem.FromDate = aircraftOther.Content.DocumentDateValidFrom;
            invItem.ToDate = aircraftOther.Content.DocumentDateValidTo;
            invItem.Notes = aircraftOther.Content.Notes;

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
