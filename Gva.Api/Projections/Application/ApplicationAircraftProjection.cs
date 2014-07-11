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
    public class ApplicationAircraftProjection : Projection<GvaViewApplication>
    {
        public ApplicationAircraftProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Aircraft")
        {
        }

        public override IEnumerable<GvaViewApplication> Execute(PartCollection parts)
        {
            var applications = parts.GetAll("aircraftDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewApplication Create(PartVersion aircraftApplication)
        {
            GvaViewApplication application = new GvaViewApplication();

            application.LotId = aircraftApplication.Part.Lot.LotId;
            application.PartId = aircraftApplication.Part.PartId;
            application.RequestDate = aircraftApplication.Content.Get<DateTime?>("requestDate");
            application.DocumentNumber = aircraftApplication.Content.Get<string>("documentNumber");
            application.ApplicationTypeId = aircraftApplication.Content.Get<int>("applicationType.nomValueId");

            return application;
        }
    }
}
