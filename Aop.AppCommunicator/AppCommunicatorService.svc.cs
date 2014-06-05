using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Common.Data;
using Common.Utils;
using Docs.Api.Models;
using Docs.Api.Repositories.DocRepository;
using Components.EmsUtils;
using Components.DocumentSigner;
using Components.XmlSchemaValidator;
using Components.VirusScanEngine;
using Components.DocumentSerializer;
using Components.DevelopmentLogger;
using Components.PortalConfigurationManager;
using RioObjects;
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
using System.Globalization;
using Aop.Api.Repositories.Aop;
using Aop.Api.Models;
using Components.ApplicationCommunicator;

namespace Aop.AppCommunicator
{
    public class AppCommunicatorService : Components.ApplicationCommunicator.IDocumentService
    {
        private IUnitOfWork unitOfWork;
        private IDocRepository docRepository;
        private IAppRepository appRepository;

        public AppCommunicatorService()
        {
            List<IDbConfiguration> configurations = new List<IDbConfiguration>();
            configurations.Add(new DocsDbConfiguration());
            configurations.Add(new CommonDbConfiguration());
            configurations.Add(new AopDbConfiguration());

            this.unitOfWork = new UnitOfWork(configurations);
            this.docRepository = new DocRepository(unitOfWork);
            this.appRepository = new AppRepository(unitOfWork);
        }

        public DocumentInfo ProcessStructuredDocument(DocumentRequest request)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                IncomingDoc incomingDoc = new IncomingDoc();
                incomingDoc.DocumentGuid = request.DocumentGuid;
                incomingDoc.IncomingDate = DateTime.Now;
                incomingDoc.IncomingDocStatusId = this.unitOfWork.DbContext.Set<IncomingDocStatus>().Single(e => e.Alias == "Pending").IncomingDocStatusId;

                IncomingDocFile incomingDocFile = new IncomingDocFile();
                incomingDocFile.DocFileTypeId = this.unitOfWork.DbContext.Set<DocFileType>().Single(e => e.Alias == "XML").DocFileTypeId;
                incomingDocFile.Name = String.Format("WebPortalApp_{0}_{1}", DateTime.Now.ToString(), Guid.NewGuid().ToString());
                incomingDocFile.DocFileName = String.Format("WebPortalApp_{0}_{1}.xml", DateTime.Now.ToString(), Guid.NewGuid().ToString());
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

                    documentInfo.AcceptedDocumentUri = new DocumentURI();
                    documentInfo.RegistrationStatus = DocumentRegistrationStatus.Registered;
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

        public CaseStatusInfo GetCaseStatusInfo(Guid guid)
        {
            var doc = this.appRepository.GetDocByPortalDocId(guid);
            if (doc != null)
            {
                string currentStageAlias = "Pending";
                if (doc.DocElectronicServiceStages.Count > 0)
                {
                    if (doc.DocElectronicServiceStages.Any(e => e.IsCurrentStage))
                    {
                        currentStageAlias = doc.DocElectronicServiceStages.FirstOrDefault(e => e.IsCurrentStage).ElectronicServiceStage.Alias;
                    }
                    else
                    {
                        currentStageAlias = doc.DocElectronicServiceStages.OrderBy(e => e.DocElectronicServiceStageId).Last().ElectronicServiceStage.Alias;
                    }
                }

                return new CaseStatusInfo()
                {
                    DocumentGuid = guid,
                    CaseDocumentUri = !String.IsNullOrWhiteSpace(doc.RegUri) ? doc.RegUri : String.Empty,
                    CaseStatus = (CaseStatus)Enum.Parse(typeof(CaseStatus), currentStageAlias)
                };
            }
            else
            {
                return null;
            }
        }

        public void RegisterElectronicPortalPmntInfo(ElectronicPortalPaymentInfo paymentInfo)
        {
            throw new NotImplementedException();
        }

        #region Private methods

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
            catch (Exception)
            {
                throw;
            }
        }

        #endregion
    }
}
