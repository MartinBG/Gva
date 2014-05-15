using Common.Rio.RioObjectExtractor;
using Gva.Portal.RioObjects;
using Gva.RioBridge.DataObjects;
using R_Abbcdn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gva.RioBridge.Extractions.AttachedDocDo
{
    public class R4240AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_4240.InitialAuthorizationMaintenanceAircraftAMLApplication>
    {
        protected override R_3994.AttachedDocumentDatasCollection GetR3994Collection1(R_4240.InitialAuthorizationMaintenanceAircraftAMLApplication rioObject)
        {
            return rioObject.AttachedDocumentDatasCollection;
        }
    }
}
