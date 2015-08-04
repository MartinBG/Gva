using System;
using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;

namespace Gva.Api.Repositories.PersonTrainingRepository
{
    public interface IPersonTrainingRepository
    {
        List<PersonTrainingViewDO> GetTrainings(int lotId, int? caseTypeId = null, int? roleId = null);
    }
}
