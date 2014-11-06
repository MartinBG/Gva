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
using Microsoft.Office.Interop.Word;
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

            var memoryStream = new MemoryStream();
            memoryStream.Write(wordTemplate.Template, 0, wordTemplate.Template.Length);

            new WordTemplateTransformer(memoryStream).Transform(json);
            memoryStream.Position = 0;

            var tmpDocFile = Path.GetTempFileName();
            var tmpPdfFile = Path.GetTempFileName();

            using(var tmpFileStream = File.OpenWrite(tmpDocFile))
            {
                memoryStream.CopyTo(tmpFileStream);
            }

            var document = new Application().Documents.Open(tmpDocFile);
            document.ExportAsFixedFormat(tmpPdfFile, WdExportFormat.wdExportFormatPDF);
            document.Close();
            File.Delete(tmpDocFile);

            var stream = new FileStream(tmpPdfFile, 
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.None,
                tmpPdfFile.Length, 
                FileOptions.DeleteOnClose);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(stream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("inline")
                {
                    FileName = templateName
                };

            return result;
        }
    }
}