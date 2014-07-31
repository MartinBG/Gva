using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Common.Json;
using Common.Linq;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.Repositories.StageRepository
{
    public class StageRepository : IStageRepository
    {
        private IUnitOfWork unitOfWork;

        public StageRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GvaStage> GetStages(string name = null)
        {
            var predicate =
                PredicateBuilder.True<GvaStage>()
                .AndStringContains(s => s.Name, name);

            return this.unitOfWork.DbContext.Set<GvaStage>().Where(predicate);
        }

        public GvaStage GetStage(int id)
        {
            return this.unitOfWork.DbContext.Set<GvaStage>()
                .Where(s => s.GvaStageId == id)
                .SingleOrDefault();
        }
    }
}
