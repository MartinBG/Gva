﻿using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.Repositories.FileRepository
{
    public interface IFileRepository
    {
        void AddFileReferences(PartVersion partVersion, dynamic files);

        void DeleteFileReferences(PartVersion partVersion);

        GvaLotFile[] GetFileReferences(int partId, int? casetype);

        GvaLotFile[] GetFileReferencesForLot(int lotId, int caseType);
    }
}