using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.Repositories.FileRepository
{
    public interface IFileRepository
    {
        void AddFileReferences(PartVersion partVersion, IEnumerable<FileDO> files);

        void DeleteFileReferences(PartVersion partVersion);

        GvaLotFile[] GetFileReferences(int partId, int? casetype);

        bool IsUniqueBPN(int lotId, int caseTypeId, string bookPageNumber, int? fileId = null);

        int? GetPageIndexInt(string pageIndex);

        int GetNextBPN(int lotId, int caseTypeId);
    }
}
