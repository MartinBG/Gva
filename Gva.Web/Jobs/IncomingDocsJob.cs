﻿using Common.Api.Models;
using Common.Data;
using Docs.Api.Models;
using Gva.Portal.Components.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Data.Entity;
using Gva.Web.Abbcdn;
using System.Reflection;
using Gva.Portal.RioObjects;
using R_0009_000016;
using Gva.Web.Jobs.Helpers;
using Docs.Api.Repositories.CorrespondentRepository;
using Docs.Api.Repositories.DocRepository;
using System.Text;
using Common.Api.UserContext;
using Common.Utils;
using Gva.Portal.RioObjects.Enums;
using Gva.Portal.Components.DocumentSerializer;
using System.Web.Http.Filters;
using NLog;
using Common.Blob;
using System.ServiceModel;
using Common.Extensions;
using System.Data.SqlClient;
using System.Configuration;
using Autofac.Features.OwnedInstances;
using Common.Tests;

namespace Gva.Web.Jobs
{
    public class IncomingDocsJob : IJob
    {
        private readonly Timer timer;
        private readonly JobHost jobHost;

        private Func<Owned<DisposableTuple<IUnitOfWork, IDocRepository, ICorrespondentRepository>>> dependencyFactory;

        private IUnitOfWork unitOfWork;
        private IDocRepository docRepository;
        private ICorrespondentRepository correspondentRepository;

        private IDocumentSerializer documentSerializer;
        private AbbcdnStorage abbcdnStorage;

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        public IncomingDocsJob(Func<Owned<DisposableTuple<IUnitOfWork, IDocRepository, ICorrespondentRepository>>> dependencyFactory)
        {
            this.dependencyFactory = dependencyFactory;

            this.documentSerializer = new DocumentSerializerImpl();
            this.abbcdnStorage = new AbbcdnStorage(new ChannelFactory<IAbbcdn>("WSHttpBinding_IAbbcdn"));

            this.timer = new Timer(this.OnTimerElapsed);
            this.jobHost = new JobHost();
        }

        private void OnTimerElapsed(object sender)
        {
            this.jobHost.DoAction(() =>
            {
                if (this.jobHost.IsShuttingDown)
                    return;

                logger.Info("IncomingDocumentsJob Started.");

                ProcessPendingDocs();

                logger.Info("IncomingDocumentsJob Finished.");
            });
        }

        public void Start()
        {
            logger.Info("IncomingDocumentsJob Initializing.");

            this.timer.Change(TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(int.Parse(System.Configuration.ConfigurationManager.AppSettings["IncomingDocsJobIntervalInSeconds"])));
        }

        public void Dispose()
        {
            this.timer.Dispose();

            logger.Info("IncomingDocumentsJob Disposed.");
        }

        private void ProcessPendingDocs()
        {
            try
            {
                List<int> pendingIncomingDocs = null;


                using (var dependencies = dependencyFactory())
                {
                    this.unitOfWork = dependencies.Value.Item1;

                    pendingIncomingDocs = this.unitOfWork.DbContext.Set<IncomingDoc>()
                        .Where(e => e.IncomingDocStatus.Alias == "Pending")
                        .Select(d => d.IncomingDocId)
                        .ToList();
                }

                foreach (int incomingDocId in pendingIncomingDocs)
                {
                    try
                    {
                        using (var dependencies = dependencyFactory())
                        {
                            this.unitOfWork = dependencies.Value.Item1;
                            this.docRepository = dependencies.Value.Item2;
                            this.correspondentRepository = dependencies.Value.Item3;

                            using (var transaction = this.unitOfWork.BeginTransaction())
                            {
                                //Init user variables
                                User systemUser = this.unitOfWork.DbContext.Set<User>().Single(e => e.Username == "system");
                                UnitUser systemUnitUser = this.unitOfWork.DbContext.Set<UnitUser>().Include(e => e.Unit).Single(e => e.UserId == systemUser.UserId);
                                UserContext systemUserContext = new UserContext(systemUser.UserId);


                                IncomingDoc incomingDoc = this.unitOfWork.DbContext.Set<IncomingDoc>()
                                    .Include(e => e.IncomingDocFiles)
                                    .Where(e => e.IncomingDocId == incomingDocId)
                                    .Single();

                                IncomingDocFile incomingDocFile = incomingDoc.IncomingDocFiles.First();

                                RioServiceHelper rioService = new RioServiceHelper(incomingDocFile.DocFileContent);
                                MarkAttachedFilesAsUsed(rioService);

                                var discrepancies = rioService.ValidateServiceData(GetRioObjectsSchemasPath(), rioService.XmlContent, false);
                                bool isDocAcknowledged = discrepancies.Count == 0;

                                incomingDoc.IncomingDocStatusId = this.unitOfWork.DbContext.Set<IncomingDocStatus>()
                                    .Where(e => e.Alias == (isDocAcknowledged ? "Registered" : "NotRegistered"))
                                    .Single()
                                    .IncomingDocStatusId;

                                DocIdentifierHelper docIdentifier = GetDocIdentifier(rioService);

                                foreach (var correspondent in docIdentifier.DocCorrespondents.Where(c => c.CorrespondentId == 0))
                                {
                                    this.unitOfWork.DbContext.Set<Correspondent>().Add(correspondent);
                                    this.unitOfWork.Save();
                                }

                                var validationErrors = rioService.GetValidationErrors(docIdentifier.DocFileType.DocTypeUri);

                                Doc rootDoc = null;
                                if (docIdentifier.ServiceHeader.DocumentURI != null)
                                {
                                    string regIndex = docIdentifier.ServiceHeader.DocumentURI.RegisterIndex.PadLeft(4, '0');
                                    int regNumber = int.Parse(docIdentifier.ServiceHeader.DocumentURI.SequenceNumber);
                                    DateTime regdDate = docIdentifier.ServiceHeader.DocumentURI.ReceiptOrSigningDate.Value;

                                    rootDoc = this.docRepository.GetDocByRegUri(regIndex, regNumber, regdDate);
                                }

                                //Initial Doc
                                Doc initialDoc = CreateInitialDoc(rioService, validationErrors, docIdentifier.DocType.DocTypeId, rootDoc == null, systemUser);
                                this.unitOfWork.DbContext.Set<Doc>().Add(initialDoc);

                                foreach (var correspondent in docIdentifier.DocCorrespondents)
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

                                AddDocClassification(initialDoc, systemUser);

                                this.unitOfWork.Save();

                                this.docRepository.spSetDocUsers(initialDoc.DocId);

                                Guid fileKey = WriteToBlob(Utf8Utils.GetBytes(incomingDocFile.DocFileContent));

                                DocFileKind publicDocFileKind = this.unitOfWork.DbContext.Set<DocFileKind>().Single(e => e.Alias == "PublicAttachedFile");

                                DocFile initialDocFile = CreateInitialDocFile(initialDoc, fileKey, docIdentifier.DocFileType.DocFileTypeId, publicDocFileKind.DocFileKindId, rioService.ServiceHeader.ApplicationSigningTime);
                                this.unitOfWork.DbContext.Set<DocFile>().Add(initialDocFile);

                                //Add attached application files as DocFiles
                                foreach(var attachedAppFile in rioService.AttachedDocuments)
                                {
                                    byte[] fileContent = null;
                                    if (attachedAppFile.UseAbbcdn)
                                    {
                                        fileContent = abbcdnStorage.DownloadFile(Guid.Parse(attachedAppFile.UniqueIdentifier)).ContentBytes;
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

                                this.unitOfWork.Save();

                                this.docRepository.spSetDocUsers(receiptDoc.DocId);

                                Guid receiptFileKey = CreateReceiptDocFileContent(initialDoc, receiptDoc, docIdentifier, rootDoc, discrepancies);

                                DocFile receiptDocFile = CreateReceiptDocFile(publicDocFileKind.DocFileKindId, receiptDoc, receiptFileKey, isDocAcknowledged);
                                this.unitOfWork.DbContext.Set<DocFile>().Add(receiptDocFile);

                                if (docIdentifier.ServiceHeader.SendApplicationWithReceiptAcknowledgedMessage)
                                {
                                    AddReceiveConfirmationEmailRecord(isDocAcknowledged, systemUser, docIdentifier);
                                }

                                this.unitOfWork.Save();

                                AddBeginningServiceStage(docIdentifier, initialDoc);

                                AddCheckRegularityServiceStage(docIdentifier, initialDoc);

                                this.unitOfWork.Save();

                                transaction.Commit();
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        using (var dependencies = dependencyFactory())
                        {
                            this.unitOfWork = dependencies.Value.Item1;

                            var incomingDoc = this.unitOfWork.DbContext.Set<IncomingDoc>().SingleOrDefault(e => e.IncomingDocId == incomingDocId);
                            incomingDoc.IncomingDocStatusId = this.unitOfWork.DbContext.Set<IncomingDocStatus>().Single(e => e.Alias == "Incorrect").IncomingDocStatusId;

                            this.unitOfWork.Save();
                        }

                        logger.Error("General error", ex);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.Error("General error", ex);
            }
        }

        private void MarkAttachedFilesAsUsed(RioServiceHelper rioService)
        {
            foreach (var attachedDoc in rioService.AttachedDocuments)
            {
                if (attachedDoc.UseAbbcdn)
                {
                    abbcdnStorage.SetIsInUse(Guid.Parse(attachedDoc.AbbcdnInfo.AttachedDocumentUniqueIdentifier), true);
                }
            }
        }

        private DocIdentifierHelper GetDocIdentifier(RioServiceHelper rioService)
        {
            DocIdentifierHelper docIdentifier = new DocIdentifierHelper();

            docIdentifier.ServiceHeader = rioService.ServiceHeader;
            docIdentifier.DocFileType = this.unitOfWork.DbContext.Set<DocFileType>()
                .SingleOrDefault(e => e.Alias == rioService.DocFileTypeAlias);
            docIdentifier.ElectronicServiceFileTypeUri = String.Format("{0}-{1}", docIdentifier.ServiceHeader.DocumentTypeURI.RegisterIndex, docIdentifier.ServiceHeader.DocumentTypeURI.BatchNumber);
            docIdentifier.DocType = this.unitOfWork.DbContext.Set<DocType>()
                .SingleOrDefault(e => e.ElectronicServiceFileTypeUri == docIdentifier.ElectronicServiceFileTypeUri);
            docIdentifier.DocServiceApplicant = docIdentifier.ServiceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant;
            docIdentifier.DocCorrespondents = GetDocumentCorrespondents(docIdentifier.ServiceHeader);

            return docIdentifier;
        }

        private List<Correspondent> GetDocumentCorrespondents(IHeaderFooterDocumentsRioApplication serviceHeader)
        {
            List<Correspondent> returnValue = new List<Correspondent>();

            if (serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection.Count > 0)
            {
                foreach (var recipient in serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection)
                {
                    Correspondent correspondent = null;

                    bool isNew = false;

                    string email = serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant.EmailAddress ?? "";

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
                        string foreignerSettlement = recipient.ForeignPerson.PlaceOfBirth.SettlementName;
                        DateTime? foreignerBirthDate = recipient.ForeignPerson.BirthDate;
                        string foreignerCountryCode = recipient.ForeignPerson.PlaceOfBirth.CountryCode;
                        int? foreignerCountryId = null;

                        if (!String.IsNullOrWhiteSpace(foreignerCountryCode))
                        {
                            var country = this.unitOfWork.DbContext.Set<Country>()
                                .SingleOrDefault(e => e.Alpha2Code == foreignerCountryCode);
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
                                .SingleOrDefault(e => e.Alpha2Code == fLegalEntityCountryCode);
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

                    if (serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData != null)
                    {
                        int? contactDistrictId = null;
                        int? contactMunicipalityId = null;
                        int? contactSettlementId = null;

                        if (!String.IsNullOrWhiteSpace(serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.DistrictCode))
                        {
                            var district = this.unitOfWork.DbContext.Set<District>()
                                .SingleOrDefault(e => e.Code == serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.DistrictCode);
                            contactDistrictId = district != null ? district.DistrictId : (int?)null;
                        }

                        if (!String.IsNullOrWhiteSpace(serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.MunicipalityCode))
                        {
                            var municipality = this.unitOfWork.DbContext.Set<Municipality>()
                                .SingleOrDefault(e => e.Code == serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.MunicipalityCode);
                            contactMunicipalityId = municipality != null ? municipality.MunicipalityId : (int?)null;
                        }

                        if (!String.IsNullOrWhiteSpace(serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.SettlementCode))
                        {
                            var settlement = this.unitOfWork.DbContext.Set<Settlement>()
                                .SingleOrDefault(e => e.Code == serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.SettlementCode);
                            contactSettlementId = settlement != null ? settlement.SettlementId : (int?)null;
                        }

                        correspondent.ContactDistrictId = contactDistrictId;
                        correspondent.ContactMunicipalityId = contactMunicipalityId;
                        correspondent.ContactSettlementId = contactSettlementId;
                        correspondent.ContactAddress = serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.AddressDescription;
                        correspondent.ContactPostCode = serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.PostCode;
                        correspondent.ContactPostOfficeBox = serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.PostOfficeBox;

                        if (serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.PhoneNumbers != null &&
                            serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.PhoneNumbers.PhoneNumberCollection != null &&
                            serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.PhoneNumbers.PhoneNumberCollection.Count > 0)
                        {
                            correspondent.ContactPhone = serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.PhoneNumbers.PhoneNumberCollection[0];
                        }

                        if (serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.FaxNumbers != null &&
                            serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.FaxNumbers.ElectronicServiceApplicantFaxNumberCollection != null &&
                            serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.FaxNumbers.ElectronicServiceApplicantFaxNumberCollection.Count > 0)
                        {
                            correspondent.ContactFax = serviceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicantContactData.FaxNumbers.ElectronicServiceApplicantFaxNumberCollection[0];
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

        private Doc CreateInitialDoc(RioServiceHelper rioService, List<string> validationErrors, int docTypeId, bool isCase, User user)
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

        private DocElectronicServiceStage AddBeginningServiceStage(DocIdentifierHelper docIdentifier, Doc doc)
        {
            DocElectronicServiceStage serviceStage = null;

            var electronicServiceStage = this.unitOfWork.DbContext.Set<ElectronicServiceStage>()
                .SingleOrDefault(e => e.Alias == "AcceptApplication" && e.DocTypeId == docIdentifier.DocType.DocTypeId);

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
            doc.DocStatusId = this.unitOfWork.DbContext.Set<DocStatus>().Single(e => e.Alias == "Prepared").DocStatusId;
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

        private Guid CreateReceiptDocFileContent(Doc initialDoc, Doc receiptDoc, DocIdentifierHelper docIdentifier, Doc rootDoc, List<ElectronicDocumentDiscrepancyTypeNomenclature> discrepancies)
        {
            bool isDocAcknowledged = discrepancies == null || discrepancies.Count() == 0;

            byte[] content = new byte[0];

            if (!isDocAcknowledged)
            {
                var receiptMessage = new R_0009_000017.ReceiptNotAcknowledgedMessage();
                receiptMessage.MessageURI = new R_0009_000001.DocumentURI();
                receiptMessage.MessageURI.RegisterIndex = receiptDoc.RegIndex;
                receiptMessage.MessageURI.SequenceNumber = receiptDoc.RegNumber.Value.ToString("D6");
                receiptMessage.MessageURI.ReceiptOrSigningDate = receiptDoc.RegDate.Value;
                receiptMessage.Applicant = docIdentifier.ServiceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant;
                receiptMessage.ElectronicServiceProvider = docIdentifier.ServiceHeader.ElectronicServiceProviderBasicData;
                receiptMessage.TransportType = "0006-000001";   //Чрез уеб базирано приложение;
                receiptMessage.DocumentTypeURI = docIdentifier.ServiceHeader.DocumentTypeURI;
                receiptMessage.DocumentTypeName = docIdentifier.ServiceHeader.DocumentTypeName;
                receiptMessage.MessageCreationTime = receiptDoc.RegDate;
                receiptMessage.Discrepancies = new R_0009_000017.Discrepancies();
                receiptMessage.Discrepancies.DiscrepancyCollection = new R_0009_000017.DiscrepancyCollection();
                foreach (var discrepancy in discrepancies)
                {
                    receiptMessage.Discrepancies.DiscrepancyCollection.Add(discrepancy.Uri);
                }

                content = Utf8Utils.GetBytes(documentSerializer.XmlSerializeObjectToString(receiptMessage));
            }
            else
            {
                string htmlFormat = @"<p>Номер на преписка: <b>{0}</b><br/>Код за достъп: <b>{1}</b><br/></p>";

                var receiptMessage = new R_0009_000019.ReceiptAcknowledgedMessage();
                receiptMessage.DocumentTypeURI = new R_0009_000003.DocumentTypeURI();
                receiptMessage.DocumentURI = new R_0009_000001.DocumentURI();
                receiptMessage.DocumentURI.RegisterIndex = initialDoc.RegIndex;
                receiptMessage.DocumentURI.SequenceNumber = initialDoc.RegNumber.Value.ToString("D6");
                receiptMessage.DocumentURI.ReceiptOrSigningDate = initialDoc.RegDate.Value;
                receiptMessage.Applicant = docIdentifier.ServiceHeader.ElectronicAdministrativeServiceHeader.ElectronicServiceApplicant;
                receiptMessage.ElectronicServiceProvider = docIdentifier.ServiceHeader.ElectronicServiceProviderBasicData;
                receiptMessage.TransportType = "0006-000001";   //Чрез уеб базирано приложение;
                receiptMessage.DocumentTypeURI = docIdentifier.ServiceHeader.DocumentTypeURI;
                receiptMessage.DocumentTypeName = docIdentifier.ServiceHeader.DocumentTypeName;
                receiptMessage.RegisteredBy = new R_0009_000019.RegisteredBy();
                receiptMessage.RegisteredBy.Officer = new R_0009_000019.Officer();
                receiptMessage.RegisteredBy.Officer.AISUserIdentifier = "Системен потребител";
                receiptMessage.RegisteredBy.AISURI = "ГВА АИС";
                receiptMessage.CaseAccessIdentifier = String.Format(htmlFormat, rootDoc != null ? rootDoc.RegUri : initialDoc.RegUri, rootDoc != null ? rootDoc.AccessCode : initialDoc.AccessCode);

                content = Utf8Utils.GetBytes(documentSerializer.XmlSerializeObjectToString(receiptMessage));
            }

            Guid fileKey = WriteToBlob(content);

            return fileKey;
        }

        private DocFile CreateReceiptDocFile(int docFileKindId, Doc doc, Guid fileKey, bool isDocAcknowledged)
        {
            DocFile docFile = new DocFile();
            docFile.Doc = doc;
            docFile.DocContentStorage = String.Empty;
            docFile.DocFileContentId = fileKey;
            docFile.DocFileTypeId = this.unitOfWork.DbContext.Set<DocFileType>().Single(e => e.Alias == "XML").DocFileTypeId;
            docFile.DocFileKindId = docFileKindId;
            docFile.Name = isDocAcknowledged ? "Съобщение, че получаването се потвърждава" : "Съобщение, че получаването не се потвърждава";
            docFile.DocFileName = isDocAcknowledged ? "ReceiptAcknowledgedMessage.xml" : "ReceiptNotAcknowledgedMessage.xml";
            docFile.DocFileOriginTypeId = this.unitOfWork.DbContext.Set<DocFileOriginType>().Single(e => e.Alias == "EApplication").DocFileOriginTypeId;
            docFile.IsPrimary = true;
            docFile.IsSigned = true;
            docFile.IsActive = true;

            return docFile;
        }

        private DocFile CreateEApplicationAttachDocFile(RioServiceHelper.AttachedDocument attachedDocument, Doc doc, int docFileKindId, Guid fileKey)
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

        private void AddReceiveConfirmationEmailRecord(bool isDocAcknowledged, User systemUser, DocIdentifierHelper docIdentifier)
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
            if (docIdentifier.DocCorrespondents.Count > 0)
            {
                email.CorrespondentId = docIdentifier.DocCorrespondents[0].CorrespondentId;
            }
            email.StatusId = emailStatus.AdministrativeEmailStatusId;
            email.Subject = emailType.Subject;
            email.Body = emailType.Body;

            this.unitOfWork.DbContext.Set<AdministrativeEmail>().Add(email);
        }

        private void AddCheckRegularityServiceStage(DocIdentifierHelper docIdentifier, Doc doc)
        {
            var electronicServiceStage = this.unitOfWork.DbContext.Set<ElectronicServiceStage>().SingleOrDefault(e => e.Alias == "CheckRegularity" && e.DocTypeId == docIdentifier.DocType.DocTypeId);

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

        private string GetRioObjectsSchemasPath()
        {
            string assemblyPath = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
            string binPath = System.IO.Path.GetDirectoryName(assemblyPath);
            string projectPath = binPath.Substring(0, binPath.Length - 4);
            string schemasPath = String.Format(@"{0}\RioSchemas", projectPath);

            return schemasPath;
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
    }
}