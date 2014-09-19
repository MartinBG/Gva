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
    public class PersonEmploymentProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public PersonEmploymentProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Person")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var employments = parts.GetAll<PersonEmploymentDO>("personDocumentEmployments");

            return employments.Select(e => this.Create(e));
        }

        private GvaViewInventoryItem Create(PartVersion<PersonEmploymentDO> personEmployment)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personEmployment.Part.Lot.LotId;
            invItem.PartId = personEmployment.Part.PartId;
            invItem.SetPartAlias = personEmployment.Part.SetPart.Alias;
            invItem.Name = personEmployment.Part.SetPart.Name;
            invItem.TypeId = personEmployment.Content.EmploymentCategory.NomValueId;
            invItem.Number = null;
            invItem.Date = personEmployment.Content.Hiredate.Value;
            invItem.Publisher = personEmployment.Content.Organization == null ? null : personEmployment.Content.Organization.Name;
            invItem.Valid = personEmployment.Content.Valid.Code == "Y";
            invItem.FromDate = personEmployment.Content.Hiredate.Value;
            invItem.ToDate = null;
            invItem.Notes = personEmployment.Content.Notes;

            invItem.CreatedBy = this.userRepository.GetUser(personEmployment.Part.CreatorId).Fullname;
            invItem.CreationDate = personEmployment.Part.CreateDate;

            if (personEmployment.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(personEmployment.CreatorId).Fullname;
                invItem.EditedDate = personEmployment.CreateDate;
            }

            return invItem;
        }
    }
}
