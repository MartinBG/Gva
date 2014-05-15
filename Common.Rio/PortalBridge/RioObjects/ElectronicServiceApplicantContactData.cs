using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.PortalBridge.RioObjects
{
    public class ElectronicServiceApplicantContactData
    {
        public string DistrictCode { get; set; }

        public string DistrictName { get; set; }

        public string MunicipalityCode { get; set; }

        public string MunicipalityName { get; set; }

        public string SettlementCode { get; set; }

        public string SettlementName { get; set; }

        public string PostCode { get; set; }

        public string AddressDescription { get; set; }

        public string PostOfficeBox { get; set; }

        public PhoneNumbers PhoneNumbers { get; set; }

        public FaxNumbers FaxNumbers { get; set; }
    }
}
