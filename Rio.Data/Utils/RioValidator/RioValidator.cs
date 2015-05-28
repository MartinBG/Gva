using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Xml.Schema;
using System.IO;
using System.Xml;
using System.Security.Cryptography.Xml;
using R_0009_000016;
using Common.Utils;

namespace Rio.Data.Utils.RioValidator
{
    public class RioValidator : IRioValidator
    {
        #region Public

        public bool CheckDocumentSize(string xml)
        {
            byte[] applicationBytes = Utf8Utils.GetBytes(xml);

            int xmlBytes = applicationBytes.Length;
            if (xmlBytes > (MAX_APPLICATION_FILE_SIZE_MB * 1024 * 1024))
            {
                return false;
            }

            return true;
        }

        public bool CheckValidXmlSchema(string xml, string schemasDirecotryPath)
        {
            //validating over the same XmlSchemaSet is NOT THREADSAFE
            lock (_syncRoot)
            {
                if (_xmlSchemaSet == null)
                {
                    _xmlSchemaSet = new XmlSchemaSet();
                    foreach (string schemaFile in Directory.GetFiles(schemasDirecotryPath, "*.xsd", SearchOption.AllDirectories))
                    {
                        using (TextReader schemaReader = new StreamReader(schemaFile))
                        {
                            _xmlSchemaSet.Add(null, XmlReader.Create(schemaReader));
                        }
                    }
                }

                XmlReader reader = null;
                List<Tuple<string, string>> errors = new List<Tuple<string, string>>();
#if DEBUG
                List<Tuple<string, string>> warnings = new List<Tuple<string, string>>();
#endif

                XmlReaderSettings settings = new XmlReaderSettings();
                settings.ValidationType = ValidationType.Schema;
                settings.ValidationFlags |= XmlSchemaValidationFlags.ReportValidationWarnings;
                settings.ValidationEventHandler +=
                    delegate(object sender, ValidationEventArgs args)
                    {
                        if (args.Severity == XmlSeverityType.Error)
                        {
                            errors.Add(Tuple.Create(args.Message, reader.Name));
                        }
#if DEBUG
                        else
                        {
                            warnings.Add(Tuple.Create(args.Message, reader.Name));
                        }
#endif
                    };
                settings.Schemas = _xmlSchemaSet;

                using (StringReader sr = new StringReader(xml))
                using (reader = XmlReader.Create(sr, settings))
                {
                    while (reader.Read())
                    {
                    }
                }

                int errorCount = errors.Count;
                if (errorCount > 0)
                {
                    string errorsText =
                        errors
                        .Select(err => err.Item1)
                        .Aggregate((err1, err2) => string.Format("{0}\n{1}", err1, err2));
                }

                return errorCount == 0;
            }
        }

        public bool CheckEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email) || !_emailRegex.IsMatch(email))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public bool CheckSupportedFileFormats(List<string> fileNames, string[] supportedFileFormats)
        {
            if (fileNames != null)
            {
                foreach (var fileName in fileNames)
                {
                    string extension = GetFileExtension(fileName).ToLowerInvariant();
                    if (!string.IsNullOrEmpty(extension))
                    {
                        if (!supportedFileFormats.Contains(extension))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public bool CheckForVirus(string xml, string virusEngineAddress, int virusEnginePort)
        {
            throw new NotImplementedException();
        }

        public bool CheckSignatureValidity(string xml, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces)
        {
            X509Certificate2 signingCertificate = null;
            if (signatureXPath != null && signatureXPathNamespaces != null)
            {
                return HasValidSignature(xml, signatureXPath, signatureXPathNamespaces, out signingCertificate);
            }

            return false;
        }

        public List<string> CheckCertificateValidity(string xml, ElectronicServiceApplicant applicant, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces)
        {
            bool missingRequiredAuthentication = false;
            bool missingRequiredSignature = false;
            X509Certificate2 signingCertificate = null;

            if (applicant != null)
            {
                missingRequiredAuthentication = !HasFilledElectronicServiceApplicant(applicant);

                if (signatureXPath != null)
                {
                    missingRequiredSignature = !HasValidSignature(xml, signatureXPath, signatureXPathNamespaces, out signingCertificate);
                }
            }

            if (missingRequiredAuthentication || missingRequiredSignature)
            {
                return new List<string>() { "NotAuthenticated" };
            }

            var x509Chain = new X509Chain();
            x509Chain.ChainPolicy.RevocationFlag = X509RevocationFlag.EntireChain;
            x509Chain.ChainPolicy.RevocationMode = X509RevocationMode.Online;

            x509Chain.Build(signingCertificate);

            signingCertificate.Verify();

            return x509Chain.ChainStatus.Select(e => e.StatusInformation).ToList();
        }

        public List<string> ValidateRioApplication(string documentTypeUri, string documentXml)
        {
            throw new NotImplementedException();

            //if (string.IsNullOrEmpty(documentTypeUri))
            //    throw new ArgumentNullException("documentTypeUri");

            //if (string.IsNullOrEmpty(documentXml))
            //    throw new ArgumentNullException("documentXml");

            //IRioApplication application = null;
            //List<string> errorsResult = new List<string>();
            //IDocumentSerializer documumentSerializer = new DocumentSerializerImpl();
            //IValidationEngine validationEngine = new CSValidationEngineGva
            //    (new AddressRepositoryImpl
            //        (new UnitOfWorkImpl()));
            //RioObjectsMetadata.RioUriMapper mapper = RioObjectsMetadata.GetRioUriMapperByDocumentTypeUri(documentTypeUri);

            //if (mapper == null)
            //    throw new NotImplementedException("Wrong documentTypeUri");

            //try
            //{
            //    RioDocumentMetadata metadata = RioDocumentMetadata.GetMetadataByRioCode(mapper.AppRioCode);
            //    application = (IRioApplication)documumentSerializer.XmlDeserializeFromString(metadata.RioObjectType, documentXml);
            //}
            //catch (Exception ex)
            //{
            //    throw new Exception("Parse error." + ex.Message);
            //}

            //IDictionary<string, IEnumerable<ValidationOption>> errors = validationEngine.Validate(mapper.AppRioCode, application, application, "", true);

            //foreach (var error in errors)
            //{
            //    foreach (var errorValue in error.Value)
            //    {
            //        errorsResult.Add(errorValue.ErrorRioMessage);
            //    }
            //}

            //return errorsResult;
        }

        #endregion

        #region Private

        private readonly int MAX_APPLICATION_FILE_SIZE_MB = 20;
        private volatile XmlSchemaSet _xmlSchemaSet = null;
        private object _syncRoot = new object();
        private Regex _emailRegex = new Regex(@"(?=^.{1,64}@)^[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])(;(?=.{1,64}@)[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+(?:\.[a-zA-Z0-9!#$%&amp;'*+/=?^_`{|}~-]+)*@(?=.{1,255}$|.{1,255};)(?:[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9])?\.)+[a-zA-Z0-9](?:[a-zA-Z0-9-]*[a-zA-Z0-9]))*$");

        //private IRioApplication GetRioApplication(string xml)
        //{
        //    var rioMetadata = GetDocumentMetadataFromXml(xml);

        //    return (IRioApplication)XmlDeserializeFromString(rioMetadata.RioObjectType, xml);
        //}

        private string GetFileExtension(string fileName)
        {
            string source = fileName.Trim();

            if (string.IsNullOrEmpty(source))
                return string.Empty;

            if (!source.Contains('.'))
                return string.Empty;
            else
                return source.Split('.').Last();
        }

        private bool HasFilledElectronicServiceApplicant(R_0009_000016.ElectronicServiceApplicant esa)
        {
            return
                esa != null &&
                esa.RecipientGroupCollection
                .Any(rg => rg.RecipientCollection.Any(esr => HasFilledElectronicServiceRecipient(esr)));
        }

        private bool HasFilledElectronicServiceRecipient(R_0009_000015.ElectronicServiceRecipient esr)
        {
            if (esr == null)
                return false;

            if (esr.Person != null)
            {
                bool hasIdentifier =
                    esr.Person.Identifier != null &&
                    (!string.IsNullOrWhiteSpace(esr.Person.Identifier.EGN) ||
                    !string.IsNullOrWhiteSpace(esr.Person.Identifier.LNCh));

                bool hasNames =
                    esr.Person.Names != null &&
                    !string.IsNullOrWhiteSpace(esr.Person.Names.First) &&
                    (!string.IsNullOrWhiteSpace(esr.Person.Names.Middle) ||
                    !string.IsNullOrWhiteSpace(esr.Person.Names.Last));

                return hasIdentifier && hasNames;
            }

            if (esr.Entity != null)
            {
                return
                    !string.IsNullOrWhiteSpace(esr.Entity.Identifier) &&
                    !string.IsNullOrWhiteSpace(esr.Entity.Name);
            }

            if (esr.ForeignPerson != null)
            {
                bool hasNames =
                    esr.ForeignPerson.Names != null &&
                    (!string.IsNullOrWhiteSpace(esr.ForeignPerson.Names.LastCyrillic) ||
                    !string.IsNullOrWhiteSpace(esr.ForeignPerson.Names.OtherCyrillic));

                bool hasBirthPlace =
                    esr.ForeignPerson.PlaceOfBirth != null &&
                    !string.IsNullOrWhiteSpace(esr.ForeignPerson.PlaceOfBirth.CountryCode) &&
                    !string.IsNullOrWhiteSpace(esr.ForeignPerson.PlaceOfBirth.CountryName) &&
                    !string.IsNullOrWhiteSpace(esr.ForeignPerson.PlaceOfBirth.SettlementName);

                return hasNames && hasBirthPlace;
            }

            if (esr.ForeignEntity != null)
            {
                bool hasName =
                    !string.IsNullOrWhiteSpace(esr.ForeignEntity.ForeignEntityName);

                bool hasCountry =
                    !string.IsNullOrWhiteSpace(esr.ForeignEntity.CountryISO3166TwoLetterCode) &&
                    !string.IsNullOrWhiteSpace(esr.ForeignEntity.CountryNameCyrillic);

                bool hasIdentificationData =
                    !string.IsNullOrWhiteSpace(esr.ForeignEntity.ForeignEntityOtherData) ||
                    (!string.IsNullOrWhiteSpace(esr.ForeignEntity.ForeignEntityRegister) &&
                    !string.IsNullOrWhiteSpace(esr.ForeignEntity.ForeignEntityIdentifier));

                return hasName && hasCountry && hasIdentificationData;
            }

            return false;
        }

        private string FillPrefixWithZeros(string value, int length)
        {
            int valueLength = value.Length;

            if (valueLength >= length)
                return value;

            for (int i = 0; i < (length - valueLength); i++)
            {
                value = string.Format("0{0}", value);
            }

            return value;
        }

        private bool HasValidSignature(string xml, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces, out X509Certificate2 signingCertificate)
        {
            if (string.IsNullOrEmpty(xml))
                throw new ArgumentException("Xml document can not be null or empty.");
            if (string.IsNullOrEmpty(signatureXPath))
                throw new ArgumentException("Signature XPath can not be null or empty.");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.LoadXml(xml);

            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(xmlDocument.NameTable);
            if (signatureXPathNamespaces != null)
            {
                foreach (var ns in signatureXPathNamespaces)
                {
                    xmlNamespaceManager.AddNamespace(ns.Key, ns.Value);
                }
            }

            return VerifyXml(xmlDocument, signatureXPath, xmlNamespaceManager, out signingCertificate);
        }

        private bool VerifyXml(XmlDocument xmlDocument, string signatureXPath, XmlNamespaceManager xmlNamespaceManager, out X509Certificate2 signingCertificate)
        {
            if (xmlDocument == null)
                throw new ArgumentException("xmlDocument");

            signingCertificate = null;

            SignedXml signedXml = new SignedXml(xmlDocument);
            XmlNode node = xmlDocument.SelectSingleNode(signatureXPath, xmlNamespaceManager);

            if (node == null)
            {
                return false;
            }

            xmlNamespaceManager.AddNamespace("ds", "http://www.w3.org/2000/09/xmldsig#");
            XmlNode signatureNode = node.SelectSingleNode("//ds:Signature", xmlNamespaceManager);

            if (!(signatureNode is XmlElement))
            {
                return false;
            }

            signedXml.LoadXml((XmlElement)signatureNode);
            bool isValid = signedXml.CheckSignature();

            if (isValid)
            {
                if (signedXml.KeyInfo != null)
                {
                    var dsaPublicKeys = signedXml.KeyInfo.OfType<DSAKeyValue>().Select(kv => kv.Key.ToXmlString(false));
                    var rsaPublicKeys = signedXml.KeyInfo.OfType<RSAKeyValue>().Select(kv => kv.Key.ToXmlString(false));
                    var publicKeys = dsaPublicKeys.Concat(rsaPublicKeys);
                    string singingKey = publicKeys.SingleOrDefault();

                    if (singingKey != null)
                    {
                        signingCertificate =
                            signedXml.KeyInfo
                            .OfType<KeyInfoX509Data>()
                            .SelectMany(ki => ki.Certificates.OfType<X509Certificate2>())
                            .Where(cert => cert.PublicKey.Key.ToXmlString(false).Equals(singingKey))
                            .SingleOrDefault();
                    }
                }
            }

            return isValid;
        }

        public object XmlDeserializeFromString(Type objectType, string documentXml)
        {
            using (System.IO.StringReader sr = new System.IO.StringReader(documentXml))
            {
                System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(objectType);
                return serializer.Deserialize(sr);
            }
        }

        #endregion
    }
}
