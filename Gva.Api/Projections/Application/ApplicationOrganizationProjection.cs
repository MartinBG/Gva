using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Common;
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
            var applications = parts.GetAll<DocumentApplicationDO>("organizationDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewApplication Create(PartVersion<DocumentApplicationDO> organizationApplication)
        {
            GvaViewApplication application = new GvaViewApplication();

            application.LotId = organizationApplication.Part.Lot.LotId;
            application.PartId = organizationApplication.Part.PartId;
            application.DocumentDate = organizationApplication.Content.DocumentDate;
            application.DocumentNumber = organizationApplication.Content.DocumentNumber;
            application.ApplicationTypeId = organizationApplication.Content.ApplicationType.NomValueId;

            return application;
        }
    }
}
