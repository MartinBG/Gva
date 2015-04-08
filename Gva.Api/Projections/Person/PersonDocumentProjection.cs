using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Gva.Api.Models.Views.Person;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Gva.Api.ModelsDO.Persons;

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
            var documentIds = parts.GetAll<PersonDocumentIdDO>("personDocumentIds")
                .Select(d => new PersonDocumentDO()
                {
                    LotId = d.Part.LotId,
                    PartIndex = d.Part.Index,
                    DocumentTypeId = d.Content.DocumentType.NomValueId,
                    DocumentDateValidFrom = d.Content.DocumentDateValidFrom,
                    DocumentPublisher = d.Content.DocumentPublisher,
                    DocumentNumber = d.Content.DocumentNumber
                });

            var checks = parts.GetAll<PersonCheckDO>("personDocumentChecks")
                 .Select(d => new PersonDocumentDO()
                 {
                     LotId = d.Part.LotId,
                     PartIndex = d.Part.Index,
                     DocumentTypeId = d.Content.DocumentType.NomValueId,
                     DocumentDateValidFrom = d.Content.DocumentDateValidFrom,
                     DocumentPublisher = d.Content.DocumentPublisher,
                     DocumentRoleId = d.Content.DocumentRole.NomValueId,
                     DocumentNumber = d.Content.DocumentNumber,
                     DocumentPersonNumber = d.Content.DocumentPersonNumber
                 });

            var educations = parts.GetAll<PersonEducationDO>("personDocumentEducations")
                .Select(d => new PersonDocumentDO()
                {
                    LotId = d.Part.LotId,
                    PartIndex = d.Part.Index,
                    DocumentNumber = d.Content.DocumentNumber
                });

            var trainings = parts.GetAll<PersonTrainingDO>("personDocumentTrainings")
                .Select(d => new PersonDocumentDO()
                {
                    LotId = d.Part.LotId,
                    PartIndex = d.Part.Index,
                    DocumentTypeId = d.Content.DocumentType.NomValueId,
                    DocumentDateValidFrom = d.Content.DocumentDateValidFrom,
                    DocumentPublisher = d.Content.DocumentPublisher,
                    DocumentRoleId = d.Content.DocumentRole.NomValueId,
                    DocumentNumber = d.Content.DocumentNumber,
                    DocumentPersonNumber = d.Content.DocumentPersonNumber
                });

            var others = parts.GetAll<PersonDocumentOtherDO>("personDocumentOthers")
                .Select(d => new PersonDocumentDO()
                {
                    LotId = d.Part.LotId,
                    PartIndex = d.Part.Index,
                    DocumentTypeId = d.Content.DocumentType.NomValueId,
                    DocumentDateValidFrom = d.Content.DocumentDateValidFrom,
                    DocumentPublisher = d.Content.DocumentPublisher,
                    DocumentRoleId = d.Content.DocumentRole.NomValueId,
                    DocumentNumber = d.Content.DocumentNumber,
                    DocumentPersonNumber = d.Content.DocumentPersonNumber
                });

            var langCertificates = parts.GetAll<PersonLangCertDO>("personDocumentLangCertificates")
                .Select(d => new PersonDocumentDO()
                {
                    LotId = d.Part.LotId,
                    PartIndex = d.Part.Index,
                    DocumentTypeId = d.Content.DocumentType.NomValueId,
                    DocumentDateValidFrom = d.Content.DocumentDateValidFrom,
                    DocumentPublisher = d.Content.DocumentPublisher,
                    DocumentRoleId = d.Content.DocumentRole.NomValueId,
                    DocumentNumber = d.Content.DocumentNumber,
                    DocumentPersonNumber = d.Content.DocumentPersonNumber
                });

            var statuses = parts.GetAll<PersonStatusDO>("personStatuses")
                .Select(d => new PersonDocumentDO()
                {
                    LotId = d.Part.LotId,
                    PartIndex = d.Part.Index,
                    DocumentDateValidFrom = d.Content.DocumentDateValidFrom,
                    DocumentNumber = d.Content.DocumentNumber
                });

            var medicals = parts.GetAll<PersonMedicalDO>("personDocumentMedicals")
                .Select(d => new PersonDocumentDO()
                {
                    LotId = d.Part.LotId,
                    PartIndex = d.Part.Index,
                    DocumentDateValidFrom = d.Content.DocumentDateValidFrom,
                    DocumentPublisher = d.Content.DocumentPublisher.Name,
                    DocumentNumber = d.Content.DocumentNumber
                });

            var reports = parts.GetAll<PersonReportDO>("personReports")
                 .Select(d => new PersonDocumentDO()
                 {
                     LotId = d.Part.LotId,
                     PartIndex = d.Part.Index,
                     DocumentDateValidFrom = d.Content.Date,
                     DocumentNumber = d.Content.DocumentNumber
                 });

            return documentIds
                .Union(checks)
                .Union(educations)
                .Union(trainings)
                .Union(others)
                .Union(langCertificates)
                .Union(statuses)
                .Union(medicals)
                .Union(reports)
                .Select(r => this.Create(r));
        }

        private GvaViewPersonDocument Create(PersonDocumentDO personDocument)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();

            document.LotId = personDocument.LotId;
            document.PartIndex = personDocument.PartIndex;
            document.DocumentNumber = personDocument.DocumentNumber;
            document.DocumentPersonNumber = personDocument.DocumentPersonNumber;
            document.DateValidFrom = personDocument.DocumentDateValidFrom.HasValue? personDocument.DocumentDateValidFrom.Value.Date : (DateTime?)null;
            document.RoleId = personDocument.DocumentRoleId;
            document.TypeId = personDocument.DocumentTypeId;
            document.Publisher = personDocument.DocumentPublisher;

            return document;
        }
    }
}
