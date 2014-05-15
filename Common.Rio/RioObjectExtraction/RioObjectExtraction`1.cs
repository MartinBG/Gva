using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.RioObjectExtraction
{
    public abstract class RioObjectExtraction<TDo> : IRioObjectExtraction
    {
        public abstract TDo Extract(object rioObject);

        public abstract Type RioObjectType { get; }

        public Type DoType
        {
            get { return typeof(TDo); }
        }
    }
}
