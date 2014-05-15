using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.PortalBridge.RioObjects
{
    public class ElectronicAdministrativeServiceHeader
    {
        public string SUNAUServiceURI { get; set; }

        public DocumentTypeURI DocumentTypeURI { get; set; }

        public string DocumentTypeName { get; set; }

        public ElectronicServiceProviderBasicData ElectronicServiceProviderBasicData { get; set; }

        public ElectronicServiceApplicant ElectronicServiceApplicant { get; set; }

        public ElectronicServiceApplicantContactData ElectronicServiceApplicantContactData { get; set; }

        public string ApplicationType { get; set; }

        public string SUNAUServiceName { get; set; }

        public DocumentURI DocumentURI { get; set; }

        public bool SendApplicationWithReceiptAcknowledgedMessage { get; set; }
    }
}
