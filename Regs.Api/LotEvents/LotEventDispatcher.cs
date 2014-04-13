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
            //TODO think of a better way to execute the principal event handlers first
            foreach (var handler in this.handlers.OrderByDescending(h => h is CommitEventHandler && ((CommitEventHandler)h).IsPrincipal))
            {
                handler.Handle(e);
            }
        }
    }
}
