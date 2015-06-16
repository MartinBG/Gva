using System.Linq;
using System.Text.RegularExpressions;
using Common.Api.Repositories.NomRepository;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class InstructorCert : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public InstructorCert(
            ILotRepository lotRepository,
            INomRepository nomRepository)
        {
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
        }

        public string GeneratorCode
        {
            get
            {
                return "instructorCert";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Сертификат за инструктор";
            }
        }

        public object GetData(int lotId, string path)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            var licence = lot.Index.GetPart<PersonLicenceDO>(path);

            var editions = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                .Where(e => e.Content.LicencePartIndex == licence.Part.Index)
                .OrderBy(e => e.Content.Index);
            var lastEdition = editions.Last().Content;

            var includedRatings = lastEdition.IncludedRatings
                .Select(i => i.Ind)
                .Distinct()
                .Select(i => lot.Index.GetPart<PersonRatingDO>("ratings/" + i.Value));
            var ratingEditions = lastEdition.IncludedRatings.Select(i => lot.Index.GetPart<PersonRatingEditionDO>("ratingEditions/" + i.Index));
            var ratingsPerInstructorAndAuthCodes = InstructorExaminerUtils.GetRatingsPerInstructorExaminerByAuthCode(includedRatings, ratingEditions, "FT", this.nomRepository);

            string code = null;
            foreach (string authCode in ratingsPerInstructorAndAuthCodes.Item2)
            {
                var authorizationGroup = nomRepository.GetNomValues("instructorExaminerCertificateAttachmentAuthorizations").Where(i =>
                    Regex.Matches(authCode, "(^" + i.Code + Regex.Escape("(") + ")").Count > 0 ||
                    Regex.Matches(authCode, "(^" + i.Code + "\\s\\w+)").Count > 0 ||
                    i.Code == authCode)
                    .FirstOrDefault();

                if (authorizationGroup != null)
                {
                    code = authorizationGroup.Code;
                    break;
                }
            }

            var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
            string number = string.Format(
                "BG/{0}/{1}", code, personData.Lin);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Content.LicenceType.NomValueId);
            
            var licenceNumber = string.Format(
                "{0} - {1} - {2}",
                licenceType.Code.Replace("/", "."),
                Utils.PadLicenceNumber(licence.Content.LicenceNumber),
                personData.Lin);

            string personNames = string.Format("{0} {1} {2}",personData.FirstName, personData.MiddleName, personData.LastName);
            string personNamesAlt = string.Format("{0} {1} {2}",personData.FirstNameAlt, personData.MiddleNameAlt, personData.LastNameAlt);

            var privileges = InstructorExaminerUtils.GetPrivileges(ratingsPerInstructorAndAuthCodes.Item2, this.nomRepository);

            var json = new
            {
                root = new
                {
                    NAMES = personNames,
                    NAMES_ALT = personNamesAlt,
                    NUMBER = number,
                    NUMBER_FOOTER = number,
                    LICENCE_NUMBER = licenceNumber,
                    RATINGS = Utils.FillBlankData(ratingsPerInstructorAndAuthCodes.Item1, 1),
                    NUMBER2 = number,
                    NUMBER_FOOTER2 = number,
                    NAMES2 = personNames,
                    NAMES_ALT2 = personNamesAlt,
                    PRIVILEGES = Utils.FillBlankData(privileges, 1),
                    ISSUE_DATE2 = lastEdition.DocumentDateValidFrom.Value
                }
            };

            return json;
        }
    }
}
