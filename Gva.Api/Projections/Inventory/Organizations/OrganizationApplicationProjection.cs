using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views;
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
            var applications = parts.GetAll("organizationDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewInventoryItem Create(PartVersion organizationApplication)
        {
            GvaViewInventoryItem invItem = new GvaViewInventoryItem();

            invItem.LotId = organizationApplication.Part.Lot.LotId;
            invItem.PartId = organizationApplication.Part.PartId;
            invItem.SetPartAlias = organizationApplication.Part.SetPart.Alias;
            invItem.Name = organizationApplication.Part.SetPart.Name;
            invItem.TypeId = organizationApplication.Content.Get<int>("applicationType.nomValueId");
            invItem.Number = organizationApplication.Content.Get<string>("documentNumber");
            invItem.Date = organizationApplication.Content.Get<DateTime>("documentDate");
            invItem.Publisher = null;
            invItem.Valid = null;
            invItem.FromDate = null;
            invItem.ToDate = null;

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
