using System.Collections.Generic;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;

namespace Gva.Api.Repositories.PersonRepository
{
    public interface IPersonRepository
    {
        IEnumerable<GvaViewPerson> GetPersons(
            int? lin = null,
            string linType = null,
            string uin = null,
            int? caseTypeId = null,
            string names = null,
            string licences = null,
            string ratings = null,
            string organization = null,
            bool isInspector = false,
            bool isExaminer = false,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        GvaViewPerson GetPerson(int personId);

        IEnumerable<ASExamVariant> GetQuestions(
            int asExamQuestionTypeId,
            string name = null,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        IEnumerable<GvaLicenceEdition> GetPrintableDocs(
            int? licenceType = null,
            int? licenceAction = null,
            int? lin = null,
            string uin = null,
            string names = null,
            bool exact = false);

        int GetNextLin(int linTypeId);

        bool IsUniqueUin(string uin, int? personId = null);

        List<GvaLicenceEdition> GetStampedDocuments();

        IEnumerable<GvaLicenceEdition> GetLicences(int lotId);

        string GetLastLicenceNumber(int lotId, string licenceTypeCode);

        IEnumerable<GvaViewPersonRating> GetRatings(int lotId);
    }
}
