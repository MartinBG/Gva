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

namespace Gva.MigrationTool.Sets
{
    public class ExaminationSystemDataMigrator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, IPersonRepository, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;
        private IUnitOfWork unitOfWork;
        private UserContext userContext;
        private IPersonRepository personRepository;

        public ExaminationSystemDataMigrator(
            OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, IPersonRepository, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
        }

        public void MigrateExaminationSystemData(
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
                this.personRepository = dependencies.Value.Item2;
                this.userContext = dependencies.Value.Item3;

                try
                {

                    var allNewQualifications = this.oracleConn.CreateStoreCommand(@"SELECT * FROM GVA_XM_QUALIFICATIONS_V@exams")
                    .Materialize(r =>
                        new GvaExSystQualification
                        {
                            Name = r.Field<string>("QLF_NAME"),
                            Code = r.Field<string>("QLF_CODE")
                        });

                    var allCurrentQualificationCodes = allNewQualifications.Select(q => q.Code).ToList();

                    this.unitOfWork.DbContext.Set<GvaExSystQualification>().AddRange(allNewQualifications);

                    var allNewExams = this.oracleConn.CreateStoreCommand(@"SELECT * FROM GVA_XM_TESTS_V@exams")
                    .Materialize(r =>
                        new GvaExSystExam
                        {
                            Name = r.Field<string>("TEST_NAME"),
                            Code = r.Field<string>("TEST_CODE"),
                            QualificationCode = r.Field<string>("QLF_CODE")
                        })
                        .Where(t => allCurrentQualificationCodes.Contains(t.QualificationCode));

                    this.unitOfWork.DbContext.Set<GvaExSystExam>().AddRange(allNewExams);
                    var allCurrentExamCodes = allNewExams.Select(q => q.Code).ToList();

                    var allNewCertCampaigns = this.oracleConn.CreateStoreCommand(@"SELECT * FROM GVA_CERT_CAMPAIGNS_V@exams")
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

                    var allNewCertPaths = this.oracleConn.CreateStoreCommand(@"SELECT * FROM GVA_XM_QUALIFICATION_PATHS_V@exams")
                    .Materialize(r =>
                        new GvaExSystCertPath
                        {
                            Name = r.Field<string>("QLF_PATH_NAME"),
                            Code = r.Field<int>("QLF_PATH_ID"),
                            ValidFrom = r.Field<DateTime?>("QLF_PATH_VALID_FROM"),
                            ValidTo = r.Field<DateTime?>("QLF_PATH_VALID_TO"),
                            ExamCode = r.Field<string>("TEST_CODE"),
                            QualificationCode = r.Field<string>("QLF_CODE")
                        })
                        .Where(t =>
                            allCurrentQualificationCodes.Contains(t.QualificationCode) &&
                            allCurrentExamCodes.Contains(t.ExamCode));

                    this.unitOfWork.DbContext.Set<GvaExSystCertPath>().AddRange(allNewCertPaths);

                    var allNewExaminees = this.oracleConn.CreateStoreCommand(@"SELECT * FROM GVA_XM_EXAMINEES_V@exams where LIN is not null or EGN is not null")
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
                        .Where(t =>
                            this.personRepository.GetPersons(lin: t.Lin, uin: t.Uin).Count() > 0 &&
                            allCurrentExamCodes.Contains(t.ExamCode) &&
                            (allCurrentCertCampaignCodes.Contains(t.CertCampCode) || t.CertCampCode == null))
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
                                CertCampCode = r.CertCampCode
                            };
                        });

                    this.unitOfWork.DbContext.Set<GvaExSystExaminee>().AddRange(allNewExaminees);

                    this.unitOfWork.DbContext.SaveChanges();
                }
                catch (Exception)
                {
                    cts.Cancel();
                    throw;
                }
            }
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
