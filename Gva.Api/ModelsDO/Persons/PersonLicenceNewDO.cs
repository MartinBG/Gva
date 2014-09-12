using System;
using System.ComponentModel.DataAnnotations;
using Common.Api.Models;
using System.Collections.Generic;

namespace Gva.Api.ModelsDO.Persons
{
    public class PersonLicenceNewDO
    {
        public ApplicationPartVersionDO<PersonLicenceDO> Licence { get; set; }

        public FilePartVersionDO<PersonLicenceEditionDO> Edition { get; set; }

    }
}
