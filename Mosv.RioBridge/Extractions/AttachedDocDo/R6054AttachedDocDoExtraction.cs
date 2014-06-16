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
    public class R6054AttachedDocDoExtraction : AttachedDocDoExtraction<R_6054.Proposal>
    {
        protected override R_6052.AttachedDocumentDescriptionIdentifiersCollection GetAttachedDocumentDescriptionIdentifiersCollection(R_6054.Proposal rioObject)
        {
            return rioObject.AttachedDocumentDescriptionIdentifiersCollection;
        }
    }
}
