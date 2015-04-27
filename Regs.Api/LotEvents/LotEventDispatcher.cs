using System.Collections.Generic;
using System.Linq;

namespace Regs.Api.LotEvents
{
    public class LotEventDispatcher : ILotEventDispatcher
    {
        private IEnumerable<ILotEventHandler> handlers;

        public LotEventDispatcher(IEnumerable<ILotEventHandler> handlers)
        {
            this.handlers = handlers;
        }

        public void Dispatch(ILotEvent e)
        {
            foreach (var handler in this.handlers)
            {
                handler.Handle(e);
            }
        }
    }
}
