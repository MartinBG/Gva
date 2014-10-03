using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Regs.Api.Models;

namespace Gva.Api.Repositories.FileRepository
{
    public interface IFileRepository
    {
        void AddFileReferences(Part part, IEnumerable<CaseDO> cases);

        void AddFileReference(Part part, CaseDO caseDO);

        void DeleteFileReferences(Part part);

        GvaLotFile[] GetFileReferences(int partId, int? casetype);

        bool IsUniqueBPN(int lotId, int caseTypeId, string bookPageNumber, int? fileId = null);

        int? GetPageIndexInt(string pageIndex);

        int GetNextBPN(int lotId, int caseTypeId);
    }
}
