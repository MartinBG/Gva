﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using Common.Api.Models;
using Common.Api.UserContext;
using Common.Data;
using Rio.Data.RioObjectExtractor;
using Docs.Api.Models;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.Repositories.DocRepository;
using NLog;
using Common.Utils;
using Common.Extensions;
using System.Data.SqlClient;
using Common.Blob;
using System.Configuration;
using Rio.Data.DataObjects;
using Autofac.Features.OwnedInstances;
using Rio.Data.Utils.RioDocumentParser;
using Rio.Objects.Enums;
using Rio.Objects;
using R_0009_000019;
using R_0009_000001;
using R_0009_000017;
using R_0009_000016;
using R_0009_000137;
using Rio.Data.Utils.RioValidator;
using System.Reflection;
using Rio.Data.Abbcdn;
using System.Text.RegularExpressions;
using Docs.Api.Repositories.EmailRepository;
using Docs.Api.Models.UnitModels;

namespace Gva.Rio.IncomingDocProcessor
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

                    List<Correspondent> docCorrespondents = this.GetDocumentCorrespondents(applicationDataDo.ElectronicServiceApplicant, applicationDataDo.ElectronicServiceApplicantContactData);
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

                    this.unitOfWork.Save();

                    this.docRepository.RegisterDoc(initialDoc, systemUnitUser, systemUserContext);

                    AddDocUnit(initialDoc, systemUnitUser.Unit, systemUser);

                    var parentDocClassifications = AddDocClassification(initialDoc, systemUser);

                    this.unitOfWork.Save();

                    this.docRepository.ExecSpSetDocTokens(docId: initialDoc.DocId);
                    this.docRepository.ExecSpSetDocUnitTokens(docId: initialDoc.DocId);

                    initialDoc.SetReceiptOrder(this.docRepository.GetNextReceiptOrder(initialDoc.DocId), systemUserContext);

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

                    //ReceiptDoc
                    Doc receiptDoc = CreateReceiptDoc(isDocAcknowledged, systemUser);
                    this.unitOfWork.DbContext.Set<Doc>().Add(receiptDoc);

                    DocIncomingDoc receiptDocIncomingDoc = new DocIncomingDoc();
                    receiptDocIncomingDoc.Doc = receiptDoc;
                    receiptDocIncomingDoc.IncomingDoc = incomingDoc;
                    receiptDocIncomingDoc.IsDocInitial = false;
                    receiptDocIncomingDoc.CreateDate = DateTime.Now;
                    this.unitOfWork.DbContext.Set<DocIncomingDoc>().Add(receiptDocIncomingDoc);

                    this.unitOfWork.Save();

                    DocRelation receiptDocRelation = CreateReceiptDocRelation(initialDoc, receiptDoc, rootDoc);
                    this.unitOfWork.DbContext.Set<DocRelation>().Add(receiptDocRelation);

                    this.unitOfWork.Save();

                    this.docRepository.RegisterDoc(receiptDoc, systemUnitUser, systemUserContext);

                    AddDocUnit(receiptDoc, systemUnitUser.Unit, systemUser);

                    AddDocClassification(receiptDoc, systemUser);

                    foreach (var item in parentDocClassifications.Where(i => i.IsInherited))
                    {
                        receiptDoc.CreateDocClassification(item.ClassificationId, item.IsInherited, systemUserContext);
                    }

                    this.unitOfWork.Save();

                    this.docRepository.ExecSpSetDocTokens(docId: receiptDoc.DocId);
                    this.docRepository.ExecSpSetDocUnitTokens(docId: receiptDoc.DocId);

                    receiptDoc.SetReceiptOrder(this.docRepository.GetNextReceiptOrder(receiptDoc.DocId), systemUserContext);

                    Guid receiptFileKey = CreateReceiptDocFileContent(initialDoc, receiptDoc, rootDoc, discrepancies, applicationDataDo);

                    DocFile receiptDocFile = CreateReceiptDocFile(publicDocFileKind.DocFileKindId, receiptDoc, receiptFileKey, isDocAcknowledged);
                    this.unitOfWork.DbContext.Set<DocFile>().Add(receiptDocFile);

                    // TODO
                    //if (applicationDataDo.SendConfirmationEmail)
                    //{
                        AddReceiveConfirmationEmailRecord(isDocAcknowledged, systemUser, docCorrespondents, rootDoc ?? initialDoc, discrepancies);
                    //}

                    this.unitOfWork.Save();

                    AddBeginningServiceStage(docTypeId, initialDoc);

                    AddCheckRegularityServiceStage(docTypeId, initialDoc);

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

            string[] supportedFileFormats = { "pdf", "doc", "docx", "xls", "xlsx", "eml", "p7s", "ats", "sxw", "txt", "rtf", "jpg", "jpeg", "j2k", "png", "tiff", "tif", };
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

        private string GetSchemasPath()
        {
            string assemblyPath = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            string binPath = System.IO.Path.GetDirectoryName(assemblyPath);
            string projectPath = binPath.Substring(0, binPath.Length - 4);

            return String.Format(@"{0}\RioSchemas", projectPath);
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

        private List<Correspondent> GetDocumentCorrespondents(ElectronicServiceApplicant electronicServiceApplicant, ElectronicServiceApplicantContactData electronicServiceApplicantContactData)
        {
            List<Correspondent> returnValue = new List<Correspondent>();

            if (electronicServiceApplicant != null && electronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection.Count > 0)
            {
                foreach (var recipient in electronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection)
                {
                    Correspondent correspondent = null;

                    bool isNew = false;

                    string email = electronicServiceApplicant.EmailAddress ?? "";

                    if (recipient.Person != null)
                    {
                        string bgCitizenFirstName = recipient.Person.Names.First;
                        string bgCitizenLastName = recipient.Person.Names.Last;
                        string bgCitizenEgn = recipient.Person.Identifier.EGN;

                        //Get EmptyCorrespondent
                        if (String.IsNullOrWhiteSpace(email) ||
                            String.IsNullOrWhiteSpace(bgCitizenFirstName) ||
                            String.IsNullOrWhiteSpace(bgCitizenLastName) ||
                            String.IsNullOrWhiteSpace(bgCitizenEgn))
                        {
                            correspondent = this.unitOfWork.DbContext.Set<Correspondent>().SingleOrDefault(e => e.Alias == "Empty");
                        }
                        else
                        {
                            correspondent = this.correspondentRepository.GetBgCitizenCorrespondent(email, bgCitizenFirstName, bgCitizenLastName, bgCitizenEgn);

                            if (correspondent == null)
                            {
                                isNew = true;

                                correspondent = new Correspondent();
                                correspondent.CorrespondentGroupId = this.unitOfWork.DbContext.Set<CorrespondentGroup>()
                                    .SingleOrDefault(e => e.Alias == "Applicants")
                                    .CorrespondentGroupId;
                                correspondent.CorrespondentTypeId = this.unitOfWork.DbContext.Set<CorrespondentType>()
                                    .SingleOrDefault(e => e.Alias == "BulgarianCitizen")
                                    .CorrespondentTypeId;
                                correspondent.BgCitizenFirstName = bgCitizenFirstName;
                                correspondent.BgCitizenLastName = bgCitizenLastName;
                                correspondent.BgCitizenUIN = bgCitizenEgn;
                            }
                        }
                    }
                    else if (recipient.ForeignPerson != null)
                    {
                        string foreignerFirstName = recipient.ForeignPerson.Names.FirstLatin;
                        string foreignerLastName = recipient.ForeignPerson.Names.LastLatin;
                        string foreignerSettlement = recipient.ForeignPerson.PlaceOfBirth != null ? recipient.ForeignPerson.PlaceOfBirth.SettlementName : string.Empty;
                        DateTime? foreignerBirthDate = recipient.ForeignPerson.BirthDate;
                        string foreignerCountryCode = recipient.ForeignPerson.PlaceOfBirth != null ? recipient.ForeignPerson.PlaceOfBirth.CountryCode : string.Empty;
                        int? foreignerCountryId = null;

                        if (!String.IsNullOrWhiteSpace(foreignerCountryCode))
                        {
                            var country = this.unitOfWork.DbContext.Set<Country>()
                                .SingleOrDefault(e => e.Code == foreignerCountryCode);
                            foreignerCountryId = country != null ? country.CountryId : (int?)null;
                        }


                        //Get EmptyCorrespondent
                        if (String.IsNullOrWhiteSpace(email) ||
                            String.IsNullOrWhiteSpace(foreignerFirstName) ||
                            String.IsNullOrWhiteSpace(foreignerLastName))
                        {
                            correspondent = this.unitOfWork.DbContext.Set<Correspondent>().SingleOrDefault(e => e.Alias == "Empty");
                        }
                        else
                        {
                            correspondent = this.correspondentRepository.GetForeignerCorrespondent(email, foreignerFirstName, foreignerLastName, foreignerCountryId, foreignerSettlement, foreignerBirthDate);

                            if (correspondent == null)
                            {
                                isNew = true;

                                correspondent = new Correspondent();
                                correspondent.CorrespondentGroupId = this.unitOfWork.DbContext.Set<CorrespondentGroup>()
                                    .SingleOrDefault(e => e.Alias == "Applicants")
                                    .CorrespondentGroupId;
                                correspondent.CorrespondentTypeId = this.unitOfWork.DbContext.Set<CorrespondentType>()
                                    .SingleOrDefault(e => e.Alias == "Foreigner")
                                    .CorrespondentTypeId;
                                correspondent.ForeignerFirstName = foreignerFirstName;
                                correspondent.ForeignerLastName = foreignerLastName;
                                correspondent.ForeignerCountryId = foreignerCountryId;
                                correspondent.ForeignerSettlement = foreignerSettlement;
                                correspondent.ForeignerBirthDate = foreignerBirthDate;
                            }
                        }
                    }
                    else if (recipient.Entity != null)
                    {
                        string legalEntityName = recipient.Entity.Name;
                        string legalEntityBulstat = recipient.Entity.Identifier;

                        //Get EmptyCorrespondent
                        if (String.IsNullOrWhiteSpace(email) ||
                            String.IsNullOrWhiteSpace(legalEntityName) ||
                            String.IsNullOrWhiteSpace(legalEntityBulstat))
                        {
                            correspondent = this.unitOfWork.DbContext.Set<Correspondent>().SingleOrDefault(e => e.Alias == "Empty");
                        }
                        else
                        {
                            correspondent = this.correspondentRepository.GetLegalEntityCorrespondent(email, legalEntityName, legalEntityBulstat);

                            if (correspondent == null)
                            {
                                isNew = true;

                                correspondent = new Correspondent();
                                correspondent.CorrespondentGroupId = this.unitOfWork.DbContext.Set<CorrespondentGroup>()
                                    .SingleOrDefault(e => e.Alias == "Applicants")
                                    .CorrespondentGroupId;
                                correspondent.CorrespondentTypeId = this.unitOfWork.DbContext.Set<CorrespondentType>()
                                    .SingleOrDefault(e => e.Alias == "LegalEntity")
                                    .CorrespondentTypeId;
                                correspondent.LegalEntityName = legalEntityName;
                                correspondent.LegalEntityBulstat = legalEntityBulstat;
                            }
                        }
                    }
                    else if (recipient.ForeignEntity != null)
                    {
                        string fLEgalEntityName = recipient.ForeignEntity.ForeignEntityName;
                        string fLegalEntityRegisterName = recipient.ForeignEntity.ForeignEntityRegister;
                        string fLegalEntityRegisterNumber = recipient.ForeignEntity.ForeignEntityIdentifier;
                        string fLegalEntityOtherData = recipient.ForeignEntity.ForeignEntityOtherData;
                        string fLegalEntityCountryCode = recipient.ForeignEntity.CountryISO3166TwoLetterCode;
                        int? fLegalEntityCountryId = null;

                        if (!String.IsNullOrWhiteSpace(fLegalEntityCountryCode))
                        {
                            var country = this.unitOfWork.DbContext.Set<Country>()
                                .SingleOrDefault(e => e.Code == fLegalEntityCountryCode);
                            fLegalEntityCountryId = country != null ? country.CountryId : (int?)null;
                        }

                        //Get EmptyCorrespondent
                        if (String.IsNullOrWhiteSpace(email) ||
                            String.IsNullOrWhiteSpace(fLEgalEntityName) ||
                            !fLegalEntityCountryId.HasValue)
                        {
                            correspondent = this.unitOfWork.DbContext.Set<Correspondent>().SingleOrDefault(e => e.Alias == "Empty");
                        }
                        else
                        {
                            correspondent = this.correspondentRepository.GetFLegalEntityCorrespondent(email, fLEgalEntityName, fLegalEntityCountryId, fLegalEntityRegisterName, fLegalEntityRegisterNumber);

                            if (correspondent == null)
                            {
                                isNew = true;

                                correspondent = new Correspondent();
                                correspondent.CorrespondentGroupId = this.unitOfWork.DbContext.Set<CorrespondentGroup>()
                                    .SingleOrDefault(e => e.Alias == "Applicants")
                                    .CorrespondentGroupId;
                                correspondent.CorrespondentTypeId = this.unitOfWork.DbContext.Set<CorrespondentType>()
                                    .SingleOrDefault(e => e.Alias == "ForeignLegalEntity")
                                    .CorrespondentTypeId;
                                correspondent.FLegalEntityName = fLEgalEntityName;
                                correspondent.FLegalEntityCountryId = fLegalEntityCountryId;
                                correspondent.FLegalEntityRegisterName = fLegalEntityRegisterName;
                                correspondent.FLegalEntityName = fLegalEntityRegisterNumber;
                                correspondent.FLegalEntityOtherData = fLegalEntityOtherData;
                            }
                        }
                    }

                    if (electronicServiceApplicantContactData != null)
                    {
                        int? contactDistrictId = null;
                        int? contactMunicipalityId = null;
                        int? contactSettlementId = null;

                        if (!String.IsNullOrWhiteSpace(electronicServiceApplicantContactData.DistrictCode))
                        {
                            var district = this.unitOfWork.DbContext.Set<District>()
                                .SingleOrDefault(e => e.Code == electronicServiceApplicantContactData.DistrictCode);
                            contactDistrictId = district != null ? district.DistrictId : (int?)null;
                        }

                        if (!String.IsNullOrWhiteSpace(electronicServiceApplicantContactData.MunicipalityCode))
                        {
                            var municipality = this.unitOfWork.DbContext.Set<Municipality>()
                                .SingleOrDefault(e => e.Code == electronicServiceApplicantContactData.MunicipalityCode);
                            contactMunicipalityId = municipality != null ? municipality.MunicipalityId : (int?)null;
                        }

                        if (!String.IsNullOrWhiteSpace(electronicServiceApplicantContactData.SettlementCode))
                        {
                            var settlement = this.unitOfWork.DbContext.Set<Settlement>()
                                .SingleOrDefault(e => e.Code == electronicServiceApplicantContactData.SettlementCode);
                            contactSettlementId = settlement != null ? settlement.SettlementId : (int?)null;
                        }

                        correspondent.ContactDistrictId = contactDistrictId;
                        correspondent.ContactMunicipalityId = contactMunicipalityId;
                        correspondent.ContactSettlementId = contactSettlementId;
                        correspondent.ContactAddress = electronicServiceApplicantContactData.AddressDescription;
                        correspondent.ContactPostCode = electronicServiceApplicantContactData.PostCode;
                        correspondent.ContactPostOfficeBox = electronicServiceApplicantContactData.PostOfficeBox;

                        if (electronicServiceApplicantContactData.PhoneNumbers != null &&
                            electronicServiceApplicantContactData.PhoneNumbers.PhoneNumberCollection != null &&
                            electronicServiceApplicantContactData.PhoneNumbers.PhoneNumberCollection.Count > 0)
                        {
                            correspondent.ContactPhone = electronicServiceApplicantContactData.PhoneNumbers.PhoneNumberCollection[0];
                        }

                        if (electronicServiceApplicantContactData.FaxNumbers != null &&
                            electronicServiceApplicantContactData.FaxNumbers.ElectronicServiceApplicantFaxNumberCollection != null &&
                            electronicServiceApplicantContactData.FaxNumbers.ElectronicServiceApplicantFaxNumberCollection.Count > 0)
                        {
                            correspondent.ContactFax = electronicServiceApplicantContactData.FaxNumbers.ElectronicServiceApplicantFaxNumberCollection[0];
                        }
                    }

                    if (isNew)
                    {
                        correspondent.RegisterIndexId = 1;
                        correspondent.Email = email;
                        correspondent.IsActive = true;
                    }

                    returnValue.Add(correspondent);
                }
            }

            return returnValue;
        }

        private ReceiptAcknowledgedMessage CreateReceiptAcknowledgedMessage(
            string registerIndex,
            string sequenceNumber,
            DateTime receiptOrSigningDate,
            string aisUserIdentifier,
            string aisURI,
            string caseAccessIdentifier,
            ApplicationDataDo applicationDataDo)
        {
            var receiptMessage = new ReceiptAcknowledgedMessage();
            receiptMessage.DocumentURI = new DocumentURI();
            receiptMessage.DocumentURI.RegisterIndex = registerIndex;
            receiptMessage.DocumentURI.SequenceNumber = sequenceNumber;
            receiptMessage.DocumentURI.ReceiptOrSigningDate = receiptOrSigningDate;
            receiptMessage.Applicant = applicationDataDo.ElectronicServiceApplicant;
            receiptMessage.ElectronicServiceProvider = applicationDataDo.ElectronicServiceProviderBasicData;
            receiptMessage.TransportType = "0006-000001";   //Чрез уеб базирано приложение;
            receiptMessage.DocumentTypeURI = applicationDataDo.DocumentTypeURI;
            receiptMessage.DocumentTypeName = applicationDataDo.DocumentTypeName;
            receiptMessage.RegisteredBy = new RegisteredBy();
            receiptMessage.RegisteredBy.Officer = new Officer();
            receiptMessage.RegisteredBy.Officer.AISUserIdentifier = aisUserIdentifier;
            receiptMessage.RegisteredBy.AISURI = aisURI;
            receiptMessage.CaseAccessIdentifier = caseAccessIdentifier;

            return receiptMessage;
        }

        private ReceiptNotAcknowledgedMessage CreateReceiptNotAcknowledgedMessage(
            string registerIndex,
            string sequenceNumber,
            DateTime receiptOrSigningDate,
            ApplicationDataDo applicationDataDo,
            List<ElectronicDocumentDiscrepancyTypeNomenclature> discrepancies)
        {
            var receiptMessage = new ReceiptNotAcknowledgedMessage();
            receiptMessage.MessageURI = new DocumentURI();
            receiptMessage.MessageURI.RegisterIndex = registerIndex;
            receiptMessage.MessageURI.SequenceNumber = sequenceNumber;
            receiptMessage.MessageURI.ReceiptOrSigningDate = receiptOrSigningDate;
            receiptMessage.Applicant = applicationDataDo.ElectronicServiceApplicant;
            receiptMessage.ElectronicServiceProvider = applicationDataDo.ElectronicServiceProviderBasicData;
            receiptMessage.TransportType = "0006-000001";   //Чрез уеб базирано приложение;
            receiptMessage.DocumentTypeURI = applicationDataDo.DocumentTypeURI;
            receiptMessage.DocumentTypeName = applicationDataDo.DocumentTypeName;
            receiptMessage.MessageCreationTime = receiptOrSigningDate;
            receiptMessage.Discrepancies = new Discrepancies();
            receiptMessage.Discrepancies.DiscrepancyCollection = new DiscrepancyCollection();
            foreach (var discrepancy in discrepancies)
            {
                receiptMessage.Discrepancies.DiscrepancyCollection.Add(discrepancy.Uri);
            }

            return receiptMessage;
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

        private List<DocClassification> AddDocClassification(Doc doc, User systemUser)
        {
            var docTypeClassifications = this.unitOfWork.DbContext.Set<DocTypeClassification>()
                .Where(e => e.DocTypeId == doc.DocTypeId.Value && e.DocDirectionId == doc.DocDirectionId)
                .ToList();

            List<DocClassification> docClassifications = new List<DocClassification>();

            foreach (var docTypeClassification in docTypeClassifications)
            {
                var docClassification = new DocClassification();
                docClassification.DocId = doc.DocId;
                docClassification.ClassificationId = docTypeClassification.ClassificationId;
                docClassification.ClassificationByUserId = systemUser.UserId;
                docClassification.ClassificationDate = DateTime.Now;
                docClassification.IsInherited = docTypeClassification.IsInherited;
                docClassification.IsActive = true;

                docClassifications.Add(docClassification);

                this.unitOfWork.DbContext.Set<DocClassification>().Add(docClassification);
            }

            return docClassifications;
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

        private Doc CreateReceiptDoc(bool isDocAcknowledged, User user)
        {
            Doc doc = new Doc();
            doc.DocDirectionId = this.unitOfWork.DbContext.Set<DocDirection>().Single(e => e.Alias == "Outgoing").DocDirectionId;
            doc.DocEntryTypeId = this.unitOfWork.DbContext.Set<DocEntryType>().Single(e => e.Alias == "Document").DocEntryTypeId;

            if (!isDocAcknowledged)
            {
                doc.DocSubject = "Съобщение, че получаването не се потвърждава";
                doc.DocTypeId = this.unitOfWork.DbContext.Set<DocType>().Single(e => e.Alias == "ReceiptNotAcknowledgedMessage").DocTypeId;
            }
            else
            {
                doc.DocSubject = "Потвърждаване за получаване";
                doc.DocTypeId = this.unitOfWork.DbContext.Set<DocType>().Single(e => e.Alias == "ReceiptAcknowledgedMessage").DocTypeId;
            }

            doc.DocBody = String.Empty;
            doc.DocSourceTypeId = null;
            doc.DocStatusId = this.unitOfWork.DbContext.Set<DocStatus>().Single(e => e.Alias == "Processed").DocStatusId;
            doc.DocDestinationTypeId = null;
            doc.DocFormatTypeId = this.unitOfWork.DbContext.Set<DocFormatType>().Single(e => e.Alias == "Electronic").DocFormatTypeId;
            doc.DocCasePartTypeId = this.unitOfWork.DbContext.Set<DocCasePartType>().Single(e => e.Alias == "Public").DocCasePartTypeId;
            doc.CorrRegNumber = null;
            doc.CorrRegDate = null;
            doc.AccessCode = GenerateAccessCode();
            doc.AssignmentTypeId = null;
            doc.AssignmentDate = null;
            doc.AssignmentDeadline = null;
            doc.IsCase = false;
            doc.IsSigned = false;
            doc.ModifyDate = null;
            doc.ModifyUserId = null;
            doc.IsActive = true;

            DocCasePartMovement dcpm = new DocCasePartMovement();
            dcpm.Doc = doc;
            dcpm.DocCasePartTypeId = doc.DocCasePartTypeId.Value;
            dcpm.UserId = user.UserId;
            dcpm.MovementDate = DateTime.Now;

            doc.DocCasePartMovements.Add(dcpm);

            return doc;
        }

        private DocRelation CreateReceiptDocRelation(Doc initialDoc, Doc receiptDoc, Doc rootDoc)
        {
            DocRelation docRelation = new DocRelation();
            docRelation.DocId = receiptDoc.DocId;
            docRelation.ParentDocId = initialDoc.DocId;
            docRelation.RootDocId = rootDoc != null ? rootDoc.DocId : initialDoc.DocId;

            return docRelation;
        }

        private Guid CreateReceiptDocFileContent(Doc initialDoc, Doc receiptDoc, Doc rootDoc, List<ElectronicDocumentDiscrepancyTypeNomenclature> discrepancies, ApplicationDataDo applicationDataDo)
        {
            bool isDocAcknowledged = discrepancies == null || discrepancies.Count() == 0;

            string registerIndex;
            string sequenceNumber;
            DateTime receiptOrSigningDate;

            string receiptMessage;
            if (!isDocAcknowledged)
            {
                registerIndex = receiptDoc.RegIndex;
                sequenceNumber = receiptDoc.RegNumber.Value.ToString("D6");
                receiptOrSigningDate = receiptDoc.RegDate.Value;

                receiptMessage = this.rioDocumentParser.XmlSerializeReceiptNotAcknowledgedMessage(
                    this.CreateReceiptNotAcknowledgedMessage(registerIndex, sequenceNumber, receiptOrSigningDate, applicationDataDo, discrepancies));
            }
            else
            {
                registerIndex = initialDoc.RegIndex;
                sequenceNumber = initialDoc.RegNumber.Value.ToString("D6");
                receiptOrSigningDate = initialDoc.RegDate.Value;

                string aisUserIdentifier = "Системен потребител";
                string aisURI = "ГВА АИС";

                string htmlFormat = @"<p>Номер на преписка: <b>{0}</b><br/>Код за достъп: <b>{1}</b><br/></p>";
                string regUri = rootDoc != null ? rootDoc.RegUri : initialDoc.RegUri;
                string accessCode = rootDoc != null ? rootDoc.AccessCode : initialDoc.AccessCode;
                string caseAccessIdentifier = String.Format(htmlFormat, regUri, accessCode);

                receiptMessage = this.rioDocumentParser.XmlSerializeReceiptAcknowledgedMessage(
                    this.CreateReceiptAcknowledgedMessage(registerIndex, sequenceNumber, receiptOrSigningDate, aisUserIdentifier, aisURI, caseAccessIdentifier, applicationDataDo));
            }

            byte[] content = Utf8Utils.GetBytes(receiptMessage);
            Guid fileKey = WriteToBlob(content);

            return fileKey;
        }

        private DocFile CreateReceiptDocFile(int docFileKindId, Doc receiptDoc, Guid fileKey, bool isDocAcknowledged)
        {
            string docFileTypeAlias = isDocAcknowledged ? "ReceiptAcknowledgedMessage" : "ReceiptNotAcknowledgedMessage";

            DocFile docFile = new DocFile();
            docFile.Doc = receiptDoc;
            docFile.DocContentStorage = String.Empty;
            docFile.DocFileContentId = fileKey;
            docFile.DocFileTypeId = this.unitOfWork.DbContext.Set<DocFileType>().Single(e => e.Alias == docFileTypeAlias).DocFileTypeId;
            docFile.DocFileKindId = docFileKindId;
            docFile.Name = isDocAcknowledged ? "Съобщение, че получаването се потвърждава" : "Съобщение, че получаването не се потвърждава";
            docFile.DocFileName = isDocAcknowledged ? "ReceiptAcknowledgedMessage.xml" : "ReceiptNotAcknowledgedMessage.xml";
            docFile.DocFileOriginTypeId = this.unitOfWork.DbContext.Set<DocFileOriginType>().Single(e => e.Alias == "EApplication").DocFileOriginTypeId;
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

        private void AddReceiveConfirmationEmailRecord(bool isDocAcknowledged, User systemUser, List<Correspondent> docCorrespondents, Doc caseDoc, List<ElectronicDocumentDiscrepancyTypeNomenclature> discrepancies)
        {
            EmailStatus pendingStatus = this.emailRepository.GetEmailStatusByAlias("Pending");

            EmailAddresseeType to = this.emailRepository.GetEmailAddresseeTypeByAlias("To");

            Email email;
            if (isDocAcknowledged)
            {
                EmailType emailType = this.emailRepository.GetEmailTypeByAlias("ReceiptAcknowledgedEmail");

                email = this.emailRepository.CreateEmail(
                    emailType.EmailTypeId,
                    pendingStatus.EmailStatusId,
                    emailType.Subject,
                    emailType.Body
                        .Replace("@@CaseNum", !string.IsNullOrEmpty(caseDoc.RegUri) ? caseDoc.RegUri : "[Няма номер на преписка]")
                        .Replace("@@CaseViewUrl", EmailRepository.CaseViewUrl)
                        .Replace("@@AccessCode", caseDoc.AccessCode));
            }
            else
            {
                EmailType emailType = this.emailRepository.GetEmailTypeByAlias("ReceiptNotAcknowledgedEmail");

                email = this.emailRepository.CreateEmail(
                    emailType.EmailTypeId,
                    pendingStatus.EmailStatusId,
                    emailType.Subject,
                    emailType.Body
                        .Replace("@@Discrepancies", discrepancies.Select(d => d.Text).Aggregate((l,r) => l + ", " + r)));
            }

            if (!string.IsNullOrEmpty(systemUser.Email) && Regex.IsMatch(systemUser.Email, Docs.Api.EmailSender.EmailSender.emailRegex))
            {
                email.EmailAddressees.Add(new EmailAddressee()
                {
                    EmailAddresseeTypeId = to.EmailAddresseeTypeId, // TODO emailAddressType - from ????
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
                            Address = item.Email
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
