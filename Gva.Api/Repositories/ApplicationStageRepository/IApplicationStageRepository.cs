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

        GvaApplicationStage DeleteApplicationStage(int applicationId, int stageId);

        GvaApplicationStage AddApplicationStage(int appId, JObject appStage);

        GvaApplicationStage UpdateApplicationStage(int appId, int stageId, JObject appStage);
    }
}
