using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Vew;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Common;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonQualificationProjection : Projection<GvaViewPersonQualification>
    {
        private INomRepository nomRepository;

        public PersonQualificationProjection(IUnitOfWork unitOfWork, INomRepository nomRepository)
            : base(unitOfWork, "Person")
        {
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewPersonQualification> Execute(PartCollection parts)
        {
            var applications = parts.GetAll<DocumentApplicationDO>("personDocumentApplications");

            return applications.Where(a => a.Content.ApplicationType.Code.StartsWith("EX-") ||
                a.Content.ApplicationType.Code.StartsWith("EX/"))
                .Select(a => this.Create(a));
        }

        private GvaViewPersonQualification Create(PartVersion<DocumentApplicationDO> personApplication)
        {
            GvaViewPersonQualification qualification = new GvaViewPersonQualification();

            qualification.LotId = personApplication.Part.Lot.LotId;
            qualification.ApplicationPartIndex = personApplication.Part.Index;


            var licenceTypeIds = this.nomRepository.GetNomValue(personApplication.Content.ApplicationType.NomValueId)
                .TextContent
                .GetItems<int>("licenceTypeIds");
            List<string> qualificationCodes = new List<string>();

            foreach (int licenceTypeId in licenceTypeIds)
            { 
                NomValue licenceType = this.nomRepository.GetNomValue(licenceTypeId);
                string qlfCode = licenceType.TextContent.Get<string>("qlf_code");
                if (!string.IsNullOrEmpty(qlfCode))
                {
                    qualificationCodes.Add(qlfCode);
                }
            }

            qualification.QualificationCodes = string.Join(",", qualificationCodes);

            return qualification;
        }
    }
}
