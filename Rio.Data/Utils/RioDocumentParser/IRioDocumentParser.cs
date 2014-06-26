using Rio.Objects;
using Rio.Objects.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rio.Data.Utils.RioDocumentParser
{
    public interface IRioDocumentParser
    {
        RioDocumentMetadata GetDocumentMetadataFromXml(string xml);
        object XmlDeserializeApplication(string xmlContent);
        string XmlSerializeReceiptAcknowledgedMessage(R_0009_000019.ReceiptAcknowledgedMessage msg);
        string XmlSerializeReceiptNotAcknowledgedMessage(R_0009_000017.ReceiptNotAcknowledgedMessage msg);
    }
}
