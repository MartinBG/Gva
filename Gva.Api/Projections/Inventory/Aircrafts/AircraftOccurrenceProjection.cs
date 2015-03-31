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
    public class AircraftOccurrenceProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public AircraftOccurrenceProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Aircraft")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var occurrences = parts.GetAll<AircraftDocumentOccurenceDO>("documentOccurrences");

            return occurrences.Select(o => this.Create(o));
        }

        private GvaViewInventoryItem Create(PartVersion<AircraftDocumentOccurenceDO> aircraftOccurrence)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = aircraftOccurrence.Part.Lot.LotId;
            invItem.PartId = aircraftOccurrence.Part.PartId;
            invItem.SetPartAlias = aircraftOccurrence.Part.SetPart.Alias;
            invItem.Name = aircraftOccurrence.Part.SetPart.Name;
            invItem.TypeId = aircraftOccurrence.Content.AircraftOccurrenceClass.NomValueId;
            invItem.Number = null;
            invItem.Date = aircraftOccurrence.Content.LocalDate.Value;
            invItem.Publisher = aircraftOccurrence.Content.Country.Name;
            invItem.Valid = null;
            invItem.FromDate = null;
            invItem.ToDate = null;
            invItem.Notes = aircraftOccurrence.Content.OccurrenceNotes;

            invItem.CreatedBy = this.userRepository.GetUser(aircraftOccurrence.Part.CreatorId).Fullname;
            invItem.CreationDate = aircraftOccurrence.Part.CreateDate;

            if (aircraftOccurrence.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(aircraftOccurrence.CreatorId).Fullname;
                invItem.EditedDate = aircraftOccurrence.CreateDate;
            }

            return invItem;
        }
    }
}
