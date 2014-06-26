using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rio.Data.RioObjectExtraction
{
    public interface IRioObjectExtraction
    {
        Type RioObjectType { get; }
        Type DoType { get; }
    }
}
