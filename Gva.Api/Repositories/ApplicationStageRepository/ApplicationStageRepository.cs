using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Common.Json;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;
using System;
using Gva.Api.Models.Views;
using Gva.Api.Repositories.ApplicationRepository;

namespace Gva.Api.Repositories.ApplicationStageRepository
{
    public class ApplicationStageRepository : IApplicationStageRepository
    {
        private IUnitOfWork unitOfWork;
        private IApplicationRepository applicationRepository;

        public ApplicationStageRepository(
            IUnitOfWork unitOfWork,
            IApplicationRepository applicationRepository)
        {
            this.unitOfWork = unitOfWork;
            this.applicationRepository = applicationRepository;
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

        public DateTime? GetApplicationTermDate(int applicationId, int gvaStageId)
        {
            dynamic stageTermDate = null;
            GvaStage gvaStage = this.unitOfWork.DbContext.Set<GvaStage>().Find(gvaStageId);
            GvaViewApplication application = this.applicationRepository.GetApplicationById(applicationId);
            int? documentDuration = application.ApplicationType.TextContent.Get<int?>("duration");

            if ((gvaStage.Alias == "new" || gvaStage.Alias == "newDocuments") && documentDuration.HasValue)
            {
                stageTermDate = DateTime.Now.AddDays(documentDuration.Value);
            }
            else if (gvaStage.Alias == "processing" || gvaStage.Alias == "declined" || gvaStage.Alias == "approved")
            {
                GvaApplicationStage lastResetTermDateStage = this.GetApplicationStages(applicationId)
                    .OrderByDescending(s => s.Ordinal).Where(s => s.GvaStage.Alias == "new" || s.GvaStage.Alias == "newDocuments")
                    .FirstOrDefault();

                if (lastResetTermDateStage != null)
                {
                    stageTermDate = lastResetTermDateStage.StageTermDate;
                }
            }

            return stageTermDate;
        }
    }
}
