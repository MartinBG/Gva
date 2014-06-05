using Common.Rio.RioObjectExtraction;
using Common.Rio.RioObjectExtractor;
using Components.DocumentSerializer;
using RioObjects;
using Aop.RioBridge.DataObjects;
using Abbcdn;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aop.RioBridge.Extractions.AttachedDocDo
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
