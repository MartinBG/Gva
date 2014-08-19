using Regs.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonInfoDO
    {
        public PersonInfoDO()
        { }

        public PersonInfoDO(PartVersion personDataPart, PartVersion inspectorDataPart)
        {
            this.PersonData = personDataPart.Content.ToObject<PersonDataDO>();
            this.InspectorData = inspectorDataPart == null ?
                null :
                inspectorDataPart.Content.ToObject<InspectorDataDO>();
        }

        public PersonDataDO PersonData { get; set; }

        public InspectorDataDO InspectorData { get; set; }
    }
}
