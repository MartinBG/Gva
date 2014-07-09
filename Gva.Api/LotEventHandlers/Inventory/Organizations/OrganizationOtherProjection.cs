using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.Inventory.Organizations
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
            var others = parts.GetAll("organizationDocumentOthers");

            return others.Select(o => this.Create(o));
        }

        private GvaViewInventoryItem Create(PartVersion organizationOther)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = organizationOther.Part.Lot.LotId;
            invItem.PartId = organizationOther.Part.PartId;
            invItem.SetPartAlias = organizationOther.Part.SetPart.Alias;

            invItem.Name = organizationOther.Content.Get<string>("documentRole.name");
            invItem.TypeId = organizationOther.Content.Get<int>("documentType.nomValueId");
            invItem.Number = organizationOther.Content.Get<string>("documentNumber");
            invItem.Date = organizationOther.Content.Get<DateTime>("documentDateValidFrom");
            invItem.Publisher = organizationOther.Content.Get<string>("documentPublisher");
            invItem.Valid = organizationOther.Content.Get<string>("valid.code") == "Y";
            invItem.FromDate = organizationOther.Content.Get<DateTime>("documentDateValidFrom");
            invItem.ToDate = organizationOther.Content.Get<DateTime?>("documentDateValidTo");

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
