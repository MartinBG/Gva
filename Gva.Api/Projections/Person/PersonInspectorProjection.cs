using System.Collections.Generic;
using Common.Data;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.Projections.Person
{
    public class PersonInspectorProjection : Projection<GvaViewPersonInspector>
    {
        public PersonInspectorProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
        }

        public override IEnumerable<GvaViewPersonInspector> Execute(PartCollection parts)
        {
            var inspectorData = parts.Get<InspectorDataDO>("inspectorData");

            if (inspectorData == null)
            {
                return new GvaViewPersonInspector[] { };
            }

            return new[] { this.Create(inspectorData) };
        }

        private GvaViewPersonInspector Create(PartVersion<InspectorDataDO> inspectorData)
        {
            GvaViewPersonInspector inspector = new GvaViewPersonInspector();

            inspector.LotId = inspectorData.Part.Lot.LotId;
            inspector.ExaminerCode = inspectorData.Content.ExaminerCode;
            inspector.CaaId = inspectorData.Content.Caa.NomValueId;
            inspector.StampNum = inspectorData.Content.StampNum;
            inspector.Valid = inspectorData.Content.Valid.Code == "Y";

            return inspector;
        }
    }
}
