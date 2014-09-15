using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Data;
using Aop.Api.Models;
using Common.Api.UserContext;
using Common.Api.Repositories;
using Common.Linq;
using Docs.Api.Models;
using System.Data.SqlClient;
using Common.Extensions;
using Common.Api.Models;

namespace Aop.Api.Repositories.Aop
{
    public class AppRepository : Repository<AopApp>, IAppRepository
    {
        public AppRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public void ExecSpSetAopApplicationTokens(int? aopApplicationId = null)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("AopApplicationId", Helper.CastToSqlDbValue(aopApplicationId)));

            this.ExecuteSqlCommand("spSetAopApplicationTokens @AopApplicationId", parameters);
        }

        public void ExecSpSetAopApplicationUnitTokens(int? aopApplicationId = null)
        {
            var parameters = new List<SqlParameter>();
            parameters.Add(new SqlParameter("AopApplicationId", Helper.CastToSqlDbValue(aopApplicationId)));

            this.ExecuteSqlCommand("spSetAopApplicationUnitTokens @AopApplicationId", parameters);
        }

        public List<AopApp> GetApps(
            int limit,
            int offset,
            UnitUser unitUser,
            ClassificationPermission readPermission,
            out int totalCount)
        {
            System.Linq.Expressions.Expression<Func<AopApp, bool>> predicate = PredicateBuilder
                .True<AopApp>();

            predicate = predicate.And(e => e.vwAopApplicationUsers.Any(v => v.ClassificationPermissionId == readPermission.ClassificationPermissionId && v.UnitId == unitUser.UnitId));

            //if (!string.IsNullOrEmpty(displayName))
            //{
            //    predicate = predicate.And(e => e.Contains(displayName));
            //    //query = query.Where(e => e.DisplayName.Contains(displayName));
            //}

            //if (!string.IsNullOrEmpty(correspondentEmail))
            //{
            //    predicate = predicate.And(e => e.DisplayName.Contains(displayName));
            //    //query = query.Where(e => e.Email.Contains(correspondentEmail));
            //}

            var query = this.unitOfWork.DbContext.Set<AopApp>()
                .Include(e => e.CreateUnit)
                .Include(e => e.AopEmployer)
                .Where(predicate);

            totalCount = query.Count();

            return query
                .OrderByDescending(e => e.AopApplicationId)
                .Skip(offset)
                .Take(limit)
                .ToList();
        }

        public AopApp CreateNewAopApp(int unitId, UserContext userContext)
        {
            AopApp app = new AopApp();
            app.CreateUnitId = unitId;

            this.unitOfWork.DbContext.Set<AopApp>().Add(app);

            return app;
        }

        public Doc GetDocByPortalDocId(Guid portalDocId)
        {
            var docsQuery =
                from d in this.unitOfWork.DbContext.Set<Doc>()
                join pdr in this.unitOfWork.DbContext.Set<AopPortalDocRelation>() on d.DocId equals pdr.DocId
                where pdr.PortalDocId == portalDocId
                select new
                {
                    docId = d.DocId
                };

            var doc = docsQuery.SingleOrDefault();

            if (doc != null)
            {
                return this.unitOfWork.DbContext.Set<Doc>()
                .Include(e => e.DocElectronicServiceStages.Select(s => s.ElectronicServiceStage))
                .SingleOrDefault(e => e.DocId == doc.docId);
            }
            else
            {
                return null;
            }
        }

        public void DeteleAopApp(int id)
        {
            AopApp app = this.Find(id);

            this.unitOfWork.DbContext.Set<AopApp>().Remove(app);
        }

        public AopEmployer CreateAopEmployer(string name, string lotNum, string uic, int aopEmployerTypeId)
        {
            AopEmployer emp = new AopEmployer();

            if (string.IsNullOrEmpty(name))
            {
                throw new Exception("Name for Aop Employer is missing");
            }

            emp.Name = name;
            emp.LotNum = lotNum;
            emp.Uic = uic;
            emp.AopEmployerTypeId = aopEmployerTypeId;

            return emp;
        }

        public NomValue GetAopEmployerTypeByAlias(string alias)
        {
            return this.unitOfWork.DbContext.Set<NomValue>().Where(nv => nv.Nom.Alias == "AopEmployerType" && nv.Alias == alias).SingleOrDefault();
        }

        public List<vwAopApplicationUser> GetvwAopApplicationUsersForAppByUnitId(int aopApplicationId, UnitUser unitUser)
        {
            return this.unitOfWork.DbContext.Set<vwAopApplicationUser>()
                .Include(e => e.ClassificationPermission)
                .Where(e => e.UnitId == unitUser.UnitId && e.AopApplicationId == aopApplicationId)
                .ToList();
        }

        public bool HasPermission(int unitId, int aopApplicationId, int permissionId)
        {
            return this.unitOfWork.DbContext.Set<vwAopApplicationUser>().Any(e => e.AopApplicationId == aopApplicationId && e.UnitId == unitId && e.ClassificationPermissionId == permissionId);
        }
    }
}
