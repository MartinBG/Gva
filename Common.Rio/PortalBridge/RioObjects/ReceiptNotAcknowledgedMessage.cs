using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.PortalBridge.RioObjects
{
    public class ReceiptNotAcknowledgedMessage
    {
        public DocumentURI MessageURI { get; set; }

        public ElectronicServiceProviderBasicData ElectronicServiceProvider { get; set; }

        public string TransportType { get; set; }

        public Discrepancies Discrepancies { get; set; }

        public ElectronicServiceApplicant Applicant { get; set; }

        public DocumentTypeURI DocumentTypeURI { get; set; }

        public string DocumentTypeName { get; set; }

        public DateTime? MessageCreationTime { get; set; }
    }
}
