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
    public class R4378AttachedDocDoExtraction : R3994CollectionAttachedDocDoExtraction<R_4378.AppointmentSmodeCodeApplication>
    {
        protected override R_3994.AttachedDocumentDatasCollection GetR3994Collection1(R_4378.AppointmentSmodeCodeApplication rioObject)
        {
            return rioObject.AttachedDocumentDatasCollection;
        }
    }
}
