using System.Collections.Generic;
using Common.Api.UserContext;
using Gva.Api.Models;
using Gva.Api.Models.Views.Aircraft;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Models;

namespace Gva.Api.Repositories.IntegrationRepository
{
    public interface IIntegrationRepository
    {
        void UpdatePersonDataCaseTypes(GvaCaseType caseType, PersonDataDO personData, Lot lot, UserContext userContext);

        List<int> GetCorrespondentIdsPerPersonLot(PersonDataDO personData, Lot lot, UserContext userContex);
    }
}
