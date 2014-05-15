using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Rio.PortalBridge.RioObjects;

namespace Common.Rio.RioObjectExtractor
{
    public interface IRioObjectExtractor
    {
        TDo Extract<TDo>(RioApplication rioApplication);
    }
}
