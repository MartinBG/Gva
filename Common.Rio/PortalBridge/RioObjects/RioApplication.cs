using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.PortalBridge.RioObjects
{
    public class RioApplication
    {
        //IRioApplication properties

        public string SUNAUServiceURI { get; set; }

        public string SUNAUServiceName { get; set; }

        public DocumentTypeURI DocumentTypeURI { get; set; }

        public string DocumentTypeName { get; set; }

        public DocumentURI DocumentURI { get; set; }

        public ElectronicServiceProviderBasicData ElectronicServiceProviderBasicData { get; set; }

        public string EmailAddress { get; set; }

        public DateTime? ApplicationSigningTime { get; set; }

        //IHeaderFooterDocumentsRioApplication  properties

        public ElectronicAdministrativeServiceHeader ElectronicAdministrativeServiceHeader { get; set; }

        public List<AttachedDocument> AttachedDocuments { get; set; }

        public ElectronicAdministrativeServiceFooter ElectronicAdministrativeServiceFooter { get; set; }

        public bool SendApplicationWithReceiptAcknowledgedMessage { get; set; }

        //Bridge Helper properties

        public object OriginalApplication { get; set; }

        public Type OriginalApplicationType { get; set; }

        public string DocFileTypeAlias { get; set; }
    }
}
