using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
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

        public void AddCaseTypes(Lot lot, dynamic caseTypes)
        {
            var lotCases = this.unitOfWork.DbContext.Set<GvaLotCase>()
                .Where(lc => lc.LotId == lot.LotId)
                .ToList();

            foreach (var lotCase in lotCases)
            {
                this.unitOfWork.DbContext.Set<GvaLotCase>().Remove(lotCase);
            }

            foreach (var caseType in caseTypes)
            {
                GvaLotCase lotCase = new GvaLotCase
                {
                    Lot = lot,
                    GvaCaseTypeId = caseType.nomValueId
                };

                this.unitOfWork.DbContext.Set<GvaLotCase>().Add(lotCase);
            }
        }

        public IEnumerable<GvaCaseType> GetCaseTypesForSet(int setId)
        {
            return this.unitOfWork.DbContext.Set<GvaCaseType>()
                .Where(ct => ct.LotSetId == setId);
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
