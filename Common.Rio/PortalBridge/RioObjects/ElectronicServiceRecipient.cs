using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.PortalBridge.RioObjects
{
    public class ElectronicServiceRecipient
    {
        public PersonBasicData Person { get; set; }

        public EntityBasicData Entity { get; set; }

        public ForeignCitizenBasicData ForeignPerson { get; set; }

        public ForeignEntityBasicData ForeignEntity { get; set; }
    }
}
