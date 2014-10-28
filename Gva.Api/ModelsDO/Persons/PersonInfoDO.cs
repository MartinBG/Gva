using Regs.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonInfoDO
    {
        public PersonInfoDO()
        { }

        public PersonInfoDO(
            PartVersion<PersonDataDO> personDataPart,
            PartVersion<InspectorDataDO> inspectorDataPart,
            PartVersion<ExaminerDataDO> examinerDataPart)
        {
            this.PersonData = personDataPart.Content;
            this.InspectorData = inspectorDataPart == null ?
                null :
                inspectorDataPart.Content;
            this.ExaminerData = examinerDataPart == null ?
                null :
                examinerDataPart.Content;
        }

        public PersonDataDO PersonData { get; set; }

        public InspectorDataDO InspectorData { get; set; }

        public ExaminerDataDO ExaminerData { get; set; }
    }
}
