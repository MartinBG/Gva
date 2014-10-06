namespace Gva.Api.ModelsDO.Persons
{
    public class PersonDO
    {
        public PersonDataDO PersonData { get; set; }

        public CaseTypesPartDO<PersonDocumentIdDO> PersonDocumentId { get; set; }

        public PersonAddressDO PersonAddress { get; set; }
    }
}
