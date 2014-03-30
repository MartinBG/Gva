using System.Collections.Generic;
using Gva.Api.Models;

namespace Gva.Api.Repositories.PersonRepository
{
    public interface IPersonRepository
    {
        IEnumerable<GvaPerson> GetPersons(
            string lin = null,
            string uin = null,
            string names = null,
            string licences = null,
            string ratings = null,
            string organization = null,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        GvaPerson GetPerson(int personId);

        void AddPerson(GvaPerson person);

        GvaCorrespondent GetGvaCorrespondentByPersonId(int lotId);

        void AddGvaCorrespondent(GvaCorrespondent gvaCorrespondent);
    }
}
