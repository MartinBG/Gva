using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Common;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Aircrafts
{
    public class AircraftApplicationProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public AircraftApplicationProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Aircraft")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var applications = parts.GetAll<DocumentApplicationDO>("aircraftDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewInventoryItem Create(PartVersion<DocumentApplicationDO> aircraftApplication)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = aircraftApplication.Part.Lot.LotId;
            invItem.PartId = aircraftApplication.Part.PartId;
            invItem.SetPartAlias = aircraftApplication.Part.SetPart.Alias;
            invItem.Name = aircraftApplication.Part.SetPart.Name;
            invItem.TypeId = aircraftApplication.Content.ApplicationType.NomValueId;
            invItem.Number = aircraftApplication.Content.DocumentNumber;
            invItem.Date = aircraftApplication.Content.DocumentDate.Value;
            invItem.Publisher = null;
            invItem.Valid = null;
            invItem.FromDate = null;
            invItem.ToDate = null;
            invItem.Notes = aircraftApplication.Content.Notes;

            invItem.CreatedBy = this.userRepository.GetUser(aircraftApplication.Part.CreatorId).Fullname;
            invItem.CreationDate = aircraftApplication.Part.CreateDate;

            if (aircraftApplication.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(aircraftApplication.CreatorId).Fullname;
                invItem.EditedDate = aircraftApplication.CreateDate;
            }

            return invItem;
        }
    }
}
