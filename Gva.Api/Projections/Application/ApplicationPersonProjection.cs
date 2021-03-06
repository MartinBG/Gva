﻿using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Common;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Application
{
    public class ApplicationPersonProjection : Projection<GvaViewApplication>
    {
        private IUnitOfWork unitOfWork;

        public ApplicationPersonProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
            this.unitOfWork = unitOfWork;
        }

        public override IEnumerable<GvaViewApplication> Execute(PartCollection parts)
        {
            var applications = parts.GetAll<DocumentApplicationDO>("personDocumentApplications");

            return applications.Select(a => this.Create(a));
        }

        private GvaViewApplication Create(PartVersion<DocumentApplicationDO> personApplication)
        {
            GvaViewApplication application = new GvaViewApplication();

            application.LotId = personApplication.Part.Lot.LotId;
            application.PartId = personApplication.Part.PartId;
            application.DocumentDate = personApplication.Content.DocumentDate;
            application.DocumentNumber = personApplication.Content.DocumentNumber;
            application.OldDocumentNumber = personApplication.Content.OldDocumentNumber;
            application.ApplicationTypeId = personApplication.Content.ApplicationType.NomValueId;
            application.ControlCardKey = personApplication.Content.ControlCard != null ? personApplication.Content.ControlCard.Key : (Guid?)null;

            return application;
        }
    }
}
