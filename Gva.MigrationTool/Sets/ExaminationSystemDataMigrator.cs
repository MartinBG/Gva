using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Tests;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.PrintRepository;
using Gva.Api.CommonUtils;
using Newtonsoft.Json.Linq;
using Oracle.DataAccess.Client;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;
using Gva.Api.Repositories.PersonRepository;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.ModelsDO;
using Common.Api.Models;
using Gva.Api.Repositories.FileRepository;
using Gva.Api.ModelsDO.ExaminationSystem;

namespace Gva.MigrationTool.Sets
{
    public class ExaminationSystemDataMigrator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, IFileRepository, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;
        private IUnitOfWork unitOfWork;
        private UserContext userContext;
        private ICaseTypeRepository caseTypeRepository;
        private IFileRepository fileRepository;

        public ExaminationSystemDataMigrator(
            OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, IFileRepository, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public void MigrateExaminationSystemData(
            Dictionary<int, int> personIdToLotId,
            ConcurrentQueue<int> personIds,
            //cancellation
            CancellationTokenSource cts,
            CancellationToken ct)
        {
            try
            {
                this.oracleConn.Open();
            }
            catch (Exception)
            {
                cts.Cancel();
                throw;
            }

            ct.ThrowIfCancellationRequested();
            using (var dependencies = this.dependencyFactory())
            {
                this.unitOfWork = dependencies.Value.Item1;
                var lotRepository = dependencies.Value.Item2;
                this.caseTypeRepository = dependencies.Value.Item3;
                this.fileRepository = dependencies.Value.Item4;
                this.userContext = dependencies.Value.Item5;

                try
                {
                    int personId;
                    while (personIds.TryDequeue(out personId))
                    {

                        var lot = lotRepository.GetLotIndex(personIdToLotId[personId], fullAccess: true);

                        this.oracleConn.CreateStoreCommand(@"SELECT DISTINCT QLF_CODE, QLF_NAME FROM CAA_DOC.EXAMS_QUALIFICATION")
                           .Materialize(r =>
                               new GvaExSystQualification
                               {
                                   Name = r.Field<string>("QLF_NAME"),
                                   Code = r.Field<string>("QLF_CODE")
                               });

                        var cases = this.caseTypeRepository.GetCaseTypesForSet("person")
                            .Select(t => new CaseDO()
                            {
                                CaseType = new NomValue()
                                {
                                    NomValueId = t.GvaCaseTypeId,
                                    Name = t.Name,
                                    Alias = t.Alias
                                },
                                IsAdded = true
                            })
                            .ToList();

                        var exams = this.GetExams(personId);
                        var states = this.GetStates(personId);
                        PersonExamSystDataDO data = new PersonExamSystDataDO()
                        {
                            Exams = exams,
                            States = states
                        };

                        PartVersion<PersonExamSystDataDO> examSystDataPartVersion = lot.CreatePart("personExamSystData", data, this.userContext);

                        this.fileRepository.AddFileReferences(examSystDataPartVersion.Part, cases);
                    }

                    this.unitOfWork.Save();
                }
                catch (Exception)
                {
                    cts.Cancel();
                    throw;
                }
            }
        }

        public void MigrateDataForExaminations(
            Dictionary<int, int> personIdToLotId,
            //cancellation
            CancellationTokenSource cts,
            CancellationToken ct)
        {
            try
            {
                this.oracleConn.Open();
            }
            catch (Exception)
            {
                cts.Cancel();
                throw;
            }

            ct.ThrowIfCancellationRequested();
            using (var dependencies = this.dependencyFactory())
            {
                this.unitOfWork = dependencies.Value.Item1;

                try
                {
                    var allNewQualifications = this.oracleConn.CreateStoreCommand(@"SELECT DISTINCT QLF_CODE, QLF_NAME FROM CAA_DOC.EXAMS_QUALIFICATION")
                           .Materialize(r =>
                               new GvaExSystQualification
                               {
                                   Name = r.Field<string>("QLF_NAME"),
                                   Code = r.Field<string>("QLF_CODE")
                               });

                    this.unitOfWork.DbContext.Set<GvaExSystQualification>().AddRange(allNewQualifications);

                    var allNewExams = this.oracleConn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.EXAMS_TEST WHERE QLF_CODE IS NOT NULL")
                    .Materialize(r =>
                        new GvaExSystExam
                        {
                            Name = r.Field<string>("TEST_NAME"),
                            Code = r.Field<string>("TEST_CODE"),
                            QualificationCode = r.Field<string>("QLF_CODE")
                        });

                    this.unitOfWork.DbContext.Set<GvaExSystExam>().AddRange(allNewExams);

                    var allNewCertCampaigns = this.oracleConn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.EXAMS_CERT_CAMPAIGN WHERE QLF_CODE IS NOT NULL")
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

                    var allNewCertPaths = this.oracleConn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.EXAMS_CERT_PATH  WHERE QLF_CODE IS NOT NULL")
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

                    var allExaminees = this.oracleConn.CreateStoreCommand(@"SELECT * FROM CAA_DOC.EXAMS_EXAMINEE where ID_PERSON IS NOT NULL")
                    .Materialize(r =>
                        new
                        {
                            Lin = r.Field<int?>("LIN"),
                            Uin = r.Field<string>("EGN"),
                            ExamCode = r.Field<string>("TEST_CODE"),
                            EndTime = r.Field<DateTime>("END_TIME"),
                            TotalScore = r.Field<float>("TOTAL_SCORE").ToString(),
                            ResultStatus = r.Field<string>("RESULT_STATUS"),
                            CertCampCode = r.Field<string>("CERT_CAMP_CODE"),
                            PersonId = r.Field<int>("ID_PERSON")
                        })
                        .Where(p => personIdToLotId.ContainsKey(p.PersonId))
                        .Select(p => new GvaExSystExaminee()
                        {
                            Lin = p.Lin,
                            Uin = p.Uin,
                            ExamCode = p.ExamCode,
                            EndTime = p.EndTime,
                            TotalScore = p.TotalScore,
                            ResultStatus = p.ResultStatus,
                            CertCampCode = p.CertCampCode,
                            LotId = personIdToLotId[p.PersonId]
                        });

                    this.unitOfWork.DbContext.Set<GvaExSystExaminee>().AddRange(allExaminees);

                    this.unitOfWork.Save();
                }
                catch (Exception)
                {
                    cts.Cancel();
                    throw;
                }
            }
        }

        private List<PersonExamSystExamDO> GetExams(int personId)
        {
            return this.oracleConn.CreateStoreCommand(
                        @"SELECT DISTINCT
                                 ee.end_time,
                                 ee.total_score,
                                 ee.result_status,
                                 ee.cert_camp_code,
                                 ee.cert_camp_name,
                                 ee.test_code,
                                 ee.test_name,
                                 ecc.qlf_code,
                                 ecc.qlf_name,
                                 ecc.cert_camp_valid_to,
                                 ecc.cert_camp_valid_from
                            FROM CAA_DOC.exams_examinee ee,
                                 (select distinct cert_camp_name, cert_camp_code, qlf_code, qlf_name, cert_camp_valid_to, cert_camp_valid_from from CAA_DOC.exams_cert_campaign) ecc,
                                 (select distinct test_code, test_name from CAA_DOC.exams_test) et,
                                 CAA_DOC.person p
                            WHERE  ee.cert_camp_code = ecc.cert_camp_code (+)
                            AND  ee.test_code = et.test_code (+)
                            AND  (p.egn = ee.egn or p.lin = ee.lin)
                            AND  {0}",
                            new DbClause("p.id = {0}", personId)
                        ).Materialize(r =>
                            new PersonExamSystExamDO
                            {
                                Exam = new GvaExSystExamDO()
                                {
                                    Code = r.Field<string>("test_code"),
                                    Name = r.Field<string>("test_name"),
                                    QualificationCode = r.Field<string>("qlf_code"),
                                    QualificationName = r.Field<string>("qlf_name")
                                },
                                CertCamp = new GvaExSystCertCampaignDO() 
                                {
                                    Code = r.Field<string>("cert_camp_code"),
                                    Name = r.Field<string>("cert_camp_name"),
                                    QualificationCode = r.Field<string>("qlf_code"),
                                    QualificationName = r.Field<string>("qlf_name"),
                                    ValidFrom = r.Field<DateTime?>("cert_camp_valid_from"),
                                    ValidTo = r.Field<DateTime?>("cert_camp_valid_to")
                                },
                                EndTime = r.Field<DateTime>("end_time"),
                                Status = r.Field<string>("result_status"),
                                TotalScore = r.Field<string>("total_score"),
                            })
                            .ToList();
        }

        private List<PersonExamSystStateDO> GetStates(int personId)
        { 
            Dictionary<int, string> states = new Dictionary<int, string> ()
            {
                { 1, "Started"}, 
                { 2, "Cenceled"},
                { 3, "Finished"}
            };

            return this.oracleConn.CreateStoreCommand(
                        @"SELECT es.id,
                            es.date_from,
                            es.date_to,
                            es.state,
                            eq.qlf_code,
                            eq.qlf_name,
                            es.id_person,
                            es.state_method,
                            es.notes_auto_state
                            FROM  CAA_DOC.EXAMS_STATE_LOG es,
                                CAA_DOC.EXAMS_QUALIFICATION eq
                            WHERE es.qlf_code = eq.qlf_code and {0}",
                            new DbClause("es.ID_PERSON = {0}", personId)
                        ).Materialize(r =>
                            new PersonExamSystStateDO
                            {
                                FromDate = r.Field<DateTime>("date_from"),
                                ToDate = r.Field<DateTime?>("date_to"),
                                State =  states[r.Field<int>("state")],
                                StateMethod = r.Field<int>("state_method") == 1 ? "Automatically" : "Manually",
                                Notes = r.Field<string>("notes_auto_state"),
                                Qualification = new GvaExSystQualification()
                                {
                                    Code = r.Field<string>("qlf_code"),
                                    Name = r.Field<string>("qlf_name")
                                }
                            })
                            .ToList();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Dispose(bool disposing)
        {
            try
            {
                if (disposing && !disposed)
                {
                    this.oracleConn.Dispose();
                }
            }
            finally
            {
                disposed = true;
            }
        }
    }
}
