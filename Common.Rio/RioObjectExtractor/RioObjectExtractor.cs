using Common.Rio.PortalBridge;
using Common.Rio.RioObjectExtraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Rio.PortalBridge.RioObjects;

namespace Common.Rio.RioObjectExtractor
{
    public class RioObjectExtractor : IRioObjectExtractor
    {
        private IEnumerable<IRioObjectExtraction> extractions;

        public RioObjectExtractor(IRioDocumentParser rioDocumentParser, IEnumerable<IRioObjectExtraction> extractions)
        {
            this.extractions = extractions;
        }

        public TDo Extract<TDo>(RioApplication rioApplication)
        {
            var extraction = (RioObjectExtraction<TDo>)this.extractions
                .Where(e => e.RioObjectType == rioApplication.OriginalApplicationType && e.DoType == typeof(TDo))
                .Single();

            return extraction.Extract(rioApplication.OriginalApplication);
        }
    }
}
