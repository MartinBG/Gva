using AutoMapper;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.FileRepository;

namespace Gva.Api.Mappers.Resolvers
{
    public class FileResolver : ValueResolver<int, FileDO[]>
    {
        private IFileRepository fileRepository;

        public FileResolver(IFileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        protected override FileDO[] ResolveCore(int partId)
        {
            return this.fileRepository.GetFileReferences(partId);
        }
    }
}
