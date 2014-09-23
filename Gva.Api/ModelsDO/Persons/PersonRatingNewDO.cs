using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonRatingNewDO
    {
        public ApplicationPartVersionDO<PersonRatingDO> Rating { get; set; }

        public ApplicationPartVersionDO<PersonRatingEditionDO> Edition { get; set; }

    }
}
