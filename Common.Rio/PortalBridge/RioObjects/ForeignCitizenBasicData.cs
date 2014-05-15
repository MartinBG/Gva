using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.PortalBridge.RioObjects
{
    public class ForeignCitizenBasicData
    {
        public ForeignCitizenNames Names { get; set; }

        public DateTime? BirthDate { get; set; }

        public ForeignCitizenPlaceOfBirth PlaceOfBirth { get; set; }

        public ForeignCitizenIdentityDocument IdentityDocument { get; set; }
    }
}
