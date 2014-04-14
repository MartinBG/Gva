using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Linq;
using Gva.Api.Models;
using System.Data.Entity;
using Gva.Api.ModelsDO;
using Regs.Api.Models;

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
            bool exact = false,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<GvaViewPerson>();

            predicate = predicate
                .AndStringMatches(p => p.Lin, lin, exact)
                .AndStringMatches(p => p.LinType, linType, exact)
                .AndStringMatches(p => p.Uin, uin, exact)
                .AndStringMatches(p => p.Names, names, exact)
                .AndCollectionContains(p => p.Licences.Select(l => l.LicenceType), licences)
                .AndCollectionContains(p => p.Ratings.Select(r => r.RatingType), ratings)
                .AndStringMatches(p => p.Organization, organization, exact);

            var persons = this.unitOfWork.DbContext.Set<GvaViewPerson>()
                .Include(p => p.Licences)
                .Include(p => p.Ratings)
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
                .Include(e => e.Licences)
                .Include(e => e.Ratings)
                .SingleOrDefault(p => p.LotId == personId);
        }
    }
}
