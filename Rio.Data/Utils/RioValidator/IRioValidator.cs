using System.Collections.Generic;
using R_0009_000016;

namespace Rio.Data.Utils.RioValidator
{
    public interface IRioValidator
    {
        bool CheckDocumentSize(string xml);

        bool CheckValidXmlSchema(string xml, string schemasDirecotryPath);
        
        bool CheckEmail(string email);
        
        bool CheckSupportedFileFormats(List<string> fileNames, string[] supportedFileFormats);

        bool CheckForVirus(string xml, string virusEngineAddress, int virusEnginePort);

        bool CheckSignatureValidity(string xml, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces);

        List<string> CheckCertificateValidity(string xml, ElectronicServiceApplicant applicant, string signatureXPath, IDictionary<string, string> signatureXPathNamespaces);

        List<string> ValidateRioApplication(string documentTypeUri, string documentXml);
    }
}
