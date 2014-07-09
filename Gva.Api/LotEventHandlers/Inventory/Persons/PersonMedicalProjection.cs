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
    public class PersonMedicalProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public PersonMedicalProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Person")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var medicals = parts.GetAll("personDocumentMedicals");

            var personData = parts.Get("personData");

            return medicals.Select(m => this.Create(m, personData));
        }

        private GvaViewInventoryItem Create(PartVersion personMedical, PartVersion personData)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personMedical.Part.Lot.LotId;
            invItem.PartId = personMedical.Part.PartId;
            invItem.SetPartAlias = personMedical.Part.SetPart.Alias;
            invItem.Name = personMedical.Part.SetPart.Name;
            invItem.TypeId = null;
            invItem.Number = string.Format(
                    "{0}-{1}-{2}-{3}",
                    personMedical.Content.Get<string>("documentNumberPrefix"),
                    personMedical.Content.Get<string>("documentNumber"),
                    personMedical.Content.Get<string>("lin"),
                    personMedical.Content.Get<string>("documentNumberSuffix"));
            invItem.Date = personMedical.Content.Get<DateTime>("documentDateValidFrom");
            invItem.Publisher = personMedical.Content.Get<string>("documentPublisher.name");
            invItem.Valid = null;
            invItem.FromDate = personMedical.Content.Get<DateTime>("documentDateValidFrom");
            invItem.ToDate = personMedical.Content.Get<DateTime>("documentDateValidTo");

            invItem.CreatedBy = this.userRepository.GetUser(personMedical.Part.CreatorId).Fullname;
            invItem.CreationDate = personMedical.Part.CreateDate;

            if (personMedical.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(personMedical.CreatorId).Fullname;
                invItem.EditedDate = personMedical.CreateDate;
            }

            return invItem;
        }
    }
}
