using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.FileRepository;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonLicenceEditionProjection : Projection<GvaViewPersonLicenceEdition>
    {
        private INomRepository nomRepository;
        private IFileRepository fileRepository;

        public PersonLicenceEditionProjection(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository,
            IFileRepository fileRepository)
            : base(unitOfWork, "Person")
        {
            this.nomRepository = nomRepository;
            this.fileRepository = fileRepository;
        }

        public override IEnumerable<GvaViewPersonLicenceEdition> Execute(PartCollection parts)
        {
            var licences = parts.GetAll<PersonLicenceDO>("licences");
            var editions = parts.GetAll<PersonLicenceEditionDO>("licenceEditions");

            List<GvaViewPersonLicenceEdition> editionsView = new List<GvaViewPersonLicenceEdition>();
            foreach (var licence in licences)
            {
                var licenceEditions = editions.Where(e => e.Content.LicencePartIndex == licence.Part.Index).ToArray();
                var firstEdition = licenceEditions.Where(ed => ed.Content.Index == licenceEditions.Min(e => e.Content.Index)).Single();
                var lastEdition = licenceEditions.Where(ed => ed.Content.Index == licenceEditions.Max(e => e.Content.Index)).Single();
                var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Content.LicenceType.NomValueId);

                foreach (var edition in licenceEditions)
                {
                    editionsView.Add(this.Create(licence, edition, firstEdition.Content, lastEdition.Content, licenceType));
                }
            }

            return editionsView;
        }

        private GvaViewPersonLicenceEdition Create(
            PartVersion<PersonLicenceDO> personLicence,
            PartVersion<PersonLicenceEditionDO> edition,
            PersonLicenceEditionDO firstEdition,
            PersonLicenceEditionDO lastEdition,
            NomValue licenceType)
        {
            GvaViewPersonLicenceEdition licenceEdition = new GvaViewPersonLicenceEdition();

            licenceEdition.LotId = personLicence.Part.Lot.LotId;
            licenceEdition.LicencePartId = personLicence.Part.PartId;
            licenceEdition.EditionPartId = edition.Part.PartId;
            licenceEdition.EditionIndex = edition.Content.Index;
            licenceEdition.LicenceTypeId = personLicence.Content.LicenceType.NomValueId;
            licenceEdition.StampNumber = edition.Content.StampNumber;
            licenceEdition.DateValidFrom = edition.Content.DocumentDateValidFrom.Value;
            licenceEdition.DateValidTo = edition.Content.DocumentDateValidTo;
            licenceEdition.LicenceActionId = edition.Content.LicenceAction.NomValueId;
            licenceEdition.LicenceNumber = personLicence.Content.LicenceNumber;
            licenceEdition.IsLastEdition = lastEdition.Index == edition.Content.Index;
            licenceEdition.LicencePartIndex = personLicence.Part.Index;
            licenceEdition.EditionPartIndex = edition.Part.Index;
            licenceEdition.FirstDocDateValidFrom = firstEdition.DocumentDateValidFrom.Value;
            licenceEdition.Valid = personLicence.Content.Valid != null && personLicence.Content.Valid.Code == "Y";
            licenceEdition.LicenceTypeCode = licenceType.TextContent.Get<string>("licenceCode");
            licenceEdition.LicenceTypeCaCode = licenceType.TextContent.Get<string>("codeCA");
            licenceEdition.PublisherCode = personLicence.Content.Publisher.Code;
            licenceEdition.Notes = lastEdition.Notes;

            if (lastEdition.Inspector != null)
            {
                licenceEdition.Inspector = lastEdition.Inspector.Name;
            }
            if (lastEdition.Limitations != null)
            {
                licenceEdition.Limitations = string.Join(", ", lastEdition.Limitations.Select(l => l.Name));
            }
            if (personLicence.Content.Statuses != null)
            {
                PersonLicenceStatusDO lastStatus = personLicence.Content.Statuses.Last();

                licenceEdition.StatusChange = string.Format("{0:d} {1}", lastStatus.ChangeDate, lastStatus.ChangeReason.Name);
            }

            return licenceEdition;
        }
    }
}
