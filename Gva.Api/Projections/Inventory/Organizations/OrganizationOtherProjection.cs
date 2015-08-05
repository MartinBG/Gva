using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.NomRepository;
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
        private INomRepository nomRepository;

        public OrganizationOtherProjection(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            INomRepository nomRepository)
            : base(unitOfWork, "Organization")
        {
            this.userRepository = userRepository;
            this.nomRepository = nomRepository;

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
            invItem.Name = organizationOther.Content.DocumentRoleId.HasValue ? this.nomRepository.GetNomValue("documentRoles", organizationOther.Content.DocumentRoleId.Value).Name : null;
            invItem.TypeId = organizationOther.Content.DocumentTypeId;
            invItem.Number = organizationOther.Content.DocumentNumber;
            invItem.Date = organizationOther.Content.DocumentDateValidFrom.Value;
            invItem.Publisher = organizationOther.Content.DocumentPublisher;
            invItem.Valid = !organizationOther.Content.ValidId.HasValue ? (bool?)null : this.nomRepository.GetNomValue("boolean", organizationOther.Content.ValidId.Value).Code == "Y";
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
