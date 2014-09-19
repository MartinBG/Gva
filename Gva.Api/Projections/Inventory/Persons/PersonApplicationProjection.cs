using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Common;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Persons
{
    public class PersonApplicationProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public PersonApplicationProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Person")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var applications = parts.GetAll<DocumentApplicationDO>("personDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewInventoryItem Create(PartVersion<DocumentApplicationDO> personApplication)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = personApplication.Part.Lot.LotId;
            invItem.PartId = personApplication.Part.PartId;
            invItem.SetPartAlias = personApplication.Part.SetPart.Alias;
            invItem.Name = personApplication.Part.SetPart.Name;
            invItem.TypeId = personApplication.Content.ApplicationType.NomValueId;
            invItem.Number = personApplication.Content.DocumentNumber;
            invItem.Date = personApplication.Content.DocumentDate.Value;
            invItem.Publisher = null;
            invItem.Valid = null;
            invItem.FromDate = null;
            invItem.ToDate = null;
            invItem.Notes = personApplication.Content.Notes;

            invItem.CreatedBy = this.userRepository.GetUser(personApplication.Part.CreatorId).Fullname;
            invItem.CreationDate = personApplication.Part.CreateDate;

            if (personApplication.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(personApplication.CreatorId).Fullname;
                invItem.EditedDate = personApplication.CreateDate;
            }

            return invItem;
        }
    }
}
