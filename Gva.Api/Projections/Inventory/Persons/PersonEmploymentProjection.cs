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
            var employments = parts.GetAll("personDocumentEmployments");

            return employments.Select(e => this.Create(e));
        }

        private GvaViewInventoryItem Create(PartVersion personEmployment)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personEmployment.Part.Lot.LotId;
            invItem.PartId = personEmployment.Part.PartId;
            invItem.SetPartAlias = personEmployment.Part.SetPart.Alias;
            invItem.Name = personEmployment.Part.SetPart.Name;
            invItem.TypeId = personEmployment.Content.Get<int>("employmentCategory.nomValueId");
            invItem.Number = null;
            invItem.Date = personEmployment.Content.Get<DateTime>("hiredate");
            invItem.Publisher = personEmployment.Content.Get<string>("organization.name");
            invItem.Valid = personEmployment.Content.Get<string>("valid.code") == "Y";
            invItem.FromDate = personEmployment.Content.Get<DateTime>("hiredate");
            invItem.ToDate = null;
            invItem.Notes = personEmployment.Content.Get<string>("notes");

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
