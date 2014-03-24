﻿using System.Collections.Generic;
using Gva.Api.Models;
using Regs.Api.Models;

namespace Gva.Api.Repositories.CaseTypeRepository
{
    public interface ICaseTypeRepository
    {
        void AddCaseTypes(Lot lot, dynamic caseTypes);

        GvaCaseType GetCaseType(int caseTypeId);

        IEnumerable<GvaCaseType> GetCaseTypesForSet(int setId);

        IEnumerable<GvaCaseType> GetCaseTypesForLot(int lotId);
    }
}