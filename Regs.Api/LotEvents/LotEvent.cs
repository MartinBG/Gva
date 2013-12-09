using System.Collections.Generic;
using Common.Data;
using Regs.Api.Models;

namespace Regs.Api.LotEvents
{
    public class LotEvent : IEvent
    {
        public LotEvent(LotOperation operation, Lot lot, List<PartVersion> changedParts)
        {
            this.Operation = operation;
            this.Lot = lot;
            this.ChangedParts = changedParts;
        }

        public LotOperation Operation { get; private set; }

        public Lot Lot { get; private set; }

        public List<PartVersion> ChangedParts { get; private set; }
    }
}
