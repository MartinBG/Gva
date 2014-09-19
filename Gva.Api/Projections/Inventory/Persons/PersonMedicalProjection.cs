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
            var medicals = parts.GetAll<PersonMedicalDO>("personDocumentMedicals");

            var personData = parts.Get<PersonDataDO>("personData");

            return medicals.Select(m => this.Create(m, personData));
        }

        private GvaViewInventoryItem Create
            (PartVersion<PersonMedicalDO> personMedical,
            PartVersion<PersonDataDO> personData)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personMedical.Part.Lot.LotId;
            invItem.PartId = personMedical.Part.PartId;
            invItem.SetPartAlias = personMedical.Part.SetPart.Alias;
            invItem.Name = personMedical.Part.SetPart.Name;
            invItem.TypeId = null;
            invItem.Number = string.Format(
                    "{0}-{1}-{2}-{3}",
                    personMedical.Content.DocumentNumberPrefix,
                    personMedical.Content.DocumentNumber,
                    personData.Content.Lin,
                    personMedical.Content.DocumentNumberSuffix);
            invItem.Date = personMedical.Content.DocumentDateValidFrom.Value;
            invItem.Publisher = personMedical.Content.DocumentPublisher.Name;
            invItem.Valid = null;
            invItem.FromDate = personMedical.Content.DocumentDateValidFrom.Value;
            invItem.ToDate = personMedical.Content.DocumentDateValidTo.Value;
            invItem.Notes = personMedical.Content.Notes;

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
