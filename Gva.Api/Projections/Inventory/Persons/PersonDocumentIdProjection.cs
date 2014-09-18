using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Persons
{
    public class PersonDocumentIdProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public PersonDocumentIdProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Person")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var documentIds = parts.GetAll<PersonDocumentIdDO>("personDocumentIds");

            return documentIds.Select(d => this.Create(d));
        }

        private GvaViewInventoryItem Create(PartVersion<PersonDocumentIdDO> personDocumentId)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personDocumentId.Part.Lot.LotId;
            invItem.PartId = personDocumentId.Part.PartId;
            invItem.SetPartAlias = personDocumentId.Part.SetPart.Alias;
            invItem.Name = personDocumentId.Part.SetPart.Name;
            invItem.TypeId = personDocumentId.Content.DocumentType.NomValueId;
            invItem.Number = personDocumentId.Content.DocumentNumber;
            invItem.Date = personDocumentId.Content.DocumentDateValidFrom;
            invItem.Publisher = personDocumentId.Content.DocumentPublisher;
            invItem.Valid = personDocumentId.Content.Valid.Code == "Y";
            invItem.FromDate = personDocumentId.Content.DocumentDateValidFrom;
            invItem.ToDate = personDocumentId.Content.DocumentDateValidTo;

            invItem.CreatedBy = this.userRepository.GetUser(personDocumentId.Part.CreatorId).Fullname;
            invItem.CreationDate = personDocumentId.Part.CreateDate;

            if (personDocumentId.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(personDocumentId.CreatorId).Fullname;
                invItem.EditedDate = personDocumentId.CreateDate;
            }

            return invItem;
        }
    }
}
