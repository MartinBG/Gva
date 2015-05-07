﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        [Route("api/printRatingEdition")]
        public HttpResponseMessage GetRatingEdition(int lotId, int licenceIndex, int licenceEditionIndex, int ratingIndex, int ratingEditionIndex, bool generateNew = false)
        {
            string path = string.Format("{0}/{1}", "licences", licenceIndex);
            var lot = lotRepository.GetLotIndex(lotId);
            string templateName = "AML_national_rating";

            PartVersion<PersonLicenceEditionDO> licenceEditionPartVersion = lot.Index.GetPart<PersonLicenceEditionDO>(string.Format("licenceEditions/{0}", licenceEditionIndex));
            Guid ratingEditionBlobKey;
            var printedRatingEdition = licenceEditionPartVersion.Content.PrintedRatingEditions.Where(e => e.RatingPartIndex == ratingIndex && e.RatingEditionPartIndex == ratingEditionIndex).SingleOrDefault();
            if(printedRatingEdition != null && !generateNew)
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

            PartVersion<PersonLicenceEditionDO> licenceEditionPartVersion = lot.Index.GetPart<PersonLicenceEditionDO>(string.Format("licenceEditions/{0}", editionPartIndex));

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
                    this.UpdateLicenceEdition(licenceEditionDocBlobKey, licenceEditionPartVersion, lot, templateName);
                }
            }

            string url = string.Format("file?fileKey={0}&fileName={1}&mimeType=application%2Fpdf&dispositionType=inline", 
                licenceEditionDocBlobKey, 
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
                    this.UpdateAirworthiness(awDocBlobKey, airworthinessPart, lot, templateName);
                }
            }

            string url = string.Format("file?fileKey={0}&fileName={1}&mimeType=application%2Fpdf&dispositionType=inline",
                awDocBlobKey,
                templateName);

            return this.printRepository.ReturnResponseMessage(url);
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

        public void UpdateLicenceEdition(Guid licenceEditionDocBlobKey, PartVersion<PersonLicenceEditionDO> licenceEditionPartVersion, Lot lot, string templateName)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                licenceEditionPartVersion.Content.PrintedDocumentBlobKey = licenceEditionDocBlobKey;
                licenceEditionPartVersion.Content.PrintedFileId = this.printRepository.SaveNewFile(templateName, licenceEditionDocBlobKey);

                lot.UpdatePart<PersonLicenceEditionDO>(string.Format("licenceEditions/{0}", licenceEditionPartVersion.Part.Index), licenceEditionPartVersion.Content, this.userContext);

                lot.Commit(this.userContext, lotEventDispatcher);
                this.lotRepository.ExecSpSetLotPartTokens(licenceEditionPartVersion.PartId);

                this.unitOfWork.Save();

                transaction.Commit();
            }
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
                    this.UpdateApplicationPart(applicationDocBlobKey, applicationPart, lot, templateName);
                }
            }

            string url = string.Format("file?fileKey={0}&fileName={1}&mimeType=application%2Fpdf&dispositionType=inline",
                applicationDocBlobKey,
                templateName);

            return this.printRepository.ReturnResponseMessage(url);
        }

        public void UpdateApplicationPart(
            Guid applicationDocBlobKey,
            PartVersion<DocumentApplicationDO> applicationPartVersion,
            Lot lot,
            string templateName) 
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                applicationPartVersion.Content.PrintedFileId = this.printRepository.SaveNewFile(templateName, applicationDocBlobKey);
                string path = string.Format("personDocumentApplications/{0}", applicationPartVersion.Part.Index);
                lot.UpdatePart(path, applicationPartVersion.Content, this.userContext);

                lot.Commit(this.userContext, lotEventDispatcher);

                this.unitOfWork.Save();

                this.lotRepository.ExecSpSetLotPartTokens(applicationPartVersion.PartId);

                transaction.Commit();
            }
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

        public void UpdateAirworthiness(Guid awDocBlobKey, PartVersion<AircraftCertAirworthinessDO> awPartVersion, Lot lot, string templateName)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                awPartVersion.Content.PrintedDocumentBlobKey = awDocBlobKey;
                int printedAwCertFileId = this.printRepository.SaveNewFile(templateName, awDocBlobKey);
                awPartVersion.Content.PrintedFileId = printedAwCertFileId;

                lot.UpdatePart<AircraftCertAirworthinessDO>(string.Format("aircraftCertAirworthinessesFM/{0}", awPartVersion.Part.Index), awPartVersion.Content, this.userContext);

                lot.Commit(this.userContext, lotEventDispatcher);
                this.lotRepository.ExecSpSetLotPartTokens(awPartVersion.PartId);

                this.unitOfWork.Save();

                transaction.Commit();
            }
        }
    }
}