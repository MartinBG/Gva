using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.WordTemplates
{
    public class ExaminerCert : IDataGenerator
    {
        private ILotRepository lotRepository;
        private INomRepository nomRepository;

        public ExaminerCert(
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
                return "examinerCert";
            }
        }

        public string GeneratorName
        {
            get
            {
                return "Сертификат за проверяващ";
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
            var ratingsPerExaminerAndAuthCodes = this.GetRatingsPerExaminerAndAuthCodes(includedRatings, ratingEditions);
         
            string code = null;
            foreach(string authCode in ratingsPerExaminerAndAuthCodes.Item2)
            {
                var authorizationCode = authCodeRegexExpToCertificatePrivilegeCode
                    .Where(a => Regex.Matches(authCode, a.Item1).Count > 0)
                    .FirstOrDefault();
                if (authorizationCode != null)
                {
                    code = authorizationCode.Item2;
                    break;
                }
            }

            var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
            string number = string.Format("BG/{0}/{1}", code, personData.Lin);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Content.LicenceType.NomValueId);
            
            var licenceNumber = string.Format(
                "{0} - {1} - {2}",
                licenceType.Code.Replace("/", "."),
                Utils.PadLicenceNumber(licence.Content.LicenceNumber),
                personData.Lin);

            string personNames = string.Format("{0} {1} {2}",personData.FirstName, personData.MiddleName, personData.LastName);
            string personNamesAlt = string.Format("{0} {1} {2}",personData.FirstNameAlt, personData.MiddleNameAlt, personData.LastNameAlt);

            var privileges = this.GetPrivileges(ratingsPerExaminerAndAuthCodes.Item2);

            var json = new
            {
                root = new
                {
                    NAMES = personNames,
                    NAMES_ALT = personNamesAlt,
                    NUMBER = number,
                    NUMBER_FOOTER = number,
                    LICENCE_NUMBER = licenceNumber,
                    RATINGS = Utils.FillBlankData(ratingsPerExaminerAndAuthCodes.Item1, 1),
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

        private List<object> GetPrivileges(List<string> authCodes)
        { 
            List<object> result = new List<object>();
            foreach(string code in authCodes)
            {
                var authorizationCode = authCodeRegexExpToCertificatePrivilegeCode
                    .Where(a => Regex.Matches(code, a.Item1).Count > 0)
                    .FirstOrDefault();

                if (authorizationCode != null)
                {
                    result = result.Union(certificatePrivileges[authorizationCode.Item2]).ToList();
                }
            }

            return result;
        }

        private Tuple<List<object>, List<string>> GetRatingsPerExaminerAndAuthCodes(
                   IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
                   IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            NomValue authorizationGroup = this.nomRepository.GetNomValues("authorizationGroups").First(nv => nv.Code == "FC");
            List<object> ratings = new List<object>();
            List<string> authCodes = new List<string>();
            foreach (var edition in ratingEditions)
            {
                var rating = includedRatings.Where(r => r.Part.Index == edition.Content.RatingPartIndex).Single();
                if (rating.Content.Authorization != null)
                {
                    NomValue authorization = nomRepository.GetNomValue(rating.Content.Authorization.NomValueId);
                    if (authorization.ParentValueId != null &&
                        authorizationGroup.NomValueId == authorization.ParentValueId.Value &&
                        authorization.Code != "RTO")
                    {
                        ratings.Add(new
                        {
                            EXAMINER = authorization.Code,
                            CLASS_TYPE = string.Format(
                                "{0} {1}",
                                rating.Content.RatingClass == null ? string.Empty : rating.Content.RatingClass.Name,
                                rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code)) : ""),
                            DATE = edition.Content.DocumentDateValidTo
                        });

                        authCodes.Add(authorization.Code);
                    }
                }
            }

            return new Tuple<List<object>, List<string>>(ratings, authCodes);
        }

        private List<Tuple<string, string>> authCodeRegexExpToCertificatePrivilegeCode = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("(^CRE" + Regex.Escape("(") +")", "CRE"),
            new Tuple<string, string>("(^FE" + Regex.Escape("(") +")", "FE"),
            new Tuple<string, string>("(^FIE" + Regex.Escape("(") +")", "FIE"),
            new Tuple<string, string>("(^IRE" + Regex.Escape("(") +")", "IRE"),
            new Tuple<string, string>("(^SFE" + Regex.Escape("(") +")", "SFE"),
            new Tuple<string, string>("(^TRE" + Regex.Escape("(") +")", "TRE")
        };

        private Dictionary<string, List<object>> certificatePrivileges = new Dictionary<string, List<object>>()
        {
            {
               "CRE",
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.1005.CRE (a)(1)",
                        PRIVILEGES = "проверки на уменията за вписване на квалификации за клас и тип",
                        PRIVILEGES_ALT = "skill tests for the issue of class and type ratings"
                    },
                    new
                    {
                        REFERENCE = "FCL.1005.CRE (b)(1)",
                        PRIVILEGES = "проверки на професионалната подготовка за потвърждаване на валидността или подновяване на квалификация за клас и тип",
                        PRIVILEGES_ALT = "proficiency checks for revalidation or renewal of class and type ratings"
                    },
                    new
                    {
                        REFERENCE = "FCL.1005.CRE (b)(2)",
                        PRIVILEGES = "проверки на професионалната подготовка за потвърждаване на валидността и подновяване на квалификация за полети по прибори",
                        PRIVILEGES_ALT = "proficiency checks for revalidation and renewal of IRs"
                    }
                }
            },
            {
                "FE",
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.1005.FE (a)(1)",
                        PRIVILEGES = "проверки на уменията за издаване на PPL(A), както и проверки на уменията и проверки на професионалната подготовка за свързаните с него квалификации за клас и тип еднопилотни самолети, с изключение на еднопилотни сложни самолети с високи летателни характеристики",
                        PRIVILEGES_ALT = "skill tests for the issue of the PPL(A) and skill tests and proficiency checks for associated single-pilot class and type ratings, except for single-pilot high performance complex aeroplanes"
                    },
                    new
                    {
                        REFERENCE = "FCL.1005.FE (a)(2)",
                        PRIVILEGES = "проверки на уменията за издаване на CPL(A), както и проверки на уменията и проверки на професионалната подготовка за свързаните с него квалификации за клас и тип еднопилотни самолети, с изключение на еднопилотни сложни самолети с високи летателни характеристики",
                        PRIVILEGES_ALT = "skill tests for the issue of the CPL(A) and skill tests and proficiency checks for the associated single-pilot class and type ratings, except for single-pilot high performance complex aeroplanes"
                    },
                    new
                    {
                        REFERENCE = "FCL.1005.FE (a)(3)",
                        PRIVILEGES = "проверки на уменията и проверки на професионалната подготовка за LAPL(A)",
                        PRIVILEGES_ALT = "skill tests and proficiency checks for the LAPL(A)"
                    },
                    new
                    {
                        REFERENCE = "FCL.1005.FE (a)(4)",
                        PRIVILEGES = "проверки на уменията и проверки на професионалната подготовка за LAPL(A)",
                        PRIVILEGES_ALT = "skill tests and proficiency checks for the LAPL(A)"
                    }
                }
            },
            {
                "FIE",
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.1005.FIE (c)",
                        PRIVILEGES = "оценки на компетентността за издаването, потвърждаването на валидността или подновяването на сертификати за инструктор за съответната дирижабли",
                        PRIVILEGES_ALT = "assessments of competence for the issue, revalidation or renewal of instructor certificates on the airships"
                    }
                }
            },
            {
                "IRE",
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.1005.IRE",
                        PRIVILEGES = "проверки на уменията за издаване и да извършва проверки на професионалната подготовка за потвърждаване на валидността или за подновяване на квалификацията за полети по прибори",
                        PRIVILEGES_ALT = "skill tests for the issue, and proficiency checks for the revalidation or renewal of IRs"
                    }
                }
            },
            {
                "SFE",
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.1005.SFE (b)(1)",
                        PRIVILEGES = "проверки на уменията и проверки на професионалната подготовка за издаване, потвърждаване на валидността или подновяване на квалификация за тип",
                        PRIVILEGES_ALT = "skill tests and proficiency checks for the issue, revalidation and renewal of type ratings; and"
                    },
                    new
                    {
                        REFERENCE = "FCL.1005.SFE (b)(2)",
                        PRIVILEGES = "проверки на професионалната подготовка за потвърждаване на валидността и подновяване на квалификация за полети по прибори",
                        PRIVILEGES_ALT = "proficiency checks for the revalidation and renewal of IRs"
                    },
                    new
                    {
                        REFERENCE = "FCL.1005.SFE (b)(3)",
                        PRIVILEGES = "проверки на уменията за издаване на ATPL(H)",
                        PRIVILEGES_ALT = "skill tests for ATPL(H) issue"
                    },
                    new
                    {
                        REFERENCE = "FCL.1005.SFE (b)(4)",
                        PRIVILEGES = "проверки на уменията и проверки на професионалната подготовка за издаване, потвърждаване на валидността или подновяване на сертификат за SFI(H)",
                        PRIVILEGES_ALT = "skill tests and proficiency checks for the issue, revalidation or renewal of an SFI(H) certificate"
                    }
                }
            },
            {   
                "TRE",
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.1005.TRE (a)(1)",
                        PRIVILEGES = "Проверки на уменията за първоначално издаване на квалификации за тип за самолети ",
                        PRIVILEGES_ALT = "The privileges of a TRE for aeroplanes are to conduct skill tests for the initial issue of type ratings for aeroplanes"
                    },
                    new
                    {
                        REFERENCE = "FCL.1005.TRE (a)(2)",
                        PRIVILEGES = "проверки на професионалната подготовка за потвърждаване на валидността или подновяване на квалификации за тип и IR",
                        PRIVILEGES_ALT = "proficiency checks for revalidation or renewal of type and IRs"
                    },
                    new
                    {
                        REFERENCE = "FCL.1005.TRE (a)(3)",
                        PRIVILEGES = "проверки на уменията за издаване на ATPL(A)",
                        PRIVILEGES_ALT = "skill tests for ATPL(A) issue"
                    },
                    new
                    {
                        REFERENCE = "FCL.1005.TRE (a)(4)",
                        PRIVILEGES = "проверки на уменията за издаване на MPL, при условие че проверяващият отговаря на изискванията на FCL.925",
                        PRIVILEGES_ALT = "skill tests for MPL issue, provided that the examiner has complied with the requirements in FCL.925"
                    },
                    new
                    {
                        REFERENCE = "FCL.1005.TRE (a)(5)",
                        PRIVILEGES = "оценки на компетентността за издаване, потвърждаване на валидността или подновяване на сертификат за TRI или SFI за съответната категория ВС, при условие че проверяващият има поне 3 години опит като TRE",
                        PRIVILEGES_ALT = "assessments of competence for the issue, revalidation or renewal of a TRI or SFI certificate in the applicable aircraft category, provided that the examiner has completed at least 3 years as a TRE"
                    }
                }
            }
        };
    }
}
