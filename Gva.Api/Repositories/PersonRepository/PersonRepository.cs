using System.Collections.Generic;
using System.Linq;
using Common.Data;
using Common.Linq;
using Gva.Api.Models;
using System.Data.Entity;

namespace Gva.Api.Repositories.PersonRepository
{
    public class PersonRepository : IPersonRepository
    {
        private IUnitOfWork unitOfWork;

        public PersonRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GvaPerson> GetPersons(
            string lin = null,
            string uin = null,
            string names = null,
            string licences = null,
            string ratings = null,
            string organization = null,
            bool exact = false,
            int offset = 0,
            int? limit = null)
        {
            var predicate = PredicateBuilder.True<GvaPerson>();

            predicate = predicate
                .AndStringMatches(p => p.Lin, lin, exact)
                .AndStringMatches(p => p.Uin, uin, exact)
                .AndStringMatches(p => p.Names, names, exact)
                .AndStringMatches(p => p.Licences, licences, exact)
                .AndStringMatches(p => p.Ratings, ratings, exact)
                .AndStringMatches(p => p.Organization, organization, exact);

            return this.unitOfWork.DbContext.Set<GvaPerson>()
                .Where(predicate)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public GvaPerson GetPerson(int personId)
        {
            return this.unitOfWork.DbContext.Set<GvaPerson>()
                .SingleOrDefault(p => p.GvaPersonLotId == personId);
        }

        public void AddPerson(GvaPerson person)
        {
            this.unitOfWork.DbContext.Set<GvaPerson>().Add(person);
        }

        public GvaCorrespondent GetGvaCorrespondentByPersonId(int lotId)
        {
            return this.unitOfWork.DbContext.Set<GvaCorrespondent>()
                .Include(e => e.Correspondent)
                .SingleOrDefault(p => p.LotId == lotId);
        }

        public void AddGvaCorrespondent(GvaCorrespondent gvaCorrespondent)
        {
            this.unitOfWork.DbContext.Set<GvaCorrespondent>().Add(gvaCorrespondent);
        }
    }
}
