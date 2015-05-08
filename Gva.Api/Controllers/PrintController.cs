using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Common.Owin;
using Common.WordTemplates;
using Gva.Api.Models;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.Common;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.PrintRepository;
using Gva.Api.WordTemplates;
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
        private IAMLNationalRatingDataGenerator AMLNationalRatingDataGenerator;
        private ILotRepository lotRepository;
        private INomRepository nomRepository;
        private IUnitOfWork unitOfWork;
        private UserContext userContext;
        private ILotEventDispatcher lotEventDispatcher;
        private IPrintRepository printRepository;

        public PrintController(
            IEnumerable<IDataGenerator> dataGenerators,
            IAMLNationalRatingDataGenerator AMLNationalRatingDataGenerator,
            ILotRepository lotRepository,
            INomRepository nomRepository,
            IPrintRepository printRepository,
            ILotEventDispatcher lotEventDispatcher,
            IUnitOfWork unitOfWork,
            UserContext userContext)
        {
            this.dataGenerators = dataGenerators;
            this.AMLNationalRatingDataGenerator = AMLNationalRatingDataGenerator;
            this.lotRepository = lotRepository;
            this.nomRepository = nomRepository;
            this.printRepository = printRepository;
            this.unitOfWork = unitOfWork;
            this.userContext = userContext;
            this.lotEventDispatcher = lotEventDispatcher;
        }

        [Route("api/print")]
        public HttpResponseMessage Get(int lotId, int licenceInd, int? editionInd = null, bool generateNew = false)
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

            string editionPath = string.Format("licenceEditions/{0}", editionPartIndex);
            PartVersion<PersonLicenceEditionDO> licenceEditionPartVersion = lot.Index.GetPart<PersonLicenceEditionDO>(editionPath);

            int licenceTypeId = lot.Index.GetPart<PersonLicenceDO>(path).Content.LicenceType.NomValueId;
            string templateName = this.nomRepository.GetNomValue("licenceTypes", licenceTypeId).TextContent.Get<string>("templateName");

            Guid licenceEditionDocBlobKey;
            if (licenceEditionPartVersion.Content.PrintedDocumentBlobKey.HasValue && !generateNew)
            {
                licenceEditionDocBlobKey = licenceEditionPartVersion.Content.PrintedDocumentBlobKey.Value;
            }
            else
            {
                using (var wordDocStream = this.GenerateWordDocument(lotId, path, templateName, null, null))
                using (var pdfDocStream = this.printRepository.ConvertWordStreamToPdfStream(wordDocStream))
                {
                    licenceEditionDocBlobKey = this.printRepository.SaveStreamToBlob(pdfDocStream, ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString);
                    licenceEditionPartVersion.Content.PrintedDocumentBlobKey = licenceEditionDocBlobKey;
                    this.UpdatePart<PersonLicenceEditionDO>(licenceEditionDocBlobKey, licenceEditionPartVersion, lot, templateName, editionPath, "PrintedFileId", licenceEditionPartVersion.Content);
                }
            }

            string url = string.Format("file?fileKey={0}&fileName={1}&mimeType=application%2Fpdf&dispositionType=inline", 
                licenceEditionDocBlobKey, 
                templateName);

            return this.printRepository.ReturnResponseMessage(url);
        }

        [Route("api/printRatingEdition")]
        public HttpResponseMessage GetRatingEdition(int lotId, int licenceIndex, int licenceEditionIndex, int ratingIndex, int ratingEditionIndex, bool generateNew = false)
        {
            string path = string.Format("{0}/{1}", "licences", licenceIndex);
            var lot = lotRepository.GetLotIndex(lotId);
            string templateName = "AML_national_rating";

            PartVersion<PersonLicenceEditionDO> licenceEditionPartVersion = lot.Index.GetPart<PersonLicenceEditionDO>(string.Format("licenceEditions/{0}", licenceEditionIndex));
            Guid ratingEditionBlobKey;
            var printedRatingEdition = licenceEditionPartVersion.Content.PrintedRatingEditions.Where(e => e.RatingPartIndex == ratingIndex && e.RatingEditionPartIndex == ratingEditionIndex).SingleOrDefault();
            if (printedRatingEdition != null && !generateNew)
            {
                ratingEditionBlobKey = printedRatingEdition.PrintedEditionBlobKey;
            }
            else
            {
                using (var wordDocStream = this.GenerateWordDocument(lotId, path, templateName, ratingIndex, ratingEditionIndex))
                using (var pdfDocStream = this.printRepository.ConvertWordStreamToPdfStream(wordDocStream))
                {
                    ratingEditionBlobKey = this.printRepository.SaveStreamToBlob(pdfDocStream, ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString);
                    this.UpdateLicenceEditionPrintedRatings(ratingEditionBlobKey, licenceEditionPartVersion, lot, templateName, ratingIndex, ratingEditionIndex);
                }
            }

            string url = string.Format("file?fileKey={0}&fileName={1}&mimeType=application%2Fpdf&dispositionType=inline",
                ratingEditionBlobKey,
                templateName);

            return this.printRepository.ReturnResponseMessage(url);
        }

        [Route("api/printAirworthiness")]
        public HttpResponseMessage GetAirworthinessDoc(int lotId, int partIndex, bool generateNew = false)
        {
            string airworthinessPath = string.Format("aircraftCertAirworthinessesFM/{0}", partIndex);
            var lot = this.lotRepository.GetLotIndex(lotId);
            var airworthinessPart = lot.Index.GetPart<AircraftCertAirworthinessDO>(airworthinessPath);
            string templateName = airworthinessPart.Content.AirworthinessCertificateType.Alias;

            Guid awDocBlobKey;
            if (airworthinessPart.Content.PrintedDocumentBlobKey.HasValue && !generateNew)
            {
                awDocBlobKey = airworthinessPart.Content.PrintedDocumentBlobKey.Value;
            }
            else
            {
                using (var wordDocStream = this.GenerateWordDocument(lotId, airworthinessPath, templateName, null, null))
                using (var pdfDocStream = this.printRepository.ConvertWordStreamToPdfStream(wordDocStream))
                {
                    awDocBlobKey = this.printRepository.SaveStreamToBlob(pdfDocStream, ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString);
                    airworthinessPart.Content.PrintedDocumentBlobKey = awDocBlobKey;
                    this.UpdatePart<AircraftCertAirworthinessDO>(awDocBlobKey, airworthinessPart, lot, templateName, airworthinessPath, "PrintedFileId", airworthinessPart.Content);
                }
            }

            string url = string.Format("file?fileKey={0}&fileName={1}&mimeType=application%2Fpdf&dispositionType=inline",
                awDocBlobKey,
                templateName);

            return this.printRepository.ReturnResponseMessage(url);
        }

        [Route("api/printExportCert")]
        public HttpResponseMessage GetExportCert(int lotId, int partIndex, bool generateNew = false)
        {
            string registrationPath = string.Format("aircraftCertRegistrationsFM/{0}", partIndex);
            var lot = this.lotRepository.GetLotIndex(lotId);
            var registrationPart = lot.Index.GetPart<AircraftCertRegistrationFMDO>(registrationPath);
            string templateName = "export_cert";

            Guid exportCertBlobKey;
            if (registrationPart.Content.Removal.Export.PrintedExportCertFileId.HasValue)
            {
                exportCertBlobKey = this.unitOfWork.DbContext.Set<GvaFile>()
                    .Where(f => f.GvaFileId == registrationPart.Content.Removal.Export.PrintedExportCertFileId.Value)
                    .Single()
                    .FileContentId;
            }
            else
            {
                using (var wordDocStream = this.GenerateWordDocument(lotId, registrationPath, templateName, null, null))
                using (var pdfDocStream = this.printRepository.ConvertWordStreamToPdfStream(wordDocStream))
                {

                    exportCertBlobKey = this.printRepository.SaveStreamToBlob(pdfDocStream, ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString);
                    this.UpdatePart<AircraftCertRegistrationFMDO>(exportCertBlobKey, registrationPart, lot, templateName, registrationPath, "PrintedExportCertFileId", registrationPart.Content.Removal.Export);
                }
            }
            string url = string.Format("file?fileKey={0}&fileName={1}&mimeType=application%2Fpdf&dispositionType=inline",
                exportCertBlobKey,
                templateName);

            return this.printRepository.ReturnResponseMessage(url);
        }

        [Route("api/printNoiseCert")]
        public HttpResponseMessage GetNoiseCert(int lotId, int partIndex, bool generateNew = false)
        {
            var lot = this.lotRepository.GetLotIndex(lotId);
            string noisePath = string.Format("aircraftCertNoises/{0}", partIndex);
            var noisePart = lot.Index.GetPart<AircraftCertNoiseDO>(noisePath);
            string templateName = "noise_cert";

            Guid noiseCertBlobKey;
            if (noisePart.Content.PrintedFileId.HasValue && !generateNew)
            {
                noiseCertBlobKey = this.unitOfWork.DbContext.Set<GvaFile>()
                    .Where(f => f.GvaFileId == noisePart.Content.PrintedFileId.Value)
                    .Single()
                    .FileContentId;
            }
            else
            {
                using (var wordDocStream = this.GenerateWordDocument(lotId, noisePath, templateName, null, null))
                using (var pdfDocStream = this.printRepository.ConvertWordStreamToPdfStream(wordDocStream))
                {
                    noiseCertBlobKey = this.printRepository.SaveStreamToBlob(pdfDocStream, ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString);
                    this.UpdatePart<AircraftCertNoiseDO>(noiseCertBlobKey, noisePart, lot, templateName, noisePath, "PrintedFileId", noisePart.Content);
                }
            }
            string url = string.Format("file?fileKey={0}&fileName={1}&mimeType=application%2Fpdf&dispositionType=inline",
                noiseCertBlobKey,
                templateName);

            return this.printRepository.ReturnResponseMessage(url);
        }

        [Route("api/printApplication")]
        public HttpResponseMessage GetApplicationNote(int lotId, int partIndex)
        {
            Lot lot = this.lotRepository.GetLotIndex(lotId);
            string path = string.Format("personDocumentApplications/{0}", partIndex);
            PartVersion<DocumentApplicationDO> applicationPart = lot.Index.GetPart<DocumentApplicationDO>(path);
            string templateName = "application_note";
            Guid applicationDocBlobKey;
            if (applicationPart.Content.PrintedFileId.HasValue)
            {
                applicationDocBlobKey = this.unitOfWork.DbContext.Set<GvaFile>()
                    .Where(f => f.GvaFileId == applicationPart.Content.PrintedFileId.Value)
                    .Single()
                    .FileContentId;
            }
            else
            {
                using (var wordDocStream = this.GenerateWordDocument(lotId, path, templateName, null, null))
                using (var pdfDocStream = this.printRepository.ConvertWordStreamToPdfStream(wordDocStream))
                {
                    applicationDocBlobKey = this.printRepository.SaveStreamToBlob(pdfDocStream, ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString);
                    this.UpdatePart<DocumentApplicationDO>(applicationDocBlobKey, applicationPart, lot, templateName, path, "PrintedFileId", applicationPart.Content);
                }
            }

            string url = string.Format("file?fileKey={0}&fileName={1}&mimeType=application%2Fpdf&dispositionType=inline",
                applicationDocBlobKey,
                templateName);

            return this.printRepository.ReturnResponseMessage(url);
        }

        public void UpdateLicenceEditionPrintedRatings(
            Guid ratingEditionBlobKey,
            PartVersion<PersonLicenceEditionDO> licenceEditionPartVersion, 
            Lot lot,
            string templateName,
            int ratingPartIndex,
            int ratingEditionPartIndex)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                int printedRatingEditionFileId = this.printRepository.SaveNewFile(templateName, ratingEditionBlobKey);

                PrintedRatingEditionDO existingEntry = licenceEditionPartVersion.Content.PrintedRatingEditions
                    .Where(re => re.RatingEditionPartIndex == ratingEditionPartIndex && re.RatingPartIndex == ratingPartIndex)
                    .SingleOrDefault();

                if (existingEntry != null)
                {
                    existingEntry.FileId = printedRatingEditionFileId;
                    existingEntry.PrintedEditionBlobKey = ratingEditionBlobKey;
                }
                else
                {
                    PrintedRatingEditionDO newEntry = new PrintedRatingEditionDO()
                    {
                        PrintedEditionBlobKey = ratingEditionBlobKey,
                        RatingPartIndex = ratingPartIndex,
                        RatingEditionPartIndex = ratingEditionPartIndex,
                        FileId = printedRatingEditionFileId
                    };

                    if(licenceEditionPartVersion.Content.PrintedRatingEditions == null)
                    {
                            licenceEditionPartVersion.Content.PrintedRatingEditions = new List<PrintedRatingEditionDO>();
                    }
                    licenceEditionPartVersion.Content.PrintedRatingEditions.Add(newEntry);
                }

                lot.UpdatePart<PersonLicenceEditionDO>(string.Format("licenceEditions/{0}", licenceEditionPartVersion.Part.Index), licenceEditionPartVersion.Content, this.userContext);

                lot.Commit(this.userContext, lotEventDispatcher);
                this.lotRepository.ExecSpSetLotPartTokens(licenceEditionPartVersion.PartId);

                this.unitOfWork.Save();

                transaction.Commit();
            }
        }

        public Stream GenerateWordDocument(int lotId, string path, string templateName, int? ratingPartIndex, int? editionPartIndex)
        {
            var dataGenerator = this.dataGenerators.FirstOrDefault(dg => dg.TemplateNames.Contains(templateName));
            object data = null;
            if (dataGenerator == null && ratingPartIndex.HasValue && editionPartIndex.HasValue)
            {
                data = this.AMLNationalRatingDataGenerator.GetData(lotId, path, ratingPartIndex.Value, editionPartIndex.Value);
            }
            else
            {
                data = dataGenerator.GetData(lotId, path);
            }

            JsonSerializer jsonSerializer = JsonSerializer.Create(App.JsonSerializerSettings);
            jsonSerializer.ContractResolver = new DefaultContractResolver();

            JObject json = JObject.FromObject(data, jsonSerializer);

            var wordTemplate = this.unitOfWork.DbContext.Set<GvaWordTemplate>()
                .SingleOrDefault(t => t.Name == templateName);

            var memoryStream = new MemoryStream();
            memoryStream.Write(wordTemplate.Template, 0, wordTemplate.Template.Length);

            new WordTemplateTransformer(memoryStream).Transform(json);
            memoryStream.Position = 0;

            return memoryStream;
        }

        public void UpdatePart<T>(Guid exportCertBlobKey, PartVersion<T> registrationPart, Lot lot, string templateName, string path, string filePropertyName, Object modelContainingFile) where T: class
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                int printedFileId = this.printRepository.SaveNewFile(templateName, exportCertBlobKey);

                PropertyInfo propertyInfo = modelContainingFile.GetType().GetProperty(filePropertyName);
                propertyInfo.SetValue(modelContainingFile, printedFileId);
                lot.UpdatePart<T>(path, registrationPart.Content, this.userContext);

                lot.Commit(this.userContext, lotEventDispatcher);
                this.lotRepository.ExecSpSetLotPartTokens(registrationPart.PartId);

                this.unitOfWork.Save();

                transaction.Commit();
            }
        }
    }
}