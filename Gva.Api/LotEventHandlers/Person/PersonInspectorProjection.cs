using System.Collections.Generic;
using Common.Data;
using Common.Json;
using Gva.Api.Models.Views.Person;
using Regs.Api.LotEvents;
using Regs.Api.Models;

namespace Gva.Api.LotEventHandlers.Person
{
    public class PersonInspectorProjection : Projection<GvaViewPersonInspector>
    {
        public PersonInspectorProjection(IUnitOfWork unitOfWork)
            : base(unitOfWork, "Person")
        {
        }

        public override IEnumerable<GvaViewPersonInspector> Execute(PartCollection parts)
        {
            var inspectorData = parts.Get("inspectorData");

            if (inspectorData == null)
            {
                return new GvaViewPersonInspector[] { };
            }

            return new[] { this.Create(inspectorData) };
        }

        private GvaViewPersonInspector Create(PartVersion inspectorData)
        {
            GvaViewPersonInspector inspector = new GvaViewPersonInspector();

            inspector.LotId = inspectorData.Part.Lot.LotId;
            inspector.ExaminerCode = inspectorData.Content.Get<string>("examinerCode");
            inspector.CaaId = inspectorData.Content.Get<int>("caa.nomValueId");
            inspector.StampNum = inspectorData.Content.Get<string>("stampNum");
            inspector.Valid = inspectorData.Content.Get<string>("valid.code") == "Y";

            return inspector;
        }
    }
}
