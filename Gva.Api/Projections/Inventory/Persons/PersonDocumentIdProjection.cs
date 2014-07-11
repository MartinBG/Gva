using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views;
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
            var documentIds = parts.GetAll("personDocumentIds");

            return documentIds.Select(d => this.Create(d));
        }

        private GvaViewInventoryItem Create(PartVersion personDocumentId)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personDocumentId.Part.Lot.LotId;
            invItem.PartId = personDocumentId.Part.PartId;
            invItem.SetPartAlias = personDocumentId.Part.SetPart.Alias;
            invItem.Name = personDocumentId.Part.SetPart.Name;
            invItem.TypeId = personDocumentId.Content.Get<int>("documentType.nomValueId");
            invItem.Number = personDocumentId.Content.Get<string>("documentNumber");
            invItem.Date = personDocumentId.Content.Get<DateTime>("documentDateValidFrom");
            invItem.Publisher = personDocumentId.Content.Get<string>("documentPublisher");
            invItem.Valid = personDocumentId.Content.Get<string>("valid.code") == "Y";
            invItem.FromDate = personDocumentId.Content.Get<DateTime>("documentDateValidFrom");
            invItem.ToDate = personDocumentId.Content.Get<DateTime>("documentDateValidTo");

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
