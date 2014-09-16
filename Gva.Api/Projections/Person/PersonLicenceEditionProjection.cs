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

namespace Gva.Api.Projections.Person
{
    public class PersonLicenceEditionProjection : Projection<GvaViewPersonLicenceEdition>
    {
        private INomRepository nomRepository;

        public PersonLicenceEditionProjection(IUnitOfWork unitOfWork, INomRepository nomRepository)
            : base(unitOfWork, "Person")
        {
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewPersonLicenceEdition> Execute(PartCollection parts)
        {
            var licences = parts.GetAll("licences");
            var editions = parts.GetAll("licenceEditions");

            return licences.SelectMany(l => {
                var licenceEditions = editions.Where(e => e.Content.Get<int>("licencePartIndex") == l.Part.Index).ToArray();
                return licenceEditions.Select(e => this.Create(l, e, licenceEditions));
            });
        }

        private GvaViewPersonLicenceEdition Create(PartVersion personLicence, PartVersion edition, PartVersion[] editions)
        {
            GvaViewPersonLicenceEdition licenceEdition = new GvaViewPersonLicenceEdition();

            var firstEdition = editions.Where(ed => ed.Content.Get<int>("index") == editions.Min(e => e.Content.Get<int>("index"))).SingleOrDefault();
            var lastEdition = editions.Where(ed => ed.Content.Get<int>("index") == editions.Max(e => e.Content.Get<int>("index"))).SingleOrDefault();
            var licenceType = this.nomRepository.GetNomValue("licenceTypes", personLicence.Content.Get<int>("licenceType.nomValueId"));

            licenceEdition.LotId = personLicence.Part.Lot.LotId;
            licenceEdition.PartId = personLicence.Part.PartId;
            licenceEdition.EditionIndex = edition.Content.Get<int>("index");
            licenceEdition.LicenceTypeId = personLicence.Content.Get<int>("licenceType.nomValueId");
            licenceEdition.StampNumber = edition.Content.Get<string>("stampNumber");
            licenceEdition.DateValidFrom = edition.Content.Get<DateTime>("documentDateValidFrom");
            licenceEdition.DateValidTo = edition.Content.Get<DateTime?>("documentDateValidTo");
            licenceEdition.LicenceActionId = edition.Content.Get<int>("licenceAction.nomValueId");
            licenceEdition.LicenceNumber = personLicence.Content.Get<int>("licenceNumber");
            licenceEdition.IsLastEdition = lastEdition.Content.Get<int>("index") == edition.Content.Get<int>("index");
            licenceEdition.GvaApplicationId = edition.Content.Get<int?>("applications[0].applicationId");
            licenceEdition.ApplicationName = edition.Content.Get<string>("applications[0].applicationName");
            licenceEdition.ApplicationPartIndex = edition.Content.Get<int?>("applications[0].partIndex");
            licenceEdition.LicencePartIndex = personLicence.Part.Index;
            licenceEdition.EditionPartIndex = edition.Part.Index;
            licenceEdition.FirstDocDateValidFrom = firstEdition.Content.Get<DateTime>("documentDateValidFrom");
            licenceEdition.Valid = personLicence.Content.Get<string>("valid.code") == "Y";
            licenceEdition.LicenceTypeCode = licenceType.TextContent.Get<string>("licenceCode");
            licenceEdition.LicenceTypeCaCode = licenceType.TextContent.Get<string>("codeCA");
            licenceEdition.PublisherCode = personLicence.Content.Get<string>("publisher.code");

            return licenceEdition;
        }
    }
}
