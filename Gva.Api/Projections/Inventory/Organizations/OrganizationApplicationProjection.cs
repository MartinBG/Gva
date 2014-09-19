using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Common;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Organizations
{
    public class OrganizationApplicationProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public OrganizationApplicationProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Organization")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var applications = parts.GetAll<DocumentApplicationDO>("organizationDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewInventoryItem Create(PartVersion<DocumentApplicationDO> organizationApplication)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = organizationApplication.Part.Lot.LotId;
            invItem.PartId = organizationApplication.Part.PartId;
            invItem.SetPartAlias = organizationApplication.Part.SetPart.Alias;
            invItem.Name = organizationApplication.Part.SetPart.Name;
            invItem.TypeId = organizationApplication.Content.ApplicationType.NomValueId;
            invItem.Number = organizationApplication.Content.DocumentNumber;
            invItem.Date = organizationApplication.Content.DocumentDate.Value;
            invItem.Publisher = null;
            invItem.Valid = null;
            invItem.FromDate = null;
            invItem.ToDate = null;
            invItem.Notes = organizationApplication.Content.Notes;

            invItem.CreatedBy = this.userRepository.GetUser(organizationApplication.Part.CreatorId).Fullname;
            invItem.CreationDate = organizationApplication.Part.CreateDate;

            if (organizationApplication.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(organizationApplication.CreatorId).Fullname;
                invItem.EditedDate = organizationApplication.CreateDate;
            }

            return invItem;
        }
    }
}
