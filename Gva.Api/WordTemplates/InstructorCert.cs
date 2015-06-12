using System;
using System.Collections.Generic;
using System.Linq;
using Common.Api.Models;
using Common.Api.Repositories.NomRepository;
using Common.Json;
using Gva.Api.ModelsDO.Persons;
using Regs.Api.Models;
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
            var ratingsPerInstructorAndAuthCodes = this.GetRatingsPerInstructorAndAuthCodes(includedRatings, ratingEditions);

            string code = ratingsPerInstructorAndAuthCodes.Item2.FirstOrDefault(i => matchAuthCodeToCertificatePrivilegeCode.ContainsKey(i));

            var personData = lot.Index.GetPart<PersonDataDO>("personData").Content;
            string number = string.Format(
                "BG/{0}/{1}", !string.IsNullOrEmpty(code) ? matchAuthCodeToCertificatePrivilegeCode[code] : null, personData.Lin);

            var licenceType = this.nomRepository.GetNomValue("licenceTypes", licence.Content.LicenceType.NomValueId);
            
            var licenceNumber = string.Format(
                "{0} - {1} - {2}",
                licenceType.Code.Replace("/", "."),
                Utils.PadLicenceNumber(licence.Content.LicenceNumber),
                personData.Lin);

            string personNames = string.Format("{0} {1} {2}",personData.FirstName, personData.MiddleName, personData.LastName);
            string personNamesAlt = string.Format("{0} {1} {2}",personData.FirstNameAlt, personData.MiddleNameAlt, personData.LastNameAlt);

            var privileges = this.GetPrivileges(ratingsPerInstructorAndAuthCodes.Item2);

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

        private List<object> GetPrivileges(List<string> authCodes)
        { 
            List<object> result = new List<object>();
            foreach(string code in authCodes)
            {
                if (matchAuthCodeToCertificatePrivilegeCode.ContainsKey(code))
                {
                    result = result.Union(certificatePrivileges[matchAuthCodeToCertificatePrivilegeCode[code]]).ToList();
                }
            }

            return result;
        }

        private Tuple<List<object>, List<string>> GetRatingsPerInstructorAndAuthCodes(
                   IEnumerable<PartVersion<PersonRatingDO>> includedRatings,
                   IEnumerable<PartVersion<PersonRatingEditionDO>> ratingEditions)
        {
            NomValue authorizationGroup = this.nomRepository.GetNomValues("authorizationGroups").First(nv => nv.Code == "FT");
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

        private Dictionary<string, string> matchAuthCodeToCertificatePrivilegeCode = new Dictionary<string, string>()
        {
            {"CRI(SEA)", "CRI"},
            {"CRI SP(A)", "CRI"},
            {"CRI(MEA)", "CRI"},
            {"CRI(A)", "CRI"},
            {"CRI SP (A)", "CRI"},
            {"FI(SA)", "FI"},
            {"FI(H)", "FI"},
            {"FI(A)", "FI"},
            {"FI(G)", "FI"},
            {"IRI(A)", "IRI"},
            {"SFI(A)", "SFI"},
            {"STI(A)", "STI"},
            {"TRI(E)", "TRI"},
            {"TRI(H)", "TRI"},
            {"TRI(A)", "TRI"}
        };

        private Dictionary<string, List<object>> certificatePrivileges = new Dictionary<string, List<object>>()
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
        };
    }
}
