using Common.Rio.RioObjectExtractor;
using RioObjects;
using Gva.RioBridge.DataObjects;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.RioBridge.Extractions.AttachedDocDo
{
    public class R4958AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_4958.EngineeringTechnicalStaffPerformingMaintenanceFundsAirTrafficManagementApplication>
    {
        protected override R_3994.AttachedDocumentDatasCollection GetR3994Collection1(R_4958.EngineeringTechnicalStaffPerformingMaintenanceFundsAirTrafficManagementApplication rioObject)
        {
            return rioObject.AttachedDocumentDatasCollection;
        }

        protected override R_4696.EAURecipientsAttachedDocumentDatasCollection GetR4696Collection(R_4958.EngineeringTechnicalStaffPerformingMaintenanceFundsAirTrafficManagementApplication rioObject)
        {
            return rioObject.EAURecipientsAttachedDocumentDatasCollection;
        }
    }
}
