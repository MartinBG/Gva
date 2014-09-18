using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Organizations;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Inventory.Organizations
{
    public class OrganizationOtherProjection : Projection<GvaViewInventoryItem>
    {
        private IUserRepository userRepository;

        public OrganizationOtherProjection(IUnitOfWork unitOfWork, IUserRepository userRepository)
            : base(unitOfWork, "Organization")
        {
            this.userRepository = userRepository;
        }

        public override IEnumerable<GvaViewInventoryItem> Execute(PartCollection parts)
        {
            var others = parts.GetAll<OrganizationDocumentOtherDO>("organizationDocumentOthers");

            return others.Select(o => this.Create(o));
        }

        private GvaViewInventoryItem Create(PartVersion<OrganizationDocumentOtherDO> organizationOther)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = organizationOther.Part.Lot.LotId;
            invItem.PartId = organizationOther.Part.PartId;
            invItem.SetPartAlias = organizationOther.Part.SetPart.Alias;

            invItem.Name = organizationOther.Content.DocumentRole.Name;
            invItem.TypeId = organizationOther.Content.DocumentType.NomValueId;
            invItem.Number = organizationOther.Content.DocumentNumber;
            invItem.Date = organizationOther.Content.DocumentDateValidFrom.Value;
            invItem.Publisher = organizationOther.Content.DocumentPublisher;
            invItem.Valid = organizationOther.Content.Valid.Code == "Y";
            invItem.FromDate = organizationOther.Content.DocumentDateValidFrom.Value;
            invItem.ToDate = organizationOther.Content.DocumentDateValidTo;
            invItem.Notes = organizationOther.Content.Notes;

            invItem.CreatedBy = this.userRepository.GetUser(organizationOther.Part.CreatorId).Fullname;
            invItem.CreationDate = organizationOther.Part.CreateDate;

            if (organizationOther.PartOperation == PartOperation.Update)
            {
                invItem.EditedBy = this.userRepository.GetUser(organizationOther.CreatorId).Fullname;
                invItem.EditedDate = organizationOther.CreateDate;
            }

            return invItem;
        }
    }
}
