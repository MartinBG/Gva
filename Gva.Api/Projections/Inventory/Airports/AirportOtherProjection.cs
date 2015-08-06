using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Airports;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Airports
{
    public class AirportOtherProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;
        private INomRepository nomRepository;

        public AirportOtherProjection(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            INomRepository nomRepository)
            : base(unitOfWork, "Airport")
        {
            this.userRepository = userRepository;
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var others = parts.GetAll<AirportDocumentOtherDO>("airportDocumentOthers");

            return others.Select(o => this.Create(o));
        }

        public GvaViewInventoryItem Create(PartVersion<AirportDocumentOtherDO> airportOther)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = airportOther.Part.Lot.LotId;
            invItem.PartId = airportOther.Part.PartId;
            invItem.SetPartAlias = airportOther.Part.SetPart.Alias;

            invItem.Name = airportOther.Content.DocumentRoleId.HasValue ? this.nomRepository.GetNomValue("documentRoles", airportOther.Content.DocumentRoleId.Value).Name : null;
            invItem.TypeId = airportOther.Content.DocumentTypeId;
            invItem.Number = airportOther.Content.DocumentNumber;
            invItem.Date = airportOther.Content.DocumentDateValidFrom;
            invItem.Publisher = airportOther.Content.DocumentPublisher;
            invItem.Valid = !airportOther.Content.ValidId.HasValue ? (bool?)null : this.nomRepository.GetNomValue("boolean", airportOther.Content.ValidId.Value).Code == "Y";
            invItem.FromDate = airportOther.Content.DocumentDateValidFrom;
            invItem.ToDate = airportOther.Content.DocumentDateValidTo;
            invItem.Notes = airportOther.Content.Notes;

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
