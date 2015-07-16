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
                licencesView.Add(this.Create(licence));
            }

            return licencesView;
        }

        private GvaViewPersonLicence Create(PartVersion<PersonLicenceDO> personLicence)
        {
            var licenceType = this.nomRepository.GetNomValue(personLicence.Content.LicenceTypeId.Value);

            GvaViewPersonLicence licence = new GvaViewPersonLicence();
            licence.LotId = personLicence.Part.Lot.LotId;
            licence.PartId = personLicence.Part.PartId;
            licence.PartIndex = personLicence.Part.Index;
            licence.LicenceTypeId = personLicence.Content.LicenceTypeId.Value;
            licence.LicenceNumber = personLicence.Content.LicenceNumber;

            if (personLicence.Content.ValidId.HasValue)
            {
                licence.Valid = this.nomRepository.GetNomValue(personLicence.Content.ValidId.Value).Code == "Y";
            }
            else
            {
                licence.Valid = false;
            }

            licence.LicenceTypeCaCode = licenceType.TextContent.Get<string>("codeCA");

            if (personLicence.Content.PublisherId.HasValue)
            {
                licence.PublisherCode = this.nomRepository.GetNomValue("caa", personLicence.Content.PublisherId.Value).Code;
            }

            licence.ForeignLicenceNumber = personLicence.Content.ForeignLicenceNumber;

            if (personLicence.Content.ForeignPublisherId.HasValue)
            {
                licence.ForeignPublisher = this.nomRepository.GetNomValue("caa", personLicence.Content.ForeignPublisherId.Value).Name;
            }

            return licence;
        }
    }
}
