using AutoMapper;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.FileRepository;

namespace Gva.Api.Mappers.Resolvers
{
    public class ApplicationsResolver : ValueResolver<int, ApplicationDO[]>
    {
        private IFileRepository fileRepository;

        public ApplicationsResolver(IFileRepository fileRepository)
        {
            this.fileRepository = fileRepository;
        }

        protected override ApplicationDO[] ResolveCore(int partId)
        {
            return this.fileRepository.GetFileApplications(partId);
        }
    }
}
