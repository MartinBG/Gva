using Common.Utils;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Rio.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rio.Data.Utils
{
    public static class RioObjectUtils
    {
        public static Encoding DefaultEncoding
        {
            get
            {
                return Encoding.UTF8;
            }
        }

        public static byte[] GetBytesFromJObject(string docTypeUri, JObject jObject)
        {
            try
            {
                string contentStr = JsonConvert.SerializeObject(jObject);

                var rioObj = Newtonsoft.Json.JsonConvert.DeserializeObject(contentStr, GetTypeByDocTypeUri(docTypeUri));

                return XmlSerializerUtils.XmlSerializeObjectToBytes(rioObj);
            }
            catch
            {
                return null;
            }
        }

        public static JObject GetJObjectFromBytes(string docTypeUri, byte[] content)
        {
            try
            {
                var rioObj = XmlSerializerUtils.XmlDeserializeFromBytes(GetTypeByDocTypeUri(docTypeUri), content);

                string contentToString = Newtonsoft.Json.JsonConvert.SerializeObject(rioObj);

                return Newtonsoft.Json.JsonConvert.DeserializeObject<JObject>(contentToString);
            }
            catch
            {
                return null;
            }
        }

        private static Type GetTypeByDocTypeUri(string docTypeUri)
        {
            RioDocumentMetadata rioDocumentMetadata = RioDocumentMetadata.GetMetadataByDocumentTypeURI(docTypeUri);

            return rioDocumentMetadata.RioObjectType;
        }

        public static byte[] CreateEmptyRioObject(string docTypeUri)
        {
            try
            {
                var rioObj = Activator.CreateInstance(GetTypeByDocTypeUri(docTypeUri));

                return XmlSerializerUtils.XmlSerializeObjectToBytes(rioObj);
            }
            catch
            {
                return null;
            }
        }

        public static byte[] CreateR6090DecisionGrantAccessPublicInformation(
            DateTime? caseUriDate = null,
            string caseUriRegisterIndex = null,
            string caseUriSequenceNumber = null,
            string degreeAccessRequestedPublicInformation = null,
            string descriptionProvidedPublicInformation = null,
            string documentTypeName = null,
            string documentTypeRegisterIndex = null,
            string documentTypeBatchNumber = null,
            DateTime? documentUriDate = null,
            string documentUriRegisterIndex = null,
            string documentUriSequenceNumber = null,
            string electronicServiceProviderId = null,
            string electronicServiceProviderName = null,
            string electronicServiceProviderBulstat = null,
            string formProvidingAccessPublicInformation = null,
            string informationPaymentCostsProvidingAccess = null,
            string legalBasisIssuanceAdministrativeAct = null,
            string bgEmployFirstName = null,
            string bgEmploySecondName = null,
            string bgEmployLastName = null,
            string foreignEmployFirstName = null,
            string foreignEmployLastName = null,
            string foreignEmployOtherInfo = null,
            string employQuality = null,
            string otherBodiesOrganizations = null,
            string periodAccessPublicInformation = null,
            string placePublicInformationAccessGiven = null,
            List<Tuple<string, string>> rejectsReasonAndPublicInfoCollection = null)
        {
            try
            {
                R_6090.DecisionGrantAccessPublicInformation rioObj = new R_6090.DecisionGrantAccessPublicInformation();

                rioObj.AISCaseURI = new R_0009_000073.AISCaseURI();
                rioObj.AISCaseURI.DocumentURI = new R_0009_000001.DocumentURI();
                rioObj.AISCaseURI.DocumentURI.ReceiptOrSigningDate = caseUriDate;
                rioObj.AISCaseURI.DocumentURI.RegisterIndex = caseUriRegisterIndex;
                rioObj.AISCaseURI.DocumentURI.SequenceNumber = caseUriSequenceNumber;

                rioObj.DegreeAccessRequestedPublicInformation = degreeAccessRequestedPublicInformation;

                rioObj.DescriptionProvidedPublicInformation = descriptionProvidedPublicInformation;

                rioObj.DocumentTypeName = documentTypeName;
                rioObj.DocumentTypeURI = new R_0009_000003.DocumentTypeURI();
                rioObj.DocumentTypeURI.BatchNumber = documentTypeBatchNumber;
                rioObj.DocumentTypeURI.RegisterIndex = documentTypeRegisterIndex;

                rioObj.DocumentURI = new R_0009_000001.DocumentURI();
                rioObj.DocumentURI = new R_0009_000001.DocumentURI();
                rioObj.DocumentURI.ReceiptOrSigningDate = documentUriDate;
                rioObj.DocumentURI.RegisterIndex = documentUriRegisterIndex;
                rioObj.DocumentURI.SequenceNumber = documentUriSequenceNumber;

                rioObj.ElectronicServiceProviderBasicData = new R_0009_000002.ElectronicServiceProviderBasicData();
                rioObj.ElectronicServiceProviderBasicData.ElectronicServiceProviderType = electronicServiceProviderId;
                rioObj.ElectronicServiceProviderBasicData.EntityBasicData = new R_0009_000013.EntityBasicData();
                rioObj.ElectronicServiceProviderBasicData.EntityBasicData.Identifier = electronicServiceProviderBulstat;
                rioObj.ElectronicServiceProviderBasicData.EntityBasicData.Name = electronicServiceProviderName;

                rioObj.FormProvidingAccessPublicInformation = formProvidingAccessPublicInformation;
                
                rioObj.InformationPaymentCostsProvidingAccess = informationPaymentCostsProvidingAccess;
                rioObj.LegalBasisIssuanceAdministrativeAct = legalBasisIssuanceAdministrativeAct;

                rioObj.OfficialCollection = new R_6090.DecisionGrantAccessPublicInformationOfficialCollection();

                var t = new R_6090.DecisionGrantAccessPublicInformationOfficial();
                rioObj.OfficialCollection.Add(new R_6090.DecisionGrantAccessPublicInformationOfficial());
                rioObj.OfficialCollection[0].ElectronicDocumentAuthorQuality = employQuality;
                if (!String.IsNullOrWhiteSpace(bgEmployFirstName) || !String.IsNullOrWhiteSpace(bgEmploySecondName) || !String.IsNullOrWhiteSpace(bgEmployLastName))
                {
                    rioObj.OfficialCollection[0].PersonNames = new R_0009_000005.PersonNames();
                    rioObj.OfficialCollection[0].PersonNames.First = bgEmployFirstName;
                    rioObj.OfficialCollection[0].PersonNames.Middle = bgEmploySecondName;
                    rioObj.OfficialCollection[0].PersonNames.Last = bgEmployLastName;
                }
                else if (!String.IsNullOrWhiteSpace(foreignEmployFirstName) || !String.IsNullOrWhiteSpace(foreignEmployLastName) || !String.IsNullOrWhiteSpace(foreignEmployOtherInfo))
                {
                    rioObj.OfficialCollection[0].ForeignCitizenNames = new R_0009_000007.ForeignCitizenNames();
                    rioObj.OfficialCollection[0].ForeignCitizenNames.FirstCyrillic = foreignEmployFirstName;
                    rioObj.OfficialCollection[0].ForeignCitizenNames.LastCyrillic = foreignEmployLastName;
                    rioObj.OfficialCollection[0].ForeignCitizenNames.OtherCyrillic = foreignEmployOtherInfo;
                }

                rioObj.OtherBodiesOrganizations = otherBodiesOrganizations;

                rioObj.PeriodAccessPublicInformation = periodAccessPublicInformation;

                rioObj.PlacePublicInformationAccessGiven = placePublicInformationAccessGiven;

                rioObj.RejectedInformationCollection = new R_6090.RejectedInformationCollection();
                if (rejectsReasonAndPublicInfoCollection != null)
                {
                    foreach (var reject in rejectsReasonAndPublicInfoCollection)
                    {
                        var rejectInformation = new R_6090.RejectedInformation();

                        rejectInformation.ReasonsNotGrandAccessPublicInformation = reject.Item1;
                        rejectInformation.RequestedPublicInformationDescription = reject.Item2;

                        rioObj.RejectedInformationCollection.Add(rejectInformation);
                    }
                }

                return XmlSerializerUtils.XmlSerializeObjectToBytes(rioObj);
            }
            catch
            {
                return null;
            }
        }

        public static byte[] CreateR3010RemovingIrregularitiesInstructions(
            string administrativeBodyName = null,
            DateTime? caseUriDate = null,
            string caseUriRegisterIndex = null,
            string caseUriSequenceNumber = null,
            DateTime? applicationUriDate = null,
            string applicationUriRegisterIndex = null,
            string applicationUriSequenceNumber = null,
            DateTime? irregularityDocUriDate = null,
            string irregularityDocUriRegisterIndex = null,
            string irregularityDocUriSequenceNumber = null,
            string applicantFirstName = null,
            string applicantSecondName = null,
            string applicantLastName = null,
            string applicantEgn = null,
            string applicantEmail = null,
            string electronicServiceProviderId = null,
            string electronicServiceProviderName = null,
            string electronicServiceProviderBulstat = null,
            TimeSpan? deadlineCorrectionPeriod = null,
            string employFirstName = null,
            string employSecondName = null,
            string employLastName = null,
            string instructionsHeader = null,
            List<Tuple<string, string>> irregularityNameAndDescriptionCollection = null)
        {
            try
            {
                R_3010.RemovingIrregularitiesInstructions rioObj = new R_3010.RemovingIrregularitiesInstructions();
                rioObj.AdministrativeBodyName = administrativeBodyName;

                rioObj.AISCaseURI = new R_0009_000073.AISCaseURI();
                rioObj.AISCaseURI.DocumentURI = new R_0009_000001.DocumentURI();
                rioObj.AISCaseURI.DocumentURI.ReceiptOrSigningDate = caseUriDate;
                rioObj.AISCaseURI.DocumentURI.RegisterIndex = caseUriRegisterIndex;
                rioObj.AISCaseURI.DocumentURI.SequenceNumber = caseUriSequenceNumber;

                rioObj.ApplicationDocumentReceiptOrSigningDate = applicationUriDate;

                rioObj.ApplicationDocumentURI = new R_0009_000001.DocumentURI();
                rioObj.ApplicationDocumentURI.ReceiptOrSigningDate = applicationUriDate;
                rioObj.ApplicationDocumentURI.RegisterIndex = applicationUriRegisterIndex;
                rioObj.ApplicationDocumentURI.SequenceNumber = applicationUriSequenceNumber;

                rioObj.ElectronicServiceApplicant = new R_0009_000016.ElectronicServiceApplicant();
                rioObj.ElectronicServiceApplicant.EmailAddress = applicantEmail;
                rioObj.ElectronicServiceApplicant.RecipientGroupCollection = new R_0009_000016.RecipientGroupCollection();
                rioObj.ElectronicServiceApplicant.RecipientGroupCollection.Add(new R_0009_000016.RecipientGroup());
                rioObj.ElectronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection = new R_0009_000016.ElectronicServiceRecipientCollection();
                rioObj.ElectronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection.Add(new R_0009_000015.ElectronicServiceRecipient());
                rioObj.ElectronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection[0].Person = new R_0009_000008.PersonBasicData();
                rioObj.ElectronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Names = new R_0009_000005.PersonNames();
                rioObj.ElectronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Names.First = applicantFirstName;
                rioObj.ElectronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Names.Middle = applicantSecondName;
                rioObj.ElectronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Names.Last = applicantLastName;
                rioObj.ElectronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Identifier = new R_0009_000006.PersonIdentifier();
                rioObj.ElectronicServiceApplicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Identifier.EGN = applicantEgn;

                if (deadlineCorrectionPeriod.HasValue)
                {
                    rioObj.DeadlineCorrectionIrregularities = System.Xml.XmlConvert.ToString(deadlineCorrectionPeriod.Value);
                }

                rioObj.ElectronicServiceProviderBasicData = new R_0009_000002.ElectronicServiceProviderBasicData();
                rioObj.ElectronicServiceProviderBasicData.ElectronicServiceProviderType = electronicServiceProviderId;
                rioObj.ElectronicServiceProviderBasicData.EntityBasicData = new R_0009_000013.EntityBasicData();
                rioObj.ElectronicServiceProviderBasicData.EntityBasicData.Identifier = electronicServiceProviderBulstat;
                rioObj.ElectronicServiceProviderBasicData.EntityBasicData.Name = electronicServiceProviderName;

                rioObj.IrregularityDocumentReceiptOrSigningDate = irregularityDocUriDate;

                rioObj.IrregularityDocumentURI = new R_0009_000001.DocumentURI();
                rioObj.IrregularityDocumentURI.ReceiptOrSigningDate = irregularityDocUriDate;
                rioObj.IrregularityDocumentURI.RegisterIndex = irregularityDocUriRegisterIndex;
                rioObj.IrregularityDocumentURI.SequenceNumber = irregularityDocUriSequenceNumber;

                rioObj.Official = new R_3010.RemovingIrregularitiesInstructionsOfficial();

                rioObj.Official.PersonNames = new R_0009_000005.PersonNames();
                rioObj.Official.PersonNames.First = employFirstName;
                rioObj.Official.PersonNames.Middle = employSecondName;
                rioObj.Official.PersonNames.Last = employLastName;

                rioObj.RemovingIrregularitiesInstructionsHeader = instructionsHeader;

                rioObj.IrregularitiesCollection = new R_3010.IrregularitiesCollection();
                if (irregularityNameAndDescriptionCollection != null)
                {
                    foreach (var irregularity in irregularityNameAndDescriptionCollection)
                    {
                        var newIrregularity = new R_3010.Irregularities();
                        newIrregularity.IrregularityType = irregularity.Item1;
                        newIrregularity.AdditionalInformationSpecifyingIrregularity = irregularity.Item2;

                        rioObj.IrregularitiesCollection.Add(newIrregularity);
                    }
                }

                return XmlSerializerUtils.XmlSerializeObjectToBytes(rioObj);
            }
            catch
            {
                return null;
            }
        }

        public static byte[] CreateR0101ReceiptAcknowledgedMessage(
            string documentTypeName = null,
            string documentTypeRegisterIndex = null,
            string documentTypeBatchNumber = null,
            string registeredByIdentifier = null,
            string registeredByFirstName = null,
            string registeredBySecondName = null,
            string registeredByLastName = null,
            string caseRegUri = null,
            string caseAccessCode = null,
            string applicantFirstName = null,
            string applicantSecondName = null,
            string applicantLastName = null,
            string applicantEgn = null,
            string applicantEmail = null,
            DateTime? documentUriDate = null,
            string documentUriRegisterIndex = null,
            string documentUriSequenceNumber = null,
            string electronicServiceProviderId = null,
            string electronicServiceProviderName = null,
            string electronicServiceProviderBulstat = null)
        {
            try
            {
                R_0009_000019.ReceiptAcknowledgedMessage rioObj = new R_0009_000019.ReceiptAcknowledgedMessage();

                rioObj.TransportType = "0006-000001";

                rioObj.DocumentTypeName = documentTypeName;
                rioObj.DocumentTypeURI = new R_0009_000003.DocumentTypeURI();
                rioObj.DocumentTypeURI.RegisterIndex = documentTypeRegisterIndex;
                rioObj.DocumentTypeURI.BatchNumber = documentTypeBatchNumber;

                rioObj.RegisteredBy = new R_0009_000019.RegisteredBy();
                rioObj.RegisteredBy.Officer = new R_0009_000019.Officer();
                rioObj.RegisteredBy.Officer.AISUserIdentifier = registeredByIdentifier;
                rioObj.RegisteredBy.Officer.PersonNames = new R_0009_000018.AISUserNames();
                rioObj.RegisteredBy.Officer.PersonNames.PersonNames = new R_0009_000005.PersonNames();
                rioObj.RegisteredBy.Officer.PersonNames.PersonNames.First = registeredByFirstName;
                rioObj.RegisteredBy.Officer.PersonNames.PersonNames.Middle = registeredBySecondName;
                rioObj.RegisteredBy.Officer.PersonNames.PersonNames.Last = registeredByLastName;

                rioObj.Applicant = new R_0009_000016.ElectronicServiceApplicant();
                rioObj.Applicant.RecipientGroupCollection = new R_0009_000016.RecipientGroupCollection();
                rioObj.Applicant.RecipientGroupCollection.Add(new R_0009_000016.RecipientGroup());
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection = new R_0009_000016.ElectronicServiceRecipientCollection();
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection.Add(new R_0009_000015.ElectronicServiceRecipient());
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person = new R_0009_000008.PersonBasicData();
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Names = new R_0009_000005.PersonNames();
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Names.First = applicantFirstName;
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Names.Middle = applicantSecondName;
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Names.Last = applicantLastName;
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Identifier = new R_0009_000006.PersonIdentifier();
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Identifier.EGN = applicantEgn;
                rioObj.Applicant.EmailAddress = applicantEmail;

                rioObj.DocumentURI = new R_0009_000001.DocumentURI();
                rioObj.DocumentURI.ReceiptOrSigningDate = documentUriDate;
                rioObj.DocumentURI.RegisterIndex = documentUriRegisterIndex;
                rioObj.DocumentURI.SequenceNumber = documentUriSequenceNumber;

                rioObj.ElectronicServiceProvider = new R_0009_000002.ElectronicServiceProviderBasicData();
                rioObj.ElectronicServiceProvider.ElectronicServiceProviderType = electronicServiceProviderId;
                rioObj.ElectronicServiceProvider.EntityBasicData = new R_0009_000013.EntityBasicData();
                rioObj.ElectronicServiceProvider.EntityBasicData.Identifier = electronicServiceProviderBulstat;
                rioObj.ElectronicServiceProvider.EntityBasicData.Name = electronicServiceProviderName;

                string htmlFormat = @"<p>Номер на преписка: <b>{0}</b><br/>Код за достъп: <b>{1}</b><br/></p>";
                rioObj.CaseAccessIdentifier = String.Format(htmlFormat, caseRegUri, caseAccessCode);

                return XmlSerializerUtils.XmlSerializeObjectToBytes(rioObj);
            }
            catch
            {
                return null;
            }
        }

        public static byte[] CreateR0090ReceiptNotAcknowledgedMessage(string documentTypeName = null,
            string documentTypeRegisterIndex = null,
            string documentTypeBatchNumber = null,
            string applicantFirstName = null,
            string applicantSecondName = null,
            string applicantLastName = null,
            string applicantEgn = null,
            string applicantEmail = null,
            DateTime? documentUriDate = null,
            string documentUriRegisterIndex = null,
            string documentUriSequenceNumber = null,
            string electronicServiceProviderId = null,
            string electronicServiceProviderName = null,
            string electronicServiceProviderBulstat = null,
            List<string> discrepancies = null)
        {
            try
            {
                R_0009_000017.ReceiptNotAcknowledgedMessage rioObj = new R_0009_000017.ReceiptNotAcknowledgedMessage();

                rioObj.TransportType = "0006-000001";

                rioObj.DocumentTypeName = documentTypeName;
                rioObj.DocumentTypeURI = new R_0009_000003.DocumentTypeURI();
                rioObj.DocumentTypeURI.RegisterIndex = documentTypeRegisterIndex;
                rioObj.DocumentTypeURI.BatchNumber = documentTypeBatchNumber;

                rioObj.Applicant = new R_0009_000016.ElectronicServiceApplicant();
                rioObj.Applicant.RecipientGroupCollection = new R_0009_000016.RecipientGroupCollection();
                rioObj.Applicant.RecipientGroupCollection.Add(new R_0009_000016.RecipientGroup());
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection = new R_0009_000016.ElectronicServiceRecipientCollection();
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection.Add(new R_0009_000015.ElectronicServiceRecipient());
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person = new R_0009_000008.PersonBasicData();
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Names = new R_0009_000005.PersonNames();
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Names.First = applicantFirstName;
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Names.Middle = applicantSecondName;
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Names.Last = applicantLastName;
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Identifier = new R_0009_000006.PersonIdentifier();
                rioObj.Applicant.RecipientGroupCollection[0].RecipientCollection[0].Person.Identifier.EGN = applicantEgn;
                rioObj.Applicant.EmailAddress = applicantEmail;

                rioObj.MessageURI = new R_0009_000001.DocumentURI();
                rioObj.MessageURI.ReceiptOrSigningDate = documentUriDate;
                rioObj.MessageURI.RegisterIndex = documentUriRegisterIndex;
                rioObj.MessageURI.SequenceNumber = documentUriSequenceNumber;

                rioObj.ElectronicServiceProvider = new R_0009_000002.ElectronicServiceProviderBasicData();
                rioObj.ElectronicServiceProvider.ElectronicServiceProviderType = electronicServiceProviderId;
                rioObj.ElectronicServiceProvider.EntityBasicData = new R_0009_000013.EntityBasicData();
                rioObj.ElectronicServiceProvider.EntityBasicData.Identifier = electronicServiceProviderBulstat;
                rioObj.ElectronicServiceProvider.EntityBasicData.Name = electronicServiceProviderName;

                rioObj.Discrepancies = new R_0009_000017.Discrepancies();
                rioObj.Discrepancies.DiscrepancyCollection = new R_0009_000017.DiscrepancyCollection();
                if (discrepancies != null)
                {
                    foreach(var item in discrepancies)
                    {
                        rioObj.Discrepancies.DiscrepancyCollection.Add(item);
                    }
                }

                return XmlSerializerUtils.XmlSerializeObjectToBytes(rioObj);
            }
            catch
            {
                return null;
            }
        }
    }
}
