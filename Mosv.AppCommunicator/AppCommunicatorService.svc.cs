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
using R_0009_000089;
using R_0009_000073;
using Common.Extensions;
using R_0009_000054;
using R_0009_000062;
using R_0009_000005;
using R_0009_000018;
using R_0009_000030;
using R_0009_000027;
using R_0009_000026;
using R_0009_000044;
using R_0009_000072;
using R_0009_000042;
using R_0009_000043;
using R_0009_000031;
using Mosv.Api.Models;
using Regs.Api.Models;
using Rio.Data.ServiceContracts.AppCommunicator;

namespace Mosv.AppCommunicator
{
    public class AppCommunicatorService : Rio.Data.ServiceContracts.AppCommunicator.IDocumentService
    {
        private IUnitOfWork unitOfWork;
        private IDocRepository docRepository;

        public AppCommunicatorService()
        {
            List<IDbConfiguration> configurations = new List<IDbConfiguration>();
            configurations.Add(new DocsDbConfiguration());
            configurations.Add(new CommonDbConfiguration());
            configurations.Add(new MosvDbConfiguration());
            configurations.Add(new RegsDbConfiguration());

            this.unitOfWork = new UnitOfWork(configurations, Enumerable.Empty<IDbContextInitializer>());
            this.docRepository = new DocRepository(unitOfWork);
        }

        public DocumentInfo ProcessStructuredDocument(DocumentRequest request)
        {
            using (var transaction = this.unitOfWork.BeginTransaction())
            {
                IncomingDoc incomingDoc = new IncomingDoc();
                incomingDoc.DocumentGuid = Guid.NewGuid();
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
            if (uri != null || String.IsNullOrWhiteSpace(publicAccessCode))
            {
                var caseDoc = this.docRepository.GetByRegUriAndAccessCode(uri.RegisterIndex, int.Parse(uri.SequenceNumber), uri.ReceiptOrSigningDate.Value, publicAccessCode);
                if (caseDoc != null)
                {
                    var mosvElectronicServiceProvider = this.unitOfWork.DbContext.Set<ElectronicServiceProvider>().Where(e => e.Code == caseDoc.DocType.ElectronicServiceProvider).FirstOrDefault();

                    CaseFileInfo caseFileInfo = new CaseFileInfo();
                    caseFileInfo.AISCaseDataInternetAccess = new AISCaseDataInternetAccess();
                    caseFileInfo.AISCaseDataInternetAccess.Name = !String.IsNullOrWhiteSpace(caseDoc.DocType.ApplicationName) ? caseDoc.DocType.ApplicationName : caseDoc.DocType.Name;
                    caseFileInfo.AISCaseDataInternetAccess.ServiceName = mosvElectronicServiceProvider != null ? mosvElectronicServiceProvider.Name : "МОСВ";
                    caseFileInfo.AISCaseDataInternetAccess.URI = new AISCaseURI();
                    caseFileInfo.AISCaseDataInternetAccess.URI.DocumentURI = uri;
                    caseFileInfo.AISCaseDataInternetAccess.Documents = new Documents();
                    caseFileInfo.AISCaseDataInternetAccess.Documents.DocumentCollection = new DocumentCollection();

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
                        document.AISDocumentRegisterDocumentData.RegisteredDocumentURI.DocumentURI = new DocumentURI();
                        document.AISDocumentRegisterDocumentData.RegisteredDocumentURI.DocumentURI.RegisterIndex = doc.RegIndex;
                        document.AISDocumentRegisterDocumentData.RegisteredDocumentURI.DocumentURI.SequenceNumber = doc.RegNumber.HasValue ? doc.RegNumber.ToString() : null;
                        document.AISDocumentRegisterDocumentData.RegisteredDocumentURI.DocumentURI.ReceiptOrSigningDate = doc.RegDate;
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
                            document.AISDocument.DocumentProcessType = "0006-000050";   //TODO: Consider value
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

        public ServiceStatus GetServiceStatus(DocumentURI uri, string serviceIdentifier)
        {
            if (uri != null)
            {
                var doc = this.docRepository.GetDocByRegUriIncludeElectronicServiceStages(uri.RegisterIndex, int.Parse(uri.SequenceNumber), uri.ReceiptOrSigningDate.Value);
                if (doc != null)
                {
                    ServiceStatus serviceStatus = new ServiceStatus();
                    serviceStatus.InitiatingDocumentURI = new InitiatingDocumentURI();
                    serviceStatus.InitiatingDocumentURI.RegisterIndex = uri.RegisterIndex;
                    serviceStatus.InitiatingDocumentURI.SequenceNumber = uri.SequenceNumber;
                    serviceStatus.InitiatingDocumentURI.ReceiptOrSigningDate = uri.ReceiptOrSigningDate;
                    //if (!String.IsNullOrWhiteSpace(doc.DocType.ElectronicServiceFileTypeUri))
                    //{
                    //    serviceStatus.ServiceURI = new AdministrativeNomenclatureServiceURI();
                    //    serviceStatus.ServiceURI.SUNAUServiceURI = doc.DocType.ElectronicServiceFileTypeUri;
                    //}
                    serviceStatus.ServiceURI = new AdministrativeNomenclatureServiceURI();
                    serviceStatus.ServiceURI.SUNAUServiceURI = !String.IsNullOrWhiteSpace(doc.DocType.ApplicationName) ? doc.DocType.ApplicationName : doc.DocType.Name;


                    var allStages = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
                        .Where(e => e.DocTypeId == doc.DocTypeId.Value)
                        .OrderBy(e => e.ElectronicServiceStageId)
                        .ToList();

                    if (doc.DocElectronicServiceStages.Where(s => s.EndingDate.HasValue).Any())
                    {
                        serviceStatus.ExecutedTasks = new ExecutedTasks();
                        serviceStatus.ExecutedTasks.TaskCollection = new TaskCollection();

                        foreach (var executedStage in doc.DocElectronicServiceStages.Where(s => s.EndingDate.HasValue))
                        {
                            var stage = allStages.Where(s => s.ElectronicServiceStageId == executedStage.ElectronicServiceStageId).FirstOrDefault();

                            if (stage != null)
                            {
                                var task = new Task();

                                task.TaskData = new AISTask();
                                task.TaskData.NameAndShortDescription = stage.Name;
                                task.TaskData.ExpandedDescription = stage.Description; ;
                                task.TaskData.ScheduledStartDate = executedStage.StartingDate;
                                task.TaskData.ScheduledCompletionDate = executedStage.ExpectedEndingDate;
                                task.TaskData.ActualStartDate = executedStage.StartingDate;
                                task.TaskData.ActualCompletionDate = executedStage.EndingDate;
                                task.TaskData.ExecutedBy = new AISTaskExecutor();
                                task.TaskData.ExecutedBy.Names = new AISUserNames();
                                task.TaskData.ExecutedBy.Names.PersonNames = new PersonNames();
                                task.TaskData.ExecutedBy.Names.PersonNames.First = doc.DocSourceType.Alias == "Internet" && stage.Alias == "AcceptApplication" ? "Системен потребител" : "Служител МОСВ";

                                serviceStatus.ExecutedTasks.TaskCollection.Add(task);
                            }
                        }
                    }

                    var unexecutedStages =
                        allStages.Where(s =>
                        !doc.DocElectronicServiceStages.Where(ds => ds.EndingDate.HasValue).Select(ds => ds.ElectronicServiceStageId).ToList()
                        .Contains(s.ElectronicServiceStageId));

                    if (unexecutedStages.Any())
                    {
                        serviceStatus.UnexecutedTasks = new UnexecutedTasks();
                        serviceStatus.UnexecutedTasks.TaskOrServiceStageCollection = new TaskOrServiceStageCollection();

                        foreach (var unexecutedStage in unexecutedStages)
                        {
                            if (unexecutedStage.Alias != "DecreeRefusal")
                            {
                                var task = new TaskOrServiceStage();
                                task.Task = new TaskOrServiceStageTask();
                                task.Task.TaskData = new AISTask();
                                task.Task.TaskData.NameAndShortDescription = unexecutedStage.Name;
                                task.Task.TaskData.ExpandedDescription = unexecutedStage.Description;
                                task.Task.TaskData.ScheduledStartDate = null;
                                task.Task.TaskData.ScheduledCompletionDate = null;
                                task.Task.TaskData.ExecutedBy = new AISTaskExecutor();
                                task.Task.TaskData.ExecutedBy.Names = new AISUserNames();
                                task.Task.TaskData.ExecutedBy.Names.PersonNames = new PersonNames();
                                task.Task.TaskData.ExecutedBy.Names.PersonNames.First = "Служител МОСВ";

                                serviceStatus.UnexecutedTasks.TaskOrServiceStageCollection.Add(task);
                            }
                        }
                    }

                    return serviceStatus;
                }
                else
                {
                    throw new Exception("Document not found.");
                }
            }
            else
            {
                throw new ArgumentException();
            }
        }

        public void RegisterElectronicPortalPmntInfo(ElectronicPortalPaymentInfo paymentInfo)
        {
            throw new NotImplementedException();
        }

        public CaseFileInfo GetCaseFileInfo(string uri)
        {
            throw new NotImplementedException();
        }

        public CaseStatusInfo GetCaseStatusInfo(Guid guid)
        {
            throw new NotImplementedException();
        }

        public CaseFileInfo GetCaseFileInfoWithCustomUri(string uri)
        {
            throw new NotImplementedException();
        }

        public Stream GetDocumentContentWithCustomUri(string uri)
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
