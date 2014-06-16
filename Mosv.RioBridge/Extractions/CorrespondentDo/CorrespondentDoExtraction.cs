using Common.Rio.RioObjectExtraction;
using Common.Rio.RioObjectExtractor;
using Components.DocumentSerializer;
using RioObjects;
using Mosv.RioBridge.DataObjects;
using Abbcdn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mosv.RioBridge.Extractions.CorrespondentDo
{
    public abstract class CorrespondentDoExtraction<TRioObject> : RioObjectExtraction<TRioObject, DataObjects.CorrespondentDo>
    {
        public override DataObjects.CorrespondentDo Extract(TRioObject rioObject)
        {
            return this.GetCorrespondent(rioObject);;
        }

        protected virtual DataObjects.CorrespondentDo GetCorrespondent(TRioObject rioObject)
        {
            return null;
        }
    }
}
