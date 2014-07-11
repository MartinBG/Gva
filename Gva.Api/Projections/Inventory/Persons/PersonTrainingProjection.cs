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
    public class PersonTrainingProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public PersonTrainingProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Person")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var trainings = parts.GetAll("personDocumentTrainings");

            return trainings.Select(t => this.Create(t));
        }

        private GvaViewInventoryItem Create(PartVersion personTraining)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personTraining.Part.Lot.LotId;
            invItem.PartId = personTraining.Part.PartId;
            invItem.SetPartAlias = personTraining.Part.SetPart.Alias;
            invItem.Name = personTraining.Content.Get<string>("documentRole.name");
            invItem.TypeId = personTraining.Content.Get<int>("documentType.nomValueId");
            invItem.Number = personTraining.Content.Get<string>("documentNumber");
            invItem.Date = personTraining.Content.Get<DateTime>("documentDateValidFrom");
            invItem.Publisher = personTraining.Content.Get<string>("documentPublisher");
            invItem.Valid = personTraining.Content.Get<string>("valid.code") == "Y";
            invItem.FromDate = personTraining.Content.Get<DateTime>("documentDateValidFrom");
            invItem.ToDate = personTraining.Content.Get<DateTime?>("documentDateValidTo");

            invItem.CreatedBy = this.userRepository.GetUser(personTraining.Part.CreatorId).Fullname;
            invItem.CreationDate = personTraining.Part.CreateDate;

            if (personTraining.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(personTraining.CreatorId).Fullname;
                invItem.EditedDate = personTraining.CreateDate;
            }

            return invItem;
        }
    }
}
