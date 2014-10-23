using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Aircrafts;

namespace Gva.Api.Repositories.AircraftRepository
{
    public interface IAircraftDocumentDebtFMRepository
    {
        List<CaseTypePartDO<AircraftDocumentDebtFMDO>> GetRegistrationDebts(int lotId, int partIndex, int? caseTypeId = null);

        List<CaseTypePartDO<AircraftDocumentDebtFMViewDO>> GetDocumentDebts(int lotId, int? partIndex = null, int? caseTypeId = null);
    }
}
