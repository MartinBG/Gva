using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Autofac.Features.OwnedInstances;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Tests;
using Gva.Api.CommonUtils;
using Gva.Api.Models;
using Gva.Api.Models.Enums;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.CaseTypeRepository;
using Gva.Api.Repositories.FileRepository;
using Oracle.ManagedDataAccess.Client;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.MigrationTool.Sets
{
    public class ExaminationSystemDataMigrator : IDisposable
    {
        private bool disposed = false;
        private Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, IFileRepository, ILotEventDispatcher, UserContext>>> dependencyFactory;
        private OracleConnection oracleConn;
        private IUnitOfWork unitOfWork;
        private UserContext context;
        private ICaseTypeRepository caseTypeRepository;
        private IFileRepository fileRepository;

        public ExaminationSystemDataMigrator(
            OracleConnection oracleConn,
            Func<Owned<DisposableTuple<IUnitOfWork, ILotRepository, ICaseTypeRepository, IFileRepository, ILotEventDispatcher, UserContext>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;
            this.oracleConn = oracleConn;
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
