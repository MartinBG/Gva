using System.Collections.Generic;
using Gva.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.Repositories.CaseTypeRepository
{
    public interface ICaseTypeRepository
    {
        void AddCaseTypes(Lot lot, IEnumerable<int> caseTypeIds);

        GvaCaseType GetCaseType(int caseTypeId);

        GvaCaseType GetCaseType(string caseTypeAlias);

        IEnumerable<GvaCaseType> GetAllCaseTypes(bool activeOnly = true);

        IEnumerable<GvaCaseType> GetCaseTypesForSet(string setAlias, bool activeOnly = true);

        IEnumerable<GvaCaseType> GetCaseTypesForLot(int lotId);
    }
}
