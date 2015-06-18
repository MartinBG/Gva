using System.Collections.Generic;
using Common.Api.UserContext;
using Docs.Api.DataObjects;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Organizations;
using Gva.Api.ModelsDO.Persons;
using R_0009_000008;
using R_0009_000011;
using R_0009_000015;
using R_4012;
using Regs.Api.Models;

namespace Gva.Api.Repositories.IntegrationRepository
{
    public interface IIntegrationRepository
    {
        void UpdateLotCaseTypes(string set, GvaCaseType caseType, Lot lot, UserContext userContext);

        PersonDataDO ConvertAppWithFlightCrewDataToPersonData(FlightCrewPersonalData flightCrewlData, GvaCaseType caseType);

        PersonDataDO ConvertAppWithPersonAndForeignCitizenBasicDataToPersonData(
            string caaPersonIdentificator,
            string email,
            PersonBasicData personBasicData, 
            ForeignCitizenBasicData foreignCitizenBasicData, 
            GvaCaseType caseType);

        CorrespondentDO ConvertPersonDataToCorrespondent(PersonDataDO personData);

        CorrespondentDO ConvertOrganizationDataToCorrespondent(OrganizationDataDO organizationData);
    }
}
