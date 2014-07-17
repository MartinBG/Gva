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
    public class ApplicationPersonProjection : Projection<GvaViewApplication>
    {
        public ApplicationPersonProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
        }

        public override IEnumerable<GvaViewApplication> Execute(PartCollection parts)
        {
            var applications = parts.GetAll("personDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewApplication Create(PartVersion personApplication)
        {
            GvaViewApplication application = new GvaViewApplication();

            application.LotId = personApplication.Part.Lot.LotId;
            application.PartId = personApplication.Part.PartId;
            application.DocumentDate = personApplication.Content.Get<DateTime?>("documentDate");
            application.DocumentNumber = personApplication.Content.Get<string>("documentNumber");
            application.ApplicationTypeId = personApplication.Content.Get<int>("applicationType.nomValueId");

            return application;
        }
    }
}
