using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using System.Web.Http;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/papers")]
    [Authorize]
    public class PapersController : ApiController
    {
        private IUnitOfWork unitOfWork;

        public PapersController(
            IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        [Route("new")]
        [HttpGet]
        public IHttpActionResult GetNewPaper()
        {
            return Ok(new PaperDO() 
            {
                IsActive = true
            });
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetPapers()
        {
            var papers = this.unitOfWork.DbContext.Set<GvaPaper>()
                .ToList()
                .Select(p => new PaperDO(p));

            return Ok(papers);
        }

        [Route("{paperId}")]
        [HttpGet]
        public IHttpActionResult GetPaper(int paperId)
        {
            GvaPaper paper = this.unitOfWork.DbContext.Set<GvaPaper>()
                    .Where(t => t.PaperId == paperId)
                    .Single();

            return Ok(paper);
        }

        [Route("{paperId}")]
        [HttpPost]
        public IHttpActionResult PostPaper(int paperId, PaperDO paper)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                GvaPaper paperData = this.unitOfWork.DbContext.Set<GvaPaper>()
               .Where(t => t.PaperId == paper.PaperId)
               .Single();

                paperData.ToDate = paper.ToDate.Value;
                paperData.FromDate = paper.FromDate.Value;
                paper.IsActive = paper.IsActive;

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [Route("createNew")]
        [HttpPost]
        public IHttpActionResult PostNewPaper(PaperDO paper)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                GvaPaper newPaper = new GvaPaper()
                {
                    Name = paper.Name,
                    IsActive = paper.IsActive,
                    FromDate = paper.FromDate.Value,
                    ToDate = paper.ToDate.Value,
                    FirstNumber = paper.FirstNumber.Value
                };

                this.unitOfWork.DbContext.Set<GvaPaper>().Add(newPaper);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [Route("{paperId}")]
        public IHttpActionResult DeletePaper(int paperId)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                GvaPaper paperToDelete = this.unitOfWork.DbContext.Set<GvaPaper>()
                    .Where(t => t.PaperId == paperId)
                    .Single();

                this.unitOfWork.DbContext.Set<GvaPaper>().Remove(paperToDelete);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [Route("isValidPaperData")]
        [HttpGet]
        public IHttpActionResult IsValidPaperData(string paperName, int? paperId = null)
        {
            var existsSimilarEntry = this.unitOfWork.DbContext.Set<GvaPaper>()
                .Where(p => p.Name.Trim() == paperName.Trim() &&
                    (paperId.HasValue? p.PaperId != paperId.Value : true))
                .Any();

            return Ok(new
            {
                isValid = !existsSimilarEntry
            });
        }
    }
}