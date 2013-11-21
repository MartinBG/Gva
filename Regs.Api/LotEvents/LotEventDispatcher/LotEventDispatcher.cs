using System;
using System.Collections.Generic;
using Regs.Api.Models;

namespace Regs.Api.LotEvents.LotEventDispatcher
{
    public class LotEventDispatcher : ILotEventDispatcher
    {
        private IList<Func<LotEvent, bool>> lotConditions;
        private IList<LotEventHandler> lotEventHandlers;

        public LotEventDispatcher()
        {
            this.lotConditions = new List<Func<LotEvent, bool>>();
            this.lotEventHandlers = new List<LotEventHandler>();
        }

        public string SetAlias { get; set; }

        public ILotEventDispatcher On(Func<LotEvent, bool> condition)
        {
            this.lotConditions.Add(condition);

            return this;
        }

        public LotEventHandler WhenPart(Func<PartVersion, bool> condition)
        {
            var lotEventHandler = new LotEventHandler() { Condition = condition };
            this.lotEventHandlers.Add(lotEventHandler);

            return lotEventHandler;
        }

        public void Handle(LotEvent lotEvent)
        {
            if (!this.ShouldHandle(lotEvent))
            {
                return;
            }

            foreach (var partVersion in lotEvent.ChangedParts)
            {
                foreach (var lotEventHandler in this.lotEventHandlers)
                {
                    if (lotEventHandler.Condition(partVersion))
                    {
                        lotEventHandler.Action(partVersion);
                    }
                }
            }
        }

        private bool ShouldHandle(LotEvent lotEvent)
        {
            if (this.SetAlias != lotEvent.Lot.Set.Alias)
            {
                return false;
            }

            bool shouldDispatchEvent = false;
            foreach (var lotCondition in this.lotConditions)
            {
                if (lotCondition(lotEvent))
                {
                    shouldDispatchEvent = true;
                    break;
                }
            }

            return shouldDispatchEvent;
        }
    }
}
