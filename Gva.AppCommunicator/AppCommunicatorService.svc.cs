using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Common.Data;
using Common.Utils;
using Docs.Api.Models;
using Docs.Api.Repositories.DocRepository;
using Gva.AppCommunicator.AppCommunicatorObjects;
using Gva.Portal.Components.EmsUtils;
using Gva.Portal.Components.DocumentSigner;
using Gva.Portal.Components.XmlSchemaValidator;
using Gva.Portal.Components.VirusScanEngine;
using Gva.Portal.Components.DocumentSerializer;
using Gva.Portal.Components.DevelopmentLogger;
using Gva.Portal.Components.PortalConfigurationManager;
using Gva.Portal.RioObjects;
using System.IO;
using R_0009_000001;
using R_0009_000085;
using R_0009_000067;
using Common.Blob;
using Common.Api.Models;
using R_0009_000046;
using R_0009_000068;
using System.Data.SqlClient;
using System.Configuration;

namespace Gva.AppCommunicator
{
    public class AppCommunicatorService : IDocumentService
    {
        private IUnitOfWork unitOfWork;
        private IDocRepository docRepository;

        private IDocumentSerializer documentSerializer;
        private IVirusScanEngine virusScanEngine;
        private IXmlSchemaValidator xmlSchemaValidator;
        private IDocumentSigner documentSigner;
        private IPortalConfigurationManager portalConfigurationManager;
        private IDevelopmentLogger developmentLogger;
        private IEmsUtils emsUtils;

        public AppCommunicatorService()
        {
            List<IDbConfiguration> configurations = new List<IDbConfiguration>();
            configurations.Add(new DocsDbConfiguration());
            configurations.Add(new CommonDbConfiguration());

            this.unitOfWork = new UnitOfWork(configurations);
            this.docRepository = new DocRepository(unitOfWork);

            this.documentSerializer = new DocumentSerializerImpl();
            this.virusScanEngine = new VirusScanEngineImpl();
            this.portalConfigurationManager = new PortalConfigurationManagerImpl();
            this.developmentLogger = new EventLogDevelopmentLoggerImpl(portalConfigurationManager);
            this.xmlSchemaValidator = new XmlSchemaValidatorImpl(developmentLogger);
            this.documentSigner = new DocumentSignerImpl(portalConfigurationManager, documentSerializer);
            this.emsUtils = new EmsUtilsImpl(documentSerializer, xmlSchemaValidator, virusScanEngine, documentSigner);
        }

        public DocumentInfo ProcessStructuredDocument(DocumentRequest request)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                RioDocumentMetadata documentMetaData = emsUtils.GetDocumentMetadataFromXml(request.DocumentData);

                IncomingDoc incomingDoc = new IncomingDoc();
                incomingDoc.DocumentGuid = Guid.NewGuid();
                incomingDoc.IncomingDate = DateTime.Now;
                incomingDoc.IncomingDocStatusId = this.unitOfWork.DbContext.Set<IncomingDocStatus>().Single(e => e.Alias == "Pending").IncomingDocStatusId;

                IncomingDocFile incomingDocFile = new IncomingDocFile();
                incomingDocFile.DocFileTypeId = this.unitOfWork.DbContext.Set<DocFileType>().Single(e => e.Alias == "XML").DocFileTypeId;
                incomingDocFile.Name = String.Format("{0}_{1}", GetRioServiceTypeName(documentMetaData), Guid.NewGuid().ToString());
                incomingDocFile.DocFileName = string.Format("WebPortal_{0}_{1}.xml", GetRioServiceTypeName(documentMetaData), Guid.NewGuid().ToString());
                incomingDocFile.DocFileContent = request.DocumentData;
                incomingDocFile.IncomingDoc = incomingDoc;

                this.unitOfWork.DbContext.Set<IncomingDocFile>().Add(incomingDocFile);
                this.unitOfWork.Save();

                transaction.Commit();

                DocumentInfo documentInfo = new DocumentInfo();
                documentInfo.DocumentGuid = incomingDoc.DocumentGuid;
                documentInfo.RegistrationStatus = DocumentRegistrationStatus.Pending;
                documentInfo.DocumentData = new AISDocument();
                documentInfo.DocumentData.Name = incomingDocFile.Name;
                documentInfo.DocumentData.Content = Utf8Utils.GetBytes(request.DocumentData);

                return documentInfo;
            }
        }

        public DocumentInfo GetDocumentInfo(R_0009_000001.DocumentURI uri, Guid? guid)
        {
            if (guid.HasValue)
            {
                IncomingDoc incomingDoc = this.docRepository.GetIncomingDocByDocumentGuid(guid.Value);

                DocumentInfo documentInfo = new DocumentInfo();
                documentInfo.DocumentGuid = guid.Value;

                if (incomingDoc.IncomingDocStatus.Alias == "Pending")
                {
                    documentInfo.RegistrationStatus = DocumentRegistrationStatus.Pending;

                    IncomingDocFile incomingDocFile = incomingDoc.IncomingDocFiles.FirstOrDefault();

                    if (incomingDocFile != null)
                    {
                        documentInfo.DocumentData = new AISDocument();
                        documentInfo.DocumentData.Name = incomingDocFile.Name;
                        documentInfo.DocumentData.Content = Utf8Utils.GetBytes(incomingDocFile.DocFileContent);
                    }
                }
                else if (incomingDoc.IncomingDocStatus.Alias == "Registered" || incomingDoc.IncomingDocStatus.Alias == "NotRegistered")
                {
                    var mainDocIncomingDoc = incomingDoc.DocIncomingDocs.FirstOrDefault(d => d.IsDocInitial);
                    if (mainDocIncomingDoc != null)
                    {
                        Doc doc = this.unitOfWork.DbContext.Set<Doc>()
                            .Include(e => e.DocFiles)
                            .SingleOrDefault(e => e.DocId == mainDocIncomingDoc.DocId);

                        DocFile primary = doc.DocFiles.FirstOrDefault(e => e.IsPrimary);

                        if (primary != null)
                        {
                            documentInfo.DocumentData = new R_0009_000085.AISDocument();
                            documentInfo.DocumentData.Name = primary.Name;
                            documentInfo.DocumentData.Content = ReadFromBlob(primary.DocFileContentId);
                        }
                    }

                    DocumentURI receiptDocumentUri = null;
                    var receiptDocIncomingDoc = incomingDoc.DocIncomingDocs.FirstOrDefault(d => !d.IsDocInitial);
                    if (receiptDocIncomingDoc != null)
                    {
                        var receiptDoc = this.unitOfWork.DbContext.Set<Doc>()
                            .SingleOrDefault(e => e.DocId == receiptDocIncomingDoc.DocId);

                        if (receiptDoc != null)
                        {
                            receiptDocumentUri = new DocumentURI();
                            receiptDocumentUri.RegisterIndex = receiptDoc.RegIndex;
                            receiptDocumentUri.SequenceNumber = receiptDoc.RegNumber.Value.ToString("D6");
                            receiptDocumentUri.ReceiptOrSigningDate = receiptDoc.RegDate;
                        }
                    }

                    if (incomingDoc.IncomingDocStatus.Alias == "Registered")
                    {
                        documentInfo.RegistrationStatus = DocumentRegistrationStatus.Registered;
                        documentInfo.AcceptedDocumentUri = receiptDocumentUri;
                    }
                    else
                    {
                        documentInfo.RegistrationStatus = DocumentRegistrationStatus.NotRegistered;
                        documentInfo.NotAcceptedDocumentUri = receiptDocumentUri;
                    }
                }

                return documentInfo;
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Stream GetDocumentContent(DocumentURI uri, Guid? guid)
        {
            var doc = this.docRepository.GetDocByRegUri(uri.RegisterIndex, int.Parse(uri.SequenceNumber), uri.ReceiptOrSigningDate.Value);

            if (doc != null)
            {
                DocFile docFile = this.docRepository.GetPrimaryOrFirstDocFileByDocId(doc.DocId);
                if (docFile != null)
                {
                    var content = ReadFromBlob(docFile.DocFileContentId);

                    return new MemoryStream(content);
                }
            }

            throw new Exception("Document not found.");
        }

        public CaseFileInfo GetCaseFileInfo(DocumentURI uri, string publicAccessCode)
        {
            throw new NotImplementedException();
        }

        public ServiceStatus GetServiceStatus(DocumentURI uri, string serviceIdentifier)
        {
            var doc = this.docRepository.GetDocByRegUri(uri.RegisterIndex, int.Parse(uri.SequenceNumber), uri.ReceiptOrSigningDate.Value);
            if (doc != null)
            {
                ServiceStatus serviceStatus = new ServiceStatus();
                serviceStatus.InitiatingDocumentURI = new InitiatingDocumentURI();
                serviceStatus.InitiatingDocumentURI.RegisterIndex = uri.RegisterIndex;
                serviceStatus.InitiatingDocumentURI.SequenceNumber = uri.SequenceNumber;
                serviceStatus.InitiatingDocumentURI.ReceiptOrSigningDate = uri.ReceiptOrSigningDate;

                DocElectronicServiceStage currentDocStage = this.docRepository.GetCurrentServiceStageByDocId(doc.DocId);

                if (currentDocStage != null)
                {

                    serviceStatus.UnexecutedTasks = new UnexecutedTasks();
                    serviceStatus.UnexecutedTasks.TaskOrServiceStageCollection = new TaskOrServiceStageCollection();

                    var task = new TaskOrServiceStage();
                    task.Task = new TaskOrServiceStageTask();
                    task.Task.TaskData = new AISTask();
                    task.Task.TaskData.NameAndShortDescription = currentDocStage.ElectronicServiceStage.Name;
                    task.Task.TaskData.ExpandedDescription = currentDocStage.ElectronicServiceStage.Description;
                    task.Task.TaskData.ActualStartDate = currentDocStage.StartingDate;
                    task.Task.TaskData.ActualCompletionDate = currentDocStage.EndingDate;

                    serviceStatus.UnexecutedTasks.TaskOrServiceStageCollection.Add(task);
                }

                return serviceStatus;
            }
            else
            {
                throw new Exception("Document not found.");
            }
        }

        public void RegisterElectronicPortalPmntInfo(ElectronicPortalPaymentInfo paymentInfo)
        {
            throw new NotImplementedException();
        }

        #region Private methods

        private string GetRioServiceTypeName(RioDocumentMetadata documentMetaData)
        {
            if (documentMetaData.RioObjectType == typeof(R_4186.InitialCertificationCommercialPilotCapacityInstrumentFlightApplication))
            {
                return "InitialCertificationCommercialPilotCapacityInstrumentFlightApplication";
            }
            else
            {
                throw new Exception("RioObjectType is unknown.");
            }
        }

        private Guid WriteToBlob(byte[] content)
        {
            using (SqlConnection connection = new SqlConnection(ConfigurationManager.ConnectionStrings["DbContext"].ConnectionString))
            {
                connection.Open();
                using (var blobWriter = new BlobWriter(connection))
                using (var stream = blobWriter.OpenStream())
                {
                    stream.Write(content, 0, content.Length);
                    return blobWriter.GetBlobKey();
                }
            }
        }

        private byte[] ReadFromBlob(Guid key)
        {
            var blob = this.unitOfWork.DbContext.Set<Blob>().SingleOrDefault(e => e.Key == key);

            return blob != null ? blob.Content : null;
        }

        private void SaveXmlToDisc(string xmlContent)
        {
            try
            {
                StreamWriter writer = new StreamWriter("G:\\" + Guid.NewGuid() + ".xml");
                writer.Write(xmlContent);
                writer.Close();
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        #endregion
    }
}
