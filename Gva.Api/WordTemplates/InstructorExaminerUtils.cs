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
    public class InstructorExaminerUtils
    {
        internal static List<object> GetPrivileges(List<string> authCodes)
        {
            List<object> result = new List<object>();
            foreach (string code in authCodes)
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

        internal static Tuple<List<object>, List<string>> GetRatingsPerInstructorExaminerByAuthCode(
                   IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
                   IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions,
                   string authCode,
                   INomRepository nomRepository)
        {
            NomValue authorizationGroup = nomRepository.GetNomValues("authorizationGroups").First(nv => nv.Code == authCode);
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
                                rating.Content.RatingClass == null ? string.Empty : rating.Content.RatingClass.Code,
                                rating.Content.RatingTypes.Count() > 0 ? string.Join(", ", rating.Content.RatingTypes.Select(rt => rt.Code)) : ""),
                            DATE = edition.Content.DocumentDateValidTo
                        });

                        authCodes.Add(authorization.Code);
                    }
                }
            }

            return new Tuple<List<object>, List<string>>(ratings, authCodes);
        }

        internal static Dictionary<string, List<object>> certificatePrivileges = new Dictionary<string, List<object>>()
        {
            {
                "CRI",
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.905.CRI (a)(1)",
                        PRIVILEGES = "издаване, потвърждаване на валидността и подновяване на квалификация за клас или тип за еднопилотни несложни самолети с ниски летателни характеристики за еднопилотна експлоатация",
                        PRIVILEGES_ALT = "the issue, revalidation or renewal of a class or type rating for non-complex non-high performance single-pilot aeroplanes in single-pilot operations"
                    },
                    new
                    {
                        REFERENCE = "FCL.905.CRI (a)(2)",
                        PRIVILEGES = "квалификация за теглене или фигурен пилотаж за категория самолети",
                        PRIVILEGES_ALT = "a towing or aerobatic rating for the aeroplane category"
                    }
                }
            },
            {   
                "FI", 
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.905.FI (a)",
                        PRIVILEGES = "летателно обучение за издаване, потвърждаване на валидността и подновяване на PPL, SPL, BPL и LAPL  на самолет ",
                        PRIVILEGES_ALT = "flight instruction for the issue, revalidation or renewal of a PPL, SPL, BPL and LAPL in the aeroplane"
                    },
                    new
                    {
                        REFERENCE = "FCL.905.FI (b)",
                        PRIVILEGES = "летателно обучение за издаване, потвърждаване на валидността и подновяване на квалификация за клас и тип за еднопилотни, еднодвигателни самолети, с изключение на еднопилотни сложни самолети с високи летателни характеристики",
                        PRIVILEGES_ALT = "flight instruction for the issue, revalidation or renewal of a class and type ratings for single-pilot, single-engine aeroplanes, except for single-pilot high performance complex aeroplanes"
                    },
                    new
                    {
                        REFERENCE = "FCL.905.FI (d)",
                        PRIVILEGES = "летателно обучение за CPL за самолет",
                        PRIVILEGES_ALT = "flight instruction for the CPL in the aeroplanes"
                    },
                    new
                    {
                        REFERENCE = "FCL.905.FI (e)",
                        RIVILIGES = "летателно обучение за квалификация за нощни полети",
                        PRIVILEGES_ALT = "flight instruction for the night rating"
                    },
                    new
                    {
                        REFERENCE = "FCL.905.FI (f)",
                        RIVILIGES = "летателно обучение за квалификация за теглене или фигурен пилотаж",
                        PRIVILEGES_ALT = "flight instruction for a towing or aerobatic rating"
                    },
                    new
                    {
                        REFERENCE = "FCL.905.FI (g)",
                        RIVILIGES = "летателно обучение за IR за самолет",
                        PRIVILEGES_ALT = "flight instruction for an IR in the aeroplane"
                    },
                    new
                    {
                        REFERENCE = "FCL.905.FI (h)",
                        RIVILIGES = "летателно обучение за квалификация за клас или тип еднопилотни, многодвигателни самолети, с изключение на еднопилотни сложни самолети с високи летателни характеристики",
                        PRIVILEGES_ALT = "flight instruction for a single-pilot multi-engine class or type ratings, except for single-pilot high performance complex aeroplanes"
                    },
                    new
                    {
                        REFERENCE = "FCL.905.FI (i)",
                        RIVILIGES = "летателно обучение за сертификат за FI, IRI, CRI, STI или MI",
                        RIVILIGES_ALT = "flight instruction for an FI, IRI, CRI, STI or MI certificate"
                    },
                    new
                    {
                        REFERENCE = "FCL.905.FI (j)",
                        RIVILIGES = "летателно обучение за MPL",
                        RIVILIGES_ALT = "flight instruction for an MPL"
                    }
                }
            },
            {
                "FTI", 
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.905.FTI (a)(1)",
                        PRIVILEGES = "придобиване на квалификации за летателни изпитания от категория 1 или 2",
                        PRIVILEGES_ALT = "the issue of category 1 or 2 flight test ratings, provided he/she holds the relevant category of flight test rating"
                    },
                    new
                    {
                        REFERENCE = "FCL.905.FTI (a)(2)",
                        PRIVILEGES = "издаване на сертификат за FTI в рамките на съответната категория квалификация за летателни изпитания",
                        PRIVILEGES_ALT = "the issue of an FTI certificate, within the relevant category of flight test rating"
                    },
                    new
                    {
                        REFERENCE = "FCL.905.FTI (b)",
                        PRIVILEGES = "Правата на FTI, който притежава квалификация за летателни изпитания от категория 1, включват предоставяне на летателно обучение и във връзка с квалификации за летателни изпитания от категория 2.",
                        PRIVILEGES_ALT = "The privileges of an FTI holding a category 1 flight test rating include the provision of flight instruction also in relation to category 2 flight test ratings"
                    }
                }
            },
            {
                "IRI", 
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.905.IRI (a)",
                        PRIVILEGES = "издаване, потвърждаване на валидността и подновяване на IR на самолет ",
                        PRIVILEGES_ALT = "issue, revalidation and renewal of an IR on the aeroplane",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.IRI (b)",
                        PRIVILEGES = "основната фаза на обучението на курс за MPL",
                        PRIVILEGES_ALT = "the basic phase of training on an MPL course",
                    }
                }
            },
            {
                "MCCIA", 
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.905.MCCI (a)(1)",
                        PRIVILEGES = "практическата част от курсове по MCC, когато не са съчетани с обучение за придобиване на квалификация за тип",
                        PRIVILEGES_ALT = "the practical part of MCC courses when not combined with type rating training",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.MCCI (a)(2)",
                        PRIVILEGES = "основната фаза на интегрирания курс за обучение за MPL",
                        PRIVILEGES_ALT = "the basic phase of the MPL integrated training course",
                    }
                }
            },
            {
                "MI", 
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.905.MI ",
                        PRIVILEGES = "летателно обучение за придобиване на квалификация за планински терени",
                        PRIVILEGES_ALT = "to carry out flight instruction for the issue of a mountain rating",
                    }
                }
            },
            {
                "SFI", 
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.905.SFI (a)",
                        PRIVILEGES = "издаване, потвърждаване на валидността и подновяване на IR",
                        PRIVILEGES_ALT = "the issue, revalidation and renewal of an IR",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.SFI (b)(1)",
                        PRIVILEGES = "издаване, потвърждаване на валидността и подновяване на квалификации за тип за еднопилотни сложни самолети с високи летателни характеристики за еднопилотна експлоатация",
                        PRIVILEGES_ALT = "the issue, revalidation and renewal of type ratings for single-pilot high performance complex aeroplanes in single-pilot operations",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.SFI (b)(2)",
                        PRIVILEGES = "издаване, потвърждаване на валидността и подновяване на квалификации за тип за еднопилотни сложни самолети с високи летателни характеристики за многопилотна експлоатация",
                        PRIVILEGES_ALT = "the issue, revalidation and renewal of type ratings for single-pilot high performance complex aeroplanes in multi-pilot operations",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.SFI (b)(2)(i)",
                        PRIVILEGES = "MCC",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.SFI (b)(2)(ii)",
                        PRIVILEGES = "курса за MPL за основната фаза",
                        PRIVILEGESA_ALT = "the MPL course on the basic phase"
                    }
                }
            },
            {
                "STI", 
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.905.STI (a)(1)",
                        PRIVILEGES = "летателно обучение на симулатор на самолет за издаване на свидетелство за правоспособност",
                        PRIVILEGES_ALT = "to carry out synthetic flight instruction in the aeroplane for the issue of a licence",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.STI (a)(2)",
                        PRIVILEGES = "издаване, потвърждаване на валидността и подновяване на IR и квалификация за клас или тип за еднопилотни ВС, с изключение на еднопилотни сложни самолети с високи летателни характеристики",
                        PRIVILEGES_ALT = "the issue, revalidation or renewal of an IR and a class or type rating for single-pilot aircraft, except for single-pilot high performance complex aeroplanes",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.STI (b)",
                        PRIVILEGES = "летателно обучение на симулатор по време на обучението по основни летателни умения от интегрирания курс за МРL",
                        PRIVILEGES_ALT = "synthetic flight instruction during the core flying skills training of the MPL integrated training course",
                    }
                }
            },
            {
                "TRI", 
                new List<object>()
                {
                    new
                    {
                        REFERENCE = "FCL.905.TRI (a)",
                        PRIVILEGES = "потвърждаване на валидността и подновяване на IR",
                        PRIVILEGES_ALT = "the revalidation and renewal of IRs",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.TRI (b)",
                        PRIVILEGES = "издаване на сертификат за TRI или SFI",
                        PRIVILEGES_ALT = "the issue of a TRI or SFI certificate",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.TRI (d)(1)(i)",
                        PRIVILEGES = "издаване, потвърждаване на валидността и подновяване на квалификации за тип за многопилотни самолети",
                        PRIVILEGES_ALT = "the issue, revalidation and renewal of type ratings for multi-pilot aeroplanes",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.TRI (d)(1)(ii)",
                        PRIVILEGES = "издаване, потвърждаване на валидността и подновяване на квалификации за тип за еднопилотни сложни самолети с високи летателни характеристики за многопилотна експлоатация",
                        PRIVILEGES_ALT = "the issue, revalidation and renewal of type ratings for single-pilot high performance complex aeroplanes in multi-pilot operations",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.TRI (d)(2)",
                        PRIVILEGES = "обучение по MCC",
                        PRIVILEGES_ALT = "MCC training",
                    },
                    new
                    {
                        REFERENCE = "FCL.905.TRI (d)(3)",
                        PRIVILEGES = "курса за MPL за основната, междинната и напредналата фаза",
                        PRIVILEGES_ALT = "the MPL course on the basic, intermediate and advanced phases",
                    }
                }
            },
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

        internal static List<Tuple<string, string>> authCodeRegexExpToCertificatePrivilegeCode = new List<Tuple<string, string>>()
        {
            new Tuple<string, string>("(^CRI" + Regex.Escape("(") +")", "CRI"),
            new Tuple<string, string>("(^CRI\\s\\w+)", "CRI"),
            new Tuple<string, string>("(^FI" + Regex.Escape("(") +")", "FI"),
            new Tuple<string, string>("(^IRI" + Regex.Escape("(") +")", "IRI"),
            new Tuple<string, string>("(^SFI" + Regex.Escape("(") +")", "SFI"),
            new Tuple<string, string>("(^STI" + Regex.Escape("(") +")", "STI"),
            new Tuple<string, string>("(^TRI" + Regex.Escape("(") +")", "TRI"),
            new Tuple<string, string>("(^MCCIA" + Regex.Escape("(") +")", "MCCIA"),
            new Tuple<string, string>("(^MI" + Regex.Escape("(") +")", "MI"),
            new Tuple<string, string>("(^FTI" + Regex.Escape("(") +")", "FTI"),
            new Tuple<string, string>("(^CRE" + Regex.Escape("(") +")", "CRE"),
            new Tuple<string, string>("(^FE" + Regex.Escape("(") +")", "FE"),
            new Tuple<string, string>("(^FIE" + Regex.Escape("(") +")", "FIE"),
            new Tuple<string, string>("(^IRE" + Regex.Escape("(") +")", "IRE"),
            new Tuple<string, string>("(^SFE" + Regex.Escape("(") +")", "SFE"),
            new Tuple<string, string>("(^TRE" + Regex.Escape("(") +")", "TRE")
        };
    }
}
