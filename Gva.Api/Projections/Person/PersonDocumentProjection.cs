using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Person;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Projections.Person
{
    public class PersonDocumentProjection : Projection<GvaViewPersonDocument>
    {
        public PersonDocumentProjection(
            IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
        }

        public override IEnumerable<GvaViewPersonDocument> Execute(PartCollection parts)
        {
            List<PartVersion<PersonDocumentDO>> documents = new List<PartVersion<PersonDocumentDO>>();
            var documentIds = parts.GetAll<PersonDocumentDO>("personDocumentIds").Where(d => d.Content.DocumentNumber != null);
            var checks = parts.GetAll<PersonDocumentDO>("personDocumentChecks").Where(d => d.Content.DocumentNumber != null);
            var educations = parts.GetAll<PersonDocumentDO>("personDocumentEducations").Where(d => d.Content.DocumentNumber != null);
            var trainings = parts.GetAll<PersonDocumentDO>("personDocumentTrainings").Where(d => d.Content.DocumentNumber != null);
            var others = parts.GetAll<PersonDocumentDO>("personDocumentOthers").Where(d => d.Content.DocumentNumber != null);
            var langCertificates = parts.GetAll<PersonDocumentDO>("personDocumentLangCertificates").Where(d => d.Content.DocumentNumber != null);
            var statuses = parts.GetAll<PersonDocumentDO>("personStatuses").Where(d => d.Content.DocumentNumber != null);
            var medicals = parts.GetAll<PersonDocumentDO>("personDocumentMedicals").Where(d => d.Content.DocumentNumber != null);
            var applications = parts.GetAll<PersonDocumentDO>("personDocumentApplications").Where(d => d.Content.DocumentNumber != null);

            documents = documentIds
                .Union(checks)
                .Union(educations)
                .Union(trainings)
                .Union(others)
                .Union(langCertificates)
                .Union(statuses)
                .Union(medicals)
                .Union(applications)
                .ToList();

            return documents.Select(r => this.Create(r));
        }

        private GvaViewPersonDocument Create(PartVersion<PersonDocumentDO> personDocument)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();

            document.LotId = personDocument.Part.Lot.LotId;
            document.PartIndex = personDocument.Part.Index;
            document.DocumentNumber = personDocument.Content.DocumentNumber;
            document.DocumentPersonNumber = personDocument.Content.DocumentPersonNumber;

            return document;
        }
    }
}
