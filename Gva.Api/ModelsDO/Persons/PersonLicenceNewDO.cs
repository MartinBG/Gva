namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceNewDO
    {
        public FilePartVersionDO<PersonLicenceDO> Licence { get; set; }

        public FilePartVersionDO<PersonLicenceEditionDO> Edition { get; set; }
    }
}
