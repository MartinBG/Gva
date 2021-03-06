﻿using Common.Data;
using Regs.Api.Models;

namespace Regs.Api.LotEvents
{
    public class ResetEvent : ILotEvent
    {
        public ResetEvent(Lot lot, Commit newIndex)
        {
            this.Lot = lot;
            this.NewIndex = newIndex;
        }

        public Lot Lot { get; private set; }

        public Commit NewIndex { get; private set; }
    }
}
