using System.Linq;
using System.Web.Http;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.Repositories.ExaminationSystemRepository;
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
        public IHttpActionResult GetDataFromExaminationSystem(bool loadExaminees)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                this.examinationSystemRepository.ExtractDataFromExaminationSystem(loadExaminees);

                this.unitOfWork.Save();

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
    }
}