using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.PortalBridge.RioObjects
{
    public class ReceiptAcknowledgedMessage
    {
        public ElectronicServiceProviderBasicData ElectronicServiceProvider { get; set; }

        public string TransportType { get; set; }

        public DocumentURI DocumentURI { get; set; }

        public DateTime? ReceiptTime { get; set; }

        public RegisteredBy RegisteredBy { get; set; }

        public string CaseAccessIdentifier { get; set; }

        public ElectronicServiceApplicant Applicant { get; set; }

        public DocumentTypeURI DocumentTypeURI { get; set; }

        public string DocumentTypeName { get; set; }
    }
}
