using System;
using System.Collections.Generic;
using System.Web.Http;
using Gva.Api.Models;
using Gva.Api.Models.Views.Person;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.ExaminationSystem;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Models;

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

        List<GvaViewPerson> GetAwExaminers(string names = null, int offset = 0, int? limit = null);

        List<GvaViewPerson> GetStaffExaminers(string names = null, int offset = 0, int? limit = null);

        GvaViewPerson GetPerson(int personId);

        IEnumerable<ASExamVariant> GetQuestions(
            int asExamQuestionTypeId,
            string name = null,
            bool exact = false,
            int offset = 0,
            int? limit = null);

        Tuple<int, IEnumerable<GvaViewPersonLicenceEditionDO>> GetPrintableDocs(
            int? licenceType = null,
            int? licenceAction = null,
            int? lin = null,
            string uin = null,
            string names = null,
            int offset = 0,
            int? limit = null);

        int? GetNextLin(int linTypeId);

        bool IsUniqueUin(string uin, int? personId = null);

        Tuple<int, IEnumerable<GvaViewPersonLicenceEditionDO>> GetStampedDocuments(
            string uin,
            string names,
            string stampNumber,
            int? lin = null,
            int? licenceNumber = null,
            int offset = 0,
            int? limit = null);

        IEnumerable<GvaLicenceEdition> GetLicences(int lotId, int? caseTypeId);

        string GetLastLicenceNumber(int lotId, string licenceTypeCode);

        int GetLastLicenceEditionIndex(int lotId, int licencePartIndex);

        IEnumerable<GvaViewPersonRating> GetRatings(int lotId, int? caseTypeId);

        int GetLastRatingEditionIndex(int lotId, int ratingPartIndex);

        GvaViewPersonDocument IsUniqueDocData(
            string documentNumber = null,
            int? documentPersonNumber = null,
            int? partIndex = null,
            int? typeId = null,
            int? roleId = null,
            string publisher = null,
            DateTime? dateValidFrom = null);

        bool IsUniqueLicenceNumber(string licenceTypeCode, int? licenceNumber);

        List<GvaViewPersonCheck> GetChecksForReport(List<int> checks);
    }
}
