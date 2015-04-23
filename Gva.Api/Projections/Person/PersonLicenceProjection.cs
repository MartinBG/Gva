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
    public class PersonLicenceProjection : Projection<GvaViewPersonLicence>
    {
        private INomRepository nomRepository;

        public PersonLicenceProjection(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository)
            : base(unitOfWork, "Person")
        {
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewPersonLicence> Execute(PartCollection parts)
        {
            var licences = parts.GetAll<PersonLicenceDO>("licences");

            List<GvaViewPersonLicence> licencesView = new List<GvaViewPersonLicence>();
            foreach (var licence in licences)
            {
                var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Content.LicenceType.NomValueId);
                licencesView.Add(this.Create(licence, licenceType));
            }

            return licencesView;
        }

        private GvaViewPersonLicence Create(
            PartVersion<PersonLicenceDO> personLicence,
            NomValue licenceType)
        {
            GvaViewPersonLicence licence = new GvaViewPersonLicence();

            licence.LotId = personLicence.Part.Lot.LotId;
            licence.PartId = personLicence.Part.PartId;
            licence.PartIndex = personLicence.Part.Index;
            licence.LicenceTypeId = personLicence.Content.LicenceType.NomValueId;
            licence.LicenceNumber = personLicence.Content.LicenceNumber;
            licence.Valid = personLicence.Content.Valid != null && personLicence.Content.Valid.Code == "Y";
            licence.LicenceTypeCaCode = licenceType.TextContent.Get<string>("codeCA");
            licence.PublisherCode = personLicence.Content.Publisher.Code;
            licence.ForeignLicenceNumber = personLicence.Content.ForeignLicenceNumber;
            licence.ForeignPublisher = personLicence.Content.ForeignPublisher != null?  personLicence.Content.ForeignPublisher.Name : null;

            return licence;
        }
    }
}
