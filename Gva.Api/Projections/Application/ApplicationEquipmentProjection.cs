using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Common;
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
            var applications = parts.GetAll<DocumentApplicationDO>("equipmentDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewApplication Create(PartVersion<DocumentApplicationDO> equipmentApplication)
        {
            GvaViewApplication application = new GvaViewApplication();

            application.LotId = equipmentApplication.Part.Lot.LotId;
            application.PartId = equipmentApplication.Part.PartId;
            application.DocumentDate = equipmentApplication.Content.DocumentDate;
            application.DocumentNumber = equipmentApplication.Content.DocumentNumber;
            application.OldDocumentNumber = equipmentApplication.Content.OldDocumentNumber;
            application.ApplicationTypeId = equipmentApplication.Content.ApplicationType.NomValueId;

            return application;
        }
    }
}
