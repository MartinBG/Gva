using Common.Api.Repositories;
using Common.Api.UserContext;
using Aop.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docs.Api.Models;
using Common.Api.Models;

namespace Aop.Api.Repositories.Aop
{
    public interface IAppRepository : IRepository<AopApp>
    {
        void ExecSpSetAopApplicationTokens(int? aopApplicationId = null);

        void ExecSpSetAopApplicationUnitTokens(int? aopApplicationId = null);

        List<AopApp> GetApps(
            int limit,
            int offset,
            UnitUser unitUser,
            ClassificationPermission readPermission,
            out int totalCount);

        AopApp CreateNewAopApp(int unitId, UserContext userContext);

        Doc GetDocByPortalDocId(Guid portalDocId);

        void DeteleAopApp(int id);

        AopEmployer CreateAopEmployer(string name, string lotNum, string uic, int aopEmployerTypeId);

        NomValue GetAopEmployerTypeByAlias(string alias);

        List<vwAopApplicationUser> GetvwAopApplicationUsersForAppByUnitId(int aopApplicationId, UnitUser unitUser);

        bool HasPermission(int unitId, int aopApplicationId, int permissionId);
    }
}
