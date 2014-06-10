using Regs.Api.Models;

namespace Gva.Api.ModelsDO
{
    public class PersonInfoDO
    {
        public PersonInfoDO(PartVersion personDataPart, PartVersion inspectorDataPart)
        {
            this.PersonData = new PartVersionDO(personDataPart);
            this.InspectorData = inspectorDataPart == null ? null : new PartVersionDO(inspectorDataPart);
        }

        public PartVersionDO PersonData { get; set; }

        public PartVersionDO InspectorData { get; set; }
    }
}
