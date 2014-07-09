using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.Inventory.Persons
{
    public class PersonOtherProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public PersonOtherProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Person")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var others = parts.GetAll("personDocumentOthers");

            return others.Select(o => this.Create(o));
        }

        private GvaViewInventoryItem Create(PartVersion personOther)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personOther.Part.Lot.LotId;
            invItem.PartId = personOther.Part.PartId;
            invItem.SetPartAlias = personOther.Part.SetPart.Alias;

            invItem.Name = personOther.Content.Get<string>("documentRole.name");
            invItem.TypeId = personOther.Content.Get<int>("documentType.nomValueId");
            invItem.Number = personOther.Content.Get<string>("documentNumber");
            invItem.Date = personOther.Content.Get<DateTime?>("documentDateValidFrom");
            invItem.Publisher = personOther.Content.Get<string>("documentPublisher");
            invItem.Valid = personOther.Content.Get<string>("valid.code") == "Y";
            invItem.FromDate = personOther.Content.Get<DateTime?>("documentDateValidFrom");
            invItem.ToDate = personOther.Content.Get<DateTime?>("documentDateValidTo");

            invItem.CreatedBy = this.userRepository.GetUser(personOther.Part.CreatorId).Fullname;
            invItem.CreationDate = personOther.Part.CreateDate;

            if (personOther.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(personOther.CreatorId).Fullname;
                invItem.EditedDate = personOther.CreateDate;
            }

            return invItem;
        }
    }
}
