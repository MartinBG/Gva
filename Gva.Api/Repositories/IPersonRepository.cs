using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.Repositories
{
    public interface IPersonRepository
    {
        IEnumerable<GvaPerson> GetPersons(string lin, string uin, string names, string licences, string ratings, string organization, bool exact);

        GvaPerson GetPerson(int personId);

        void AddPerson(GvaPerson person);
    }
}
