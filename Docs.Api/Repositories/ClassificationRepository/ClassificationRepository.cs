using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Docs.Api.Models;
using Common.Api.UserContext;
using Common.Extensions;
using Common.Linq;
using Common.Api.Models;
using Docs.Api.Enums;
using System.Data.SqlClient;
using Common.Api.Repositories;
using Common.Utils;
using System.Linq.Expressions;
using System.Data.Entity.Core;

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

        public bool HasPermission(int unitId, int docId, int permissionId)
        {
            return this.unitOfWork.DbContext.Set<vwDocUser>().Any(e => e.DocId == docId && e.UnitId == unitId && e.ClassificationPermissionId == permissionId);
        }
    }
}
