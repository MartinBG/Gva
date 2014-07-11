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
    public class PersonEducationProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public PersonEducationProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Person")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var educations = parts.GetAll("personDocumentEducations");

            return educations.Select(e => this.Create(e));
        }

        private GvaViewInventoryItem Create(PartVersion personEducation)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personEducation.Part.Lot.LotId;
            invItem.PartId = personEducation.Part.PartId;
            invItem.SetPartAlias = personEducation.Part.SetPart.Alias;
            invItem.Name = personEducation.Part.SetPart.Name;
            invItem.TypeId = personEducation.Content.Get<int>("graduation.nomValueId");
            invItem.Number = personEducation.Content.Get<string>("documentNumber");
            invItem.Date = personEducation.Content.Get<DateTime>("completionDate");
            invItem.Publisher = personEducation.Content.Get<string>("school.name");
            invItem.Valid = null;
            invItem.FromDate = null;
            invItem.ToDate = null;

            invItem.CreatedBy = this.userRepository.GetUser(personEducation.Part.CreatorId).Fullname;
            invItem.CreationDate = personEducation.Part.CreateDate;

            if (personEducation.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(personEducation.CreatorId).Fullname;
                invItem.EditedDate = personEducation.CreateDate;
            }

            return invItem;
        }
    }
}
