using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Common;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Airports
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
            var applications = parts.GetAll<DocumentApplicationDO>("airportDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewInventoryItem Create(PartVersion<DocumentApplicationDO> airportApplication)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = airportApplication.Part.Lot.LotId;
            invItem.PartId = airportApplication.Part.PartId;
            invItem.SetPartAlias = airportApplication.Part.SetPart.Alias;
            invItem.Name = airportApplication.Part.SetPart.Name;
            invItem.TypeId = airportApplication.Content.ApplicationType.NomValueId;
            invItem.Number = airportApplication.Content.DocumentNumber;
            invItem.Date = airportApplication.Content.DocumentDate.Value;
            invItem.Publisher = null;
            invItem.Valid = null;
            invItem.FromDate = null;
            invItem.ToDate = null;
            invItem.Notes = airportApplication.Content.Notes;

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
