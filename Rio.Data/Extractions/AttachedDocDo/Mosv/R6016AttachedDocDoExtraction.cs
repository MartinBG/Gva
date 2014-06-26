using Abbcdn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Mosv;
using Mosv.RioBridge.Extractions.AttachedDocDo;
using Rio.Data.Extractions.AttachedDocDo;

namespace Mosv.RioBridge.Extractions.AttachedDocDo.Mosv
{
    public class R6016AttachedDocDoExtraction : AttachedDocDoExtraction<R_6016.GrantPublicAccessInformation>
    {
        protected override List<KeyValuePair<string, string>> ExtractFileNames(R_6016.GrantPublicAccessInformation rioObject)
        {
            return null;
        }

        protected override List<R_0009_000139.AttachedDocument> ExtractAttachedDocuments(R_6016.GrantPublicAccessInformation rioObject)
        {
            return null;
        }
    }
}
