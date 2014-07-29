using Common.Api.Repositories;
using Common.Api.UserContext;
using Aop.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Docs.Api.Models;

namespace Aop.Api.Repositories.Aop
{
    public interface IAppRepository : IRepository<AopApp>
    {
        List<AopApp> GetApps(int limit, int offset, out int totalCount);

        AopApp CreateNewAopApp(int unitId, UserContext userContext);

        Doc GetDocByPortalDocId(Guid portalDocId);

        void DeteleAopApp(int id);

        AopEmployer CreateAopEmployer(string name, string lotNum, string uic, int aopEmployerTypeId);

        AopEmployerType GetAopEmployerTypeByAlias(string alias);
    }
}
