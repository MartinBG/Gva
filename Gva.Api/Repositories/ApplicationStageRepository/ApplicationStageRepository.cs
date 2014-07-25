using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using System;

namespace Gva.Api.Repositories.ApplicationStageRepository
{
    public class ApplicationStageRepository : IApplicationStageRepository
    {
        private IUnitOfWork unitOfWork;

        public ApplicationStageRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GvaApplicationStage> GetApplicationStages(int applicationId)
        {
            return this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                .Include(e => e.GvaStage)
                .Include(e => e.Inspector.Person)
                .Where(e => e.GvaApplicationId == applicationId)
                .OrderBy(e => e.Ordinal)
                .ToList();
        }

        public GvaApplicationStage GetApplicationStage(int applicationId, int stageId)
        {
            return this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                .Include(e => e.GvaStage)
                .Include(e => e.Inspector.Person)
                .Where(e => e.GvaAppStageId == stageId)
                .SingleOrDefault();
        }
    }
}
