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
    public class PersonCheckProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public PersonCheckProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Person")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var checks = parts.GetAll("personDocumentChecks");

            return checks.Select(c => this.Create(c));
        }

        private GvaViewInventoryItem Create(PartVersion personCheck)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personCheck.Part.Lot.LotId;
            invItem.PartId = personCheck.Part.PartId;
            invItem.SetPartAlias = personCheck.Part.SetPart.Alias;

            invItem.Name = personCheck.Content.Get<string>("documentRole.name");
            invItem.TypeId = personCheck.Content.Get<int>("documentType.nomValueId");
            invItem.Number = personCheck.Content.Get<string>("documentNumber");
            invItem.Date = personCheck.Content.Get<DateTime>("documentDateValidFrom");
            invItem.Publisher = personCheck.Content.Get<string>("documentPublisher");
            invItem.Valid = personCheck.Content.Get<string>("valid.code") == "Y";
            invItem.FromDate = personCheck.Content.Get<DateTime>("documentDateValidFrom");
            invItem.ToDate = personCheck.Content.Get<DateTime?>("documentDateValidTo");
            invItem.Notes = personCheck.Content.Get<string>("notes");

            invItem.CreatedBy = this.userRepository.GetUser(personCheck.Part.CreatorId).Fullname;
            invItem.CreationDate = personCheck.Part.CreateDate;

            if (personCheck.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(personCheck.CreatorId).Fullname;
                invItem.EditedDate = personCheck.CreateDate;
            }

            return invItem;
        }
    }
}
