using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Newtonsoft.Json.Linq;
using Regs.Api.Models;

namespace Gva.Api.Repositories.FileRepository
{
    public interface IFileRepository
    {
        void AddFileReferences(Part part, dynamic files);

        void DeleteFileReferences(int partId);

        GvaLotFile[] GetFileReferences(int partId);

        GvaApplication[] GetApplications(int lotId);

        void AddApplication(GvaApplication application);

        void DeleteApplication(int gvaAppLotPartId);
    }
}
