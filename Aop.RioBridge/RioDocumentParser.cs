using Common.Rio.PortalBridge;
using Components.DevelopmentLogger;
using Components.DocumentSerializer;
using Components.DocumentSigner;
using Components.EmsUtils;
using Components.PortalConfigurationManager;
using Components.VirusScanEngine;
using Components.XmlSchemaValidator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RioObjects.Enums;
using System.Reflection;
using RioObjects;

namespace Aop.Rio.PortalBridge
{
    public class RioDocumentParser : IRioDocumentParser
    {
        private IDocumentSerializer documentSerializer { get; set; }
        private IVirusScanEngine virusScanEngine { get; set; }
        private IPortalConfigurationManager portalConfigurationManager;
        private IDevelopmentLogger developmentLogger;
        private IXmlSchemaValidator xmlSchemaValidator { get; set; }
        private IDocumentSigner documentSigner { get; set; }
        private IEmsUtils emsUtils { get; set; }

        public RioDocumentParser()
        {
            this.documentSerializer = new DocumentSerializerImpl();
            this.virusScanEngine = new VirusScanEngineImpl();
            this.portalConfigurationManager = new PortalConfigurationManagerImpl();
            this.developmentLogger = new EventLogDevelopmentLoggerImpl(portalConfigurationManager);
            this.xmlSchemaValidator = new XmlSchemaValidatorImpl(developmentLogger);
            this.documentSigner = new DocumentSignerImpl(portalConfigurationManager, documentSerializer);
            this.emsUtils = new EmsUtilsAop(documentSerializer, xmlSchemaValidator, virusScanEngine, documentSigner);
        }

        public Common.Rio.PortalBridge.RioObjects.RioApplication XmlDeserializeApplication(string xmlContent)
        {
            Type applicationType = this.emsUtils.GetDocumentMetadataFromXml(xmlContent).RioObjectType;
            object rioApplication = this.documentSerializer.XmlDeserializeFromString(applicationType, xmlContent);
            var mappedApplication = (Common.Rio.PortalBridge.RioObjects.RioApplication)AutoMapper.Mapper.Map(rioApplication, typeof(IHeaderFooterDocumentsRioApplication), typeof(Common.Rio.PortalBridge.RioObjects.RioApplication));
            mappedApplication.OriginalApplication = rioApplication;
            mappedApplication.OriginalApplicationType = applicationType;
            mappedApplication.DocFileTypeAlias = applicationType.Namespace.Replace('_', '-');
            return mappedApplication;
        }

        public string XmlSerializeReceiptAcknowledgedMessage(Common.Rio.PortalBridge.RioObjects.ReceiptAcknowledgedMessage msg)
        {
            var rioMsg = AutoMapper.Mapper.Map<Common.Rio.PortalBridge.RioObjects.ReceiptAcknowledgedMessage, R_0009_000019.ReceiptAcknowledgedMessage>(msg);
            return this.documentSerializer.XmlSerializeObjectToString(rioMsg);
        }

        public string XmlSerializeReceiptNotAcknowledgedMessage(Common.Rio.PortalBridge.RioObjects.ReceiptNotAcknowledgedMessage msg)
        {
            var rioMsg = AutoMapper.Mapper.Map<Common.Rio.PortalBridge.RioObjects.ReceiptNotAcknowledgedMessage, R_0009_000017.ReceiptNotAcknowledgedMessage>(msg);
            return this.documentSerializer.XmlSerializeObjectToString(rioMsg);
        }

        public List<Common.Rio.PortalBridge.RioObjects.ElectronicDocumentDiscrepancyTypeNomenclature> GetValidationDiscrepancies(string xmlContent)
        {
            List<RioObjects.Enums.ElectronicDocumentDiscrepancyTypeNomenclature> discrepancies = new List<RioObjects.Enums.ElectronicDocumentDiscrepancyTypeNomenclature>();

            //if (!emsUtils.CheckEmail(xmlContent))
            //{
            //    discrepancies.Add(RioObjects.Enums.ElectronicDocumentDiscrepancyTypeNomenclature.NoEmail);
            //}

            if (!emsUtils.CheckDocumentSize(xmlContent))
            {
                discrepancies.Add(RioObjects.Enums.ElectronicDocumentDiscrepancyTypeNomenclature.SizeTooLarge);
            }

            //TODO: Bug in chema validation
            //schemasPath = GetRioObjectsSchemasPath();
            //if (!emsUtils.CheckValidXmlSchema(xmlContent, schemasPath))
            //{
            //    discrepancies.Add(RioObjects.Enums.ElectronicDocumentDiscrepancyTypeNomenclature.IncorrectFormat);
            //}

            //TODO: Bug in attached file extension (.xml added)
            //if (!emsUtils.CheckSupportedFileFormats(xmlContent))
            //{
            //    discrepancies.Add(RioObjects.Enums.ElectronicDocumentDiscrepancyTypeNomenclature.IncorrectAttachmentsFormat);
            //}

            if (!emsUtils.CheckSignatureValidity(xmlContent))
            {
                discrepancies.Add(RioObjects.Enums.ElectronicDocumentDiscrepancyTypeNomenclature.NotAuthenticated);
            }

            //TODO: must be setup
            //bool skipCertificateChainValidation = take it from Web.config
            //if (!skipCertificateChainValidation)
            //{
            //    var revocationErrors = emsUtils.CheckCertificateValidity(xml);
            //    if (revocationErrors != null && revocationErrors.Count() > 0)
            //    {
            //        discrepancies.Add(RioObjects.Enums.ElectronicDocumentDiscrepancyTypeNomenclature.NotAuthenticated);
            //    }
            //}

            //TODO: Implement
            //if (!emsUtils.CheckForVirus())
            //{
            //    discrepancies.Add(RioObjects.Enums.ElectronicDocumentDiscrepancyTypeNomenclature.IncorrectAttachmentsFormat);
            //}

            return AutoMapper.Mapper.Map<List<RioObjects.Enums.ElectronicDocumentDiscrepancyTypeNomenclature>, List<Common.Rio.PortalBridge.RioObjects.ElectronicDocumentDiscrepancyTypeNomenclature>>(discrepancies);
        }

        public List<string> GetValidationErrors(string docTypeUri, string xmlContent)
        {
            //return emsUtils.ValidateRioApplication(docTypeUri, xmlContent);

            //TODO: Uncomment
            return new List<string>();
        }

        private static string schemasPath = null;
        private static string GetRioObjectsSchemasPath()
        {
            if (schemasPath == null)
            {
                string assemblyPath = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                string binPath = System.IO.Path.GetDirectoryName(assemblyPath);
                string projectPath = binPath.Substring(0, binPath.Length - 4);
                schemasPath = String.Format(@"{0}\RioSchemas", projectPath);
            }

            return schemasPath;
        }
    }
}
