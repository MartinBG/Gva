using Docs.Api.Models;
using Common.Api.Repositories;
using Docs.Api.Models.ClassificationModels;

namespace Docs.Api.Repositories.ClassificationRepository
{
    public interface IClassificationRepository : IRepository<Classification>
    {
        ClassificationPermission GetByAlias(string alias);

        DocStatus GetDocStatusByAlias(string alias);

        bool HasPermission(int unitId, int docId, int permissionId);
    }
}
