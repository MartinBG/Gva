using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Person;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonLicenceProjection : Projection<GvaViewPersonLicence>
    {
        public PersonLicenceProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
        }

        public override IEnumerable<GvaViewPersonLicence> Execute(PartCollection parts)
        {
            var licences = parts.GetAll("licences")
                .Where(pv => pv.Content.Get("licenceType") != null);

            return licences.Select(l => this.Create(l));
        }

        private GvaViewPersonLicence Create(PartVersion personLicence)
        {
            GvaViewPersonLicence licence = new GvaViewPersonLicence();

            licence.LotId = personLicence.Part.Lot.LotId;
            licence.PartIndex = personLicence.Part.Index;
            licence.LicenceTypeId = personLicence.Content.Get<int>("licenceType.nomValueId");

            return licence;
        }
    }
}
