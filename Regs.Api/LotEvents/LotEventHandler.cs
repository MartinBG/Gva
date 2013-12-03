using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Regs.Api.Models;

namespace Regs.Api.LotEvents
{
    public class LotEventHandler
    {
        public Func<PartVersion, bool> Condition { get; set; }

        public Action<PartVersion> Action { get; set; }

        public void Dispatch(Action<PartVersion> action)
        {
            this.Action = action;
        }
    }
}
