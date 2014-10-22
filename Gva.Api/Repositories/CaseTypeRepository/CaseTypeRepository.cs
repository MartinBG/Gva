using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.Repositories.CaseTypeRepository
{
    public class CaseTypeRepository : ICaseTypeRepository
    {
        private IUnitOfWork unitOfWork;

        public CaseTypeRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public void AddCaseTypes(Lot lot, IEnumerable<int> caseTypeIds)
        {
            var lotCases = this.unitOfWork.DbContext.Set<GvaLotCase>()
                .Where(lc => lc.LotId == lot.LotId)
                .ToList();

            foreach (var lotCase in lotCases)
            {
                this.unitOfWork.DbContext.Set<GvaLotCase>().Remove(lotCase);
            }

            foreach (var caseType in caseTypeIds)
            {
                GvaLotCase lotCase = new GvaLotCase
                {
                    LotId = lot.LotId,
                    GvaCaseTypeId = caseType
                };

                this.unitOfWork.DbContext.Set<GvaLotCase>().Add(lotCase);
            }
        }

        public GvaCaseType GetCaseType(int caseTypeId)
        {
            return this.unitOfWork.DbContext.Set<GvaCaseType>()
                .SingleOrDefault(ct => ct.GvaCaseTypeId == caseTypeId);
        }

        public GvaCaseType GetCaseType(string caseTypeAlias)
        {
            return this.unitOfWork.DbContext.Set<GvaCaseType>()
                .SingleOrDefault(ct => ct.Alias == caseTypeAlias);
        }
        public IEnumerable<GvaCaseType> GetCaseTypesForSet(string setAlias)
        {
            return this.unitOfWork.DbContext.Set<GvaCaseType>()
                .Where(ct => ct.LotSet.Alias == setAlias)
                .ToList();
        }

        public IEnumerable<GvaCaseType> GetCaseTypesForLot(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaLotCase>()
                .Include(lc => lc.GvaCaseType)
                .Where(lc => lc.LotId == lotId)
                .Select(lc => lc.GvaCaseType);
        }
    }
}
