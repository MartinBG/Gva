using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using Common.Data;
using Common.Utils;
using Docs.Api.Models;
using Docs.Api.Repositories.DocRepository;
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
using Aop.Api.Repositories.Aop;
using Aop.Api.Models;
using R_0009_000089;
using R_0009_000073;
using R_0009_000030;
using R_0009_000027;
using Common.Extensions;
using R_0009_000018;
using R_0009_000005;
using R_0009_000072;
using R_0009_000044;
using R_0009_000043;
using R_0009_000042;
using R_0009_000031;
using R_0009_000026;
using Rio.Data.ServiceContracts.AppCommunicator;

namespace Aop.AppCommunicator
{
    public class AppCommunicatorService : Rio.Data.ServiceContracts.AppCommunicator.IDocumentService
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

            this.unitOfWork = new UnitOfWork(configurations, Enumerable.Empty<IDbContextInitializer>());
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
                string currentStageAlias = "Pending"; //? pending?
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

        public CaseFileInfo GetCaseFileInfoWithCustomUri(string uri)
        {
            if (!String.IsNullOrWhiteSpace(uri))
            {
                var caseDoc = this.docRepository.GetDocByRegUri(uri);
                
                if (caseDoc != null)
                {
                    CaseFileInfo caseFileInfo = new CaseFileInfo();
                    caseFileInfo.AISCaseDataInternetAccess = new AISCaseDataInternetAccess();
                    caseFileInfo.AISCaseDataInternetAccess.Name = !String.IsNullOrWhiteSpace(caseDoc.DocType.ApplicationName) ? caseDoc.DocType.ApplicationName : caseDoc.DocType.Name;
                    caseFileInfo.AISCaseDataInternetAccess.URI = new AISCaseURI();
                    caseFileInfo.AISCaseDataInternetAccess.URI.DocumentInInternalRegisterURI = caseDoc.RegUri;
                    caseFileInfo.AISCaseDataInternetAccess.Documents = new Documents();
                    caseFileInfo.AISCaseDataInternetAccess.Documents.DocumentCollection = new DocumentCollection();
                    caseFileInfo.AISCaseDataInternetAccess.ServiceName = caseDoc.DocType.Name;

                    var serviceStages = 
                        this.unitOfWork.DbContext.Set<DocElectronicServiceStage>()
                        .Include(s => s.ElectronicServiceStage.ElectronicServiceStageExecutors)
                        .Where(s => s.DocId == caseDoc.DocId)
                        .OrderBy(s => s.ElectronicServiceStageId)
                        .ToList();

                    if (serviceStages.Count > 0)
                    {

                        caseFileInfo.AISCaseDataInternetAccess.Stages = new AISCaseDataInternetAccessStages();
                        caseFileInfo.AISCaseDataInternetAccess.Stages.StageCollection = new AISCaseDataInternetAccessStagesStageCollection();

                        foreach (var serviceStage in serviceStages)
                        {
                            var stage = new AISCaseDataInternetAccessStagesStage();
                            stage.ActualCompletionDate = serviceStage.EndingDate;
                            stage.StageDescription = serviceStage.ElectronicServiceStage.Description;
                            stage.StageName = serviceStage.ElectronicServiceStage.Name;

                            string executorPosition = String.Empty;
                            string executorName = String.Empty;

                            if (serviceStage.ElectronicServiceStage.ElectronicServiceStageExecutors.Count > 0)
                            {
                                Tuple<string, string> postionAndName = this.docRepository.GetPositionAndNameById(serviceStage.ElectronicServiceStage.ElectronicServiceStageExecutors.First().UnitId);

                                executorPosition = postionAndName.Item1;
                                executorName = postionAndName.Item2;
                            }

                            stage.Executor = new R_0009_000041.ServiceStageExecutor();
                            stage.Executor.PositionInAdministrationOrAISUser = new PositionInAdministrationOrAISUser();
                            stage.Executor.PositionInAdministrationOrAISUser.AISUserBasicData = new AISUserBasicData();
                            stage.Executor.PositionInAdministrationOrAISUser.AISUserBasicData.Position = new AISUserPositionInAdministration();
                            stage.Executor.PositionInAdministrationOrAISUser.AISUserBasicData.Position.Position = new Position();
                            stage.Executor.PositionInAdministrationOrAISUser.AISUserBasicData.Position.Position.EKDAPositonName = executorPosition;

                            Tuple<string, string, string> splitNames = Helper.SplitNames(executorName);
                            stage.Executor.PositionInAdministrationOrAISUser.AISUserBasicData.Names = new AISUserNames();
                            stage.Executor.PositionInAdministrationOrAISUser.AISUserBasicData.Names.PersonNames = new PersonNames();
                            stage.Executor.PositionInAdministrationOrAISUser.AISUserBasicData.Names.PersonNames.First = splitNames.Item1;
                            stage.Executor.PositionInAdministrationOrAISUser.AISUserBasicData.Names.PersonNames.Middle = splitNames.Item2;
                            stage.Executor.PositionInAdministrationOrAISUser.AISUserBasicData.Names.PersonNames.Last = splitNames.Item3;

                            caseFileInfo.AISCaseDataInternetAccess.Stages.StageCollection.Add(stage);
                        }
                    }

                    var docs = this.docRepository.FindPublicLeafsByDocId(caseDoc.DocId);

                    foreach (var doc in docs)
                    {

                        DocFile docFile =
                            this.unitOfWork.DbContext.Set<DocFile>()
                            .Include(d => d.DocFileKind)
                            .Include(d => d.DocFileType)
                            .Where(d => d.DocId == doc.DocId)
                            .OrderByDescending(d => d.IsPrimary)
                            .ThenBy(d => d.DocFileId)
                            .FirstOrDefault();

                        Document document = new Document();
                        document.AccessIdentifier = doc.RegUri;
                        document.AISDocumentRegisterDocumentData = new AISDocumentRegisterDocumentData();
                        document.AISDocumentRegisterDocumentData.RegistrationTime = doc.RegDate;
                        document.AISDocumentRegisterDocumentData.RegisteredDocumentURI = new RegisteredDocumentURI();
                        document.AISDocumentRegisterDocumentData.RegisteredDocumentURI.DocumentInInternalRegisterURI = doc.RegUri;
                        document.AISDocumentRegisterDocumentData.DocumentType = new AdministrativeNomenclatureDocumentTypeBasicData();
                        document.AISDocumentRegisterDocumentData.DocumentType.Name = doc.DocType.Name;
                        if (doc.DocType.IsElectronicService)
                        {
                            document.AISDocumentRegisterDocumentData.DocumentType.URI = new AdministrativeNomenclatureDocumentTypeURI();
                            document.AISDocumentRegisterDocumentData.DocumentType.URI.SegmentUnifiedDataURI = new UnifiedDataURI();
                            if (!String.IsNullOrWhiteSpace(doc.DocType.ElectronicServiceFileTypeUri))
                            {
                                document.AISDocumentRegisterDocumentData.DocumentType.URI.SegmentUnifiedDataURI.RegisterIndex = doc.DocType.ElectronicServiceFileTypeUri.Split(new char[] { '-' })[0];
                                document.AISDocumentRegisterDocumentData.DocumentType.URI.SegmentUnifiedDataURI.BatchNumber = doc.DocType.ElectronicServiceFileTypeUri.Split(new char[] { '-' })[1];
                            }
                        }

                        document.AISDocument = new R_0009_000085.AISDocument();

                        if (docFile != null && docFile.DocFileKind.Alias.ToLower() == "PublicAttachedFile".ToLower())
                        {
                            document.AISDocument.Name = docFile.DocFileName;
                            document.AISDocument.DocumentProcessType = "0006-000050";
                            document.AISDocument.ObjectCreationData = new R_0009_000032.AISObjectCreationData();
                            document.AISDocument.ObjectCreationData.CreationTime = doc.RegDate;

                            document.AISDocumentRegisterDocumentData.DocumentElectronicTransportType = docFile.DocFileType.MimeType;
                        }

                        string registratorPosition = String.Empty;
                        string registratorName = String.Empty;
                        if (doc.DocWorkflows.Any(e => e.DocWorkflowAction.Alias == "Registration"))
                        {
                            int? unitId = doc.DocWorkflows.Where(e => e.DocWorkflowAction.Alias == "Registration").First().PrincipalUnitId;
                            if (unitId.HasValue)
                            {
                                Tuple<string, string> postionAndName = this.docRepository.GetPositionAndNameById(unitId.Value);

                                registratorPosition = postionAndName.Item1;
                                registratorName = postionAndName.Item2;
                            }
                        }

                        Tuple<string, string, string> splitNames = Helper.SplitNames(registratorName);
                        document.AISDocumentRegisterDocumentData.RegisteredBy = new R_0009_000070.RegistrationInDocumentRegisterRegistrar();
                        document.AISDocumentRegisterDocumentData.RegisteredBy.AISUserBasicData = new R_0009_000027.AISUserBasicData();
                        document.AISDocumentRegisterDocumentData.RegisteredBy.AISUserBasicData.Names = new R_0009_000018.AISUserNames();
                        document.AISDocumentRegisterDocumentData.RegisteredBy.AISUserBasicData.Names.PersonNames = new R_0009_000005.PersonNames();
                        document.AISDocumentRegisterDocumentData.RegisteredBy.AISUserBasicData.Names.PersonNames.First = splitNames.Item1;
                        document.AISDocumentRegisterDocumentData.RegisteredBy.AISUserBasicData.Names.PersonNames.Middle = splitNames.Item2;
                        document.AISDocumentRegisterDocumentData.RegisteredBy.AISUserBasicData.Names.PersonNames.Last = splitNames.Item3;

                        document.AISDocumentRegisterDocumentData.RegisteredBy.AISUserBasicData.Position = new R_0009_000026.AISUserPositionInAdministration();
                        document.AISDocumentRegisterDocumentData.RegisteredBy.AISUserBasicData.Position.Position = new R_0009_000026.Position();
                        document.AISDocumentRegisterDocumentData.RegisteredBy.AISUserBasicData.Position.Position.EKDAPositonName = registratorPosition;

                        caseFileInfo.AISCaseDataInternetAccess.Documents.DocumentCollection.Add(document);
                    }

                    return caseFileInfo;
                }
                else
                {
                    return null;
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public Stream GetDocumentContentWithCustomUri(string uri)
        {
            var doc = this.docRepository.GetDocByRegUri(uri);

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
