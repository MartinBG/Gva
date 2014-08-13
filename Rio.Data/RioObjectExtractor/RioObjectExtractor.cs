using Rio.Data.Utils.RioDocumentParser;
using Rio.Data.RioObjectExtraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rio.Data.RioObjectExtractor
{
    public class RioObjectExtractor : IRioObjectExtractor
    {
        private IEnumerable<IRioObjectExtraction> extractions;

        public RioObjectExtractor(IEnumerable<IRioObjectExtraction> extractions)
        {
            this.extractions = extractions;
        }

        public TDo Extract<TDo>(object rioApplication)
        {
            var extraction = (RioObjectExtraction<TDo>)this.extractions
                .Where(e => e.RioObjectType == rioApplication.GetType() && e.DoType == typeof(TDo))
                .SingleOrDefault();

            if (extraction != null)
            {
                return extraction.Extract(rioApplication);
            }
            else
            {
                return default(TDo);
            }
        }
    }
}
