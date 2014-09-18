using Regs.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonInfoDO
    {
        public PersonInfoDO()
        { }

        public PersonInfoDO(PartVersion<PersonDataDO> personDataPart, PartVersion<InspectorDataDO> inspectorDataPart)
        {
            this.PersonData = personDataPart.Content;
            this.InspectorData = inspectorDataPart == null ?
                null :
                inspectorDataPart.Content;
        }

        public PersonDataDO PersonData { get; set; }

        public InspectorDataDO InspectorData { get; set; }
    }
}
