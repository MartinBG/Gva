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
            string uin = null,
            string names = null,
            string licences = null,
            string ratings = null,
            string organization = null,
            bool exact = false,
            int offset = 0,
            int? limit = null)
        {
            var gvaPersons = this.unitOfWork.DbContext.Set<GvaViewPersonData>();
            var gvaRatings = this.unitOfWork.DbContext.Set<GvaViewPersonRating>();
            var gvaLicences = this.unitOfWork.DbContext.Set<GvaViewPersonLicence>();

            var predicate = PredicateBuilder.True<GvaViewPerson>();

            predicate = predicate
                .AndStringMatches(p => p.Data.Lin, lin, exact)
                .AndStringMatches(p => p.Data.Uin, uin, exact)
                .AndStringMatches(p => p.Data.Names, names, exact)
                .AndCollectionContains(p => p.Licences.Select(l => l.LicenceType), licences)
                .AndCollectionContains(p => p.Ratings.Select(r => r.RatingType), ratings)
                .AndStringMatches(p => p.Data.Organization, organization, exact);

            return gvaPersons
                .GroupJoin(
                    gvaRatings,
                    p => p.GvaPersonLotId,
                    pr => pr.LotId,
                    (p, pr) =>
                        new
                        {
                            Data = p,
                            Ratings = pr
                        })
                .GroupJoin(
                    gvaLicences,
                    p => p.Data.GvaPersonLotId,
                    pl => pl.LotId,
                    (p, pl) =>
                        new GvaViewPerson
                        {
                            Data = p.Data,
                            Ratings = p.Ratings,
                            Licences = pl
                        })
                .Where(predicate)
                .OrderBy(p => p.Data.Names)
                .WithOffsetAndLimit(offset, limit)
                .ToList();
        }

        public GvaViewPersonData GetPerson(int personId)
        {
            return this.unitOfWork.DbContext.Set<GvaViewPersonData>()
                .SingleOrDefault(p => p.GvaPersonLotId == personId);
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
