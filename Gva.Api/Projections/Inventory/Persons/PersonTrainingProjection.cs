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
            var trainings = parts.GetAll<PersonTrainingDO>("personDocumentTrainings");

            return trainings.Select(t => this.Create(t));
        }

        private GvaViewInventoryItem Create(PartVersion<PersonTrainingDO> personTraining)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personTraining.Part.Lot.LotId;
            invItem.PartId = personTraining.Part.PartId;
            invItem.SetPartAlias = personTraining.Part.SetPart.Alias;
            invItem.Name = personTraining.Content.DocumentRole.Name;
            invItem.TypeId = personTraining.Content.DocumentType.NomValueId;
            invItem.Number = personTraining.Content.DocumentNumber;
            invItem.Date = personTraining.Content.DocumentDateValidFrom.Value;
            invItem.Publisher = personTraining.Content.DocumentPublisher;
            invItem.Valid = personTraining.Content.Valid.Code == "Y";
            invItem.FromDate = personTraining.Content.DocumentDateValidFrom.Value;
            invItem.ToDate = personTraining.Content.DocumentDateValidTo;
            invItem.Notes = personTraining.Content.Notes;

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
