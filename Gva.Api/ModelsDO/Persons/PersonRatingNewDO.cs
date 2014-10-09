using System.ComponentModel.DataAnnotations;
using Common.Api.Models;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonRatingNewDO
    {
        public CaseTypePartDO<PersonRatingDO> Rating { get; set; }

        public CaseTypePartDO<PersonRatingEditionDO> Edition { get; set; }

    }
}
