using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Data.Entity;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Linq;
using Gva.Api.CommonUtils;
using Gva.Api.Models;
using Gva.Api.Models.Vew;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.ExaminationSystem;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.Repositories.PersonRepository;
using Oracle.ManagedDataAccess.Client;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Repositories.ExaminationSystemRepository
{
    public class ExaminationSystemRepository : IExaminationSystemRepository
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IPersonRepository personRepository;
        private IFileRepository fileRepository;
        private ICaseTypeRepository caseTypeRepository;
        private ILotEventDispatcher lotEventDispatcher;
        private UserContext userContext;

        public ExaminationSystemRepository(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IPersonRepository personRepository,
            IFileRepository fileRepository,
            ICaseTypeRepository caseTypeRepository,
            ILotEventDispatcher lotEventDispatcher,
            UserContext userContext)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.personRepository = personRepository;
            this.fileRepository = fileRepository;
            this.caseTypeRepository = caseTypeRepository;
            this.lotEventDispatcher = lotEventDispatcher;
            this.userContext = userContext;
        }

        public List<GvaExSystCertCampaignDO> GetCertCampaigns(string code = null) {
            var predicate = PredicateBuilder.True<GvaExSystCertCampaign>();

            if (!string.IsNullOrEmpty(code))
            {
                predicate = predicate.And(q => q.Code == code);
            }

            return this.unitOfWork.DbContext.Set<GvaExSystCertCampaign>()
                .Select(c => new GvaExSystCertCampaignDO
                {
                        Code = c.Code,
                        Name = c.Name,
                        ValidFrom = c.ValidFrom,
                        ValidTo = c.ValidTo,
                        QualificationName = c.Qualification.Name
                })
                .ToList();
        }

        public List<GvaExSystCertPathDO> GetCertPaths()
        {
            return this.unitOfWork.DbContext.Set<GvaExSystCertPath>()
                .Select(c => new GvaExSystCertPathDO
                {
                    Code = c.Code,
                    Name = c.Name,
                    ValidFrom = c.ValidFrom,
                    ValidTo = c.ValidTo,
                    TestName = c.Test.Name,
                    QualificationName = c.Qualification.Name
                })
                .ToList();
        }

        public List<GvaExSystTestDO> GetTests(
            string qualificationCode = null,
            string certCampCode = null,
            string testCode = null,
            int? certPathCode = null)
        {
            var predicate = PredicateBuilder.True<GvaExSystTest>();

            if (!string.IsNullOrEmpty(qualificationCode))
            {
                predicate = predicate.And(q => q.QualificationCode == qualificationCode);
            }

            if (!string.IsNullOrEmpty(qualificationCode))
            {
                predicate = predicate.And(q => q.Code == testCode);
            }

            if (!string.IsNullOrEmpty(certCampCode))
            {
                predicate = predicate.And(q => q.Qualification.CertCampaigns.Select(c => c.Code).Contains(certCampCode));
            }

            var request = this.unitOfWork.DbContext.Set<GvaExSystTest>().Where(predicate);
            if (certPathCode.HasValue)
            {
                request = request.Where(r => r.CertPaths.Any(p => p.Code == certPathCode));
            }

            return request
                .Select(c => new GvaExSystTestDO()
                {
                    Code = c.Code,
                    Name = c.Name,
                    QualificationName = c.Qualification.Name
                })
                .ToList();
        }

        public List<GvaExSystExamineeDO> GetExaminees()
        {
            return (from e in this.unitOfWork.DbContext.Set<GvaExSystExaminee>()
                    join t in this.unitOfWork.DbContext.Set<GvaExSystTest>() on e.TestCode equals t.Code
                    select new GvaExSystExamineeDO
                    {
                        TotalScore = e.TotalScore,
                        CertCampName = e.CertCampaign.Name,
                        ResultStatus = e.ResultStatus,
                        Uin = e.Uin,
                        Lin = e.Lin,
                        LotId = e.LotId,
                        EndTime = e.EndTime,
                        TestName = t.Name
                    }).ToList();
        }

        public List<GvaExSystQualification> GetQualifications(string qualificationCode = null)
        {
            var predicate = PredicateBuilder.True<GvaExSystQualification>();

            if (!string.IsNullOrEmpty(qualificationCode))
            {
                predicate = predicate.And(q => q.Code == qualificationCode);
            }

            return this.unitOfWork.DbContext.Set<GvaExSystQualification>()
                .Where(predicate)
                .ToList();
        }

        private void SaveDataPerPerson(int lotId, List<PersonExamSystExamDO> examsData)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            PartVersion<PersonExamSystDataDO> examSystDataPartVersion = lot.Index.GetPart<PersonExamSystDataDO>("personExamSystData");

            if (examSystDataPartVersion == null)
            {
                var cases = this.caseTypeRepository.GetCaseTypesForSet("person")
                .Select(ct => new CaseDO()
                    {
                        CaseType = new NomValue()
                        {
                            NomValueId = ct.GvaCaseTypeId,
                            Name = ct.Name,
                            Alias = ct.Alias
                        },
                        IsAdded = true
                    })
                .ToList();

                PersonExamSystDataDO data = new PersonExamSystDataDO()
                {
                    Exams = examsData
                };

                examSystDataPartVersion = lot.CreatePart("personExamSystData", data, this.userContext);

                this.fileRepository.AddFileReferences(examSystDataPartVersion.Part, cases);
            }
            else
            {
                examSystDataPartVersion.Content.Exams = examSystDataPartVersion.Content.Exams
                    .Union(examsData)
                    .ToList();

                lot.UpdatePart("personExamSystData", examSystDataPartVersion.Content, this.userContext);
            }

            lot.Commit(this.userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            this.lotRepository.ExecSpSetLotPartTokens(examSystDataPartVersion.PartId);

        }

        public void ExtractDataFromExaminationSystem(bool extractExaminees) 
        {
            using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["ExaminationSystem"].ConnectionString))
            {
                connection.Open();
                OracleCommand command = new OracleCommand();
                command.Connection = connection;
                command.CommandText = "ad_application_context_p.set_default_context";
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();

                var allExaminees = from ex in this.unitOfWork.DbContext.Set<GvaExSystExaminee>() select ex;
                this.unitOfWork.DbContext.Set<GvaExSystExaminee>().RemoveRange(allExaminees);

                var allCertPaths = from cp in this.unitOfWork.DbContext.Set<GvaExSystCertPath>() select cp;
                this.unitOfWork.DbContext.Set<GvaExSystCertPath>().RemoveRange(allCertPaths);
                this.unitOfWork.Save();

                var allCertCampaigns = from cc in this.unitOfWork.DbContext.Set<GvaExSystCertCampaign>() select cc;
                this.unitOfWork.DbContext.Set<GvaExSystCertCampaign>().RemoveRange(allCertCampaigns);

                var allTests = from t in this.unitOfWork.DbContext.Set<GvaExSystTest>() select t;
                this.unitOfWork.DbContext.Set<GvaExSystTest>().RemoveRange(allTests);
                this.unitOfWork.Save();

                var allQualifications = from q in this.unitOfWork.DbContext.Set<GvaExSystQualification>() select q;
                this.unitOfWork.DbContext.Set<GvaExSystQualification>().RemoveRange(allQualifications);
                this.unitOfWork.Save();

                var allNewQualifications = connection.CreateStoreCommand(@"SELECT * FROM GVA_XM_QUALIFICATIONS_V@exams")
                .Materialize(r => 
                    new GvaExSystQualification
                    {
                        Name = r.Field<string>("QLF_NAME"),
                        Code = r.Field<string>("QLF_CODE")
                    });

                var allCurrentQualificationCodes = allNewQualifications.Select(q => q.Code).ToList();

                this.unitOfWork.DbContext.Set<GvaExSystQualification>().AddRange(allNewQualifications);

                var allNewTests = connection.CreateStoreCommand(@"SELECT * FROM GVA_XM_TESTS_V@exams")
                .Materialize(r => 
                    new GvaExSystTest
                    {
                        Name = r.Field<string>("TEST_NAME"),
                        Code = r.Field<string>("TEST_CODE"),
                        QualificationCode = r.Field<string>("QLF_CODE")
                    })
                    .Where(t => allCurrentQualificationCodes.Contains(t.QualificationCode));

                this.unitOfWork.DbContext.Set<GvaExSystTest>().AddRange(allNewTests);
                var allCurrentTestCodes = allNewTests.Select(q => q.Code).ToList();
                
                var allNewCertCampaigns = connection.CreateStoreCommand(@"SELECT * FROM GVA_CERT_CAMPAIGNS_V@exams")
                    .Materialize(r =>
                    new GvaExSystCertCampaign
                    {
                        Name = r.Field<string>("CERT_CAMP_NAME"),
                        Code = r.Field<string>("CERT_CAMP_CODE"),
                        ValidFrom = r.Field<DateTime?>("CERT_CAMP_VALID_FROM"),
                        ValidTo = r.Field<DateTime?>("CERT_CAMP_VALID_TO"),
                        QualificationCode = r.Field<string>("QLF_CODE")
                    })
                    .Where(t => allCurrentQualificationCodes.Contains(t.QualificationCode));

                this.unitOfWork.DbContext.Set<GvaExSystCertCampaign>().AddRange(allNewCertCampaigns);
                var allCurrentCertCampaignCodes = allNewCertCampaigns.Select(q => q.Code).ToList();

                var allNewCertPaths = connection.CreateStoreCommand(@"SELECT * FROM GVA_XM_QUALIFICATION_PATHS_V@exams")
                .Materialize(r =>
                    new GvaExSystCertPath
                    {
                        Name = r.Field<string>("QLF_PATH_NAME"),
                        Code = r.Field<int>("QLF_PATH_ID"),
                        ValidFrom = r.Field<DateTime?>("QLF_PATH_VALID_FROM"),
                        ValidTo = r.Field<DateTime?>("QLF_PATH_VALID_TO"),
                        TestCode = r.Field<string>("TEST_CODE"),
                        QualificationCode = r.Field<string>("QLF_CODE")
                    })
                    .Where(t =>
                        allCurrentQualificationCodes.Contains(t.QualificationCode) &&
                        allCurrentTestCodes.Contains(t.TestCode));

               this.unitOfWork.DbContext.Set<GvaExSystCertPath>().AddRange(allNewCertPaths);

               if (extractExaminees)
               {
                   this.ExtractDataFromExaminationSystemForExaminees(connection, allNewTests, allNewCertCampaigns, allNewQualifications);
               }
            }
        }

        private void ExtractDataFromExaminationSystemForExaminees(
            OracleConnection connection,
            IEnumerable<GvaExSystTest> allNewTests,
            IEnumerable<GvaExSystCertCampaign> allNewCertCampaigns,
            IEnumerable<GvaExSystQualification> allNewQualifications)
        {
            var allNewExaminees = connection.CreateStoreCommand(@"SELECT * FROM GVA_XM_EXAMINEES_V@exams where LIN is not null or EGN is not null")
                .Materialize(r =>
                new
                {
                    Lin = r.Field<int?>("LIN"),
                    Uin = r.Field<string>("EGN"),
                    TestCode = r.Field<string>("TEST_CODE"),
                    EndTime = r.Field<DateTime>("END_TIME"),
                    TotalScore = r.Field<float>("TOTAL_SCORE").ToString(),
                    ResultStatus = r.Field<string>("RESULT_STATUS"),
                    CertCampCode = r.Field<string>("CERT_CAMP_CODE")
                })
                .Where(r => this.personRepository.GetPersons(lin: r.Lin, uin: r.Uin).Count() > 0)
                .Select(r =>
                {
                    int lotId = this.personRepository.GetPersons(lin: r.Lin, uin: r.Uin).First().LotId;

                    return new GvaExSystExaminee
                    {
                        Lin = r.Lin,
                        LotId = lotId,
                        Uin = r.Uin,
                        TestCode = r.TestCode,
                        TotalScore = r.TotalScore,
                        ResultStatus = r.ResultStatus,
                        CertCampCode = r.CertCampCode,
                        EndTime = r.EndTime
                    };
                });

            this.unitOfWork.DbContext.Set<GvaExSystExaminee>().AddRange(allNewExaminees);

            Dictionary<int, List<PersonExamSystExamDO>> examsPerPersonData =
                (from e in allNewExaminees
                 join t in allNewTests on e.TestCode equals t.Code
                 select new
                 {
                     TotalScore = e.TotalScore,
                     CertCamp = allNewCertCampaigns.Where(c => c.Code == e.CertCampCode).Single(),
                     ResultStatus = e.ResultStatus,
                     LotId = e.LotId,
                     EndTime = e.EndTime,
                     Test = t
                 })
                .GroupBy(g => g.LotId)
                .ToDictionary(g => g.Key,
                    g => g.Select(e => new PersonExamSystExamDO()
                    {
                        TotalScore = e.TotalScore,
                        CertCamp = 
                            new GvaExSystCertCampaignDO() 
                            {
                                Code = e.CertCamp.Code,
                                Name = e.CertCamp.Name,
                                QualificationName = allNewQualifications.Where(q => q.Code == e.CertCamp.QualificationCode).Single().Name
                            },
                        Status = e.ResultStatus,
                        EndTime = e.EndTime,
                        Test =
                            new GvaExSystTestDO()
                            {
                                Code = e.Test.Code,
                                Name = e.Test.Name,
                                QualificationName = allNewQualifications.Where(q => q.Code == e.Test.QualificationCode).Single().Name,
                                QualificationCode = allNewQualifications.Where(q => q.Code == e.Test.QualificationCode).Single().Code
                            }
                    }).ToList());

            foreach (int lotId in examsPerPersonData.Keys)
            {
                this.SaveDataPerPerson(lotId, examsPerPersonData[lotId]);
            }
        }

        public void ReloadStates()
        {
            var allPersonsWithQualificationsToReload = this.unitOfWork.DbContext.Set<GvaExSystExaminee>()
                .GroupBy(q => q.LotId);

            foreach (var person in allPersonsWithQualificationsToReload)
            {
                this.ReloadStatePerPerson(person.Key);
            }
        }

        private void ReloadStatePerPerson(int lotId)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            PartVersion<PersonExamSystDataDO> examSystDataPartVersion = lot.Index.GetPart<PersonExamSystDataDO>("personExamSystData");
            List<PersonExamSystExamDO> allPersonExams = examSystDataPartVersion.Content.Exams;

            var qualificationsCodes = allPersonExams.Select(q => q.Test.QualificationCode);
            List <GvaExSystQualification> allPersonQualifications = this.unitOfWork.DbContext.Set<GvaExSystQualification>()
                .Include(c => c.CertCampaigns)
                .Include(c => c.CertPaths)
                .Where(q => qualificationsCodes.Contains(q.Code))
                .ToList();

            foreach (GvaExSystQualification qualification in allPersonQualifications)
            {
                var certPaths = qualification.CertPaths.GroupBy(p => p.Code);
                foreach (var certPath in certPaths)
                {
                    List<string> requiredTestCodes = certPath.Select(p => p.TestCode).Distinct().ToList();

                    PersonExamSystStateDO lastStatePerQualification = examSystDataPartVersion.Content.States
                        .Where(s => s.Qualification.Code == qualification.Code && s.State == "Started")
                        .OrderByDescending(s => s.FromDate)
                        .FirstOrDefault();

                    List<PersonExamSystExamDO> availableTests = allPersonExams
                            .Where(e => e.Test.QualificationCode == qualification.Code &&
                                requiredTestCodes.Contains(e.Test.Code))
                                 .ToList();

                    if (availableTests.Count() == 0)
                    {
                        continue;
                    }

                    if (lastStatePerQualification != null)
                    {
                        availableTests = availableTests
                            .Where(t => t.EndTime >= lastStatePerQualification.FromDate)
                            .ToList();

                        if (DateTime.Compare(DateTime.Now, lastStatePerQualification.ToDate) > 0)
                        {
                            lastStatePerQualification.Notes = "Не са взети всички необходими изпити за квалификацията за срока от 18 месеца";
                            lastStatePerQualification.StateMethod = "Automatic";
                            lastStatePerQualification.State = "Canceled";
                            continue;
                        }
                    }

                    if (availableTests.Select(t => t.CertCamp).Count() > 6 && lastStatePerQualification != null)
                    {
                        lastStatePerQualification.Notes = "Достигнат е пределно допустим брой сесиии";
                        lastStatePerQualification.StateMethod = "Automatic";
                        lastStatePerQualification.State = "Canceled";

                        continue;
                    }
                    else if (availableTests.GroupBy(c => c.Test.Code).Any(t => t.Count() > 4 && t.All(te => te.Status == "failed")) && lastStatePerQualification != null)
                    {
                        lastStatePerQualification.Notes = "Достигнат е пределно допустим брой на невзети изпити за тест";
                        lastStatePerQualification.StateMethod = "Automatic";
                        lastStatePerQualification.State = "Canceled";
                        continue;
                    }

                    var firstTestDate = availableTests.OrderByDescending(t => t.EndTime).Last().EndTime;
                    if (requiredTestCodes.All(rtc => availableTests.Where(t => t.Status == "passed").Select(t => t.Test.Code).Contains(rtc)))
                    {
                        PersonExamSystStateDO newStateFinished = new PersonExamSystStateDO()
                        {
                            FromDate = firstTestDate,
                            ToDate = firstTestDate.AddMonths(18),
                            Qualification = qualification,
                            StateMethod = "Automatic",
                            State = "Finished"
                        };

                        examSystDataPartVersion.Content.States.Add(newStateFinished);
                        continue;
                    }

                    if(availableTests.Where(t => t.Status == "passed").Count() > 0)
                    {
                        PersonExamSystStateDO newState = new PersonExamSystStateDO()
                        {
                            FromDate = firstTestDate,
                            ToDate = firstTestDate.AddMonths(18),
                            Qualification = qualification,
                            StateMethod = "Automatic",
                            State = "Started"
                        };

                        examSystDataPartVersion.Content.States.Add(newState);
                    }
                }
            }

            lot.UpdatePart("personExamSystData", examSystDataPartVersion.Content, this.userContext);

            lot.Commit(this.userContext, lotEventDispatcher);

            this.unitOfWork.Save();

            this.lotRepository.ExecSpSetLotPartTokens(examSystDataPartVersion.PartId);
        }
    }
}