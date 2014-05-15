using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.PortalBridge.RioObjects
{
    public class ElectronicStatementAuthor
    {
        public PersonBasicData Person { get; set; }

        public ForeignCitizenBasicData ForeignCitizen { get; set; }
    }
}
