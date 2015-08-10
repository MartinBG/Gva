using System.Collections.Generic;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonExaminerProjection : Projection<GvaViewPersonExaminer>
    {
        private INomRepository nomRepository;

        public PersonExaminerProjection(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository)
            : base(unitOfWork, "Person")
        {
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewPersonExaminer> Execute(PartCollection parts)
        {
            var examinerData = parts.Get<ExaminerDataDO>("examinerData");
            int trueValueId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId;

            if (examinerData == null)
            {
                return new GvaViewPersonExaminer[] { };
            }

            return new[] { this.Create(examinerData, trueValueId) };
        }

        private GvaViewPersonExaminer Create(PartVersion<ExaminerDataDO> examinerData, int trueValueId)
        {
            GvaViewPersonExaminer examiner = new GvaViewPersonExaminer();

            examiner.LotId = examinerData.Part.Lot.LotId;
            examiner.ExaminerCode = examinerData.Content.ExaminerCode;
            examiner.StampNum = examinerData.Content.StampNum;
            examiner.Valid = examinerData.Content.ValidId == trueValueId;

            return examiner;
        }
    }
}
