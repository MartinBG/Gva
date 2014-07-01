using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Data;
using Common.Json;
using Common.WordTemplates;
using Gva.Api.Models;
using Gva.Api.WordTemplates;
using Newtonsoft.Json.Linq;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    public class PrintController : ApiController
    {
        private IEnumerable<IDataGenerator> dataGenerators;
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IUnitOfWork unitOfWork;

        public PrintController(
            IEnumerable<IDataGenerator> dataGenerators,
            ILotRepository lotRepository,
            INomRepository nomRepository,
            IUnitOfWork unitOfWork)
        {
            this.dataGenerators = dataGenerators;
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.unitOfWork = unitOfWork;
        }

        [Route(@"api/print")]
        public HttpResponseMessage Get(int lotId, string path, int index)
        {
            int licenceTypeId = this.lotRepository
                .GetLotIndex(lotId)
                .Index.GetPart(path)
                .Content.Get<int>("licenceType.nomValueId");
            string templateName = this.nomRepository.GetNomValue("licenceTypes", licenceTypeId).TextContent.Get<string>("templateName");

            var dataGenerator = this.dataGenerators.First(dg => dg.TemplateNames.Contains(templateName));
            JObject json = dataGenerator.GetData(lotId, path, index);

            var wordTemplate = this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                .SingleOrDefault(t => t.Name == templateName);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new PushStreamContent(
                (outputStream, httpContent, transportContext) =>
                {
                    using (outputStream)
                    {
                        using (var memoryStream = new MemoryStream())
                        {
                            memoryStream.Write(wordTemplate.Template, 0, wordTemplate.Template.Length);

                            WordTemplateTransformer tt = new WordTemplateTransformer(memoryStream);
                            tt.Transform(json);

                            memoryStream.Position = 0;
                            memoryStream.CopyTo(outputStream);
                        }
                    }
                });
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/vnd.openxmlformats-officedocument.wordprocessingml.document");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("attachment")
                {
                    FileName = templateName
                };

            return result;
        }
    }
}