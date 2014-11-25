using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http;
using Common.Api.Controllers;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Blob;
using Common.Data;
using Common.Json;
using Common.Owin;
using Common.WordTemplates;
using Gva.Api.Models;
using Gva.Api.ModelsDO;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.WordTemplates;
using Microsoft.Office.Interop.Word;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Serialization;
using Regs.Api.LotEvents;
using Regs.Api.Models;
using Regs.Api.Repositories.LotRepositories;

namespace Gva.Api.Controllers
{
    public class PrintController : ApiController
    {
        private IEnumerable<IDataGenerator> dataGenerators;
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IUnitOfWork unitOfWork;
        private UserContext userContext;
        private ILotEventDispatcher lotEventDispatcher;

        public PrintController(
            IEnumerable<IDataGenerator> dataGenerators,
            ILotRepository lotRepository,
            INomRepository nomRepository,
            ILotEventDispatcher lotEventDispatcher,
            IUnitOfWork unitOfWork,
            UserContext userContext)
        {
            this.dataGenerators = dataGenerators;
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.unitOfWork = unitOfWork;
            this.userContext = userContext;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("api/print")]
        public HttpResponseMessage Get(int lotId, int licenceInd, int? editionInd = null)
        {
            string path = string.Format("{0}/{1}", "licences", licenceInd);
            var lot = lotRepository.GetLotIndex(lotId);

            int? editionPartIndex = (int?)null;
            if (editionInd.HasValue)
            {
                editionPartIndex = editionInd.Value;
            }
            else
            {
                editionPartIndex = lot.Index.GetParts<PersonLicenceEditionDO>("licenceEditions")
                    .Where(e => e.Content.LicencePartIndex == licenceInd)
                    .OrderByDescending(e => e.Content.Index)
                    .Last().Part.Index;
            }

            PartVersion<PersonLicenceEditionDO> licenceEditionPartVersion = lot.Index.GetPart<PersonLicenceEditionDO>(string.Format("licenceEditions/{0}", editionPartIndex));

            var memoryStream = new MemoryStream();

            int licenceTypeId = lot.Index.GetPart<PersonLicenceDO>(path)
                .Content.LicenceType.NomValueId;
            string templateName = this.nomRepository.GetNomValue("licenceTypes", licenceTypeId).TextContent.Get<string>("templateName");

            //this licence has been already printed once
            if (licenceEditionPartVersion.Content.PrintedDocumentBlobKey.HasValue)
            {
                this.CopyBlobContentToStream(licenceEditionPartVersion.Content.PrintedDocumentBlobKey.Value, memoryStream);
            }
            else
            {
                this.GenerateWordDocument(lotId, path, templateName, memoryStream);

                //save newly generated word document as blob and update edition's part to reference it
                this.SaveStreamToBlob(memoryStream, licenceEditionPartVersion, lot);
            }

            var fileStream = this.ConvertMemoryStreamToPdfFile(memoryStream);

            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            result.Content = new StreamContent(fileStream);
            result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            result.Content.Headers.ContentDisposition =
                new ContentDispositionHeaderValue("inline")
                {
                    FileName = templateName
                };

            return result;
        }

        public void SaveStreamToBlob(MemoryStream memoryStream, PartVersion<PersonLicenceEditionDO> licenceEditionPartVersion, Lot lot)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                Guid licenceEditionDocBlobKey = Guid.Empty;

                using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
                {
                    connection.Open();
                    using (var blobWriter = new BlobWriter(connection))
                    using (var blobStream = blobWriter.OpenStream())
                    {
                        blobStream.Write(memoryStream.ToArray(), 0, (int)memoryStream.Length);
                        licenceEditionDocBlobKey = blobWriter.GetBlobKey();
                    }
                }

                licenceEditionPartVersion.Content.PrintedDocumentBlobKey = licenceEditionDocBlobKey;

                lot.UpdatePart<PersonLicenceEditionDO>(string.Format("licenceEditions/{0}", licenceEditionPartVersion.Part.Index), licenceEditionPartVersion.Content, this.userContext);

                lot.Commit(this.userContext, lotEventDispatcher);
                this.lotRepository.ExecSpSetLotPartTokens(licenceEditionPartVersion.PartId);

                this.unitOfWork.Save();

                transaction.Commit();
            }
        }

        public void GenerateWordDocument(int lotId, string path, string templateName, MemoryStream memoryStream)
        {
            var dataGenerator = this.dataGenerators.First(dg => dg.TemplateNames.Contains(templateName));
            object data = dataGenerator.GetData(lotId, path);

            JsonSerializer jsonSerializer = JsonSerializer.Create(App.JsonSerializerSettings);
            jsonSerializer.ContractResolver = new DefaultContractResolver();

            JObject json = JObject.FromObject(data, jsonSerializer);

            var wordTemplate = this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                .SingleOrDefault(t => t.Name == templateName);

            memoryStream.Write(wordTemplate.Template, 0, wordTemplate.Template.Length);

            new WordTemplateTransformer(memoryStream).Transform(json);
            memoryStream.Position = 0;
        }

        public void CopyBlobContentToStream(Guid blobKey, MemoryStream memoryStream)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                connection.Open();

                var blobStream = new BlobReadStream(connection, "dbo", "Blobs", "Content", "Key", blobKey);
                blobStream.CopyTo(memoryStream);
                memoryStream.Position = 0;
            }
        }

        public FileStream ConvertMemoryStreamToPdfFile(MemoryStream memoryStream)
        {
            var tmpDocFile = Path.GetTempFileName();
            var tmpPdfFile = Path.GetTempFileName();

            using (var tmpFileStream = File.OpenWrite(tmpDocFile))
            {
                memoryStream.CopyTo(tmpFileStream);
                memoryStream.Close();
            }

            var document = new Application().Documents.Open(tmpDocFile);
            document.ExportAsFixedFormat(tmpPdfFile, WdExportFormat.wdExportFormatPDF);
            document.Close();
            File.Delete(tmpDocFile);

            return new FileStream(tmpPdfFile,
                FileMode.OpenOrCreate,
                FileAccess.ReadWrite,
                FileShare.None,
                tmpPdfFile.Length,
                FileOptions.DeleteOnClose);
        }
    }
}