using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Application
{
    public class ApplicationOrganizationProjection : Projection<GvaViewApplication>
    {
        public ApplicationOrganizationProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Organization")
        {
        }

        public override IEnumerable<GvaViewApplication> Execute(PartCollection parts)
        {
            var applications = parts.GetAll("organizationDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewApplication Create(PartVersion organizationApplication)
        {
            GvaViewApplication application = new GvaViewApplication();

            application.LotId = organizationApplication.Part.Lot.LotId;
            application.PartId = organizationApplication.Part.PartId;
            application.DocumentDate = organizationApplication.Content.Get<DateTime?>("documentDate");
            application.DocumentNumber = organizationApplication.Content.Get<string>("documentNumber");
            application.ApplicationTypeId = organizationApplication.Content.Get<int>("applicationType.nomValueId");

            return application;
        }
    }
}
