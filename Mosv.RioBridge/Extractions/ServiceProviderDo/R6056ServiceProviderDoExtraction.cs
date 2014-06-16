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

namespace Mosv.RioBridge.Extractions.ServiceProviderDo
{
    public class R6056ServiceProviderDoExtraction : ServiceProviderDoExtraction<R_6056.Signal>
    {
        protected override DataObjects.ServiceProviderDo GetCorrespondent(R_6056.Signal rioObject)
        {
            return new DataObjects.ServiceProviderDo()
            {
                ElectronicServiceProvider = rioObject.ServiceInstructions
            };
        }
    }
}
