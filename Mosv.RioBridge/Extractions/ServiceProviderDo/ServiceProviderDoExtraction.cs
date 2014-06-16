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

namespace Mosv.RioBridge.Extractions.ServiceProviderDo
{
    public abstract class ServiceProviderDoExtraction<TRioObject> : RioObjectExtraction<TRioObject, DataObjects.ServiceProviderDo>
    {
        public override DataObjects.ServiceProviderDo Extract(TRioObject rioObject)
        {
            return this.GetCorrespondent(rioObject);;
        }

        protected virtual DataObjects.ServiceProviderDo GetCorrespondent(TRioObject rioObject)
        {
            return null;
        }
    }
}
