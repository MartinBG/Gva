using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Common;
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
            var applications = parts.GetAll<DocumentApplicationDO>("airportDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewApplication Create(PartVersion<DocumentApplicationDO> airportApplication)
        {
            GvaViewApplication application = new GvaViewApplication();

            application.LotId = airportApplication.Part.Lot.LotId;
            application.PartId = airportApplication.Part.PartId;
            application.DocumentDate = airportApplication.Content.DocumentDate;
            application.OldDocumentNumber = airportApplication.Content.OldDocumentNumber;
            application.DocumentNumber = airportApplication.Content.DocumentNumber;
            application.ApplicationTypeId = airportApplication.Content.ApplicationType.NomValueId;

            return application;
        }
    }
}
