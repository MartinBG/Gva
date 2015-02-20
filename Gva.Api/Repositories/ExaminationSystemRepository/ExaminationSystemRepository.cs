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
using Gva.Api.Models.Enums;

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
                    ExamName = c.Exam.Name,
                    QualificationName = c.Qualification.Name
                })
                .ToList();
        }

        public List<GvaExSystExamDO> GetExams(
            string qualificationCode = null,
            string certCampCode = null,
            string examCode = null,
            int? certPathCode = null)
        {
            var predicate = PredicateBuilder.True<GvaExSystExam>();

            if (!string.IsNullOrEmpty(qualificationCode))
            {
                predicate = predicate.And(q => q.QualificationCode == qualificationCode);
            }

            if (!string.IsNullOrEmpty(qualificationCode))
            {
                predicate = predicate.And(q => q.Code == examCode);
            }

            if (!string.IsNullOrEmpty(certCampCode))
            {
                predicate = predicate.And(q => q.Qualification.CertCampaigns.Select(c => c.Code).Contains(certCampCode));
            }

            var request = this.unitOfWork.DbContext.Set<GvaExSystExam>().Where(predicate);
            if (certPathCode.HasValue)
            {
                request = request.Where(r => r.CertPaths.Any(p => p.Code == certPathCode));
            }

            return request
                .Select(c => new GvaExSystExamDO()
                {
                    Code = c.Code,
                    Name = c.Name,
                    QualificationName = c.Qualification.Name
                })
                .ToList();
        }

        public List<GvaExSystExamineeDO> GetExaminees(int? lotId = null)
        {
            if (lotId.HasValue)
            {
                return (from e in this.unitOfWork.DbContext.Set<GvaExSystExaminee>()
                                 join t in this.unitOfWork.DbContext.Set<GvaExSystExam>() on e.ExamCode equals t.Code
                                 where e.LotId == lotId
                                 select new { Examinee = e, Test = t })
                                 .Select(r => new GvaExSystExamineeDO()
                                 {
                                     TotalScore = r.Examinee.TotalScore,
                                     CertCampCode = r.Examinee.CertCampaign.Code,
                                     CertCampName = r.Examinee.CertCampaign.Name,
                                     ResultStatus = r.Examinee.ResultStatus,
                                     Uin = r.Examinee.Uin,
                                     Lin = r.Examinee.Lin,
                                     LotId = r.Examinee.LotId,
                                     EndTime = r.Examinee.EndTime,
                                     ExamName = r.Test.Name,
                                     ExamCode = r.Test.Code,
                                     QualificationCode = r.Test.QualificationCode
                                 }).ToList();
            }
            else
            {
                return (from e in this.unitOfWork.DbContext.Set<GvaExSystExaminee>()
                        join t in this.unitOfWork.DbContext.Set<GvaExSystExam>() on e.ExamCode equals t.Code
                        select new { Examinee = e, Test = t })
                        .Select(r => new GvaExSystExamineeDO()
                        {
                            TotalScore = r.Examinee.TotalScore,
                            CertCampCode = r.Examinee.CertCampaign.Code,
                            CertCampName = r.Examinee.CertCampaign.Name,
                            ResultStatus = r.Examinee.ResultStatus,
                            Uin = r.Examinee.Uin,
                            Lin = r.Examinee.Lin,
                            LotId = r.Examinee.LotId,
                            EndTime = r.Examinee.EndTime,
                            ExamName = r.Test.Name,
                            ExamCode = r.Test.Code,
                            QualificationCode = r.Test.QualificationCode
                        }).ToList();
            }
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

        public void ExtractDataFromExaminationSystem() 
        {
            using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["ExaminationSystem"].ConnectionString))
            {
                connection.Open();
                OracleCommand command = new OracleCommand();
                command.Connection = connection;
                command.CommandText = "ad_application_context_p.set_default_context";
                command.CommandType = CommandType.StoredProcedure;
                command.ExecuteNonQuery();

                this.unitOfWork.DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.GvaExSystExaminees");
                this.unitOfWork.DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.GvaExSystCertPaths");
                this.unitOfWork.DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.GvaExSystCertCampaigns");
                this.unitOfWork.DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.GvaExSystExams");
                this.unitOfWork.DbContext.Database.ExecuteSqlCommand("TRUNCATE TABLE dbo.GvaExSystQualifications");
                this.unitOfWork.Save();

                var allNewQualifications = connection.CreateStoreCommand(@"SELECT * FROM GVA_XM_QUALIFICATIONS_V@exams")
                .Materialize(r => 
                    new GvaExSystQualification
                    {
                        Name = r.Field<string>("QLF_NAME"),
                        Code = r.Field<string>("QLF_CODE")
                    });

                this.unitOfWork.DbContext.Set<GvaExSystQualification>().AddRange(allNewQualifications);

                var allNewExams = connection.CreateStoreCommand(@"SELECT * FROM GVA_XM_TESTS_V@exams where QLF_CODE is not null")
                .Materialize(r => 
                    new GvaExSystExam
                    {
                        Name = r.Field<string>("TEST_NAME"),
                        Code = r.Field<string>("TEST_CODE"),
                        QualificationCode = r.Field<string>("QLF_CODE")
                    });

                this.unitOfWork.DbContext.Set<GvaExSystExam>().AddRange(allNewExams);

                var allNewCertCampaigns = connection.CreateStoreCommand(@"SELECT * FROM GVA_CERT_CAMPAIGNS_V@exams where QLF_CODE is not null")
                    .Materialize(r =>
                    new GvaExSystCertCampaign
                    {
                        Name = r.Field<string>("CERT_CAMP_NAME"),
                        Code = r.Field<string>("CERT_CAMP_CODE"),
                        ValidFrom = r.Field<DateTime?>("CERT_CAMP_VALID_FROM"),
                        ValidTo = r.Field<DateTime?>("CERT_CAMP_VALID_TO"),
                        QualificationCode = r.Field<string>("QLF_CODE")
                    });

                this.unitOfWork.DbContext.Set<GvaExSystCertCampaign>().AddRange(allNewCertCampaigns);

                var allNewCertPaths = connection.CreateStoreCommand(@"SELECT * FROM GVA_XM_QUALIFICATION_PATHS_V@exams where QLF_CODE is not null")
                .Materialize(r =>
                    new GvaExSystCertPath
                    {
                        Name = r.Field<string>("QLF_PATH_NAME"),
                        Code = r.Field<int>("QLF_PATH_ID"),
                        ValidFrom = r.Field<DateTime?>("QLF_PATH_VALID_FROM"),
                        ValidTo = r.Field<DateTime?>("QLF_PATH_VALID_TO"),
                        ExamCode = r.Field<string>("TEST_CODE"),
                        QualificationCode = r.Field<string>("QLF_CODE")
                    });

               this.unitOfWork.DbContext.Set<GvaExSystCertPath>().AddRange(allNewCertPaths);

               var allNewExaminees = connection.CreateStoreCommand(@"SELECT * FROM GVA_XM_EXAMINEES_V@exams where LIN is not null or EGN is not null")
                .Materialize(r =>
                new
                {
                    Lin = r.Field<int?>("LIN"),
                    Uin = r.Field<string>("EGN"),
                    ExamCode = r.Field<string>("TEST_CODE"),
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
                        ExamCode = r.ExamCode,
                        TotalScore = r.TotalScore,
                        ResultStatus = r.ResultStatus,
                        CertCampCode = r.CertCampCode,
                        EndTime = r.EndTime
                    };
                });

               this.unitOfWork.DbContext.Set<GvaExSystExaminee>().AddRange(allNewExaminees);
            }
        }

        public void ReloadStates()
        {
            var allPersonsWithQualificationsToReload =
                this.unitOfWork.DbContext.Set<GvaExSystExaminee>()
                .Include(e => e.Exam)
                .ToList()
                .GroupBy(q => q.LotId);

            Dictionary<string, GvaExSystQualification> allQualifications = this.unitOfWork.DbContext.Set<GvaExSystQualification>()
                .Include(c => c.CertPaths)
                .ToDictionary(q => q.Code, q => q);

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

            foreach (var person in allPersonsWithQualificationsToReload)
            {
                this.ReloadStatePerPerson(person.Key, person.ToList(), allQualifications, cases);
            }
        }

        private void ReloadStatePerPerson(
            int lotId,
            List<GvaExSystExaminee> allPersonExams,
            Dictionary<string, GvaExSystQualification> allQualifications,
            List<CaseDO> cases)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            PartVersion<PersonExamSystDataDO> examSystDataPartVersion = lot.Index.GetPart<PersonExamSystDataDO>("personExamSystData");

            var qualificationCodes = allPersonExams.Select(e => e.Exam.QualificationCode).Distinct();
            List<GvaExSystQualification> allPersonQualifications = new List<GvaExSystQualification>();
            foreach(var qualificationCode in qualificationCodes)
            {
                allPersonQualifications.Add(allQualifications[qualificationCode]);
            }

            List<PersonExamSystStateDO> newStates = new List<PersonExamSystStateDO>();
            foreach (GvaExSystQualification qualification in allPersonQualifications)
            {
                var certPaths = qualification.CertPaths.GroupBy(p => p.Code);
                foreach (var certPath in certPaths)
                {
                    List<string> requiredExamCodes = certPath.Select(p => p.ExamCode).Distinct().ToList();
                    List<GvaExSystExaminee> availableExams = allPersonExams
                            .Where(e => requiredExamCodes.Contains(e.ExamCode))
                            .ToList();

                    PersonExamSystStateDO lastStartedState = null;
                    if (examSystDataPartVersion != null)
                    {
                        lastStartedState = examSystDataPartVersion.Content.States
                            .Where(s => s.QualificationCode == qualification.Code && s.State == "Started")
                            .OrderByDescending(s => s.FromDate)
                            .FirstOrDefault();

                        PersonExamSystStateDO lastFinishedState = examSystDataPartVersion.Content.States
                            .Where(s => s.QualificationCode == qualification.Code && s.State == "Finished")
                            .OrderByDescending(s => s.FromDate)
                            .FirstOrDefault();

                        if (lastFinishedState != null)
                        {
                            availableExams = availableExams
                                .Where(t => t.EndTime >= lastFinishedState.FromDate)
                                .ToList();
                        }

                        if (lastStartedState != null)
                        {
                            if (availableExams.Count() == 0)
                            {
                                continue;
                            }

                            var lastExam = availableExams.OrderBy(e => e.EndTime).Last();
                            if (lastStartedState.ToDate.HasValue ? DateTime.Compare(lastExam.EndTime, lastStartedState.ToDate.Value) > 0 : false)
                            {
                                lastStartedState.State = QualificationState.Canceled.ToString();
                                lastStartedState.Notes = "Съществува изпит след датата на приключване на статуса";
                                lastStartedState.StateMethod = QualificationStateMethod.Automatically.ToString();

                                newStates.Add(new PersonExamSystStateDO()
                                {
                                    FromDate = lastExam.EndTime,
                                    ToDate = lastExam.EndTime.AddMonths(18),
                                    QualificationCode = qualification.Code,
                                    QualificationName = qualification.Name,
                                    StateMethod = QualificationStateMethod.Automatically.ToString(),
                                    State = QualificationState.Started.ToString()
                                });
                                break;
                            }

                            if (availableExams.Select(t => t.CertCampCode).Count() > 6)
                            {
                                lastStartedState.Notes = "Достигнат е пределно допустим брой сесиии";
                                lastStartedState.StateMethod = QualificationStateMethod.Automatically.ToString();
                                lastStartedState.State = QualificationState.Canceled.ToString();
                                break;
                            }
                            else if (availableExams.GroupBy(c => c.ExamCode).Any(t => t.Count() > 4))
                            {
                                lastStartedState.Notes = "Достигнат е пределно допустим брой на невзети изпити за тест";
                                lastStartedState.StateMethod = QualificationStateMethod.Automatically.ToString();
                                lastStartedState.State = QualificationState.Canceled.ToString();
                                break;
                            }
                        }
                    }

                    if (availableExams.Count() == 0)
                    {
                        continue;
                    }

                    var firstExam = availableExams.OrderBy(t => t.EndTime).First();
                    bool allExamsArePassed = true;
                    foreach (string requiredExamCode in requiredExamCodes)
                    {
                        if (!availableExams.Any(t => t.ResultStatus == "passed" && t.ExamCode == requiredExamCode))
                        {
                            allExamsArePassed = false;
                            break;
                        }
                    }

                    if (allExamsArePassed)
                    {
                        newStates.Add(new PersonExamSystStateDO()
                        {
                            FromDate = firstExam.EndTime,
                            ToDate = firstExam.EndTime.AddMonths(18),
                            QualificationCode = qualification.Code,
                            QualificationName = qualification.Name,
                            StateMethod = QualificationStateMethod.Automatically.ToString(),
                            State = QualificationState.Finished.ToString()
                        });
                        break;
                    }

                    if (availableExams.Where(t => t.ResultStatus == "passed").Count() > 0 && lastStartedState == null)
                    {
                        newStates.Add(new PersonExamSystStateDO()
                        {
                            FromDate = firstExam.EndTime,
                            ToDate = firstExam.EndTime.AddMonths(18),
                            QualificationCode = qualification.Code,
                            QualificationName = qualification.Name,
                            StateMethod = QualificationStateMethod.Automatically.ToString(),
                            State = QualificationState.Started.ToString()
                        });
                        break;
                    }
                }
            }

            if (newStates.Count() > 0)
            {
                if (examSystDataPartVersion == null)
                {
                    examSystDataPartVersion = lot.CreatePart("personExamSystData", new PersonExamSystDataDO() { States = newStates}, this.userContext);
                    this.fileRepository.AddFileReferences(examSystDataPartVersion.Part, cases);
                }
                else
                {
                    examSystDataPartVersion.Content.States.AddRange(newStates);
                    lot.UpdatePart("personExamSystData", examSystDataPartVersion.Content, this.userContext);
                }

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(examSystDataPartVersion.PartId);
            }
        }
    }
}