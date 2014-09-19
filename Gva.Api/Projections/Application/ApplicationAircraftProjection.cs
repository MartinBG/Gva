using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Common;
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
            var applications = parts.GetAll<DocumentApplicationDO>("aircraftDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewApplication Create(PartVersion<DocumentApplicationDO> aircraftApplication)
        {
            GvaViewApplication application = new GvaViewApplication();

            application.LotId = aircraftApplication.Part.Lot.LotId;
            application.PartId = aircraftApplication.Part.PartId;
            application.DocumentDate = aircraftApplication.Content.DocumentDate;
            application.DocumentNumber = aircraftApplication.Content.DocumentNumber;
            application.ApplicationTypeId = aircraftApplication.Content.ApplicationType.NomValueId;

            return application;
        }
    }
}
