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
            public string DocKind { get; set; }
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

            List<KeyValuePair<string, string>> attachedFileNames = new List<KeyValuePair<string, string>>();

            //Gva applications
            if (this.DocumentMetaData.RioObjectType == typeof(R_4186.InitialCertificationCommercialPilotCapacityInstrumentFlightApplication))
            {
                ServiceHeader = (R_4186.InitialCertificationCommercialPilotCapacityInstrumentFlightApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4186.InitialCertificationCommercialPilotCapacityInstrumentFlightApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4186";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4240.InitialAuthorizationMaintenanceAircraftAMLApplication))
            {
                ServiceHeader = (R_4240.InitialAuthorizationMaintenanceAircraftAMLApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4240.InitialAuthorizationMaintenanceAircraftAMLApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4240";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4242.InitialIssueLicenseFlightDispatcherApplication))
            {
                ServiceHeader = (R_4242.InitialIssueLicenseFlightDispatcherApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4242.InitialIssueLicenseFlightDispatcherApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4242";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4244.LicenseFlightCrewCabinCrewFlightEngineersNavigatorsFlightConvoyApplication))
            {
                ServiceHeader = (R_4244.LicenseFlightCrewCabinCrewFlightEngineersNavigatorsFlightConvoyApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4244.LicenseFlightCrewCabinCrewFlightEngineersNavigatorsFlightConvoyApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4244";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4284.LicenseControllersAssistantFlightsATMCoordinatorsApplication))
            {
                ServiceHeader = (R_4284.LicenseControllersAssistantFlightsATMCoordinatorsApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4284.LicenseControllersAssistantFlightsATMCoordinatorsApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4284";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4296.RecognitionLicenseForeignNationals))
            {
                ServiceHeader = (R_4296.RecognitionLicenseForeignNationals)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4296.RecognitionLicenseForeignNationals>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4296";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4356.AircraftRegistrationCertificateApplication))
            {
                ServiceHeader = (R_4356.AircraftRegistrationCertificateApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4356.AircraftRegistrationCertificateApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4356";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4378.AppointmentSmodeCodeApplication))
            {
                ServiceHeader = (R_4378.AppointmentSmodeCodeApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4378.AppointmentSmodeCodeApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4378";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4396.AircraftDeletionApplication))
            {
                ServiceHeader = (R_4396.AircraftDeletionApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4396.AircraftDeletionApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4396";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4470.AircraftAirworthinessCertificateApplication))
            {
                ServiceHeader = (R_4470.AircraftAirworthinessCertificateApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4470.AircraftAirworthinessCertificateApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4470";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4490.AuthorizationUseRadioAircraftApplication))
            {
                ServiceHeader = (R_4490.AuthorizationUseRadioAircraftApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4490.AuthorizationUseRadioAircraftApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4490";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4514.NoiseCertificateApplication))
            {
                ServiceHeader = (R_4514.NoiseCertificateApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4514.NoiseCertificateApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4514";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4544.ReviewAircraftAirworthinessApplication))
            {
                ServiceHeader = (R_4544.ReviewAircraftAirworthinessApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4544.ReviewAircraftAirworthinessApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4544";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4566.AirworthinessExportCertificateApplication))
            {
                ServiceHeader = (R_4566.AirworthinessExportCertificateApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4566.AirworthinessExportCertificateApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4566";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4576.RegistrationAmateurBuiltAircraftApplication))
            {
                ServiceHeader = (R_4576.RegistrationAmateurBuiltAircraftApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4576.RegistrationAmateurBuiltAircraftApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4576";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                } 
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4578.AirworthinessAmateurBuiltAircraftApplication))
            {
                ServiceHeader = (R_4578.AirworthinessAmateurBuiltAircraftApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4578.AirworthinessAmateurBuiltAircraftApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4578";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4588.CertificateFitnessAirportApplication))
            {
                ServiceHeader = (R_4588.CertificateFitnessAirportApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4588.CertificateFitnessAirportApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4588";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.UnnumberedAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.UnnumberedAttachedDocumentDatasCollection.UnnumberedAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4590.CertificateFitnessAirfieldApplication))
            {
                ServiceHeader = (R_4590.CertificateFitnessAirfieldApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4590.CertificateFitnessAirfieldApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4590";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.AdditionalAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AdditionalAttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4598.AuthorizationAirportOperatorApplication))
            {
                ServiceHeader = (R_4598.AuthorizationAirportOperatorApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4598.AuthorizationAirportOperatorApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4598";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                } 
                if (deserializedObject.AdditionalAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AdditionalAttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4606.LicenseOperatorApplication))
            {
                ServiceHeader = (R_4606.LicenseOperatorApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4606.LicenseOperatorApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4606";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.AdditionalAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AdditionalAttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4614.GroundHandlingEquipmentApplication))
            {
                ServiceHeader = (R_4614.GroundHandlingEquipmentApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4614.GroundHandlingEquipmentApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4614";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.AdditionalAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AdditionalAttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4686.AirCarrierOperationLicenseApplication))
            {
                ServiceHeader = (R_4686.AirCarrierOperationLicenseApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4686.AirCarrierOperationLicenseApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4686";
                if (deserializedObject.TwentyOrMorePlacesAdditionalAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.TwentyOrMorePlacesAdditionalAttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4738.AirNavigationServiceProviderApplication))
            {
                ServiceHeader = (R_4738.AirNavigationServiceProviderApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4738.AirNavigationServiceProviderApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4738";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.UnnumberedAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.UnnumberedAttachedDocumentDatasCollection.UnnumberedAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4764.FitnessAutomatedATMApplication))
            {
                ServiceHeader = (R_4764.FitnessAutomatedATMApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4764.FitnessAutomatedATMApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4764";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4766.NavigationalAidsApplication))
            {
                ServiceHeader = (R_4766.NavigationalAidsApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4766.NavigationalAidsApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4766";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4810.IssuanceMaintenanceCertificateCompetencePerformingTasksApplication))
            {
                ServiceHeader = (R_4810.IssuanceMaintenanceCertificateCompetencePerformingTasksApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4810.IssuanceMaintenanceCertificateCompetencePerformingTasksApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4810";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4824.TeacherAviationTrainingCentersApplication))
            {
                ServiceHeader = (R_4824.TeacherAviationTrainingCentersApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4824.TeacherAviationTrainingCentersApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4824";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4834.VocationalAviationTrainingCenterApplication))
            {
                ServiceHeader = (R_4834.VocationalAviationTrainingCenterApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4834.VocationalAviationTrainingCenterApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4834";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4860.UseAviationSimulatorApplication))
            {
                ServiceHeader = (R_4860.UseAviationSimulatorApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4860.UseAviationSimulatorApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4860";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4862.AviationTrainingCenterAnotherCountryApplication))
            {
                ServiceHeader = (R_4862.AviationTrainingCenterAnotherCountryApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4862.AviationTrainingCenterAnotherCountryApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4862";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4864.LicenseCabinCrewApplication))
            {
                ServiceHeader = (R_4864.LicenseCabinCrewApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4864.LicenseCabinCrewApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4864";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4900.AviationSimulatorApplication))
            {
                ServiceHeader = (R_4900.AviationSimulatorApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4900.AviationSimulatorApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4900";
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4926.PermitFlyApplication))
            {
                ServiceHeader = (R_4926.PermitFlyApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4926.PermitFlyApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4926";
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_4958.EngineeringTechnicalStaffPerformingMaintenanceFundsAirTrafficManagementApplication))
            {
                ServiceHeader = (R_4958.EngineeringTechnicalStaffPerformingMaintenanceFundsAirTrafficManagementApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_4958.EngineeringTechnicalStaffPerformingMaintenanceFundsAirTrafficManagementApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-4958";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_5000.AOCConductingAerialWorkApplication))
            {
                ServiceHeader = (R_5000.AOCConductingAerialWorkApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_5000.AOCConductingAerialWorkApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-5000";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.UnnumberedAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.UnnumberedAttachedDocumentDatasCollection.UnnumberedAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
            }
            else if (this.DocumentMetaData.RioObjectType == typeof(R_5090.EngagedCommercialTransportationPassengersCargoApplication))
            {
                ServiceHeader = (R_5090.EngagedCommercialTransportationPassengersCargoApplication)this.RioObject;
                var deserializedObject = documentSerializer.XmlDeserializeFromString<R_5090.EngagedCommercialTransportationPassengersCargoApplication>(this.XmlContent);
                RioApplication = deserializedObject;
                DocFileTypeAlias = "R-5090";
                if (deserializedObject.AttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.AttachedDocumentDatasCollection.AttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.EAURecipientsAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.EAURecipientsAttachedDocumentDatasCollection.EAURecipientAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
                if (deserializedObject.UnnumberedAttachedDocumentDatasCollection != null)
                {
                    attachedFileNames.AddRange(deserializedObject.UnnumberedAttachedDocumentDatasCollection.UnnumberedAttachedDocumentDataCollection.Where(e => e.AttachedDocumentUniqueIdentifier != null).Select(e =>
                        new KeyValuePair<string, string>(e.AttachedDocumentUniqueIdentifier, e.AttachedDocumentKind.AttachedDocumentKindName)));
                }
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
                    attachedDoc.AbbcdnInfo = DeserializeAbbcdn(document.AttachedDocumentFileContent);
                    attachedDoc.UniqueIdentifier = attachedDoc.AbbcdnInfo.AttachedDocumentUniqueIdentifier;
                    attachedDoc.UseAbbcdn = attachedDoc.AbbcdnInfo != null;

                    if (attachedFileNames.Any(e => e.Key == document.AttachedDocumentUniqueIdentifier))
                    {
                        attachedDoc.DocKind = attachedFileNames.Single(e => e.Key == document.AttachedDocumentUniqueIdentifier).Value;
                    }
                    else
                    {
                        attachedDoc.DocKind = "Прикачен файл";
                    }

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