using Common.Api.UserContext;
using Docs.Api.Enums;
using Docs.Api.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Api.Repositories;
using System.Linq.Expressions;
using Common.Api.Models;

namespace Docs.Api.Repositories.ClassificationRepository
{
    public interface IClassificationRepository : IRepository<Classification>
    {
        ClassificationPermission GetByAlias(string alias);

        bool HasPermission(int unitId, int docId, int permissionId);
    }
}
