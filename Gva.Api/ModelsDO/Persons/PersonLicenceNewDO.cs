namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceNewDO
    {
        public CaseTypePartDO<PersonLicenceDO> Licence { get; set; }

        public CaseTypePartDO<PersonLicenceEditionDO> Edition { get; set; }
    }
}
