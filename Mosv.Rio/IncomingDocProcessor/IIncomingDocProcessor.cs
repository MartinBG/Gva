using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rio.Data.Abbcdn;

namespace Mosv.Rio.IncomingDocProcessor
{
    public interface IIncomingDocProcessor
    {
        AbbcdnStorage AbbcdnStorage { get; set; }
        void Process(int pendingIncomingDocId);
    }
}
