using System.Collections.Generic;
using Common.Api.UserContext;
using Docs.Api.DataObjects;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Persons;
using R_0009_000015;
using Regs.Api.Models;

namespace Gva.Api.Repositories.IntegrationRepository
{
    public interface IIntegrationRepository
    {
        void UpdateLotCaseTypes(string set, GvaCaseType caseType, Lot lot, UserContext userContext);

        List<int> GetCorrespondentIdsPerPersonLot(PersonDataDO personData, UserContext userContext);

        List<int> CreateCorrespondent(CorrespondentDO correspondent, UserContext userContext);

        CorrespondentDO ConvertElServiceRecipientToCorrespondent(ElectronicServiceRecipient applicant);
    }
}
