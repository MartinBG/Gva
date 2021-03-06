﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Docs.Api.Models;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.Repositories.DocRepository;
using Docs.Api.Repositories.EmailRepository;
using NLog;
using Common.Utils;
using Common.Extensions;
using System.Data.SqlClient;
using Common.Blob;
using System.Configuration;
using Aop.Api.Models;
using Autofac.Features.OwnedInstances;
using Rio.Data.Abbcdn;
using Rio.Data.RioObjectExtractor;
using Rio.Data.Utils.RioDocumentParser;
using Rio.Data.Utils.RioValidator;
using Rio.Data.DataObjects;
using Rio.Objects.Enums;
using Rio.Objects;
using System.Text.RegularExpressions;
using Docs.Api.Models.UnitModels;

namespace Aop.Rio.IncomingDocProcessor
{
    public class IncomingDocProcessor : IIncomingDocProcessor
    {
        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        private Func<Owned<IUnitOfWork>> unitOfWorkFactory;

        private IUnitOfWork unitOfWork;
        private IDocRepository docRepository;
        private IEmailRepository emailRepository;
        private ICorrespondentRepository correspondentRepository;
        private IRioObjectExtractor rioObjectExtractor;
        private IRioDocumentParser rioDocumentParser;
        private IRioValidator rioValidator;

        public IncomingDocProcessor(
            Func<Owned<IUnitOfWork>> unitOfWorkFactory,
            IUnitOfWork unitOfWork,
            IDocRepository docRepository,
            IEmailRepository emailRepository,
            ICorrespondentRepository correspondentRepository,
            IRioObjectExtractor rioObjectExtractor,
            IRioDocumentParser rioDocumentParser,
            IRioValidator rioValidator)
        {
            this.unitOfWorkFactory = unitOfWorkFactory;
            this.unitOfWork = unitOfWork;
            this.docRepository = docRepository;
            this.emailRepository = emailRepository;
            this.correspondentRepository = correspondentRepository;
            this.rioObjectExtractor = rioObjectExtractor;
            this.rioDocumentParser = rioDocumentParser;
            this.rioValidator = rioValidator;
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
                    object rioApplication = rioDocumentParser.XmlDeserializeApplication(xmlContent);

                    ApplicationDataDo applicationDataDo = rioObjectExtractor.Extract<ApplicationDataDo>(rioApplication);

                    IList<AttachedDocDo> attachedDocuments = rioObjectExtractor.Extract<IList<AttachedDocDo>>(rioApplication);
                    MarkAttachedFilesAsUsed(attachedDocuments);

                    List<ElectronicDocumentDiscrepancyTypeNomenclature> discrepancies = GetValidationDiscrepancies(xmlContent, applicationDataDo, attachedDocuments);
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

                    //TODO: Implement
                    //var validationErrors = rioValidator.ValidateRioApplication(null, xmlContent)
                    List<string> validationErrors = new List<string>();

                    Doc rootDoc = null;
                    if (applicationDataDo.DocumentURI != null)
                    {
                        string regIndex = applicationDataDo.DocumentURI.RegisterIndex.PadLeft(4, '0');
                        int regNumber = int.Parse(applicationDataDo.DocumentURI.SequenceNumber);
                        DateTime regdDate = applicationDataDo.DocumentURI.ReceiptOrSigningDate.Value;

                        rootDoc = this.docRepository.GetDocByRegUri(regIndex, regNumber, regdDate);
                    }

                    string electronicServiceFileTypeUri = String.Format("{0}-{1}", applicationDataDo.DocumentTypeURI.RegisterIndex, applicationDataDo.DocumentTypeURI.BatchNumber);
                    int docTypeId = this.unitOfWork.DbContext.Set<DocType>().Single(e => e.ElectronicServiceFileTypeUri == electronicServiceFileTypeUri).DocTypeId;

                    //Initial Doc
                    Doc initialDoc = CreateInitialDoc(isDocAcknowledged, validationErrors, docTypeId, rootDoc == null, systemUser);
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

                    this.docRepository.ExecSpSetDocTokens(docId: initialDoc.DocId);
                    this.docRepository.ExecSpSetDocUnitTokens(docId: initialDoc.DocId);

                    Guid fileKey = WriteToBlob(Utf8Utils.GetBytes(incomingDocFile.DocFileContent));

                    DocFileKind publicDocFileKind = this.unitOfWork.DbContext.Set<DocFileKind>().Single(e => e.Alias == "PublicAttachedFile");

                    var docFileType = this.unitOfWork.DbContext.Set<DocFileType>().SingleOrDefault(e => e.Alias == applicationDataDo.DocFileTypeAlias);

                    DocFile initialDocFile = CreateInitialDocFile(initialDoc, fileKey, docFileType.DocFileTypeId, publicDocFileKind.DocFileKindId, applicationDataDo.ApplicationSigningTime);
                    this.unitOfWork.DbContext.Set<DocFile>().Add(initialDocFile);

                    //Add attached application files as DocFiles
                    if (attachedDocuments != null && attachedDocuments.Count > 0)
                    {
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
                    }

                    this.unitOfWork.Save();

                    if (applicationDataDo.SendConfirmationEmail)
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

                using (var factory = unitOfWorkFactory())
                {
                    this.unitOfWork = factory.Value;

                    var incomingDoc = this.unitOfWork.DbContext.Set<IncomingDoc>().SingleOrDefault(e => e.IncomingDocId == pendingIncomingDocId);

                    incomingDoc.IncomingDocStatusId = this.unitOfWork.DbContext.Set<IncomingDocStatus>().Single(e => e.Alias == "Incorrect").IncomingDocStatusId;

                    this.unitOfWork.Save();
                }
            }
        }

        public List<ElectronicDocumentDiscrepancyTypeNomenclature> GetValidationDiscrepancies(string xmlContent, ApplicationDataDo applicationDataDo, IList<AttachedDocDo> attachedDocuments)
        {
            List<ElectronicDocumentDiscrepancyTypeNomenclature> discrepancies = new List<ElectronicDocumentDiscrepancyTypeNomenclature>();

            string[] supportedFileFormats = { "fed", "doc", "docx" };
            RioDocumentMetadata documentMetaData = rioDocumentParser.GetDocumentMetadataFromXml(xmlContent);

            if (documentMetaData.IsZeuService)
            {
                if (!rioValidator.CheckEmail(applicationDataDo.Email))
                {
                    discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.NoEmail);
                }
            }

            if (!rioValidator.CheckDocumentSize(xmlContent))
            {
                discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.SizeTooLarge);
            }

            if (!rioValidator.CheckSignatureValidity(xmlContent, documentMetaData.SignatureXPath, documentMetaData.SignatureXPathNamespaces))
            {
                discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.NotAuthenticated);
            }

            if (attachedDocuments != null && attachedDocuments.Count > 0)
            {
                if (!rioValidator.CheckSupportedFileFormats(attachedDocuments.Select(e => e.FileName).ToList(), supportedFileFormats))
                {
                    discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.IncorrectAttachmentsFormat);
                }
            }

            //TODO: Implement
            //if (!rioValidator.CheckValidXmlSchema(xmlContent, GetSchemasPath()))
            //{
            //    discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.IncorrectFormat);
            //}

            //TODO: Implement
            //if (!skipCertificateChainValidation) //take it from Web.config
            //{
            //    var revocationErrors = rioValidator.CheckCertificateValidity(xmlContent, applicationDataDo.ElectronicServiceApplicant, documentMetaData.SignatureXPath, documentMetaData.SignatureXPathNamespaces);
            //    if (revocationErrors != null && revocationErrors.Count() > 0)
            //    {
            //        discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.NotAuthenticated);
            //    }
            //}

            //TODO: Implement
            //if (!rioValidator.CheckForVirus(xmlContent, null, 0))
            //{
            //    discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.IncorrectAttachmentsFormat);
            //}

            return discrepancies;
        }

        //TODO
        private void MarkAttachedFilesAsUsed(IList<AttachedDocDo> attachedDocuments)
        {
            if (attachedDocuments != null && attachedDocuments.Count > 0)
            {
                foreach (var attachedDoc in attachedDocuments)
                {
                    if (attachedDoc.UseAbbcdn)
                    {
                        AbbcdnStorage.SetIsInUse(Guid.Parse(attachedDoc.AbbcdnInfo.AttachedDocumentUniqueIdentifier), true);
                    }
                }
            }
        }

        private List<Correspondent> GetDocumentCorrespondents(object rioApplication)
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

        private Doc CreateInitialDoc(bool isDocAcknowledged, List<string> validationErrors, int docTypeId, bool isCase, User user)
        {
            Doc doc = new Doc();
            doc.DocDirectionId = this.unitOfWork.DbContext.Set<DocDirection>().Where(e => e.Alias == "Incomming").Single().DocDirectionId;
            doc.DocEntryTypeId = this.unitOfWork.DbContext.Set<DocEntryType>().Where(e => e.Alias == "Document").Single().DocEntryTypeId;
            doc.DocSubject = "Заявление подадено през портала за електронни административни услуги";
            doc.DocSourceTypeId = this.unitOfWork.DbContext.Set<DocSourceType>().Where(e => e.Alias == "Internet").Single().DocSourceTypeId;
            doc.DocStatusId = this.unitOfWork.DbContext.Set<DocStatus>().Where(e => e.Alias == "Processed").Single().DocStatusId;
            doc.DocDestinationTypeId = null;
            if (isDocAcknowledged)
            {
                doc.DocTypeId = docTypeId;
            }
            else
            {
                doc.DocTypeId = this.unitOfWork.DbContext.Set<DocType>().Where(e => e.Alias == "InvalidService").Single().DocTypeId;
            }
            doc.DocFormatTypeId = this.unitOfWork.DbContext.Set<DocFormatType>().Where(e => e.Alias == "Electronic").Single().DocFormatTypeId;
            doc.DocCasePartTypeId = this.unitOfWork.DbContext.Set<DocCasePartType>().Where(e => e.Alias == "Public").Single().DocCasePartTypeId;
            doc.RegDate = DateTime.Now;
            doc.CorrRegNumber = null;
            doc.CorrRegDate = null;
            doc.RegDate = DateTime.Now;
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
                .SingleOrDefault(e => e.Alias == "Pending" && e.DocTypeId == docTypeId); //? pending?

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
                docClassification.IsInherited = docTypeClassification.IsInherited;
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

            int docFileTypeId = this.unitOfWork.DbContext.Set<DocFileType>().SingleOrDefault(e => e.Alias.ToLower() == "UnknownBinary").DocFileTypeId;
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
            EmailStatus pendingStatus = this.emailRepository.GetEmailStatusByAlias("Pending");

            EmailAddresseeType to = this.emailRepository.GetEmailAddresseeTypeByAlias("To");

            EmailType emailType = null;

            if (isDocAcknowledged)
            {
                emailType = this.emailRepository.GetEmailTypeByAlias("ReceiptAcknowledgedEmail");
            }
            else
            {
                emailType = this.emailRepository.GetEmailTypeByAlias("ReceiptNotAcknowledgedEmail");
            }

            Email email = this.emailRepository.CreateEmail(emailType.EmailTypeId, pendingStatus.EmailStatusId, emailType.Subject, emailType.Body);

            if (!string.IsNullOrEmpty(systemUser.Email) && Regex.IsMatch(systemUser.Email, Docs.Api.EmailSender.EmailSender.emailRegex))
            {
                email.EmailAddressees.Add(new EmailAddressee()
                {
                    EmailAddresseeTypeId = to.EmailAddresseeTypeId,
                    Address = systemUser.Email
                });
            }

            if (docCorrespondents.Any())
            {
                foreach (var item in docCorrespondents)
                {
                    if (!string.IsNullOrEmpty(item.Email) && Regex.IsMatch(item.Email, Docs.Api.EmailSender.EmailSender.emailRegex))
                    {
                        email.EmailAddressees.Add(new EmailAddressee()
                        {
                            EmailAddresseeTypeId = to.EmailAddresseeTypeId,
                            Address = systemUser.Email
                        });
                    }
                }
            }

            this.unitOfWork.DbContext.Set<Email>().Add(email);
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
            CodeGeneratorUtils codeGenerator = new CodeGeneratorUtils();
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
