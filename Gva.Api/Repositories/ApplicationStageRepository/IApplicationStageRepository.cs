using System;
using System.Collections.Generic;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.Repositories.ApplicationStageRepository
{
    public interface IApplicationStageRepository
    {
        IEnumerable<GvaApplicationStage> GetApplicationStages(int applicationId);

        GvaApplicationStage GetApplicationStage(int applicationId, int stageId);

        DateTime? GetApplicationTermDate(int applicationId, int gvaStageId);
    }
}
