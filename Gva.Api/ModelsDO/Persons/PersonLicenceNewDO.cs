namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceNewDO
    {
        public CaseTypePartDO<PersonLicenceDO> Licence { get; set; }

        public CaseTypesPartDO<PersonLicenceEditionDO> Edition { get; set; }
    }
}
