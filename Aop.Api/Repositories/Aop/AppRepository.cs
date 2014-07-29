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

namespace Aop.Api.Repositories.Aop
{
    public class AppRepository : Repository<AopApp>, IAppRepository
    {
        public AppRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public List<AopApp> GetApps(int limit, int offset, out int totalCount)
        {
            System.Linq.Expressions.Expression<Func<AopApp, bool>> predicate = PredicateBuilder
                .True<AopApp>();

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

        public AopEmployerType GetAopEmployerTypeByAlias(string alias)
        {
            return this.unitOfWork.DbContext.Set<AopEmployerType>().FirstOrDefault(e => e.Alias.ToLower() == alias.ToLower());
        }
    }
}
