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
            var others = parts.GetAll<PersonDocumentOtherDO>("personDocumentOthers");

            return others.Select(o => this.Create(o));
        }

        private GvaViewInventoryItem Create(PartVersion<PersonDocumentOtherDO> personOther)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personOther.Part.Lot.LotId;
            invItem.PartId = personOther.Part.PartId;
            invItem.SetPartAlias = personOther.Part.SetPart.Alias;

            invItem.Name = personOther.Content.DocumentRole.Name;
            invItem.TypeId = personOther.Content.DocumentType.NomValueId;
            invItem.Number = personOther.Content.DocumentNumber;
            invItem.Date = personOther.Content.DocumentDateValidFrom;
            invItem.Publisher = personOther.Content.DocumentPublisher;
            invItem.Valid = personOther.Content.Valid.Code == "Y";
            invItem.FromDate = personOther.Content.DocumentDateValidFrom;
            invItem.ToDate = personOther.Content.DocumentDateValidTo;
            invItem.Notes = personOther.Content.Notes;

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
