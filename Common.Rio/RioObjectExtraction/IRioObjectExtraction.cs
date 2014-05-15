using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Rio.RioObjectExtraction
{
    public interface IRioObjectExtraction
    {
        Type RioObjectType { get; }
        Type DoType { get; }
    }
}
