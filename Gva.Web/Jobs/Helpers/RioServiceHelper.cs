using Gva.Portal.Components.DevelopmentLogger;
using Gva.Portal.Components.DocumentSerializer;
using Gva.Portal.Components.DocumentSigner;
using Gva.Portal.Components.EmsUtils;
using Gva.Portal.Components.PortalConfigurationManager;
using Gva.Portal.Components.VirusScanEngine;
using Gva.Portal.Components.XmlSchemaValidator;
using Gva.Portal.RioObjects;
using Gva.Portal.RioObjects.Enums;
using R_0009_000054;
using R_Abbcdn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.Web.Jobs.Helpers
{
    public class RioServiceHelper
    {
        public class AttachedDocument
        {
            public string FileName { get; set; }
            public string Description { get; set; }
            public string MimeType { get; set; }
            public string UniqueIdentifier { get; set; }
            public byte[] BytesContent { get; set; }
            public bool UseAbbcdn { get; set; }
            public Abbcdnconfig AbbcdnInfo { get; set; }
        }

        private IDocumentSerializer documentSerializer { get; set; }
        private IVirusScanEngine virusScanEngine { get; set; }
        private IPortalConfigurationManager portalConfigurationManager;
        private IDevelopmentLogger developmentLogger;
        private IXmlSchemaValidator xmlSchemaValidator { get; set; }
        private IDocumentSigner documentSigner { get; set; }
        private IEmsUtils emsUtils { get; set; }

        public string XmlContent { get; private set; }
        public object RioObject { get; private set; }
        public RioDocumentMetadata DocumentMetaData { get; private set; }

        public IHeaderFooterDocumentsRioApplication ServiceHeader { get; private set; }
        public IRioApplication RioApplication { get; private set; }
        public string DocFileTypeAlias { get; private set; }

        public List<AttachedDocument> AttachedDocuments { get; private set; }

        public RioServiceHelper(string xmlContent)
        {
            this.XmlContent = xmlContent;
            //this.BytesContent = Converter.GetBytes(this.XmlContent);
            
            InitUtils();
            InitMemebers();
        }

        private void InitUtils()
        {
            this.documentSerializer = documentSerializer = new DocumentSerializerImpl();
            this.virusScanEngine = new VirusScanEngineImpl();
            this.portalConfigurationManager = new PortalConfigurationManagerImpl();
            this.developmentLogger = new EventLogDevelopmentLoggerImpl(portalConfigurationManager);
            this.xmlSchemaValidator = new XmlSchemaValidatorImpl(developmentLogger);
            this.documentSigner = new DocumentSignerImpl(portalConfigurationManager, documentSerializer);
            this.emsUtils = new EmsUtilsImpl(documentSerializer, xmlSchemaValidator, virusScanEngine, documentSigner);
        }

        private void InitMemebers()
        {
            this.DocumentMetaData = emsUtils.GetDocumentMetadataFromXml(this.XmlContent);
            this.RioObject = documentSerializer.XmlDeserializeFromString(this.DocumentMetaData.RioObjectType, this.XmlContent);

            //Gva applications
            if (this.DocumentMetaData.RioObjectType == typeof(R_4186.InitialCertificationCommercialPilotCapacityInstrumentFlightApplication))
            {
                ServiceHeader = (R_4186.InitialCertificationCommercialPilotCapacityInstrumentFlightApplication)this.RioObject;
                RioApplication = documentSerializer.XmlDeserializeFromString<R_4186.InitialCertificationCommercialPilotCapacityInstrumentFlightApplication>(this.XmlContent);
                DocFileTypeAlias = "R-4186";
            }

            //Answers
            //else if (this.DocumentMetaData.RioObjectType == typeof(R_0009_000017.ReceiptNotAcknowledgedMessage))
            //{
            //    ServiceHeader = null;
            //    ServiceURI = null;
            //    ServiceInstructions = null;
            //    RioApplication = documentSerializer.XmlDeserializeFromString<R_0009_000017.ReceiptNotAcknowledgedMessage>(this.XmlContent);
            //    DocFileTypeAlias = "ReceiptNotAcknowledgedMessage";
            //}
            //else if (this.DocumentMetaData.RioObjectType == typeof(R_0009_000019.ReceiptAcknowledgedMessage))
            //{
            //    ServiceHeader = null;
            //    ServiceURI = null; 
            //    ServiceInstructions = null;
            //    RioApplication = documentSerializer.XmlDeserializeFromString<R_0009_000019.ReceiptAcknowledgedMessage>(this.XmlContent);
            //    DocFileTypeAlias = "ReceiptAcknowledgedMessage";
            //}
            //else if (this.DocumentMetaData.RioObjectType == typeof(R_3010.RemovingIrregularitiesInstructions))
            //{
            //    ServiceHeader = null;
            //    ServiceURI = null;
            //    ServiceInstructions = null;
            //    RioApplication = documentSerializer.XmlDeserializeFromString<R_3010.RemovingIrregularitiesInstructions>(this.XmlContent);
            //    DocFileTypeAlias = "RemovingIrregularitiesInstructions";
            //}
            else
            {
                throw new ArgumentException();
            }

            AttachedDocuments = new List<AttachedDocument>();

            if (ServiceHeader != null && ServiceHeader.AttachedDocuments != null)
            {
                foreach (var document in ServiceHeader.AttachedDocuments)
                {
                    AttachedDocument attachedDoc = new AttachedDocument();
                    attachedDoc.FileName = document.AttachedDocumentFileName;
                    attachedDoc.Description = document.AttachedDocumentDescription;
                    attachedDoc.MimeType = document.FileType;
                    attachedDoc.BytesContent = document.AttachedDocumentFileContent;
                    attachedDoc.UniqueIdentifier = document.AttachedDocumentUniqueIdentifier;
                    attachedDoc.AbbcdnInfo = DeserializeAbbcdn(document.AttachedDocumentFileContent);
                    attachedDoc.UseAbbcdn = attachedDoc.AbbcdnInfo != null;

                    AttachedDocuments.Add(attachedDoc);
                }
            }
        }

        private Abbcdnconfig DeserializeAbbcdn(byte[] xml)
        {
            try
            {
                return documentSerializer.XmlDeserializeFromBytes<Abbcdnconfig>(xml);
            }
            catch
            {
                return null;
            }
        }

        public List<ElectronicDocumentDiscrepancyTypeNomenclature> ValidateServiceData(string schemasPath, string xml, bool skipCertificateChainValidation)
        {
            List<ElectronicDocumentDiscrepancyTypeNomenclature> discrepancies = new List<ElectronicDocumentDiscrepancyTypeNomenclature>();

            if (!emsUtils.CheckEmail(this.XmlContent))
            {
                discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.NoEmail);
            }

            if (!emsUtils.CheckDocumentSize(this.XmlContent))
            {
                discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.SizeTooLarge);
            }

            //TODO: Bug in chema validation
            //if (!emsUtils.CheckValidXmlSchema(this.XmlContent, schemasPath))
            //{
            //    discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.IncorrectFormat);
            //}

            //TODO: Bug in attached file extension (.xml added)
            //if (!emsUtils.CheckSupportedFileFormats(this.XmlContent))
            //{
            //    discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.IncorrectAttachmentsFormat);
            //}

            if (!emsUtils.CheckSignatureValidity(this.XmlContent))
            {
                discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.NotAuthenticated);
            }

            //TODO: must be setup
            //if (!skipCertificateChainValidation)
            //{
            //    var revocationErrors = emsUtils.CheckCertificateValidity(xml);
            //    if (revocationErrors != null && revocationErrors.Count() > 0)
            //    {
            //        discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.NotAuthenticated);
            //    }
            //}

            //TODO: Implement
            //if (!emsUtils.CheckForVirus())
            //{
            //    discrepancies.Add(ElectronicDocumentDiscrepancyTypeNomenclature.IncorrectAttachmentsFormat);
            //}

            return discrepancies;
        }

        public List<string> GetValidationErrors(string docTypeUri)
        {

            //return emsUtils.ValidateRioApplication(docTypeUri, this.XmlContent);

            //TODO: Uncomment
            return new List<string>();
        }

        public void SetDocumentUriToContent(R_0009_000001.DocumentURI documentUri)
        {
            //Gva applications
            if (this.DocumentMetaData.RioObjectType == typeof(R_4186.InitialCertificationCommercialPilotCapacityInstrumentFlightApplication))
            {
                var application = (R_4186.InitialCertificationCommercialPilotCapacityInstrumentFlightApplication)RioObject;
                application.ElectronicAdministrativeServiceHeader.DocumentURI = documentUri;

                RioObject = application;
                RioApplication = application;
                ServiceHeader = application;

                XmlContent = documentSerializer.XmlSerializeObjectToString(application);
                //BytesContent = documentSerializer.XmlSerializeObjectToBytes(application);
            }

            //Answers
            //else if (this.DocumentMetaData.RioObjectType == typeof(R_0009_000017.ReceiptNotAcknowledgedMessage))
            //{
            //    var application = (R_0009_000017.ReceiptNotAcknowledgedMessage)RioObject;
            //    application.MessageURI = documentUri;

            //    RioObject = application;
            //    RioApplication = application;
            //    ServiceHeader = null;

            //    XmlContent = documentSerializer.XmlSerializeObjectToString(application);
            //    BytesContent = documentSerializer.XmlSerializeObjectToBytes(application);
            //}
            //else if (this.DocumentMetaData.RioObjectType == typeof(R_0009_000019.ReceiptAcknowledgedMessage))
            //{
            //    //There is not MessageUri property in this service
            //}
            //else if (this.DocumentMetaData.RioObjectType == typeof(R_3010.RemovingIrregularitiesInstructions))
            //{
            //    //TODO: Za tozi tip dokument trqbva da se set-vat o6te ne6ta osven URI-to
                
            //    //Полето „Дата на подписване на указания за отстраняване на нередовности” от секцията „Указания за отстраняване на нередовности” трябва да е попълнено
            //    //??? (po princip go set-vam prei tova, no trqbva da se vidi dali togava ima stoinost) Полето „Дата на подписване на искане за предоставяне на ЕАУ” от секцията „Указания за отстраняване на нередовности” трябва да е попълнено
            //    var application = (R_3010.RemovingIrregularitiesInstructions)RioObject;
            //    application.IrregularityDocumentURI = documentUri;

            //    RioObject = application;
            //    RioApplication = application;
            //    ServiceHeader = null;

            //    XmlContent = documentSerializer.XmlSerializeObjectToString(application);
            //    BytesContent = documentSerializer.XmlSerializeObjectToBytes(application);
            //}
        }
    }
}