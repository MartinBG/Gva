using System.Linq;
using Common.Data;
using Docs.Api.Models;
using Common.Api.Repositories;
using Docs.Api.Models.ClassificationModels;

namespace Docs.Api.Repositories.ClassificationRepository
{
    public class ClassificationRepository : Repository<Classification>, IClassificationRepository
    {
        public ClassificationRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public ClassificationPermission GetByAlias(string alias)
        {
            return this.unitOfWork.DbContext.Set<ClassificationPermission>().FirstOrDefault(e => e.Alias.ToLower() == alias.ToLower());
        }

        public DocStatus GetDocStatusByAlias(string alias)
        {
            return this.unitOfWork.DbContext.Set<DocStatus>().FirstOrDefault(e => e.Alias.ToLower() == alias.ToLower());
        }

        public bool HasPermission(int unitId, int docId, int permissionId)
        {
            return this.unitOfWork.DbContext.Set<vwDocUser>().Any(e => e.DocId == docId && e.UnitId == unitId && e.ClassificationPermissionId == permissionId);
        }
    }
}
