using System.Collections.Generic;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.Repositories.CaseTypeRepository
{
    public interface ICaseTypeRepository
    {
        void AddCaseTypes(Lot lot, IEnumerable<int> caseTypeIds);

        GvaCaseType GetCaseType(int caseTypeId);

        IEnumerable<GvaCaseType> GetCaseTypesForSet(string setAlias);

        IEnumerable<GvaCaseType> GetCaseTypesForLot(int lotId);
    }
}
