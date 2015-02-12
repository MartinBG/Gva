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
            var examSystData = parts.GetAll<PersonExamSystDataDO>("personExamSystData").SingleOrDefault();

            List<GvaViewPersonQualification> qualifications = new List<GvaViewPersonQualification>();
            if (examSystData != null)
            {
                foreach (var states in examSystData.Content.States.GroupBy(s => s.Qualification.Code))
                {
                    PersonExamSystStateDO lastState = states.OrderBy(s => s.FromDate).Last();
                    qualifications.Add(this.Create(lastState, examSystData.Part.LotId));
                }
            }
            return qualifications;
        }

        private GvaViewPersonQualification Create(PersonExamSystStateDO state, int lotId)
        {
            NomValue licenceType = this.nomRepository.GetNomValues("licenceTypes")
                .Where(s => s.TextContent.Get<string>("qlfCode") == state.Qualification.Code).Single();
            return new GvaViewPersonQualification()
            {
                QualificationCode = state.Qualification.Code,
                QualificationName = state.Qualification.Name,
                State = state.State,
                StateMethod = state.StateMethod,
                LotId = lotId,
                LicenceTypeCode = licenceType.Code
            };
        }
    }
}
