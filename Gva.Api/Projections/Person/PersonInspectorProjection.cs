using System.Collections.Generic;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonInspectorProjection : Projection<GvaViewPersonInspector>
    {
        private INomRepository nomRepository;

        public PersonInspectorProjection(
            IUnitOfWork unitOfWork,
            INomRepository nomRepository)
            : base(unitOfWork, "Person")
        {
            this.nomRepository = nomRepository;
        }

        public override IEnumerable<GvaViewPersonInspector> Execute(PartCollection parts)
        {
            var inspectorData = parts.Get<InspectorDataDO>("inspectorData");
            int trueValueId = this.nomRepository.GetNomValue("boolean", "yes").NomValueId;

            if (inspectorData == null)
            {
                return new GvaViewPersonInspector[] { };
            }

            return new[] { this.Create(inspectorData, trueValueId) };
        }

        private GvaViewPersonInspector Create(PartVersion<InspectorDataDO> inspectorData, int trueValueId)
        {
            GvaViewPersonInspector inspector = new GvaViewPersonInspector();

            inspector.LotId = inspectorData.Part.Lot.LotId;
            inspector.ExaminerCode = inspectorData.Content.ExaminerCode;
            inspector.CaaId = inspectorData.Content.CaaId.Value;
            inspector.StampNum = inspectorData.Content.StampNum;
            inspector.Valid = inspectorData.Content.ValidId == trueValueId;

            return inspector;
        }
    }
}
