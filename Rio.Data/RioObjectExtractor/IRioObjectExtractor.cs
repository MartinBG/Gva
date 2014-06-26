using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rio.Data.RioObjectExtractor
{
    public interface IRioObjectExtractor
    {
        TDo Extract<TDo>(object rioApplication);
    }
}
