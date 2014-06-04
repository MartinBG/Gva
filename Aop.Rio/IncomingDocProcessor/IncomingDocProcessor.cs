using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Common.Rio.PortalBridge;
using Common.Rio.RioObjectExtractor;
using Docs.Api.Models;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.Repositories.DocRepository;
using Aop.Rio.Abbcdn;
using NLog;
using Common.Utils;
using Common.Extensions;
using System.Data.SqlClient;
using Common.Blob;
using System.Configuration;
using Aop.RioBridge.DataObjects;
using Common.Rio.PortalBridge.RioObjects;
using Aop.Api.Models;
using Aop.RioBridge.DataObjects;

namespace Aop.Rio.IncomingDocProcessor
{
    public class IncomingDocProcessor : IIncomingDocProcessor
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private IUnitOfWork unitOfWork;
        private IDocRepository docRepository;
        private ICorrespondentRepository correspondentRepository;
        private IRioObjectExtractor rioObjectExtractor;
        private IRioDocumentParser rioDocumentParser;

        public IncomingDocProcessor(
            IUnitOfWork unitOfWork,
            IDocRepository docRepository,
            ICorrespondentRepository correspondentRepository,
            IRioObjectExtractor rioObjectExtractor,
            IRioDocumentParser rioDocumentParser)
        {
            this.unitOfWork = unitOfWork;
            this.docRepository = docRepository;
            this.correspondentRepository = correspondentRepository;
            this.rioObjectExtractor = rioObjectExtractor;
            this.rioDocumentParser = rioDocumentParser;
        }

        public AbbcdnStorage AbbcdnStorage { get; set; }

        public void Process(int pendingIncomingDocId)
        {
            try
            {
                using (var transaction = this.unitOfWork.BeginTransaction())
                {
                    //Init user variables
                    User systemUser = this.unitOfWork.DbContext.Set<User>().Single(e => e.Username == "system");
                    UnitUser systemUnitUser = this.unitOfWork.DbContext.Set<UnitUser>().Include(e => e.Unit).Single(e => e.UserId == systemUser.UserId);
                    UserContext systemUserContext = new UserContext(systemUser.UserId);

                    IncomingDoc incomingDoc = this.unitOfWork.DbContext.Set<IncomingDoc>()
                        .Include(e => e.IncomingDocFiles)
                        .Where(e => e.IncomingDocId == pendingIncomingDocId)
                        .Single();

                    IncomingDocFile incomingDocFile = incomingDoc.IncomingDocFiles.First();

                    string xmlContent = incomingDocFile.DocFileContent;
                    RioApplication rioApplication = rioDocumentParser.XmlDeserializeApplication(xmlContent);

                    IList<AttachedDocDo> attachedDocuments = rioObjectExtractor.Extract<IList<AttachedDocDo>>(rioApplication);
                    MarkAttachedFilesAsUsed(attachedDocuments);

                    List<ElectronicDocumentDiscrepancyTypeNomenclature> discrepancies = rioDocumentParser.GetValidationDiscrepancies(xmlContent);
                    bool isDocAcknowledged = discrepancies.Count == 0;

                    incomingDoc.IncomingDocStatusId = this.unitOfWork.DbContext.Set<IncomingDocStatus>()
                        .Where(e => e.Alias == (isDocAcknowledged ? "Registered" : "NotRegistered"))
                        .Single()
                        .IncomingDocStatusId;

                    List<Correspondent> docCorrespondents = this.GetDocumentCorrespondents(rioApplication);
                    foreach (var correspondent in docCorrespondents.Where(c => c.CorrespondentId == 0))
                    {
                        this.unitOfWork.DbContext.Set<Correspondent>().Add(correspondent);
                        this.unitOfWork.Save();
                    }

                    //string docFileTypeAlias = rioApplication.DocFileTypeAlias;
                    string docFileTypeAlias = "R-0001";
                    var docFileType = this.unitOfWork.DbContext.Set<DocFileType>().SingleOrDefault(e => e.Alias == docFileTypeAlias);
                    var validationErrors = rioDocumentParser.GetValidationErrors(docFileType.DocTypeUri, xmlContent);

                    Doc rootDoc = null;
                    if (rioApplication.DocumentURI != null)
                    {
                        string regIndex = rioApplication.DocumentURI.RegisterIndex.PadLeft(4, '0');
                        int regNumber = int.Parse(rioApplication.DocumentURI.SequenceNumber);
                        DateTime regdDate = rioApplication.DocumentURI.ReceiptOrSigningDate.Value;

                        rootDoc = this.docRepository.GetDocByRegUri(regIndex, regNumber, regdDate);
                    }

                    //string electronicServiceFileTypeUri = String.Format("{0}-{1}", rioApplication.DocumentTypeURI.RegisterIndex, rioApplication.DocumentTypeURI.BatchNumber);
                    string electronicServiceFileTypeUri = "0010-aop";
                    int docTypeId = this.unitOfWork.DbContext.Set<DocType>().Single(e => e.ElectronicServiceFileTypeUri == electronicServiceFileTypeUri).DocTypeId;

                    //Initial Doc
                    Doc initialDoc = CreateInitialDoc(validationErrors, docTypeId, rootDoc == null, systemUser);
                    this.unitOfWork.DbContext.Set<Doc>().Add(initialDoc);

                    foreach (var correspondent in docCorrespondents)
                    {
                        DocCorrespondent docCorrespondent = new DocCorrespondent();
                        docCorrespondent.Doc = initialDoc;
                        docCorrespondent.Correspondent = correspondent;
                        this.unitOfWork.DbContext.Set<DocCorrespondent>().Add(docCorrespondent);
                    }

                    DocIncomingDoc initialDocIncomingDoc = new DocIncomingDoc();
                    initialDocIncomingDoc.Doc = initialDoc;
                    initialDocIncomingDoc.IncomingDoc = incomingDoc;
                    initialDocIncomingDoc.IsDocInitial = true;
                    initialDocIncomingDoc.CreateDate = DateTime.Now;
                    this.unitOfWork.DbContext.Set<DocIncomingDoc>().Add(initialDocIncomingDoc);

                    this.unitOfWork.Save();

                    DocRelation initialDocRelation = CreateInitialDocRelation(initialDoc, rootDoc);
                    this.unitOfWork.DbContext.Set<DocRelation>().Add(initialDocRelation);

                    AopPortalDocRelation portalDocRelation = new AopPortalDocRelation();
                    portalDocRelation.PortalDocId = incomingDoc.DocumentGuid;
                    portalDocRelation.DocId = initialDoc.DocId;
                    this.unitOfWork.DbContext.Set<AopPortalDocRelation>().Add(portalDocRelation);

                    this.unitOfWork.Save();

                    //this.docRepository.RegisterDoc(initialDoc, systemUnitUser, systemUserContext);

                    AddDocUnit(initialDoc, systemUnitUser.Unit, systemUser);

                    AddDocClassification(initialDoc, systemUser);

                    this.unitOfWork.Save();

                    this.docRepository.spSetDocUsers(initialDoc.DocId);

                    Guid fileKey = WriteToBlob(Utf8Utils.GetBytes(incomingDocFile.DocFileContent));

                    DocFileKind publicDocFileKind = this.unitOfWork.DbContext.Set<DocFileKind>().Single(e => e.Alias == "PublicAttachedFile");

                    DateTime? applicationSigningTime = rioApplication.ApplicationSigningTime;
                    DocFile initialDocFile = CreateInitialDocFile(initialDoc, fileKey, docFileType.DocFileTypeId, publicDocFileKind.DocFileKindId, applicationSigningTime);
                    this.unitOfWork.DbContext.Set<DocFile>().Add(initialDocFile);

                    //Add attached application files as DocFiles
                    foreach (var attachedAppFile in attachedDocuments)
                    {
                        byte[] fileContent = null;
                        if (attachedAppFile.UseAbbcdn)
                        {
                            fileContent = AbbcdnStorage.DownloadFile(Guid.Parse(attachedAppFile.UniqueIdentifier)).ContentBytes;
                        }
                        else
                        {
                            fileContent = attachedAppFile.BytesContent;
                        }

                        Guid key = WriteToBlob(fileContent);

                        DocFile attachedAppDocFile = CreateEApplicationAttachDocFile(attachedAppFile, initialDoc, publicDocFileKind.DocFileKindId, key);
                        this.unitOfWork.DbContext.Set<DocFile>().Add(attachedAppDocFile);
                    }

                    this.unitOfWork.Save();

                    if (rioApplication.SendApplicationWithReceiptAcknowledgedMessage)
                    {
                        AddReceiveConfirmationEmailRecord(isDocAcknowledged, systemUser, docCorrespondents);
                    }

                    this.unitOfWork.Save();

                    AddBeginningServiceStage(docTypeId, initialDoc);

                    this.unitOfWork.Save();

                    transaction.Commit();
                }
            }
            catch (Exception ex)
            {
                logger.Error("IncommingDocProcessor Exception: " + Helper.GetDetailedExceptionInfo(ex));

                var incomingDoc = this.unitOfWork.DbContext.Set<IncomingDoc>().SingleOrDefault(e => e.IncomingDocId == pendingIncomingDocId);
                this.unitOfWork.DbContext.Entry(incomingDoc).Reload();

                incomingDoc.IncomingDocStatusId = this.unitOfWork.DbContext.Set<IncomingDocStatus>().Single(e => e.Alias == "Incorrect").IncomingDocStatusId;

                this.unitOfWork.Save();
            }
        }

        //TODO
        private void MarkAttachedFilesAsUsed(IList<AttachedDocDo> attachedDocuments)
        {
            foreach (var attachedDoc in attachedDocuments)
            {
                if (attachedDoc.UseAbbcdn)
                {
                    AbbcdnStorage.SetIsInUse(Guid.Parse(attachedDoc.AbbcdnInfo.AttachedDocumentUniqueIdentifier), true);
                }
            }
        }

        private List<Correspondent> GetDocumentCorrespondents(RioApplication rioApplication)
        {
            CorrespondentDo correspondentDo = rioObjectExtractor.Extract<CorrespondentDo>(rioApplication);

            List<Correspondent> returnValue = new List<Correspondent>();

            Correspondent correspondent = new Correspondent();
            correspondent.CorrespondentGroupId = this.unitOfWork.DbContext.Set<CorrespondentGroup>()
                .SingleOrDefault(e => e.Alias == "Applicants")
                .CorrespondentGroupId;
            correspondent.CorrespondentTypeId = this.unitOfWork.DbContext.Set<CorrespondentType>()
                .SingleOrDefault(e => e.Alias == "BulgarianCitizen")
                .CorrespondentTypeId;
            correspondent.BgCitizenFirstName = correspondentDo.FirstName;
            correspondent.BgCitizenLastName = correspondentDo.LastName;
            correspondent.BgCitizenPosition = correspondentDo.Position;
            correspondent.ContactPhone = correspondentDo.Phone;
            correspondent.RegisterIndexId = 1;

            correspondent.IsActive = true;

            returnValue.Add(correspondent);

            return returnValue;
        }

        private Doc CreateInitialDoc(List<string> validationErrors, int docTypeId, bool isCase, User user)
        {
            Doc doc = new Doc();
            doc.DocDirectionId = this.unitOfWork.DbContext.Set<DocDirection>().Where(e => e.Alias == "Incomming").Single().DocDirectionId;
            doc.DocEntryTypeId = this.unitOfWork.DbContext.Set<DocEntryType>().Where(e => e.Alias == "Document").Single().DocEntryTypeId;
            doc.DocSubject = "Заявление подадено през портала за електронни административни услуги";
            doc.DocSourceTypeId = this.unitOfWork.DbContext.Set<DocSourceType>().Where(e => e.Alias == "Internet").Single().DocSourceTypeId;
            doc.DocStatusId = this.unitOfWork.DbContext.Set<DocStatus>().Where(e => e.Alias == "Prepared").Single().DocStatusId;
            doc.DocDestinationTypeId = null;
            doc.DocTypeId = docTypeId;
            doc.DocFormatTypeId = this.unitOfWork.DbContext.Set<DocFormatType>().Where(e => e.Alias == "Electronic").Single().DocFormatTypeId;
            doc.DocCasePartTypeId = this.unitOfWork.DbContext.Set<DocCasePartType>().Where(e => e.Alias == "Public").Single().DocCasePartTypeId;
            doc.CorrRegNumber = null;
            doc.CorrRegDate = null;
            doc.AccessCode = GenerateAccessCode();
            doc.AssignmentTypeId = null;
            doc.AssignmentDate = null;
            doc.AssignmentDeadline = null;
            doc.IsCase = isCase;
            doc.IsSigned = true;
            doc.ModifyDate = null;
            doc.ModifyUserId = null;
            doc.IsActive = true;

            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < validationErrors.Count; i++)
            {
                builder.Append(String.Format("- {0}", validationErrors[i]));
                if (i != validationErrors.Count - 1)
                {
                    builder.AppendLine();
                }
            }

            doc.DocBody = builder.ToString();

            DocCasePartMovement dcpm = new DocCasePartMovement();
            dcpm.Doc = doc;
            dcpm.DocCasePartTypeId = doc.DocCasePartTypeId.Value;
            dcpm.UserId = user.UserId;
            dcpm.MovementDate = DateTime.Now;

            doc.DocCasePartMovements.Add(dcpm);

            return doc;
        }

        private DocElectronicServiceStage AddBeginningServiceStage(int docTypeId, Doc doc)
        {
            DocElectronicServiceStage serviceStage = null;

            var electronicServiceStage = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
                .SingleOrDefault(e => e.Alias == "AcceptApplication" && e.DocTypeId == docTypeId);

            if (electronicServiceStage != null)
            {
                serviceStage = new DocElectronicServiceStage();
                serviceStage.DocId = doc.DocId;
                serviceStage.ElectronicServiceStageId = electronicServiceStage.ElectronicServiceStageId;
                serviceStage.StartingDate = DateTime.Now;
                serviceStage.ExpectedEndingDate = DateTime.Now;
                serviceStage.EndingDate = null;
                serviceStage.IsCurrentStage = true;
                serviceStage.EndingDate = DateTime.Now;
                serviceStage.IsCurrentStage = false;

                this.unitOfWork.DbContext.Set<DocElectronicServiceStage>().Add(serviceStage);
            }

            return serviceStage;
        }

        private DocRelation CreateInitialDocRelation(Doc initialDoc, Doc rootDoc)
        {
            DocRelation docRelation = new DocRelation();
            docRelation.DocId = initialDoc.DocId;
            docRelation.ParentDocId = rootDoc != null ? rootDoc.DocId : (int?)null;
            docRelation.RootDocId = rootDoc != null ? rootDoc.DocId : initialDoc.DocId;

            return docRelation;
        }

        private void AddDocUnit(Doc doc, Unit systemUnit, User systemUser)
        {
            var docTypeUnitRoles = this.unitOfWork.DbContext.Set<DocTypeUnitRole>()
                .Where(e => e.DocTypeId == doc.DocTypeId.Value && e.DocDirectionId == doc.DocDirectionId)
                .ToList();

            foreach (var docTypeUnitRole in docTypeUnitRoles)
            {
                var docUnit = new DocUnit();
                docUnit.DocId = doc.DocId;
                docUnit.UnitId = docTypeUnitRole.UnitId;
                docUnit.DocUnitRoleId = docTypeUnitRole.DocUnitRoleId;
                docUnit.AddUserId = systemUser.UserId;
                docUnit.AddDate = DateTime.Now;

                this.unitOfWork.DbContext.Set<DocUnit>().Add(docUnit);
            }

            if (systemUnit != null)
            {
                var importedByRole = this.unitOfWork.DbContext.Set<DocUnitRole>().SingleOrDefault(du => du.Alias.ToLower() == "ImportedBy".ToLower());
                if (importedByRole != null)
                {
                    var dUnit = new DocUnit();
                    dUnit.DocId = doc.DocId;
                    dUnit.UnitId = systemUnit.UnitId;
                    dUnit.DocUnitRoleId = importedByRole.DocUnitRoleId;
                    dUnit.AddUserId = systemUser.UserId;
                    dUnit.AddDate = DateTime.Now;

                    this.unitOfWork.DbContext.Set<DocUnit>().Add(dUnit);
                }
            }
        }

        private void AddDocClassification(Doc doc, User systemUser)
        {
            var docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                .Where(e => e.DocTypeId == doc.DocTypeId.Value && e.DocDirectionId == doc.DocDirectionId)
                .ToList();

            foreach (var docTypeClassification in docTypeClassifications)
            {
                var docClassification = new DocClassification();
                docClassification.DocId = doc.DocId;
                docClassification.ClassificationId = docTypeClassification.ClassificationId;
                docClassification.ClassificationByUserId = systemUser.UserId;
                docClassification.ClassificationDate = DateTime.Now;
                docClassification.IsActive = true;

                this.unitOfWork.DbContext.Set<DocClassification>().Add(docClassification);
            }
        }

        private DocFile CreateInitialDocFile(Doc doc, Guid fileKey, int docFileTypeId, int docFileKindId, DateTime? applicationSigningTime)
        {
            DocFile docFile = new DocFile();
            docFile.DocFileContentId = fileKey;
            docFile.DocContentStorage = String.Empty;

            docFile.Doc = doc;
            docFile.DocFileTypeId = docFileTypeId;
            docFile.DocFileKindId = docFileKindId;
            docFile.Name = "Електронно заявление";
            docFile.DocFileName = "E-Application.xml";
            docFile.DocFileOriginTypeId = this.unitOfWork.DbContext.Set<DocFileOriginType>().Single(e => e.Alias == "EApplication").DocFileOriginTypeId;
            docFile.SignDate = applicationSigningTime;
            docFile.IsPrimary = true;
            docFile.IsSigned = true;
            docFile.IsActive = true;

            return docFile;
        }

        private DocFile CreateEApplicationAttachDocFile(AttachedDocDo attachedDocument, Doc doc, int docFileKindId, Guid fileKey)
        {
            DocFile docFile = new DocFile();
            docFile.Doc = doc;

            int docFileTypeId = docFileTypeId = this.unitOfWork.DbContext.Set<DocFileType>().SingleOrDefault(e => e.Alias.ToLower() == "UnknownBinary").DocFileTypeId;
            var fileExtension = MimeTypeHelper.GetFileExtenstionByMimeType(attachedDocument.MimeType);
            if (!String.IsNullOrWhiteSpace(fileExtension))
            {
                fileExtension = fileExtension.Substring(1);
                var docFileType = this.unitOfWork.DbContext.Set<DocFileType>().SingleOrDefault(e => e.Alias.ToLower() == fileExtension);

                if (docFileType != null)
                {
                    docFileTypeId = docFileType.DocFileTypeId;
                }
            }

            docFile.DocFileTypeId = docFileTypeId;
            docFile.DocFileKindId = docFileKindId;
            docFile.DocFileOriginTypeId = this.unitOfWork.DbContext.Set<DocFileOriginType>().Single(e => e.Alias == "EApplicationAttachedFile").DocFileOriginTypeId;
            docFile.Name = attachedDocument.DocKind;
            docFile.DocFileName = attachedDocument.AbbcdnInfo.AttachedDocumentFileName;
            docFile.DocContentStorage = String.Empty;
            docFile.DocFileContentId = fileKey;
            docFile.IsPrimary = false;
            docFile.IsSigned = false;
            docFile.IsActive = true;

            return docFile;
        }

        private void AddReceiveConfirmationEmailRecord(bool isDocAcknowledged, User systemUser, List<Correspondent> docCorrespondents)
        {
            var emailStatus = this.unitOfWork.DbContext.Set<AdministrativeEmailStatus>().Single(e => e.Alias == "New");

            AdministrativeEmailType emailType = null;
            if (isDocAcknowledged)
            {
                emailType = this.unitOfWork.DbContext.Set<AdministrativeEmailType>().Single(e => e.Alias == "ReceiptAcknowledgedEmail");
            }
            else
            {
                emailType = this.unitOfWork.DbContext.Set<AdministrativeEmailType>().Single(e => e.Alias == "ReceiptNotAcknowledgedEmail");
            }

            AdministrativeEmail email = new AdministrativeEmail();
            email.TypeId = emailType.AdministrativeEmailTypeId;
            email.UserId = systemUser.UserId;
            if (docCorrespondents.Count > 0)
            {
                email.CorrespondentId = docCorrespondents[0].CorrespondentId;
            }
            email.StatusId = emailStatus.AdministrativeEmailStatusId;
            email.Subject = emailType.Subject;
            email.Body = emailType.Body;

            this.unitOfWork.DbContext.Set<AdministrativeEmail>().Add(email);
        }

        private void AddCheckRegularityServiceStage(int docTypeId, Doc doc)
        {
            var electronicServiceStage = this.unitOfWork.DbContext.Set<ElectronicServiceStage>().SingleOrDefault(e => e.Alias == "CheckRegularity" && e.DocTypeId == docTypeId);

            if (electronicServiceStage != null)
            {
                DocElectronicServiceStage serviceStage = new DocElectronicServiceStage();
                serviceStage.DocId = doc.DocId;
                serviceStage.ElectronicServiceStageId = electronicServiceStage.ElectronicServiceStageId;
                serviceStage.StartingDate = DateTime.Now;
                serviceStage.ExpectedEndingDate = electronicServiceStage.Duration.HasValue ? serviceStage.StartingDate.AddDays(electronicServiceStage.Duration.Value) : (DateTime?)null;
                serviceStage.EndingDate = null;
                serviceStage.IsCurrentStage = true;

                this.unitOfWork.DbContext.Set<DocElectronicServiceStage>().Add(serviceStage);
            }
        }

        private string GenerateAccessCode()
        {
            CodeGenerator codeGenerator = new CodeGenerator();
            codeGenerator.Minimum = 10;
            codeGenerator.Maximum = 10;
            codeGenerator.ConsecutiveCharacters = true;
            codeGenerator.RepeatCharacters = true;

            while (true)
            {
                string code = codeGenerator.Generate();

                if (!this.docRepository.CheckForExistingAccessCode(code))
                {
                    return code;
                }
            }
        }

        private Guid WriteToBlob(byte[] content)
        {
            //TODO reuse the unitOfWork connection if possible
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
    }
}
