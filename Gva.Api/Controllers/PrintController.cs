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
using Common.Owin;
using Common.WordTemplates;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.WordTemplates;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
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

        [Route("api/print")]
        public HttpResponseMessage Get(int lotId, int licenceInd)
        {
            string path = string.Format("{0}/{1}", "licences", licenceInd);
            int licenceTypeId = this.lotRepository
                .GetLotIndex(lotId)
                .Index.GetPart<PersonLicenceDO>(path)
                .Content.LicenceType.NomValueId;

            string templateName = this.nomRepository.GetNomValue("licenceTypes", licenceTypeId).TextContent.Get<string>("templateName");

            var dataGenerator = this.dataGenerators.First(dg => dg.TemplateNames.Contains(templateName));
            object data = dataGenerator.GetData(lotId, path);

            JsonSerializer jsonSerializer = JsonSerializer.Create(App.JsonSerializerSettings);
            jsonSerializer.ContractResolver = new DefaultContractResolver();

            JObject json = JObject.FromObject(data, jsonSerializer);

            var wordTemplate = this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                .SingleOrDefault(t => t.Name == templateName);

            var memoryStream = new MemoryStream();  // no need to dispose the memory stream as StreamContent handles that
            memoryStream.Write(wordTemplate.Template, 0, wordTemplate.Template.Length);

            new WordTemplateTransformer(memoryStream).Transform(json);
            memoryStream.Position = 0;

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(memoryStream);
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