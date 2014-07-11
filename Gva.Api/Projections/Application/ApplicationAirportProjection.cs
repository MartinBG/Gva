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
    public class ApplicationAirportProjection : Projection<GvaViewApplication>
    {
        public ApplicationAirportProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Airport")
        {
        }

        public override IEnumerable<GvaViewApplication> Execute(PartCollection parts)
        {
            var applications = parts.GetAll("airportDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewApplication Create(PartVersion airportApplication)
        {
            GvaViewApplication application = new GvaViewApplication();

            application.LotId = airportApplication.Part.Lot.LotId;
            application.PartId = airportApplication.Part.PartId;
            application.RequestDate = airportApplication.Content.Get<DateTime?>("requestDate");
            application.DocumentNumber = airportApplication.Content.Get<string>("documentNumber");
            application.ApplicationTypeId = airportApplication.Content.Get<int>("applicationType.nomValueId");

            return application;
        }
    }
}
