namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceNewDO
    {
        public ApplicationPartVersionDO<PersonLicenceDO> Licence { get; set; }

        public FilePartVersionDO<PersonLicenceEditionDO> Edition { get; set; }
    }
}
