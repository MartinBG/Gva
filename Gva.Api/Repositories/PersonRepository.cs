using Common.Data;
using Gva.Api.Models;
using System.Collections.Generic;
using System.Linq;

namespace Gva.Api.Repositories
{
    public class PersonRepository : IPersonRepository
    {
        private IUnitOfWork unitOfWork;

        public PersonRepository(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public IEnumerable<GvaPerson> GetPersons(string lin, string uin, string names, string licences, string ratings, string organization, bool exact)
        {
            var persons = this.unitOfWork.DbContext.Set<GvaPerson>().AsQueryable();

            if (!string.IsNullOrWhiteSpace(lin))
            {
                persons = persons.Where(p => exact ? p.Lin == lin : p.Lin.Contains(lin));
            }

            if (!string.IsNullOrWhiteSpace(uin))
            {
                persons = persons.Where(p => exact ? p.Uin == uin : p.Uin.Contains(uin));
            }

            if (!string.IsNullOrWhiteSpace(names))
            {
                persons = persons.Where(p => exact ? p.Names == names : p.Names.Contains(names));
            }

            if (!string.IsNullOrWhiteSpace(licences))
            {
                persons = persons.Where(p => exact ? p.Licences == licences : p.Licences.Contains(licences));
            }

            if (!string.IsNullOrWhiteSpace(ratings))
            {
                persons = persons.Where(p => exact ? p.Ratings == ratings : p.Ratings.Contains(ratings));
            }

            if (!string.IsNullOrWhiteSpace(organization))
            {
                persons = persons.Where(p => exact ? p.Organization == organization : p.Organization.Contains(organization));
            }

            return persons;
        }

        public GvaPerson GetPerson(int personId)
        {
            return this.unitOfWork.DbContext.Set<GvaPerson>()
                .SingleOrDefault(p => p.GvaPersonLotId == personId);
        }
    }
}
