using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Person;
using Newtonsoft.Json.Linq;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonLicenceEditionProjection : Projection<GvaViewPersonLicenceEdition>
    {
        public PersonLicenceEditionProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
        }

        public override IEnumerable<GvaViewPersonLicenceEdition> Execute(PartCollection parts)
        {
            var licences = parts.GetAll("licences");

            return licences.SelectMany(l => l.Content.GetItems<JObject>("editions").Select(e => this.Create(l, e)));
        }

        private GvaViewPersonLicenceEdition Create(PartVersion personLicence, JObject edition)
        {
            GvaViewPersonLicenceEdition licenceEdition = new GvaViewPersonLicenceEdition();

            var lastEdition = personLicence.Content.GetItems<JObject>("editions").Last();
            licenceEdition.LotId = personLicence.Part.Lot.LotId;
            licenceEdition.PartId = personLicence.Part.PartId;
            licenceEdition.EditionIndex = edition.Get<int>("index");
            licenceEdition.LicenceTypeId = personLicence.Content.Get<int>("licenceType.nomValueId");
            licenceEdition.StampNumber = edition.Get<string>("stampNumber");
            licenceEdition.DateValidFrom = edition.Get<DateTime>("documentDateValidFrom");
            licenceEdition.DateValidTo = edition.Get<DateTime>("documentDateValidTo");
            licenceEdition.LicenceActionId = edition.Get<int>("licenceAction.nomValueId");
            licenceEdition.LicenceNumber = personLicence.Content.Get<string>("licenceNumber");
            licenceEdition.IsLastEdition = lastEdition.Get<int>("index") == edition.Get<int>("index");
            licenceEdition.GvaApplicationId = edition.Get<int?>("applications[0].applicationId");
            licenceEdition.ApplicationName = edition.Get<string>("applications[0].applicationName");
            licenceEdition.ApplicationPartIndex = edition.Get<int?>("applications[0].partIndex");

            return licenceEdition;
        }
    }
}
