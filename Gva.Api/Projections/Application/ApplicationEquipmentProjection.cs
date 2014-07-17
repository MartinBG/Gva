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
    public class ApplicationEquipmentProjection : Projection<GvaViewApplication>
    {
        public ApplicationEquipmentProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Equipment")
        {
        }

        public override IEnumerable<GvaViewApplication> Execute(PartCollection parts)
        {
            var applications = parts.GetAll("equipmentDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewApplication Create(PartVersion equipmentApplication)
        {
            GvaViewApplication application = new GvaViewApplication();

            application.LotId = equipmentApplication.Part.Lot.LotId;
            application.PartId = equipmentApplication.Part.PartId;
            application.DocumentDate = equipmentApplication.Content.Get<DateTime?>("documentDate");
            application.DocumentNumber = equipmentApplication.Content.Get<string>("documentNumber");
            application.ApplicationTypeId = equipmentApplication.Content.Get<int>("applicationType.nomValueId");

            return application;
        }
    }
}
