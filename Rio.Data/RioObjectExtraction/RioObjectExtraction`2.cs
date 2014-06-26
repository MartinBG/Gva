using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rio.Data.RioObjectExtraction
{
    public abstract class RioObjectExtraction<TRioObject, TDo> : RioObjectExtraction<TDo>
    {
        public abstract TDo Extract(TRioObject rioObject);

        public override TDo Extract(object rioObject)
        {
            return this.Extract((TRioObject)rioObject);
        }

        public override Type RioObjectType
        {
            get { return typeof(TRioObject); }
        }
    }
}
