using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.PortalBridge.RioObjects
{
    public class ForeignEntityBasicData
    {
        public string ForeignEntityName { get; set; }

        public string CountryISO3166TwoLetterCode { get; set; }

        public string CountryNameCyrillic { get; set; }

        public string ForeignEntityRegister { get; set; }

        public string ForeignEntityIdentifier { get; set; }

        public string ForeignEntityOtherData { get; set; }
    }
}
