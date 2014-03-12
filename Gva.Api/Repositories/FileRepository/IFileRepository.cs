using Gva.Api.ModelsDO;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.Repositories.FileRepository
{
    public interface IFileRepository
    {
        void AddFileReferences(Part part, dynamic files);

        void DeleteFileReferences(int partId);

        FileDO[] GetFileReferences(int partId);

        ApplicationDO[] GetFileApplications(int partId);
    }
}
