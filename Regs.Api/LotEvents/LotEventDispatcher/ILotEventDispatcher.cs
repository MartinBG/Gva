using System;
using Regs.Api.Models;

namespace Regs.Api.LotEvents.LotEventDispatcher
{
    public interface ILotEventDispatcher
    {
        string SetAlias { get; set; }

        ILotEventDispatcher On(Func<LotEvent, bool> condition);

        LotEventHandler WhenPart(Func<PartVersion, bool> condition);

        void Handle(LotEvent lotEvent);
    }
}
