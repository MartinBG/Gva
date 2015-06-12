using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Web.Http;
using Common.Api.Repositories.NomRepository;
using Common.Api.UserContext;
using Common.Data;
using Common.Json;
using Gva.Api.ModelsDO.Aircrafts;
using Gva.Api.ModelsDO.Persons;
using Gva.Api.Repositories.PrintRepository;
using Gva.Api.WordTemplates;
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
        private Dictionary<string, Tuple<string, string, bool>> requestToPrintParameters =
            new Dictionary<string, Tuple<string, string, bool>>()
            {
                {"printRadioCert", new Tuple<string, string, bool>( "aircraftCertRadios", "radio_cert",false)},
                {"printRegCert", new Tuple<string, string, bool>("aircraftCertRegistrationsFM", "reg_cert", false)},
                {"printNoiseCert", new Tuple<string, string, bool>("aircraftCertNoises", "noise_cert", false)},
                {"printDeregCert", new Tuple<string, string, bool>("aircraftCertRegistrationsFM", "dereg_cert", false)},
                {"printExportCert", new Tuple<string, string, bool>("aircraftCertRegistrationsFM","export_cert", false)},
                {"printApplication", new Tuple<string, string, bool>("personDocumentApplications", "application_note", true)},
                {"printExaminerCert", new Tuple<string, string, bool>("licences", "examiner_cert", false)},
                {"printInstructorCert", new Tuple<string, string, bool>("licences", "instructor_cert", false)}
            };
        private Dictionary<string,string> airworthinessTypeAliasToTemplateName =
            new Dictionary<string,string>()
            {
                {"15aReissue", "15a"},
                {"directive8Reissue", "directive8"},
                {"vlaReissue", "vla"}
            };

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
                using (var wordDocStream = this.printRepository.GenerateWordDocument(lotId, path, templateName, null, null))
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
                using (var wordDocStream = this.printRepository.GenerateWordDocument(lotId, path, templateName, ratingIndex, ratingEditionIndex))
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
        public HttpResponseMessage GetAirworthinessDoc(int lotId, int partIndex)
        {
            string airworthinessPath = string.Format("aircraftCertAirworthinessesFM/{0}", partIndex);
            var lot = this.lotRepository.GetLotIndex(lotId);
            var airworthinessPart = lot.Index.GetPart<AircraftCertAirworthinessDO>(airworthinessPath);
            string templateName = null;
            string certTypeAlias = airworthinessPart.Content.AirworthinessCertificateType.Alias;
            if (airworthinessTypeAliasToTemplateName.ContainsKey(certTypeAlias))
            {
                templateName = airworthinessTypeAliasToTemplateName[certTypeAlias];
            }
            else
            {
                templateName = certTypeAlias;
            }

            return this.printRepository.GenerateDocumentWithoutSave(lotId, airworthinessPath, templateName, false);
        }

        [Route("api/{request:regex(^(printRadioCert|printExportCert|printNoiseCert|printRegCert|printDeregCert|printApplication|printExaminerCert|printInstructorCert)$)}")]
        public HttpResponseMessage GetDocument(int lotId, int partIndex, string request)
        {
            var result = this.requestToPrintParameters[request];
            return this.printRepository.GenerateDocumentWithoutSave(lotId, string.Format("{0}/{1}", result.Item1, partIndex), result.Item2, result.Item3); 
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

        public void UpdatePart<T>(Guid blobKey, PartVersion<T> registrationPart, Lot lot, string templateName, string path, string filePropertyName, Object modelContainingFile) where T: class
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                int printedFileId = this.printRepository.SaveNewFile(templateName, blobKey);

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