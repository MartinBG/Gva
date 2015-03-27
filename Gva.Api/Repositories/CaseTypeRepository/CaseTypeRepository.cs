using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Common.Linq;
using Gva.Api.Models;
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
                .Include(c => c.LotSet)
                .SingleOrDefault(ct => ct.Alias == caseTypeAlias);
        }
        public IEnumerable<GvaCaseType> GetCaseTypesForSet(string setAlias, bool activeOnly = true)
        {
            var predicate = PredicateBuilder.True<GvaCaseType>()
                .And(ct => ct.LotSet.Alias == setAlias);

            if (activeOnly)
            {
                predicate = predicate.And(ct => ct.IsActive);
            }

            return this.unitOfWork.DbContext.Set<GvaCaseType>()
                .Where(predicate)
                .ToList();
        }

        public IEnumerable<GvaCaseType> GetCaseTypesForLot(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaLotCase>()
                .Include(lc => lc.GvaCaseType)
                .Where(lc => lc.LotId == lotId && lc.GvaCaseType.IsActive)
                .Select(lc => lc.GvaCaseType);
        }
    }
}
