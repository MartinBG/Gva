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
                .Include(e => e.Inspector)
                .Where(e => e.GvaApplicationId == applicationId)
                .ToList();
        }

        public GvaApplicationStage GetApplicationStage(int applicationId, int stageId)
        {
            return this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                .Include(e => e.GvaStage)
                .Include(e => e.Inspector.Person)
                .Include(e => e.Inspector)
                .Where(e => e.GvaAppStageId == stageId)
                .SingleOrDefault();
        }

        public GvaApplicationStage DeleteApplicationStage(int applicationId, int stageId)
        {
            var appStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                .Where(e => e.GvaAppStageId == stageId)
                .SingleOrDefault();

            var applicationStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>()
                .Remove(appStage);

            this.unitOfWork.Save();

            return applicationStage;
        }

        public GvaApplicationStage AddApplicationStage(int appId, JObject appStage)
        {
            GvaApplicationStage stage = new GvaApplicationStage()
            {
                GvaApplicationId = appId,
                GvaStageId = appStage.Get<int>("stageId"),
                StartingDate = appStage.Get<DateTime>("date"),
                InspectorLotId = appStage.Get<int?>("inspectorId"),
                Ordinal = appStage.Get<int>("ordinal")
            };

            var applicationStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>().Add(stage);

            this.unitOfWork.Save();

            return applicationStage;
        }

        public GvaApplicationStage UpdateApplicationStage(int appId, int stageId, JObject appStage)
        {
            var applicationStage = this.unitOfWork.DbContext.Set<GvaApplicationStage>().Find(stageId);
            if (applicationStage != null)
            {
                applicationStage.GvaStageId = appStage.Get<int>("stageId");
                applicationStage.StartingDate = appStage.Get<DateTime>("date");
                applicationStage.InspectorLotId = appStage.Get<int?>("inspectorId");
                applicationStage.Ordinal = appStage.Get<int>("ordinal");
            }

            this.unitOfWork.Save();

            return applicationStage;
        }
    }
}
