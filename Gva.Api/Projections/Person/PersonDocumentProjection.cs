using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Common;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.OrganizationRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonDocumentProjection : Projection<GvaViewPersonDocument>
    {
        private IUserRepository userRepository;
        private INomRepository nomRepository;
        private IOrganizationRepository organizationRepository;

        public PersonDocumentProjection(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            INomRepository nomRepository,
            IOrganizationRepository organizationRepository)
            : base(unitOfWork, "Person")
        {
            this.userRepository = userRepository;
            this.nomRepository = nomRepository;
            this.organizationRepository = organizationRepository;
        }

        public override IEnumerable<GvaViewPersonDocument> Execute(PartCollection parts)
        {
            int validTrueId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId;
            var personData = parts.Get<PersonDataDO>("personData");
            int personEducationRoleId = this.nomRepository.GetNomValue("documentRoles", "personEducation").NomValueId;
            int personEmploymentRoleId = this.nomRepository.GetNomValue("documentRoles", "personEmployment").NomValueId;
            int personDocumentIdRoleId = this.nomRepository.GetNomValue("documentRoles", "personDocumentId").NomValueId;
            int personLicenceRoleId = this.nomRepository.GetNomValue("documentRoles", "personLicence").NomValueId;
            int personMedicalRoleId = this.nomRepository.GetNomValue("documentRoles", "personMedical").NomValueId;
            int personApplicationRoleId = this.nomRepository.GetNomValue("documentRoles", "personApplication").NomValueId;
            int personReportRoleId = this.nomRepository.GetNomValue("documentRoles", "personReport").NomValueId;
            int personStatusRoleId = this.nomRepository.GetNomValue("documentRoles", "personStatus").NomValueId;

            var documentIds = parts.GetAll<PersonDocumentIdDO>("personDocumentIds").Select(d => this.Create(d, personDocumentIdRoleId, validTrueId));
            var checks = parts.GetAll<PersonCheckDO>("personDocumentChecks").Select(d => this.Create(d, validTrueId));
            var trainings = parts.GetAll<PersonTrainingDO>("personDocumentTrainings").Select(d => this.Create(d, validTrueId));
            var others = parts.GetAll<PersonDocumentOtherDO>("personDocumentOthers").Select(d => this.Create(d, validTrueId));
            var langCertificates = parts.GetAll<PersonLangCertDO>("personDocumentLangCertificates").Select(d => this.Create(d, validTrueId));
            var statuses = parts.GetAll<PersonStatusDO>("personStatuses").Select(d => this.Create(d, personStatusRoleId));
            var medicals = parts.GetAll<PersonMedicalDO>("personDocumentMedicals").Select(d => this.Create(d, personData, personMedicalRoleId));
            var reports = parts.GetAll<PersonReportDO>("personReports").Select(d => this.Create(d, personReportRoleId));
            var employments = parts.GetAll<PersonEmploymentDO>("personDocumentEmployments").Select(d => this.Create(d, personEmploymentRoleId, validTrueId));
            var educations = parts.GetAll<PersonEducationDO>("personDocumentEducations").Select(d => this.Create(d, personEducationRoleId));
            var applications = parts.GetAll<DocumentApplicationDO>("personDocumentApplications").Select(d => this.Create(d, personApplicationRoleId));

            var licences = parts.GetAll<PersonLicenceDO>("licences");
            var editions = parts.GetAll<PersonLicenceEditionDO>("licenceEditions");

            List<GvaViewPersonDocument> licenceDocs = new List<GvaViewPersonDocument>();
            foreach (var licence in licences)
            {
                var licenceEditions = editions.Where(e => e.Content.LicencePartIndex == licence.Part.Index);

                foreach (var edition in licenceEditions)
                {
                    licenceDocs.Add(this.Create(licence, edition, personLicenceRoleId, validTrueId));
                }
            }

            return documentIds
                .Union(checks)
                .Union(trainings)
                .Union(others)
                .Union(langCertificates)
                .Union(statuses)
                .Union(medicals)
                .Union(reports)
                .Union(employments)
                .Union(educations)
                .Union(applications)
                .Union(licenceDocs);
        }

        private GvaViewPersonDocument Create(PartVersion<PersonDocumentIdDO> personDocumentId, int roleId, int validTrueId)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();

            document.LotId = personDocumentId.Part.Lot.LotId;
            document.PartId = personDocumentId.Part.PartId;
            document.SetPartAlias = personDocumentId.Part.SetPart.Alias;
            document.RoleId = roleId;
            document.TypeId = personDocumentId.Content.DocumentTypeId;
            document.Date = personDocumentId.Content.DocumentDateValidFrom;
            document.Publisher = personDocumentId.Content.DocumentPublisher;
            document.Valid = personDocumentId.Content.ValidId == validTrueId;
            document.FromDate = personDocumentId.Content.DocumentDateValidFrom;
            document.ToDate = personDocumentId.Content.DocumentDateValidTo;
            document.DocumentNumber = personDocumentId.Content.DocumentNumber;

            document.CreatedBy = this.userRepository.GetUser(personDocumentId.Part.CreatorId).Fullname;
            document.CreationDate = personDocumentId.Part.CreateDate;

            if (personDocumentId.PartOperation == PartOperation.Update)
            {
                document.EditedBy = this.userRepository.GetUser(personDocumentId.CreatorId).Fullname;
                document.EditedDate = personDocumentId.CreateDate;
            }

            return document;
        }

        private GvaViewPersonDocument Create(PartVersion<PersonCheckDO> personCheck, int validTrueId)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();

            document.LotId = personCheck.Part.Lot.LotId;
            document.PartId = personCheck.Part.PartId;
            document.SetPartAlias = personCheck.Part.SetPart.Alias;
            document.RoleId = personCheck.Content.DocumentRoleId;
            document.TypeId = personCheck.Content.DocumentTypeId;
            document.DocumentNumber = personCheck.Content.DocumentNumber;
            document.DocumentPersonNumber = personCheck.Content.DocumentPersonNumber;
            document.Date = personCheck.Content.DocumentDateValidFrom.Value;
            document.Publisher = personCheck.Content.DocumentPublisher;
            document.Valid = personCheck.Content.ValidId.HasValue ? personCheck.Content.ValidId.Value == validTrueId : (bool?)null;
            document.FromDate = personCheck.Content.DocumentDateValidFrom.Value;
            document.ToDate = personCheck.Content.DocumentDateValidTo;
            document.Notes = personCheck.Content.Notes;

            document.CreatedBy = this.userRepository.GetUser(personCheck.Part.CreatorId).Fullname;
            document.CreationDate = personCheck.Part.CreateDate;

            if (personCheck.PartOperation == PartOperation.Update)
            {
                document.EditedBy = this.userRepository.GetUser(personCheck.CreatorId).Fullname;
                document.EditedDate = personCheck.CreateDate;
            }

            return document;
        }

        private GvaViewPersonDocument Create(PartVersion<PersonTrainingDO> personTraining, int validTrueId)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();
            document.LotId = personTraining.Part.Lot.LotId;
            document.PartId = personTraining.Part.PartId;
            document.SetPartAlias = personTraining.Part.SetPart.Alias;
            document.RoleId = personTraining.Content.DocumentRoleId;
            document.TypeId = personTraining.Content.DocumentTypeId;
            document.DocumentNumber = personTraining.Content.DocumentNumber;
            document.DocumentPersonNumber = personTraining.Content.DocumentPersonNumber;
            document.Date = personTraining.Content.DocumentDateValidFrom.Value;
            document.Publisher = personTraining.Content.DocumentPublisher;
            document.Valid = personTraining.Content.ValidId.HasValue ? personTraining.Content.ValidId.Value == validTrueId : (bool?)null;
            document.FromDate = personTraining.Content.DocumentDateValidFrom.Value;
            document.ToDate = personTraining.Content.DocumentDateValidTo;
            document.Notes = personTraining.Content.Notes;

            document.CreatedBy = this.userRepository.GetUser(personTraining.Part.CreatorId).Fullname;
            document.CreationDate = personTraining.Part.CreateDate;

            if (personTraining.PartOperation == PartOperation.Update)
            {
                document.EditedBy = this.userRepository.GetUser(personTraining.CreatorId).Fullname;
                document.EditedDate = personTraining.CreateDate;
            }

            return document;
        }

        private GvaViewPersonDocument Create(PartVersion<PersonDocumentOtherDO> personOther, int validTrueId)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();

            document.LotId = personOther.Part.Lot.LotId;
            document.PartId = personOther.Part.PartId;
            document.SetPartAlias = personOther.Part.SetPart.Alias;
            document.RoleId = personOther.Content.DocumentRoleId;
            document.TypeId = personOther.Content.DocumentTypeId;
            document.DocumentNumber = personOther.Content.DocumentNumber;
            document.DocumentPersonNumber = personOther.Content.DocumentPersonNumber;
            document.Date = personOther.Content.DocumentDateValidFrom;
            document.Publisher = personOther.Content.DocumentPublisher;
            document.Valid = personOther.Content.ValidId.HasValue ? personOther.Content.ValidId.Value == validTrueId : (bool?)null;
            document.FromDate = personOther.Content.DocumentDateValidFrom;
            document.ToDate = personOther.Content.DocumentDateValidTo;
            document.Notes = personOther.Content.Notes;

            document.CreatedBy = this.userRepository.GetUser(personOther.Part.CreatorId).Fullname;
            document.CreationDate = personOther.Part.CreateDate;

            if (personOther.PartOperation == PartOperation.Update)
            {
                document.EditedBy = this.userRepository.GetUser(personOther.CreatorId).Fullname;
                document.EditedDate = personOther.CreateDate;
            }

            return document;
        }

        private GvaViewPersonDocument Create(PartVersion<PersonLangCertDO> langCert, int validTrueId)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();

            document.LotId = langCert.Part.Lot.LotId;
            document.PartId = langCert.Part.PartId;
            document.SetPartAlias = langCert.Part.SetPart.Alias;
            document.RoleId = langCert.Content.DocumentRoleId;
            document.TypeId = langCert.Content.DocumentTypeId;
            document.DocumentNumber = langCert.Content.DocumentNumber;
            document.DocumentPersonNumber = langCert.Content.DocumentPersonNumber;
            document.Date = langCert.Content.DocumentDateValidFrom;
            document.Publisher = langCert.Content.DocumentPublisher;
            document.Valid = langCert.Content.ValidId.HasValue ? langCert.Content.ValidId.Value == validTrueId : (bool?)null;
            document.FromDate = langCert.Content.DocumentDateValidFrom;
            document.ToDate = langCert.Content.DocumentDateValidTo;
            document.Notes = langCert.Content.Notes;

            document.CreatedBy = this.userRepository.GetUser(langCert.Part.CreatorId).Fullname;
            document.CreationDate = langCert.Part.CreateDate;

            if (langCert.PartOperation == PartOperation.Update)
            {
                document.EditedBy = this.userRepository.GetUser(langCert.CreatorId).Fullname;
                document.EditedDate = langCert.CreateDate;
            }

            return document;
        }

        private GvaViewPersonDocument Create(PartVersion<PersonStatusDO> status, int roleId)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();
            document.LotId = status.Part.Lot.LotId;
            document.PartId = status.Part.PartId;
            document.SetPartAlias = status.Part.SetPart.Alias;
            document.DocumentNumber = status.Content.DocumentNumber;
            document.Date = status.Content.DocumentDateValidFrom;
            document.RoleId = roleId;
            document.FromDate = status.Content.DocumentDateValidFrom;
            document.ToDate = status.Content.DocumentDateValidTo;
            document.Notes = status.Content.Notes;

            document.CreatedBy = this.userRepository.GetUser(status.Part.CreatorId).Fullname;
            document.CreationDate = status.Part.CreateDate;

            if (status.PartOperation == PartOperation.Update)
            {
                document.EditedBy = this.userRepository.GetUser(status.CreatorId).Fullname;
                document.EditedDate = status.CreateDate;
            }

            return document;
        }

        private GvaViewPersonDocument Create
            (PartVersion<PersonMedicalDO> personMedical,
            PartVersion<PersonDataDO> personData,
            int roleId)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();
            document.LotId = personMedical.Part.Lot.LotId;
            document.PartId = personMedical.Part.PartId;
            document.SetPartAlias = personMedical.Part.SetPart.Alias;
            document.RoleId = roleId;
            document.DocumentNumber = string.Format(
                    "{0}-{1}-{2}-{3}",
                    personMedical.Content.DocumentNumberPrefix,
                    personMedical.Content.DocumentNumber,
                    personData.Content.Lin,
                    personMedical.Content.DocumentNumberSuffix);

            document.Date = personMedical.Content.DocumentDateValidFrom.Value;
            document.Publisher = personMedical.Content.DocumentPublisher.Name;
            document.Valid = null;
            document.FromDate = personMedical.Content.DocumentDateValidFrom.Value;
            document.ToDate = personMedical.Content.DocumentDateValidTo.Value;
            document.Notes = personMedical.Content.Notes;
            document.Limitations = personMedical.Content.Limitations.Count() > 0 ? string.Join(GvaConstants.ConcatenatingExp, personMedical.Content.Limitations.Select(l => l.Name)) : null;
            document.MedClassId = personMedical.Content.MedClass != null ? personMedical.Content.MedClass.NomValueId : (int?)null;
            document.CreatedBy = this.userRepository.GetUser(personMedical.Part.CreatorId).Fullname;
            document.CreationDate = personMedical.Part.CreateDate;

            if (personMedical.PartOperation == PartOperation.Update)
            {
                document.EditedBy = this.userRepository.GetUser(personMedical.CreatorId).Fullname;
                document.EditedDate = personMedical.CreateDate;
            }

            return document;
        }

        private GvaViewPersonDocument Create(PartVersion<PersonReportDO> report, int roleId)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();
            document.LotId = report.Part.Lot.LotId;
            document.PartId = report.Part.PartId;
            document.SetPartAlias = report.Part.SetPart.Alias;
            document.DocumentNumber = report.Content.DocumentNumber;
            document.Date = report.Content.Date;
            document.RoleId = roleId;
            document.FromDate = report.Content.Date;
            document.CreatedBy = this.userRepository.GetUser(report.Part.CreatorId).Fullname;

            document.CreationDate = report.Part.CreateDate;

            if (report.PartOperation == PartOperation.Update)
            {
                document.EditedBy = this.userRepository.GetUser(report.CreatorId).Fullname;
                document.EditedDate = report.CreateDate;
            }

            return document;
        }

        private GvaViewPersonDocument Create(PartVersion<PersonEmploymentDO> personEmployment, int roleId, int validTrueId)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();
            document.LotId = personEmployment.Part.Lot.LotId;
            document.PartId = personEmployment.Part.PartId;
            document.SetPartAlias = personEmployment.Part.SetPart.Alias;
            document.RoleId = roleId;
            document.Date = personEmployment.Content.Hiredate.Value;

            var organization = personEmployment.Content.OrganizationId.HasValue ?
                        this.organizationRepository.GetOrganization(personEmployment.Content.OrganizationId.Value) : null;
            NomValue organizationNom = organization != null ?
                new NomValue()
                {
                    NomValueId = organization.LotId,
                    Name = organization.Name,
                    NameAlt = organization.NameAlt
                } : null;

            document.Publisher = !personEmployment.Content.OrganizationId.HasValue ? null : organizationNom.Name;
            document.Valid = personEmployment.Content.ValidId == validTrueId;
            document.FromDate = personEmployment.Content.Hiredate.Value;
            document.ToDate = null;
            document.Notes = personEmployment.Content.Notes;

            document.CreatedBy = this.userRepository.GetUser(personEmployment.Part.CreatorId).Fullname;
            document.CreationDate = personEmployment.Part.CreateDate;

            if (personEmployment.PartOperation == PartOperation.Update)
            {
                document.EditedBy = this.userRepository.GetUser(personEmployment.CreatorId).Fullname;
                document.EditedDate = personEmployment.CreateDate;
            }

            return document;
        }

        private GvaViewPersonDocument Create(PartVersion<PersonEducationDO> personEducation, int roleId)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();
            document.LotId = personEducation.Part.Lot.LotId;
            document.PartId = personEducation.Part.PartId;
            document.SetPartAlias = personEducation.Part.SetPart.Alias;
            document.RoleId = roleId;
            document.DocumentNumber = personEducation.Content.DocumentNumber;
            document.Date = personEducation.Content.CompletionDate.Value;
            document.Publisher = personEducation.Content.SchoolId.HasValue ? this.nomRepository.GetNomValue("schools", personEducation.Content.SchoolId.Value).Name : null;
            document.FromDate = document.Date = personEducation.Content.CompletionDate.Value;
            document.CreatedBy = this.userRepository.GetUser(personEducation.Part.CreatorId).Fullname;
            document.CreationDate = personEducation.Part.CreateDate;

            if (personEducation.PartOperation == PartOperation.Update)
            {
                document.EditedBy = this.userRepository.GetUser(personEducation.CreatorId).Fullname;
                document.EditedDate = personEducation.CreateDate;
            }

            return document;
        }

        private GvaViewPersonDocument Create(PartVersion<DocumentApplicationDO> personApplication, int roleId)
        {
            GvaViewPersonDocument document = new GvaViewPersonDocument();

            document.LotId = personApplication.Part.Lot.LotId;
            document.PartId = personApplication.Part.PartId;
            document.SetPartAlias = personApplication.Part.SetPart.Alias;
            document.RoleId = roleId;
            document.DocumentNumber = personApplication.Content.DocumentNumber;
            document.Date = personApplication.Content.DocumentDate.Value;
            document.Publisher = null;
            document.Valid = null;
            document.FromDate = personApplication.Content.DocumentDate.Value;
            document.ToDate = null;
            document.Notes = personApplication.Content.Notes;

            document.CreatedBy = this.userRepository.GetUser(personApplication.Part.CreatorId).Fullname;
            document.CreationDate = personApplication.Part.CreateDate;

            if (personApplication.PartOperation == PartOperation.Update)
            {
                document.EditedBy = this.userRepository.GetUser(personApplication.CreatorId).Fullname;
                document.EditedDate = personApplication.CreateDate;
            }

            return document;
        }

        private GvaViewPersonDocument Create(PartVersion<PersonLicenceDO> personLicence, PartVersion<PersonLicenceEditionDO> edition, int roleId, int validTrueId)
        {
            var licenceType = this.nomRepository.GetNomValue(personLicence.Content.LicenceTypeId.Value);

            GvaViewPersonDocument document = new GvaViewPersonDocument();

            document.LotId = personLicence.Part.Lot.LotId;
            document.PartId = edition.Part.PartId;
            document.ParentPartId = personLicence.Part.PartId;
            document.SetPartAlias = personLicence.Part.SetPart.Alias;
            document.RoleId = roleId;

            string publisherCode = null;
            if(personLicence.Content.PublisherId.HasValue)
            {
                publisherCode = this.nomRepository.GetNomValue(personLicence.Content.PublisherId.Value).Code;
            }
            document.DocumentNumber = personLicence.Content.LicenceNumber.HasValue ?
                string.Format("{0} {1} - {2}", publisherCode, licenceType.TextContent.Get<string>("codeCA"), personLicence.Content.LicenceNumber) :
                null;
            document.Date = edition.Content.DocumentDateValidFrom.Value;
            document.Publisher =  publisherCode;
            document.Valid = !personLicence.Content.ValidId.HasValue ? (bool?)null : personLicence.Content.ValidId.Value == validTrueId;
            document.FromDate = edition.Content.DocumentDateValidFrom.Value;
            document.ToDate = edition.Content.DocumentDateValidTo;
            document.Notes = edition.Content.Notes;

            document.CreatedBy = this.userRepository.GetUser(personLicence.Part.CreatorId).Fullname;
            document.CreationDate = personLicence.Part.CreateDate;

            if (personLicence.PartOperation == PartOperation.Update)
            {
                document.EditedBy = this.userRepository.GetUser(personLicence.CreatorId).Fullname;
                document.EditedDate = personLicence.CreateDate;
            }

            return document;
        }
    }
}
