using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rio.Objects.Enums
{
    public class DocumentElectronicTransportType
    {
        public string Text { get; set; }
        public string Uri { get; set; }

        public static readonly DocumentElectronicTransportType ByWebApplication = new DocumentElectronicTransportType { Text = "Чрез уеб базирано приложение", Uri = "0006-000001" };
        public static readonly DocumentElectronicTransportType ByMail = new DocumentElectronicTransportType { Text = "Чрез електронна поща", Uri = "0006-000002" };
        public static readonly DocumentElectronicTransportType ByPhysicalCarrier = new DocumentElectronicTransportType { Text = "Чрез физически носител", Uri = "0006-000003" };
        public static readonly DocumentElectronicTransportType ByESOED = new DocumentElectronicTransportType { Text = "Чрез ЕСОЕД", Uri = "0006-000004" };

        public static readonly IEnumerable<DocumentElectronicTransportType> Values =
            new List<DocumentElectronicTransportType>
            {
                ByWebApplication,
                ByMail,
                ByPhysicalCarrier,
                ByESOED,
            };
    }
}
