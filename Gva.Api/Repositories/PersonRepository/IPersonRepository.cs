using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.ModelsDO;

namespace Gva.Api.Repositories.PersonRepository
{
    public interface IPersonRepository
    {
        IEnumerable<GvaViewPerson> GetPersons(
            string lin = null,
            string uin = null,
            string names = null,
            string licences = null,
            string ratings = null,
            string organization = null,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        GvaViewPerson GetPerson(int personId);

        GvaCorrespondent GetGvaCorrespondentByPersonId(int lotId);

        void AddGvaCorrespondent(GvaCorrespondent gvaCorrespondent);
    }
}
