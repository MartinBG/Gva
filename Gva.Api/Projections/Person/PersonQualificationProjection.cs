using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Api.Repositories.UserRepository;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views;
using Gva.Api.ModelsDO.Common;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonQualificationProjection : Projection<GvaViewPersonQualification>
    {
        private INomRepository nomRepository;

        public PersonQualificationProjection(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository)
            : base(unitOfWork, "Person")
        {
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewPersonQualification> Execute(PartCollection parts)
        {
            var examSystData = parts.Get<PersonExamSystDataDO>("personExamSystData");

            List<GvaViewPersonQualification> qualifications = new List<GvaViewPersonQualification>();
            if (examSystData != null)
            {
                var allLicenceTypesWithQlfCode = this.nomRepository.GetNomValues("licenceTypes")
                    .Where(s => s.TextContent.Get<string>("qlfCode") != null);

                foreach (var states in examSystData.Content.States.GroupBy(s => s.QualificationCode))
                {
                    PersonExamSystStateDO lastState = states.OrderBy(s => s.FromDate).Last();

                    NomValue licenceType = allLicenceTypesWithQlfCode
                        .Where(s => s.TextContent.Get<string>("qlfCode") == lastState.QualificationCode)
                        .SingleOrDefault();

                    if (licenceType != null)
                    { 
                        qualifications.Add(new GvaViewPersonQualification()
                        {
                            QualificationCode = lastState.QualificationCode,
                            QualificationName = lastState.QualificationName,
                            State = lastState.State,
                            StateMethod = lastState.StateMethod,
                            LotId = examSystData.Part.LotId,
                            LicenceTypeCode = licenceType.Code
                        });
                    }
                }
            }
            return qualifications;
        }
    }
}
