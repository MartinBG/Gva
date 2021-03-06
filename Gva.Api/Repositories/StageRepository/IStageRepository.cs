﻿using System.Collections.Generic;
using Gva.Api.Models;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.Repositories.StageRepository
{
    public interface IStageRepository
    {
        IEnumerable<GvaStage> GetStages(string name = null);

        GvaStage GetStage(int id);
    }
}
