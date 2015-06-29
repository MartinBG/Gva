using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Common.Data;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.Repositories.WordTemplateRepository;

namespace Gva.Api.Controllers
{
    [RoutePrefix("api/wordTemplates")]
    [Authorize]
    public class WordTemplatesController : ApiController
    {
        private IWordTemplateRepository wordTemplateRepository;
        private IUnitOfWork unitOfWork;

        public WordTemplatesController(
            IWordTemplateRepository wordTemplateRepository,
            IUnitOfWork unitOfWork)
        {
            this.wordTemplateRepository = wordTemplateRepository;
            this.unitOfWork = unitOfWork;
        }

        [Route("new")]
        [HttpGet]
        public IHttpActionResult GetNewTemplate()
        {
            return Ok(new WordTemplateDO());
        }

        [Route("")]
        [HttpGet]
        public IHttpActionResult GetTemplates()
        {
            return Ok(this.wordTemplateRepository.GetTemplates());
        }

        [Route("{templateId}")]
        [HttpGet]
        public IHttpActionResult GetTemplate(int templateId)
        {
            return Ok(this.wordTemplateRepository.GetTemplate(templateId));
        }

        [Route("{templateId}")]
        [HttpPost]
        public IHttpActionResult PostTemplate(int templateId, WordTemplateDO template)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                 this.wordTemplateRepository.ChangeTemplateData(templateId, template);

                transaction.Commit();

                return Ok();
            }
        }

        [Route("createNew")]
        [HttpPost]
        public IHttpActionResult PostNewTemplate(WordTemplateDO template)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                this.wordTemplateRepository.CreateNewTemplate(template);

                transaction.Commit();

                return Ok();
            }
        }

        [Route("{templateId}")]
        public IHttpActionResult DeleteTemplate(int templateId)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                GvaWordTemplate templateToDelete = this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                    .Where(t => t.GvaWordTemplateId == templateId)
                    .Single();

                this.unitOfWork.DbContext.Set<GvaWordTemplate>().Remove(templateToDelete);

                this.unitOfWork.Save();

                transaction.Commit();

                return Ok();
            }
        }

        [Route("download")]
        [HttpGet]
        public HttpResponseMessage DownloadWordTemplate(int templateId)
        {
            GvaWordTemplate wordTemplate = this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                    .Where(t => t.GvaWordTemplateId == templateId)
                    .Single();

            var memoryStream = new MemoryStream();
            memoryStream.Write(wordTemplate.Template, 0, wordTemplate.Template.Length);
            memoryStream.Position = 0;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(memoryStream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/msword");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("inline")
                {
                    FileName = string.Format("{0}.docx", wordTemplate.Name)
                };

            return result;
        }

        [Route("isUniqueTemplateName")]
        [HttpGet]
        public IHttpActionResult IsUniqueTemplateName(string templateName)
        {
            return Ok(new
            {
                isUnique = !this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                    .Where(t => t.Name == templateName)
                    .Any()
            });
        }
    }
}