using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rio.Objects.Enums;
using System.Reflection;
using Rio.Objects;
using R_0009_000019;
using R_0009_000017;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using Rio.Data.Utils.RioValidator;
using Common.Utils;

namespace Rio.Data.Utils.RioDocumentParser
{
    public class RioDocumentParser : IRioDocumentParser
    {
        private IRioValidator rioValidator { get; set; }

        public RioDocumentParser()
        {
            this.rioValidator = new RioValidator.RioValidator();
        }

        public Rio.Objects.RioDocumentMetadata GetDocumentMetadataFromXml(string xml)
        {
            //TODO: Refactoring... Fix for Aop and other projects
            XDocument xmlDocument = new XDocument();
            xmlDocument = XDocument.Parse(xml, LoadOptions.PreserveWhitespace);

            string documentNamespace = xmlDocument.Root.GetDefaultNamespace().ToString();

            if (documentNamespace == @"http://ereg.egov.bg/segment/Aop")
            {
                return RioDocumentMetadata.AopApplicationMetadata;
            }
            else
            {
                Regex namespaceRegex = new Regex(@"[\dR]{1,4}-{1}\d{1,6}");

                string uri = string.Empty;

                if (namespaceRegex.IsMatch(documentNamespace))
                    uri = namespaceRegex.Match(documentNamespace).Value;

                string registerIndex = uri.Split('-').FirstOrDefault();
                string batchNumber = uri.Split('-').LastOrDefault();

                //TODO: FIX: DELETE AFTER RIO REGISTRATION
                registerIndex = registerIndex.Contains('R') ? "9" : registerIndex;

                registerIndex = FillPrefixWithZeros(registerIndex, 4);
                batchNumber = FillPrefixWithZeros(batchNumber, 6);

                return RioDocumentMetadata.GetMetadataBySegmentTypeUri(registerIndex, batchNumber);
            }
        }

        public object XmlDeserializeApplication(string xmlContent)
        {
            RioDocumentMetadata metaData = GetDocumentMetadataFromXml(xmlContent);
            Type applicationType = metaData.RioObjectType;

            return XmlSerializerUtils.XmlDeserializeFromString(applicationType, xmlContent);
        }

        public string XmlSerializeReceiptAcknowledgedMessage(ReceiptAcknowledgedMessage msg)
        {
            return XmlSerializerUtils.XmlSerializeObjectToString(msg);
        }

        public string XmlSerializeReceiptNotAcknowledgedMessage(ReceiptNotAcknowledgedMessage msg)
        {
            return XmlSerializerUtils.XmlSerializeObjectToString(msg);
        }

        private static string _schemasPathValue;
        private static string SchemasPath
        {
            get
            {
                if (_schemasPathValue == null)
                {
                    string assemblyPath = (new System.Uri(Assembly.GetExecutingAssembly().CodeBase)).LocalPath;
                    string binPath = System.IO.Path.GetDirectoryName(assemblyPath);
                    string projectPath = binPath.Substring(0, binPath.Length - 4);
                    _schemasPathValue = String.Format(@"{0}\RioSchemas", projectPath);
                }

                return _schemasPathValue;
            }
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
    }
}
