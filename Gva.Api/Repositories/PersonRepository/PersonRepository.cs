using System.Collections.Generic;
using System.Linq;
using Common.Data;
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
