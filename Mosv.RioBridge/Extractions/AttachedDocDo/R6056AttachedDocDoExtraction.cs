using Common.Rio.RioObjectExtractor;
using RioObjects;
using Mosv.RioBridge.DataObjects;
using Abbcdn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mosv;
using Mosv.RioBridge.Extractions.AttachedDocDo;

namespace Mosv.RioBridge.Extractions.AttachedDocDo
{
    public class R6056AttachedDocDoExtraction : AttachedDocDoExtraction<R_6056.Signal>
    {
        protected override R_6052.AttachedDocumentDescriptionIdentifiersCollection GetAttachedDocumentDescriptionIdentifiersCollection(R_6056.Signal rioObject)
        {
            return rioObject.AttachedDocumentDescriptionIdentifiersCollection;
        }
    }
}
