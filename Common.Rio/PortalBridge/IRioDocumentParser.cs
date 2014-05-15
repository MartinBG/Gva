using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.PortalBridge
{
    public interface IRioDocumentParser
    {
        Common.Rio.PortalBridge.RioObjects.RioApplication XmlDeserializeApplication(string xmlContent);
        string XmlSerializeReceiptAcknowledgedMessage(Common.Rio.PortalBridge.RioObjects.ReceiptAcknowledgedMessage msg);
        string XmlSerializeReceiptNotAcknowledgedMessage(Common.Rio.PortalBridge.RioObjects.ReceiptNotAcknowledgedMessage msg);
        List<Common.Rio.PortalBridge.RioObjects.ElectronicDocumentDiscrepancyTypeNomenclature> GetValidationDiscrepancies(string xmlContent);
        List<string> GetValidationErrors(string docTypeUri, string xmlContent);
    }
}
