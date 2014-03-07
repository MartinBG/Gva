using System;
using Regs.Api.Models;

namespace Gva.Api.Repositories.FileRepository
{
    public interface IFileRepository
    {
        void AddFileReference(Part part, Guid key, string name);

        void GetFileReference(int partId);
    }
}
