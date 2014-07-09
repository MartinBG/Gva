using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Common.Data;
using Common.Linq;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Repositories.PersonRepository
{
    public class PersonRepository : IPersonRepository
    {
        private IUnitOfWork unitOfWork;

        public PersonRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GvaViewPerson> GetPersons(
            string lin = null,
            string linType = null,
            string uin = null,
            string names = null,
            string licences = null,
            string ratings = null,
            string organization = null,
            string caseTypeAlias = null,
            bool isInspector = false,
            bool isExaminer = false,
            bool exact = false,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<GvaViewPerson>();

            predicate = predicate
                .AndStringMatches(p => p.Lin, lin, exact)
                .AndStringMatches(p => p.LinType.Name, linType, exact)
                .AndStringMatches(p => p.Uin, uin, exact)
                .AndStringMatches(p => p.Names, names, exact)
                .AndStringCollectionContains(p => p.Licences.Select(l => l.LicenceType.Name), licences)
                .AndStringCollectionContains(p => p.Ratings.Select(r => r.RatingType.Name), ratings)
                .AndStringMatches(p => p.Organization == null ? null : p.Organization.Name, organization, exact);

            if (isExaminer)
            {
                predicate = predicate.And(p => p.OrganizationExaminers.Count != 0);
            }

            if (isInspector)
            {
                predicate = predicate.And(p => p.Inspector != null);
            }

            var persons = this.unitOfWork.DbContext.Set<GvaViewPerson>()
                .Include(p => p.LinType)
                .Include(p => p.Organization)
                .Include(p => p.Employment)
                .Include(p => p.Inspector)
                .Include(p => p.OrganizationExaminers)
                .Include(p => p.Licences)
                .Include(p => p.Licences.Select(l => l.LicenceType))
                .Include(p => p.Ratings)
                .Include(p => p.Ratings.Select(r => r.RatingType))
                .Where(predicate);

            if (!string.IsNullOrEmpty(caseTypeAlias))
            {
                persons = from p in persons
                          join lc in this.unitOfWork.DbContext.Set<GvaLotCase>() on p.LotId equals lc.LotId
                          join ct in this.unitOfWork.DbContext.Set<GvaCaseType>() on lc.GvaCaseTypeId equals ct.GvaCaseTypeId
                          where ct.Alias == caseTypeAlias
                          select p;
            }

            return persons
                .OrderBy(p => p.Names)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public GvaViewPerson GetPerson(int personId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewPerson>()
                .Include(p => p.LinType)
                .Include(p => p.Organization)
                .Include(p => p.Employment)
                .Include(p => p.Inspector)
                .Include(p => p.OrganizationExaminers)
                .Include(e => e.Licences)
                .Include(p => p.Licences.Select(l => l.LicenceType))
                .Include(e => e.Ratings)
                .Include(p => p.Ratings.Select(r => r.RatingType))
                .SingleOrDefault(p => p.LotId == personId);
        }

        public IEnumerable<ASExamVariant> GetQuestions(
            int asExamQuestionTypeId,
            string name = null,
            bool exact = false,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<ASExamVariant>();

            predicate = predicate
                .AndStringMatches(p => p.Name, name, exact)
                .And(e => e.ASExamQuestionTypeId == asExamQuestionTypeId);

            var asExamVariants = this.unitOfWork.DbContext.Set<ASExamVariant>()
                .Where(predicate);

            return asExamVariants
                .OrderBy(p => p.Name)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }
    }
}
