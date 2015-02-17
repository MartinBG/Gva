﻿using System.Configuration;
using System.Linq;
using System.Web.Http;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.Repositories.ExaminationSystemRepository;
using Oracle.ManagedDataAccess.Client;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/examinationSystem")]
    public class ExaminationSystemController : ApiController
    {
        private IUnitOfWork unitOfWork;
        private ILotRepository lotRepository;
        private IExaminationSystemRepository examinationSystemRepository;

        public ExaminationSystemController(
            IUnitOfWork unitOfWork,
            ILotRepository lotRepository,
            IExaminationSystemRepository examinationSystemRepository)
        {
            this.unitOfWork = unitOfWork;
            this.lotRepository = lotRepository;
            this.examinationSystemRepository = examinationSystemRepository;
        }

        [Route("updateStates")]
        [HttpGet]
        public IHttpActionResult UpdateStates()
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                this.examinationSystemRepository.ReloadStates();

                transaction.Commit();

                return Ok();
            }
        }

        [Route("loadData")]
        [HttpGet]
        public IHttpActionResult GetDataFromExaminationSystem()
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                try
                {
                    this.unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = false;
                    this.examinationSystemRepository.ExtractDataFromExaminationSystem();

                    this.unitOfWork.Save();
                }
                finally
                {
                    this.unitOfWork.DbContext.Configuration.AutoDetectChangesEnabled = true;
                }

                transaction.Commit();

                return Ok();
            }
        }

        [Route("exams")]
        [HttpGet]
        public IHttpActionResult GetExams(string qualificationCode = null, string certCampCode = null)
        {
            return Ok(this.examinationSystemRepository.GetExams(qualificationCode, certCampCode));
        }

        [Route("examinees")]
        [HttpGet]
        public IHttpActionResult GetExaminees()
        {
            return Ok(this.examinationSystemRepository.GetExaminees());
        }

        [Route("certPaths")]
        [HttpGet]
        public IHttpActionResult GetCertPaths()
        {
            return Ok(this.examinationSystemRepository.GetCertPaths());
        }

        [Route("certCampaigns")]
        [HttpGet]
        public IHttpActionResult GetCertCampaigns()
        {
            return Ok(this.examinationSystemRepository.GetCertCampaigns());
        }

        [Route("qualifications")]
        [HttpGet]
        public IHttpActionResult GetQualifications(string qualificationCode = null)
        {
            return Ok(this.examinationSystemRepository.GetQualifications(qualificationCode));
        }

        [Route("personExams")]
        [HttpGet]
        public IHttpActionResult GetPersonExams(int lotId)
        {
            return Ok(this.examinationSystemRepository.GetExaminees(lotId));
        }

        [Route("checkConnection")]
        [HttpGet]
        public IHttpActionResult CheckConnection()
        {
            using (OracleConnection connection = new OracleConnection(ConfigurationManager.ConnectionStrings["ExaminationSystem"].ConnectionString))
            {
                try
                {
                    connection.Open();
                    return Ok(new { isConnected = true });
                }
                catch
                {
                    return Ok(new { isConnected = false });
                }
            }
        }
    }
}