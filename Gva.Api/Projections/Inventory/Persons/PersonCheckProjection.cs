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
            var checks = parts.GetAll<PersonCheckDO>("personDocumentChecks");

            return checks.Select(c => this.Create(c));
        }

        private GvaViewInventoryItem Create(PartVersion<PersonCheckDO> personCheck)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personCheck.Part.Lot.LotId;
            invItem.PartId = personCheck.Part.PartId;
            invItem.SetPartAlias = personCheck.Part.SetPart.Alias;

            invItem.Name = personCheck.Content.DocumentRole.Name;
            invItem.TypeId = personCheck.Content.DocumentType.NomValueId;
            invItem.Number = personCheck.Content.DocumentNumber;
            invItem.Date = personCheck.Content.DocumentDateValidFrom.Value;
            invItem.Publisher = personCheck.Content.DocumentPublisher;
            invItem.Valid = personCheck.Content.Valid.Code == "Y";
            invItem.FromDate = personCheck.Content.DocumentDateValidFrom.Value;
            invItem.ToDate = personCheck.Content.DocumentDateValidTo;
            invItem.Notes = personCheck.Content.Notes;

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
