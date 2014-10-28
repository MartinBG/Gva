using System.Collections.Generic;
using Common.Data;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonExaminerProjection : Projection<GvaViewPersonExaminer>
    {
        public PersonExaminerProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
        }

        public override IEnumerable<GvaViewPersonExaminer> Execute(PartCollection parts)
        {
            var examinerData = parts.Get<ExaminerDataDO>("examinerData");

            if (examinerData == null)
            {
                return new GvaViewPersonExaminer[] { };
            }

            return new[] { this.Create(examinerData) };
        }

        private GvaViewPersonExaminer Create(PartVersion<ExaminerDataDO> examinerData)
        {
            GvaViewPersonExaminer examiner = new GvaViewPersonExaminer();

            examiner.LotId = examinerData.Part.Lot.LotId;
            examiner.ExaminerCode = examinerData.Content.ExaminerCode;
            examiner.StampNum = examinerData.Content.StampNum;
            examiner.Valid = examinerData.Content.Valid.Code == "Y";

            return examiner;
        }
    }
}
